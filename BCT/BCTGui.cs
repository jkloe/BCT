using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace BCT
{
    
    public delegate void jobFinishedDelegate();
    public delegate void allJobsFinishedDelegate();
    public delegate void clearErrorsDelegate();
    public delegate void addErrorMessageDelegate(string message, string file);
    public delegate void updateTimeDelegate(TimeSpan time);

    public enum FileStatus
    {
        ALL_MISSING,
        BIN_MISSING,
        CONVERT_MISSING,
        COMPLETED,
    }

    public partial class BCTGui : Form
    {
        public static WaitHandle[] doneEvents;
        BCT gppWrapper = null;

        public string ImageMagickPath { get { return GetFullPath("ImageMagick/convert.exe"); } }
        public string GraphicsMagickPath { get { return imPathTextField.Text; } }
        public string GppPath { get { return gppPathTextField.Text; } }
        public string BinarizationPath { get { return binarizationPathTextField.Text; } } // the new binarization
        public string KakaduPath { get { return kakPathTextField.Text; } }

        public string BinPath {
            get {
                var binPath = userNewBinCb.Checked ? binarizationPathTextField.Text : gppPathTextField.Text;
                return binPath;
            }
        }

        public string ConvertPath {
            get {
                if (UseGraphicsMagick)
                    return GraphicsMagickPath;
                else
                    return ImageMagickPath;
            }
        }

        public string ConvertOption {
            get {
                if (UseGraphicsMagick)
                    return "convert";
                else
                    return "";
            }
        }
        
        private static string PROGRAM_TITLE = "BCT - Binarisation and Conversion Tool";
        private static string VERSION = "1.0.4";
        private static string CONTACT = "sebastian.colutto@uibk.ac.at";

        public bool UseGraphicsMagick { get { return useGMCb.Checked;  } }

        public BCTGui() {
            InitializeComponent();

            //Console.WriteLine("exists = "+File.Exists("gpp/gpp.exe"));
            //Console.WriteLine("full path = "+ Path.GetFullPath("gpp/gpp.exe"));
            //Console.WriteLine("filename = " + Path.GetFileName("gpp/gpp.exe"));
        }

        private void GppGuiLoad(object sender, EventArgs e)
        {
            inputFolderTb.Text = "";
            outputFolderTb.Text = "";
            infoLabel.Text = "";
            timeLabel.Text = "";
            compareLabel.Text = "";

            nThreadsCb.SelectedIndex = nThreadsCb.FindString(System.Environment.ProcessorCount + "");

            viewingFilesFormat.SelectedIndex = 0;

            //inputFolderTb.Text = Directory.GetCurrentDirectory();
            //outputFolderTb.Text = Directory.GetCurrentDirectory();
            //inputFolderTb.Text = @"C:\bin_test_in_folder";
            //outputFolderTb.Text = @"C:\bin_test_in_folder";

            autoSetPathsButton_Click(this, null);

            Text = PROGRAM_TITLE + " - Version " + VERSION;

            enableButtons(true);
        }

        public static string GetFullPath(string relativePath)
        {
            if (File.Exists(relativePath))
                return Path.GetFullPath(relativePath);

            string fileName = Path.GetFileName(relativePath);

            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(';'))
            {
                var fullPath = Path.Combine(path.Trim(), fileName);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }

        private string[] GetExts()
        {
            var list = new List<string>();
            if (tifCb.Checked)
            {
                list.Add("tif");
                list.Add("tiff");
            }
            if (jpgCb.Checked)
            {
                list.Add("jpg");
                list.Add("jpeg");
                list.Add("jpe");
                list.Add("jfif");
            }
            if (pngCb.Checked)
            {
                list.Add("png");
            }
            if (jp2Cb.Checked)
                list.Add("jp2");

            return list.ToArray();
        }

        private void buttonInputFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                inputFolderTb.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonOutputFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                outputFolderTb.Text = folderBrowserDialog1.SelectedPath; 
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(inputFolderTb.Text))
            {
                MessageBox.Show("Cannot parse input folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(outputFolderTb.Text))
            {
                MessageBox.Show("Cannot parse output folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Console.WriteLine("Starting");
            timeLabel.Text = "";
            List<string> inFiles = GetInputFiles(inputFolderTb.Text, GetExts());

            string binPrefix = "";
            string binSuffix = "";
            List<string> outFilesBin = GetBinOutputFileNames(inputFolderTb.Text, outputFolderTb.Text, binPrefix, binSuffix, inFiles);

            string convertPrefix = "";
            string convertSuffix = "";
            List<string> outFilesConvert = GetConvertOutputFileNames(inputFolderTb.Text, outputFolderTb.Text, convertPrefix, convertSuffix, inFiles);

            bool doBin = binCb.Checked;
            bool doConvert = convertCb.Checked;

            // check if imagemagick or graphicsmagick convert exists:
            Trace.WriteLine(ConvertPath);
            Trace.WriteLine(BinPath);
            Trace.WriteLine(KakaduPath);

            if (!File.Exists(ConvertPath)) {
                DialogResult res = MessageBox.Show(this,
                                                   "GraphicsMagick or ImageMagick's convert.exe cannot be found in the path. Viewing files cannot be created - start anyway?",
                                                   "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes) {
                    doConvert = false;
                }
                else {
                    return;
                }
            }
            // check if bin tool exists:
            if (!File.Exists(BinPath)) {
                DialogResult res = MessageBox.Show(this,
                                                   "The selected binarization tool ("+BinPath+") cannot be found in the path. Binarization will not be done - start anyway?",
                                                   "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes) {
                    doBin = false;
                }
                else {
                    return;
                }
            }
            // check if kakakdu exists:
            if (!File.Exists(KakaduPath) && viewingFilesFormat.SelectedItem.ToString().StartsWith("jp2")) {
                DialogResult res = MessageBox.Show(this,
                                   "Kakadu kdu_compress.exe cannot be found in the path. Viewing files cannot be created - start anyway?",
                                   "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes) {
                    doConvert = false;
                }
                else {
                    return;
                }
            }

            if (!doBin && !doConvert) return;

            enableButtons(false);

            int maxThreads = Convert.ToInt32(nThreadsCb.SelectedItem.ToString());
            if (useNProcessorsCb.Checked)
                maxThreads = System.Environment.ProcessorCount;

            //int maxThreads = Thread

            Console.WriteLine("Now creating GppWrapper");

            var binJobs = GetBinJobs();
            var convertJobs = GetConvertJobs();

            var jobs = new List<GppJob>();
            jobs.AddRange(binJobs);
            jobs.AddRange(convertJobs);

            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = jobs.Count;
            progressBar.Step = 1;

            if (binJobs.Count==0 && convertJobs.Count==0)
            {
                this.allJobsFinished();
                return;
            }
            gppWrapper = new BCT(this, maxThreads, jobs);
            clearErrors();
            gppWrapper.Start();
            UpdateInfo();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.stopButton.Text = "Stopping...";
            this.stopButton.Enabled = false;
            gppWrapper.StopThreads();
        }

        public void jobFinished()
        {
            progressBar.PerformStep();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (gppWrapper == null)
            {
                infoLabel.Text = "";
                timeLabel.Text = "";
            }
            else
            {
                infoLabel.Text = "Finished job " + progressBar.Value + " of " + progressBar.Maximum;
                infoLabel.Text += ", Binarizations: " + gppWrapper.getCompletedBins() + "/" + gppWrapper.getNBins();
                infoLabel.Text += ", Viewing files: " + gppWrapper.getCompletedConverts() + "/" + gppWrapper.getNConverts();

                //TimeSpan ts = gppWrapper.GetElapsedTime();
                //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                //Console.WriteLine("Elapsed Time " + elapsedTime);
                //this.timeLabel.Text = "Elapsed Time: " + elapsedTime;
            }
        }

        public void UpdateTime(TimeSpan time) {
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds / 10);
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
            this.timeLabel.Text = "Elapsed Time: " + elapsedTime; 
        }

        public void allJobsFinished()
        {
            UpdateInfo();
            compareButton_Click(this, null);

            enableButtons(true);
        }

        private void enableButtons(bool value)
        {
            this.startButton.Enabled = value;
            this.stopButton.Enabled = !value;
            this.stopButton.Text = "Stop";
            this.compareButton.Enabled = value;
            this.autoSetPathsButton.Enabled = value;
            this.updateFilesButton.Enabled = value;
            this.showMissingBinFilesButton.Enabled = value;
            this.showMissingViewingFilesButton.Enabled = value;
            this.buttonInputFolder.Enabled = value;
            this.buttonOutputFolder.Enabled = value;

            this.progressBar.Enabled = !value;
        }
        
        private void updateFilesButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(inputFolderTb.Text))
            {
                MessageBox.Show("Cannot parse input folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Enabled = false;
            List<string> files = GetInputFiles(inputFolderTb.Text, GetExts());

            filesListBox.Items.Clear();
            filesListBox.Items.AddRange(files.ToArray());

            //this.filesListBox.DataSource = files;
            Application.UseWaitCursor = false;
            this.Enabled = true;
        }

        public void clearErrors()
        {
            filesListBox.Items.Clear();
        }

        public void addErrorMessage(string message, string file)
        {
            filesListBox.Items.Add(message+"; file: "+file);
        }

        private void nThreadsCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void useNProcessorsCb_CheckedChanged(object sender, EventArgs e)
        {
            nThreadsCb.Enabled = !useNProcessorsCb.Checked;
        }

        private void autoSetPathsButton_Click(object sender, EventArgs e)
        {
            //ImageMagickPath = GetFullPath("ImageMagick/convert.exe");
            //GraphicsMagickPath = GetFullPath("GraphicsMagick/gm.exe");
            //GppPath = GetFullPath("gpp/gpp.exe");
            //BinarizationPath = GetFullPath("Binarization.exe");
            //KakaduPath = GetFullPath("kdu_compress.exe");

            imPathTextField.Text = GetFullPath("GraphicsMagick/gm.exe") ?? "GraphicsMagick (gm.exe) cannot not be found!";
            gppPathTextField.Text = GetFullPath("gpp/gpp.exe") ?? "Binarization tool (gpp.exe) cannot not be found!";
            binarizationPathTextField.Text = GetFullPath("Binarization.exe") ?? "New Binarization tool (Binarization.exe) cannot be found!";
            kakPathTextField.Text = GetFullPath("kdu_compress.exe") ?? "Kakadu compress (kdu_compress.exe) cannot not be found!";
        }

        public List<GppJob> GetBinJobs()
        {
            List<string> inFiles = GetInputFiles(inputFolderTb.Text, GetExts());
            string binPrefix = "";
            string binSuffix = "";
            List<string> outFilesBin = GetBinOutputFileNames(inputFolderTb.Text, outputFolderTb.Text, "", "", inFiles);

            List<GppJob> binJobs = new List<GppJob>();
            for (int i=0; i<outFilesBin.Count; i++)
            {
                if ((overwriteCb.Checked || !File.Exists(outFilesBin[i])) && binCb.Checked)
                {
                    var binJob = new GppJob(inFiles[i], outFilesBin[i], "bin");
                    binJobs.Add(binJob);
                }
            }
            return binJobs;
        }

        public List<GppJob> GetConvertJobs()
        {
            List<string> inFiles = GetInputFiles(inputFolderTb.Text, GetExts());
            List<string> outFilesConvert = GetConvertOutputFileNames(inputFolderTb.Text, outputFolderTb.Text, "", "", inFiles);

            List<GppJob> convertJobs = new List<GppJob>();
            for (int i = 0; i < outFilesConvert.Count; i++)
            {
                if ((overwriteCb.Checked || !File.Exists(outFilesConvert[i])) && convertCb.Checked)
                {
                    GppJob binJob = new GppJob(inFiles[i], outFilesConvert[i], "convert");
                    convertJobs.Add(binJob);
                }
            }
            return convertJobs;
        }

        public List<string> GetInputFiles(string dirin, string[] exts)
        {
            if (!Directory.Exists(dirin)) return null;

            var allFiles = new List<string>();
            foreach (string ext in exts)
            {
                try
                {
                    string[] files = Directory.GetFiles(dirin, "*." + ext, SearchOption.AllDirectories);
                    foreach (string f in files)
                    {
                        // double check for duplicate file entries
                        if (!f.EndsWith("."+ext))
                            continue;

                        Trace.WriteLine("got file: " + f);
                        string relFn = f.Substring(dirin.TrimEnd('\\').Count());
                        //Console.WriteLine("relFn = "+relFn);

                        if (!relFn.Contains("binarized_files") && !relFn.Contains("viewing_files") || !ignoreOutputFoldersCb.Checked)
                            allFiles.Add(f);
                    }
                    //allFiles.AddRange(files);
                }
                catch (DirectoryNotFoundException e)
                {
                    MessageBox.Show("Cannot find input directory: " + e.Message);
                    return new List<string>();
                }
            }


            return allFiles;
        }

        public List<String> GetBinOutputFileNames(string dirin, string dirout, string prefix, string suffix,
                                                         List<String> inFiles)
        {
            Console.WriteLine("GetBinOutputFileNames");
            var outNames = new List<String>();
            foreach (string f in inFiles)
            {
                string tmp = dirin.TrimEnd('\\');
                //Console.WriteLine(@"file = " + tmp);
                string relName = f.Substring(tmp.Length + 1);
                string relNameWoExt = Path.ChangeExtension(relName, null);
                string outname = dirout.TrimEnd('\\') + '\\' + "binarized_files\\" + (new DirectoryInfo(dirin)).Name + '\\' + prefix + relNameWoExt + suffix +
                                 ".tif";
                outNames.Add(outname);
                //Console.WriteLine(@"outfile = "+outname);
            }
            return outNames;
        }

        public List<String> GetConvertOutputFileNames(string dirin, string dirout, string prefix, string suffix,
                                                             List<String> inFiles)
        {
            Console.WriteLine("GetConvertOutputFileNames");
            var outNames = new List<String>();
            foreach (string f in inFiles)
            {
                string tmp = dirin.TrimEnd('\\');
                //Console.WriteLine(@"file = " + tmp);
                string relName = f.Substring(tmp.Length + 1);
                string relNameWoExt = Path.ChangeExtension(relName, null);
                //string outname = dirout.TrimEnd('\\') + '\\' + "viewing_files\\" + relNameWoExt + suffix + ".jpg";

                string extension = viewingFilesFormat.SelectedItem.ToString().StartsWith("jp2") ? "jp2" : viewingFilesFormat.SelectedItem.ToString();
                string outname = dirout.TrimEnd('\\') + '\\' + "viewing_files\\" + (new DirectoryInfo(dirin)).Name + '\\' + relNameWoExt + suffix + "." + extension;
                outNames.Add(outname);
                //Console.WriteLine(@"outfile = "+outname);
            }
            return outNames;
            
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(inputFolderTb.Text) || !Directory.Exists(outputFolderTb.Text))
            {
                MessageBox.Show("Cannot parse input or output folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }

            var fileStatuses = getFileStatuses();

            int nBinFiles = 0;
            int nConvertFiles = 0;
            foreach (var entry in fileStatuses)
            {
                if (entry.Value == FileStatus.CONVERT_MISSING) nBinFiles++;
                if (entry.Value == FileStatus.BIN_MISSING) nConvertFiles++;
                if (entry.Value == FileStatus.COMPLETED) { nBinFiles++; nConvertFiles++; }
            }
            int nInFiles = fileStatuses.Count;
            compareLabel.Text = "Nr. of input files: " + nInFiles + ", nr. of binarized files in output folder: " +
                    nBinFiles + "/" + nInFiles + ", nr. of viewing files in output folder: " + nConvertFiles + "/" + nInFiles;
        }

        private Dictionary<string, FileStatus> getFileStatuses()
        {
            List<string> inFiles = GetInputFiles(inputFolderTb.Text, GetExts());
            List<string> outFilesBin = GetBinOutputFileNames(inputFolderTb.Text, outputFolderTb.Text, "", "", inFiles);
            List<string> outFilesConvert = GetConvertOutputFileNames(inputFolderTb.Text, outputFolderTb.Text, "", "", inFiles);

            var missingFiles = new Dictionary<string, FileStatus>();
            for (int i=0; i<inFiles.Count; ++i)
            {
                bool binExists = File.Exists(outFilesBin[i]);
                bool convertExists = File.Exists(outFilesConvert[i]);

                var status = FileStatus.ALL_MISSING;
                if (binExists && !convertExists) status = FileStatus.CONVERT_MISSING;
                if (!binExists && convertExists) status = FileStatus.BIN_MISSING;
                if (binExists && convertExists) status = FileStatus.COMPLETED;

                missingFiles.Add(inFiles[i], status);
            }
            return missingFiles;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Binarization and Viewing Files Creator\nVersion " + VERSION + "\nContact: " + CONTACT, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showMissingFilesButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(inputFolderTb.Text) || !Directory.Exists(outputFolderTb.Text))
            {
                MessageBox.Show("Cannot parse input or output folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var fileStatuses = getFileStatuses();

            filesListBox.Items.Clear();
            foreach (KeyValuePair<String, FileStatus> entry in fileStatuses)
            {
                //if (entry.Value == FileStatus.COMPLETED) continue;
                //string missingStr = "";
                //if (entry.Value == FileStatus.BIN_MISSING) missingStr = "Missing: binarization";
                //if (entry.Value == FileStatus.CONVERT_MISSING) missingStr = "Missing: viewing file";
                //if (entry.Value == FileStatus.ALL_MISSING) missingStr = "Missing: binarization and viewing files";
                if (entry.Value == FileStatus.BIN_MISSING || entry.Value == FileStatus.ALL_MISSING)
                {
                    filesListBox.Items.Add(entry.Key);    
                }

            }
        }

        private void showMissingViewingFilesButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(inputFolderTb.Text) || !Directory.Exists(outputFolderTb.Text))
            {
                MessageBox.Show("Cannot parse input or output folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var fileStatuses = getFileStatuses();

            filesListBox.Items.Clear();
            foreach (KeyValuePair<String, FileStatus> entry in fileStatuses)
            {
                //if (entry.Value == FileStatus.COMPLETED) continue;
                //string missingStr = "";
                //if (entry.Value == FileStatus.BIN_MISSING) missingStr = "Missing: binarization";
                //if (entry.Value == FileStatus.CONVERT_MISSING) missingStr = "Missing: viewing file";
                //if (entry.Value == FileStatus.ALL_MISSING) missingStr = "Missing: binarization and viewing files";
                if (entry.Value == FileStatus.CONVERT_MISSING || entry.Value == FileStatus.ALL_MISSING)
                {
                    filesListBox.Items.Add(entry.Key);
                }
            }
        }

        private void GppGui_KeyDown(object sender, KeyEventArgs e)
        {



        }

        private void inputFolderTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                useGMCb.Visible = !useGMCb.Visible;
                deleteIntermediate.Visible = !deleteIntermediate.Visible;
            }
        }
        
    }
}
