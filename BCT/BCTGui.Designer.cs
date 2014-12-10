namespace BCT
{
    partial class BCTGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCTGui));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonInputFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOutputFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.jp2Cb = new System.Windows.Forms.CheckBox();
            this.pngCb = new System.Windows.Forms.CheckBox();
            this.jpgCb = new System.Windows.Forms.CheckBox();
            this.tifCb = new System.Windows.Forms.CheckBox();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.updateFilesButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.nThreadsCb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.binCb = new System.Windows.Forms.CheckBox();
            this.convertCb = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.imPathTextField = new System.Windows.Forms.TextBox();
            this.gppPathTextField = new System.Windows.Forms.TextBox();
            this.useNProcessorsCb = new System.Windows.Forms.CheckBox();
            this.autoSetPathsButton = new System.Windows.Forms.Button();
            this.overwriteCb = new System.Windows.Forms.CheckBox();
            this.compareButton = new System.Windows.Forms.Button();
            this.compareLabel = new System.Windows.Forms.Label();
            this.ignoreOutputFoldersCb = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.viewingFilesFormat = new System.Windows.Forms.ComboBox();
            this.inputFolderTb = new System.Windows.Forms.TextBox();
            this.outputFolderTb = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMissingBinFilesButton = new System.Windows.Forms.Button();
            this.showMissingViewingFilesButton = new System.Windows.Forms.Button();
            this.useGMCb = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.deleteIntermediate = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kakPathTextField = new System.Windows.Forms.TextBox();
            this.binarizationPathTextField = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.userNewBinCb = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInputFolder
            // 
            this.buttonInputFolder.Location = new System.Drawing.Point(12, 37);
            this.buttonInputFolder.Name = "buttonInputFolder";
            this.buttonInputFolder.Size = new System.Drawing.Size(129, 23);
            this.buttonInputFolder.TabIndex = 1;
            this.buttonInputFolder.Text = "Set input folder...";
            this.buttonInputFolder.UseVisualStyleBackColor = true;
            this.buttonInputFolder.Click += new System.EventHandler(this.buttonInputFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(147, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current input folder:";
            // 
            // buttonOutputFolder
            // 
            this.buttonOutputFolder.Location = new System.Drawing.Point(13, 67);
            this.buttonOutputFolder.Name = "buttonOutputFolder";
            this.buttonOutputFolder.Size = new System.Drawing.Size(129, 23);
            this.buttonOutputFolder.TabIndex = 5;
            this.buttonOutputFolder.Text = "Set output folder...";
            this.buttonOutputFolder.UseVisualStyleBackColor = true;
            this.buttonOutputFolder.Click += new System.EventHandler(this.buttonOutputFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(147, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Current output folder:";
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(204, 133);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(98, 23);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "Start!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.jp2Cb);
            this.groupBox1.Controls.Add(this.pngCb);
            this.groupBox1.Controls.Add(this.jpgCb);
            this.groupBox1.Controls.Add(this.tifCb);
            this.groupBox1.Location = new System.Drawing.Point(12, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 78);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Formats";
            // 
            // jp2Cb
            // 
            this.jp2Cb.AutoSize = true;
            this.jp2Cb.Checked = true;
            this.jp2Cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.jp2Cb.Location = new System.Drawing.Point(76, 43);
            this.jp2Cb.Name = "jp2Cb";
            this.jp2Cb.Size = new System.Drawing.Size(73, 17);
            this.jp2Cb.TabIndex = 3;
            this.jp2Cb.Text = "JPG-2000";
            this.toolTip1.SetToolTip(this.jp2Cb, resources.GetString("jp2Cb.ToolTip"));
            this.jp2Cb.UseVisualStyleBackColor = true;
            // 
            // pngCb
            // 
            this.pngCb.AutoSize = true;
            this.pngCb.Checked = true;
            this.pngCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pngCb.Location = new System.Drawing.Point(77, 19);
            this.pngCb.Name = "pngCb";
            this.pngCb.Size = new System.Drawing.Size(49, 17);
            this.pngCb.TabIndex = 2;
            this.pngCb.Text = "PNG";
            this.pngCb.UseVisualStyleBackColor = true;
            // 
            // jpgCb
            // 
            this.jpgCb.AutoSize = true;
            this.jpgCb.Checked = true;
            this.jpgCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.jpgCb.Location = new System.Drawing.Point(13, 43);
            this.jpgCb.Name = "jpgCb";
            this.jpgCb.Size = new System.Drawing.Size(46, 17);
            this.jpgCb.TabIndex = 1;
            this.jpgCb.Text = "JPG";
            this.jpgCb.UseVisualStyleBackColor = true;
            // 
            // tifCb
            // 
            this.tifCb.AutoSize = true;
            this.tifCb.Checked = true;
            this.tifCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tifCb.Location = new System.Drawing.Point(13, 19);
            this.tifCb.Name = "tifCb";
            this.tifCb.Size = new System.Drawing.Size(42, 17);
            this.tifCb.TabIndex = 0;
            this.tifCb.Text = "TIF";
            this.tifCb.UseVisualStyleBackColor = true;
            // 
            // filesListBox
            // 
            this.filesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.HorizontalScrollbar = true;
            this.filesListBox.Location = new System.Drawing.Point(221, 365);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size(676, 355);
            this.filesListBox.TabIndex = 10;
            // 
            // updateFilesButton
            // 
            this.updateFilesButton.Location = new System.Drawing.Point(221, 337);
            this.updateFilesButton.Name = "updateFilesButton";
            this.updateFilesButton.Size = new System.Drawing.Size(90, 22);
            this.updateFilesButton.TabIndex = 3;
            this.updateFilesButton.Text = "Show input files";
            this.updateFilesButton.UseVisualStyleBackColor = true;
            this.updateFilesButton.Click += new System.EventHandler(this.updateFilesButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(417, 133);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(482, 23);
            this.progressBar.TabIndex = 12;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(308, 133);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(103, 23);
            this.stopButton.TabIndex = 13;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(201, 160);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(35, 13);
            this.infoLabel.TabIndex = 14;
            this.infoLabel.Text = "label4";
            // 
            // nThreadsCb
            // 
            this.nThreadsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nThreadsCb.Enabled = false;
            this.nThreadsCb.FormattingEnabled = true;
            this.nThreadsCb.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.nThreadsCb.Location = new System.Drawing.Point(84, 427);
            this.nThreadsCb.Name = "nThreadsCb";
            this.nThreadsCb.Size = new System.Drawing.Size(123, 21);
            this.nThreadsCb.TabIndex = 15;
            this.nThreadsCb.SelectedIndexChanged += new System.EventHandler(this.nThreadsCb_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 430);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Nr. of Threads:";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(201, 182);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(35, 13);
            this.timeLabel.TabIndex = 17;
            this.timeLabel.Text = "label4";
            // 
            // binCb
            // 
            this.binCb.AutoSize = true;
            this.binCb.Checked = true;
            this.binCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.binCb.Location = new System.Drawing.Point(12, 137);
            this.binCb.Name = "binCb";
            this.binCb.Size = new System.Drawing.Size(63, 17);
            this.binCb.TabIndex = 18;
            this.binCb.Text = "Binarize";
            this.binCb.UseVisualStyleBackColor = true;
            // 
            // convertCb
            // 
            this.convertCb.AutoSize = true;
            this.convertCb.Checked = true;
            this.convertCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.convertCb.Location = new System.Drawing.Point(81, 137);
            this.convertCb.Name = "convertCb";
            this.convertCb.Size = new System.Drawing.Size(117, 17);
            this.convertCb.TabIndex = 19;
            this.convertCb.Text = "Create viewing files";
            this.convertCb.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "GraphicsMagick path:";
            // 
            // imPathTextField
            // 
            this.imPathTextField.Location = new System.Drawing.Point(166, 275);
            this.imPathTextField.Name = "imPathTextField";
            this.imPathTextField.Size = new System.Drawing.Size(523, 20);
            this.imPathTextField.TabIndex = 22;
            // 
            // gppPathTextField
            // 
            this.gppPathTextField.Location = new System.Drawing.Point(153, 223);
            this.gppPathTextField.Name = "gppPathTextField";
            this.gppPathTextField.Size = new System.Drawing.Size(536, 20);
            this.gppPathTextField.TabIndex = 24;
            this.gppPathTextField.Visible = false;
            // 
            // useNProcessorsCb
            // 
            this.useNProcessorsCb.AutoSize = true;
            this.useNProcessorsCb.Checked = true;
            this.useNProcessorsCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useNProcessorsCb.Location = new System.Drawing.Point(88, 452);
            this.useNProcessorsCb.Name = "useNProcessorsCb";
            this.useNProcessorsCb.Size = new System.Drawing.Size(98, 17);
            this.useNProcessorsCb.TabIndex = 25;
            this.useNProcessorsCb.Text = "Use nr of cores";
            this.useNProcessorsCb.UseVisualStyleBackColor = true;
            this.useNProcessorsCb.CheckedChanged += new System.EventHandler(this.useNProcessorsCb_CheckedChanged);
            // 
            // autoSetPathsButton
            // 
            this.autoSetPathsButton.Location = new System.Drawing.Point(695, 273);
            this.autoSetPathsButton.Name = "autoSetPathsButton";
            this.autoSetPathsButton.Size = new System.Drawing.Size(124, 23);
            this.autoSetPathsButton.TabIndex = 26;
            this.autoSetPathsButton.Text = "Auto set paths";
            this.autoSetPathsButton.UseVisualStyleBackColor = true;
            this.autoSetPathsButton.Click += new System.EventHandler(this.autoSetPathsButton_Click);
            // 
            // overwriteCb
            // 
            this.overwriteCb.AutoSize = true;
            this.overwriteCb.Location = new System.Drawing.Point(12, 161);
            this.overwriteCb.Name = "overwriteCb";
            this.overwriteCb.Size = new System.Drawing.Size(104, 17);
            this.overwriteCb.TabIndex = 27;
            this.overwriteCb.Text = "Overwrite output\r\n";
            this.overwriteCb.UseVisualStyleBackColor = true;
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(12, 96);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(129, 23);
            this.compareButton.TabIndex = 28;
            this.compareButton.Text = "Compare Folders";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // compareLabel
            // 
            this.compareLabel.AutoSize = true;
            this.compareLabel.Location = new System.Drawing.Point(150, 101);
            this.compareLabel.Name = "compareLabel";
            this.compareLabel.Size = new System.Drawing.Size(35, 13);
            this.compareLabel.TabIndex = 29;
            this.compareLabel.Text = "label7";
            // 
            // ignoreOutputFoldersCb
            // 
            this.ignoreOutputFoldersCb.AutoSize = true;
            this.ignoreOutputFoldersCb.Checked = true;
            this.ignoreOutputFoldersCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreOutputFoldersCb.Location = new System.Drawing.Point(12, 182);
            this.ignoreOutputFoldersCb.Name = "ignoreOutputFoldersCb";
            this.ignoreOutputFoldersCb.Size = new System.Drawing.Size(190, 30);
            this.ignoreOutputFoldersCb.TabIndex = 30;
            this.ignoreOutputFoldersCb.Text = "Ignore default output folders\r\n(\'binarized_files\' and \'viewing_files\')\r\n";
            this.ignoreOutputFoldersCb.UseVisualStyleBackColor = true;
            this.ignoreOutputFoldersCb.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 490);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Viewing files output format:";
            // 
            // viewingFilesFormat
            // 
            this.viewingFilesFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewingFilesFormat.FormattingEnabled = true;
            this.viewingFilesFormat.Items.AddRange(new object[] {
            "jpg",
            "png",
            "gif",
            "jp2 (via kakadu)"});
            this.viewingFilesFormat.Location = new System.Drawing.Point(133, 487);
            this.viewingFilesFormat.Name = "viewingFilesFormat";
            this.viewingFilesFormat.Size = new System.Drawing.Size(82, 21);
            this.viewingFilesFormat.TabIndex = 32;
            // 
            // inputFolderTb
            // 
            this.inputFolderTb.Location = new System.Drawing.Point(285, 40);
            this.inputFolderTb.Name = "inputFolderTb";
            this.inputFolderTb.Size = new System.Drawing.Size(614, 20);
            this.inputFolderTb.TabIndex = 33;
            this.inputFolderTb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputFolderTb_KeyDown);
            // 
            // outputFolderTb
            // 
            this.outputFolderTb.Location = new System.Drawing.Point(285, 69);
            this.outputFolderTb.Name = "outputFolderTb";
            this.outputFolderTb.Size = new System.Drawing.Size(614, 20);
            this.outputFolderTb.TabIndex = 34;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // showMissingBinFilesButton
            // 
            this.showMissingBinFilesButton.Location = new System.Drawing.Point(317, 337);
            this.showMissingBinFilesButton.Name = "showMissingBinFilesButton";
            this.showMissingBinFilesButton.Size = new System.Drawing.Size(169, 22);
            this.showMissingBinFilesButton.TabIndex = 36;
            this.showMissingBinFilesButton.Text = "Show missing binarization files\r\n\r\n";
            this.showMissingBinFilesButton.UseVisualStyleBackColor = true;
            this.showMissingBinFilesButton.Click += new System.EventHandler(this.showMissingFilesButton_Click);
            // 
            // showMissingViewingFilesButton
            // 
            this.showMissingViewingFilesButton.Location = new System.Drawing.Point(492, 337);
            this.showMissingViewingFilesButton.Name = "showMissingViewingFilesButton";
            this.showMissingViewingFilesButton.Size = new System.Drawing.Size(151, 22);
            this.showMissingViewingFilesButton.TabIndex = 38;
            this.showMissingViewingFilesButton.Text = "Show missing viewing files\r\n\r\n";
            this.showMissingViewingFilesButton.UseVisualStyleBackColor = true;
            this.showMissingViewingFilesButton.Click += new System.EventHandler(this.showMissingViewingFilesButton_Click);
            // 
            // useGMCb
            // 
            this.useGMCb.AutoSize = true;
            this.useGMCb.Checked = true;
            this.useGMCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useGMCb.Location = new System.Drawing.Point(12, 564);
            this.useGMCb.Name = "useGMCb";
            this.useGMCb.Size = new System.Drawing.Size(125, 17);
            this.useGMCb.TabIndex = 39;
            this.useGMCb.Text = "Use GraphicsMagick";
            this.useGMCb.UseVisualStyleBackColor = true;
            this.useGMCb.Visible = false;
            // 
            // deleteIntermediate
            // 
            this.deleteIntermediate.AutoSize = true;
            this.deleteIntermediate.Checked = true;
            this.deleteIntermediate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteIntermediate.Location = new System.Drawing.Point(12, 587);
            this.deleteIntermediate.Name = "deleteIntermediate";
            this.deleteIntermediate.Size = new System.Drawing.Size(133, 17);
            this.deleteIntermediate.TabIndex = 46;
            this.deleteIntermediate.Text = "Delete intermediate file";
            this.toolTip1.SetToolTip(this.deleteIntermediate, "Delete intermediate file when creating jp2 files - or not...");
            this.deleteIntermediate.UseVisualStyleBackColor = true;
            this.deleteIntermediate.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Kakadu compress path:";
            // 
            // kakPathTextField
            // 
            this.kakPathTextField.Location = new System.Drawing.Point(166, 297);
            this.kakPathTextField.Name = "kakPathTextField";
            this.kakPathTextField.Size = new System.Drawing.Size(523, 20);
            this.kakPathTextField.TabIndex = 41;
            // 
            // binarizationPathTextField
            // 
            this.binarizationPathTextField.Location = new System.Drawing.Point(192, 249);
            this.binarizationPathTextField.Name = "binarizationPathTextField";
            this.binarizationPathTextField.Size = new System.Drawing.Size(497, 20);
            this.binarizationPathTextField.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Old Binarization (gpp.exe):";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(172, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "New Binarization (Binarization.exe):";
            // 
            // userNewBinCb
            // 
            this.userNewBinCb.AutoSize = true;
            this.userNewBinCb.Checked = true;
            this.userNewBinCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.userNewBinCb.Location = new System.Drawing.Point(695, 250);
            this.userNewBinCb.Name = "userNewBinCb";
            this.userNewBinCb.Size = new System.Drawing.Size(124, 17);
            this.userNewBinCb.TabIndex = 45;
            this.userNewBinCb.Text = "Use new binarization";
            this.userNewBinCb.UseVisualStyleBackColor = true;
            this.userNewBinCb.Visible = false;
            // 
            // GppGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 692);
            this.Controls.Add(this.deleteIntermediate);
            this.Controls.Add(this.userNewBinCb);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.binarizationPathTextField);
            this.Controls.Add(this.kakPathTextField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.useGMCb);
            this.Controls.Add(this.showMissingViewingFilesButton);
            this.Controls.Add(this.showMissingBinFilesButton);
            this.Controls.Add(this.outputFolderTb);
            this.Controls.Add(this.inputFolderTb);
            this.Controls.Add(this.viewingFilesFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ignoreOutputFoldersCb);
            this.Controls.Add(this.compareLabel);
            this.Controls.Add(this.compareButton);
            this.Controls.Add(this.overwriteCb);
            this.Controls.Add(this.autoSetPathsButton);
            this.Controls.Add(this.useNProcessorsCb);
            this.Controls.Add(this.gppPathTextField);
            this.Controls.Add(this.imPathTextField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.convertCb);
            this.Controls.Add(this.binCb);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nThreadsCb);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.updateFilesButton);
            this.Controls.Add(this.filesListBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOutputFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonInputFolder);
            this.Controls.Add(this.menuStrip1);
            this.Name = "GppGui";
            this.Text = "BCT - Binarisation and Conversion Tool";
            this.Load += new System.EventHandler(this.GppGuiLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GppGui_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonInputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox pngCb;
        private System.Windows.Forms.CheckBox jpgCb;
        private System.Windows.Forms.CheckBox tifCb;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.Button updateFilesButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.ComboBox nThreadsCb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.CheckBox binCb;
        private System.Windows.Forms.CheckBox convertCb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox imPathTextField;
        private System.Windows.Forms.TextBox gppPathTextField;
        private System.Windows.Forms.CheckBox useNProcessorsCb;
        private System.Windows.Forms.Button autoSetPathsButton;
        private System.Windows.Forms.CheckBox overwriteCb;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.Label compareLabel;
        private System.Windows.Forms.CheckBox ignoreOutputFoldersCb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox viewingFilesFormat;
        private System.Windows.Forms.TextBox inputFolderTb;
        private System.Windows.Forms.TextBox outputFolderTb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button showMissingBinFilesButton;
        private System.Windows.Forms.Button showMissingViewingFilesButton;
        private System.Windows.Forms.CheckBox jp2Cb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox kakPathTextField;
        private System.Windows.Forms.TextBox binarizationPathTextField;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.CheckBox userNewBinCb;
        public System.Windows.Forms.CheckBox useGMCb;
        public System.Windows.Forms.CheckBox deleteIntermediate;
    }
}

