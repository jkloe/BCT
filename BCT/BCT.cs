using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Timers;

namespace BCT
{
    //public class GppInfo
    //{
    //    public int index = -1;
    //    public string file_in = "";
    //    public string file_out_bin = "";
    //    public string file_out_convert = "";
    //}

    public class GppJob
    {
        public GppJob(string fi, string fo, string type)
        {
            file_in = fi;
            file_out = fo;
            job_type = type;
        }

        public string job_type = "";
        public string file_in = "";
        public string file_out = "";
    }

    public class RangePair
    {
        public int start=-1;
        public int end=-1; 
    }

    public class BCT
    {
        private int nThreads;

        private List<GppJob> jobs;

        private volatile int completedBins = 0;
        private volatile int completedConverts = 0;
        private int nBinJobs = 0;
        private int nConvertJobs = 0;

        private volatile int completedThreads = 0;

        private List<Thread> threads = null;

        private volatile Boolean stop = false;
        private BCTGui gui;

        Stopwatch stopWatch=new Stopwatch();
        System.Timers.Timer timer = new System.Timers.Timer();
        

        public BCT(BCTGui gui, int nThreads, List<GppJob> jobs)
        {
            this.gui = gui;
            this.nThreads = nThreads;

            this.jobs = jobs;

            ComputeNBinJobs();
            ComputeNConvertJobs();

            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 1000;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e) {
            gui.Invoke(new updateTimeDelegate(gui.UpdateTime), stopWatch.Elapsed);
        }

        private void ComputeNBinJobs()
        {
            foreach (GppJob j in jobs)
            {
                if (j.job_type.Equals("bin"))
                    nBinJobs++;
            }
        }

        private void ComputeNConvertJobs()
        {
            foreach (GppJob j in jobs)
            {
                if (j.job_type.Equals("convert"))
                    nConvertJobs++;
            }
        }

        public TimeSpan GetElapsedTime()
        {
            return stopWatch.Elapsed;
        }

        private void BinCompleted()
        {
            completedBins++;
            Console.WriteLine("Bins completed = " + completedBins);
            gui.Invoke(new jobFinishedDelegate(gui.jobFinished));
        }

        private void ConvertCompleted()
        {
            completedConverts++;
            Console.WriteLine("Converts completed = " + completedConverts);
            gui.Invoke(new jobFinishedDelegate(gui.jobFinished));
        }

        private void ThreadCompleted()
        {
            completedThreads++;
            if (completedThreads == threads.Count)
            {
                stopWatch.Stop();
                gui.Invoke(new allJobsFinishedDelegate(gui.allJobsFinished));
                timer.Stop();
            }
        }

        public void Start()
        {
            Console.WriteLine("Starting ThreadPool with " + nThreads + " threads!");

            //threads = new Thread[this.nThreads];
            threads = new List<Thread>();
            //int N = infiles.Count;
            //if (this.nThreads > N) this.nThreads = N;

            int portion = (int) Math.Round((double) jobs.Count/(double) nThreads);
            if (portion == 0) portion = 1;

            stopWatch.Reset();
            stopWatch.Start();
            timer.Start();
            for (int i = 0; i < nThreads; ++i)
            {
                int start = i * (portion);
                int end = (i + 1) * (portion);
                if (i == nThreads - 1) end = jobs.Count;

                var rp = new RangePair();

                if (start < jobs.Count && end <= jobs.Count)
                {
                    rp.start = start;
                    rp.end = end;
                }

                if (rp.start != -1)
                {
                    Console.WriteLine("Starting thread " + i + " si = " + rp.start + " ei = " + rp.end + " total = " +jobs.Count);

                    var t = new Thread(new ParameterizedThreadStart(doWork)) {Name = "BCTThread_" + i};
                    threads.Add(t);
                    threads.Last().Start(rp); 
                }
            }
            Console.WriteLine("Started all threads! (nThreads = "+threads.Count+")");
        }

        public void StopThreads()
        {
            Console.WriteLine("stopping all threads...");
            stop = true;
            //foreach (Thread t in threads)
            //{
            //    Console.WriteLine("Waiting for thread "+t.Name+" to end...");
            //    t.Join();
            //}
            //stopWatch.Stop();
            //gui.Invoke(new allJobsFinishedDelegate(gui.allJobsFinished));
        }

        public void doWork(object obj)
        {
            var pair = (RangePair) obj;

            if (!stop)
            for (int i = pair.start; i < pair.end; ++i)
            {
                if (jobs[i].job_type.Equals("bin"))
                {
                    StartGpp(jobs[i]);
                    BinCompleted();
                }
                else if (jobs[i].job_type.Equals("convert"))
                {
                    StartConvert(jobs[i]);
                    ConvertCompleted();                    
                }

                if (stop)
                {
                    Console.WriteLine("Stopping thread " + Thread.CurrentThread.Name);
                    break;
                }
            }

            ThreadCompleted();
        }

        private void StartGpp(object startInfo)
        {
            var job = (GppJob) startInfo;

            Trace.WriteLine("Hi, I am thread " + Thread.CurrentThread.Name + ", binarizing file " + job.file_in + " to " + job.file_out);

            // create output folder if not existent:
            Directory.CreateDirectory(Path.GetDirectoryName(job.file_out));

            bool isJpeg2000 = Path.GetExtension(job.file_in).Equals(".jp2");
            string fconvert = "";
            if (isJpeg2000) // convert to tiff for jpeg2000 files
            {
                var processinfoConvert = new ProcessStartInfo();
                if (gui.UseGraphicsMagick)
                    processinfoConvert.FileName = "\"" + gui.GraphicsMagickPath + "\"";
                else
                    processinfoConvert.FileName = "\"" + gui.ImageMagickPath + "\"";

                processinfoConvert.RedirectStandardOutput = true;
                processinfoConvert.UseShellExecute = false;
                processinfoConvert.CreateNoWindow = true;

                fconvert = job.file_out + "_tmp.tif";
                //fconvert = Path.ChangeExtension(job.file_in, ".tif");
                //Trace.WriteLine("fconvert = " + fconvert);
                
                if (gui.UseGraphicsMagick)
                    processinfoConvert.Arguments = " convert \"" + job.file_in + "\" \"" + fconvert + "\"";
                else
                    processinfoConvert.Arguments = "\"" + job.file_in + "\" \"" + fconvert + "\"";

                try
                {
                    using (Process exeProcess = Process.Start(processinfoConvert))
                    {
                        exeProcess.WaitForExit();
                        Trace.WriteLine("exit code of convert-to-tiff file " + job.file_in + ": " + exeProcess.ExitCode);
                        if (exeProcess.ExitCode > 0)
                            throw new Exception("Convert to tiff failed for: " + job.file_in);

                        job.file_in = fconvert;
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Exception caught while converting to tiff: " + e.Message);
                    Trace.WriteLine(e.StackTrace);
                    
                    gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: "+e.Message, job.file_in);

                    if (File.Exists(job.file_out)) File.Delete(job.file_out);
                    if (File.Exists(fconvert)) File.Delete(fconvert);
                    return;
                }
            } // end convert to tiff for jpeg2000 files

            // Start actual binarization:
            var processinfo = new ProcessStartInfo
                                  {
                                      FileName = "\"" + gui.BinPath + "\"",
                                      RedirectStandardOutput = false,
                                      UseShellExecute = false,
                                      CreateNoWindow = true,
                                      Arguments = "\"" + job.file_in + "\" \"" + job.file_out + "\""
                                  };
            //processinfo.FileName = @"c:\GPP_Binarization\gpp.exe";
            //processinfo.FileName = @"gpp.exe";

            //Trace.WriteLine("1FileName = " + processinfo.FileName);
            //Trace.WriteLine("1Arguments = " + processinfo.Arguments);

            
            try
            {
                using (Process exeProcess = Process.Start(processinfo))
                {
                    exeProcess.WaitForExit();
                    Trace.WriteLine("Exit code of bin-file " + job.file_in + ": " + exeProcess.ExitCode);
                    if (exeProcess.ExitCode > 0)
                    {
                        // generate error message depending on return code:
                        string errorMessage = "";
                        switch (exeProcess.ExitCode)
                        {
                            case 1:
                                errorMessage = "Wrong number of input parameters (SHOULD NEVER HAPPEN!)";
                                break;
                            case 2:
                                errorMessage = "Input file does not exist (SHOULD NEVER HAPPEN!)";
                                break;
                            case 3:
                                errorMessage = "Input file cannot be read (not a valid image format)!";
                                break;
                            case 4:
                                errorMessage = "Unknown error has occured during binarization! (is file broken?)";
                                break;
                            case 5:
                                errorMessage = "System date exceeds 1/5/2015";
                                break;
                            default:
                                errorMessage = "Not a valid error code (SHOULD NEVER HAPPEN!)";
                                break;
                        }

                        string message = "Binarization of file " + job.file_in + ", error message: " + errorMessage;
                        throw new Exception(message);                        
                    }
                    //gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "file successful: "+job.file_out);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception caught while binarizing: " + e.Message);
                Trace.WriteLine(e.StackTrace);
                
                gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: " + e.Message, job.file_in);

                if (File.Exists(job.file_out)) File.Delete(job.file_out);
            }
            finally
            {
                if (File.Exists(fconvert)) File.Delete(fconvert);
            }
        } // end StartGpp

        private void StartConvertKakadu(object startInfo) {
            var job = (GppJob)startInfo;
            Trace.WriteLine("Hi, I am thread " + Thread.CurrentThread.Name + " kakadu converting file " + job.file_in + " to " +
                  job.file_out);
            // create output folder if not existent:
            Directory.CreateDirectory(Path.GetDirectoryName(job.file_out));

            if (gui.KakaduPath == null) {
                gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: kdu_compress.exe not found!", job.file_in);
                return;
            }

            //bool isTif = job.file_in.EndsWith(".tif") || job.file_in.EndsWith(".tiff");
            //if (!isTif) {
            //    gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: Kakadu jp2 creation only allowed for tif input files!", job.file_in);
            //    return;
            //}

            // remove compression from tif files:
            
            bool didConvert = false;
            if (true) {
                var tif_no_compress = job.file_out + "_no_compress.tif";
                var processinfo = new ProcessStartInfo();

                processinfo.FileName = "\""+gui.ConvertPath+"\"";
                processinfo.Arguments = " " + gui.ConvertOption + " -compress none "
                                            //+"\"" + job.file_in + "\" -colorspace RGB +profile \"*\" \"" + tif_no_compress + "\"";
                                            +"\"" + job.file_in + "\" \"" + tif_no_compress + "\"";
                processinfo.RedirectStandardOutput = true;
                processinfo.RedirectStandardError = true;
                processinfo.UseShellExecute = false;
                processinfo.CreateNoWindow = true;

                try {
                    using (Process exeProcess = Process.Start(processinfo)) {
                        exeProcess.WaitForExit();
                        Trace.WriteLine("exit code of convert-file " + job.file_in + ": " + exeProcess.ExitCode);
                        if (exeProcess.ExitCode != 0) {
                            Trace.WriteLine("error message: "+exeProcess.StandardError.ReadToEnd());
                            throw new Exception("Viewing file creation failed for " + job.file_in);
                        }
                    }
                    job.file_in = tif_no_compress;
                    didConvert = true;
                }
                catch (Exception e) {
                    Trace.WriteLine("Exception caught while creating non-compressed tif file for " + job.file_in + ": " + e.Message);
                    gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: " + e.Message, job.file_in);

                    if (gui.deleteIntermediate.Checked && File.Exists(tif_no_compress)) File.Delete(tif_no_compress);
                    return;
                }
            }

            // now create jp2 file using kakadu:
            var kakaduproc = new ProcessStartInfo();
            kakaduproc.FileName = "\"" + gui.KakaduPath + "\"";
            kakaduproc.RedirectStandardOutput = true;
            kakaduproc.RedirectStandardError = true;
            kakaduproc.UseShellExecute = false;
            kakaduproc.CreateNoWindow = true;
            kakaduproc.Arguments = " -i \"" + job.file_in + "\" -o \"" + job.file_out + "\"" +
                                    @" -rate 1.0,0.84,0.7,0.6,0.5,0.4,0.35,0.3,0.25,0.21,0.18,0.15,0.125,0.1,0.088,0.075,0.0625,0.05,0.04419,0.03716,0.03125,0.025,0.0221,0.01858,0.015625 Clevels=6 Stiles={1024,1024} Cmodes={BYPASS} Corder=RLCP Cblk={64,64} -jp2_space sLUM";
            var errorMessage = "";
            try {
                using (Process exeProcess = Process.Start(kakaduproc)) {
                    exeProcess.WaitForExit();
                    Trace.WriteLine("exit code of convert-file " + job.file_in + ": " + exeProcess.ExitCode);
                    if (exeProcess.ExitCode != 0) {
                        errorMessage = exeProcess.StandardError.ReadToEnd();
                        throw new Exception("Viewing file creation failed for " + job.file_in);
                    }
                }
            }
            catch (Exception e) {
                Trace.WriteLine("Exception caught while creating kakaku viewing file for "+job.file_in+": " + e.Message);
                gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: " + e.Message, job.file_in);
                Trace.WriteLine(errorMessage);

                if (File.Exists(job.file_out)) File.Delete(job.file_out);
            }
            finally {
                // clean up temporary tif:
                if (gui.deleteIntermediate.Checked && didConvert && File.Exists(job.file_in)) File.Delete(job.file_in);
            } 
        }

        private void StartConvert(object startInfo)
        {
            var job = (GppJob)startInfo;
            if (job.file_out.EndsWith(".jp2")) {
                StartConvertKakadu(job);
                return;
            }

            Trace.WriteLine("Hi, I am thread " + Thread.CurrentThread.Name + " converting file " + job.file_in + " to " +
                              job.file_out);

            // create output folder if not existent:
            if (!Directory.Exists(Path.GetDirectoryName(job.file_out))) {
                Directory.CreateDirectory(Path.GetDirectoryName(job.file_out));
            }

            var processinfo = new ProcessStartInfo();
            //processinfo.FileName = @"C:\Program Files (x86)\ImageMagick-6.7.8-Q16\convert.exe";
            //processinfo.FileName = "convert.exe";

            if (gui.UseGraphicsMagick)
                processinfo.FileName = "\"" + gui.GraphicsMagickPath + "\"";
            else
                processinfo.FileName = "\""+gui.ImageMagickPath+"\"";

            processinfo.RedirectStandardOutput = true;
            processinfo.UseShellExecute = false;
            processinfo.CreateNoWindow = true;

            if (gui.UseGraphicsMagick)
                processinfo.Arguments = @" convert -compress JPEG -quality 35 -sampling-factor 4:4:4 -unsharp 0 " + "\"" +
                                    job.file_in + "\" \"" + job.file_out + "\"";
            else
                processinfo.Arguments = @" -compress JPEG -quality 35 -sampling-factor 4:4:4 -unsharp 0 " + "\"" +
                        job.file_in + "\" \"" + job.file_out + "\"";

            //Console.WriteLine("FileName = " + processinfo.FileName);
            //Console.WriteLine("Arguments = " + processinfo.Arguments);

            try
            {
                using (Process exeProcess = Process.Start(processinfo))
                {
                    exeProcess.WaitForExit();
                    Trace.WriteLine("exit code of convert-file " + job.file_in + ": " + exeProcess.ExitCode);
                    if (exeProcess.ExitCode > 0)
                    {
                        throw new Exception("Viewing file creation failed for " + job.file_in); 
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception caught while creating viewing file: " + e.Message);
                //Trace.WriteLine(e.StackTrace);

                gui.Invoke(new addErrorMessageDelegate(gui.addErrorMessage), "ERROR: " + e.Message, job.file_in);

                if (File.Exists(job.file_out)) File.Delete(job.file_out);
            }
        } // end StartConvert

        public int getCompletedBins()
        {
            return completedBins;
        }

        public int getCompletedConverts()
        {
            return completedConverts;
        }

        public int getNBins()
        {
            return nBinJobs;
        }

        public int getNConverts()
        {
            return nConvertJobs;
        }

        public int getNJobs()
        {
            return getNBins() + getNConverts();
        }

    } // end class BCT
} // end namespace
