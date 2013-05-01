namespace DriverDownloader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuBarStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.modelBox = new System.Windows.Forms.GroupBox();
            this.viewDriversBtn = new System.Windows.Forms.Button();
            this.osLabel = new System.Windows.Forms.Label();
            this.osCombox = new System.Windows.Forms.ComboBox();
            this.markLabel = new System.Windows.Forms.Label();
            this.modelLabel = new System.Windows.Forms.Label();
            this.markCombox = new System.Windows.Forms.ComboBox();
            this.modelCombox = new System.Windows.Forms.ComboBox();
            this.dirLabel = new System.Windows.Forms.Label();
            this.driverwaitWorker = new System.ComponentModel.BackgroundWorker();
            this.dirLinkLabel = new System.Windows.Forms.LinkLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.displayLabel = new System.Windows.Forms.Label();
            this.countLabel = new System.Windows.Forms.Label();
            this.downloadBtn = new System.Windows.Forms.Button();
            this.selectAllCheckbox = new System.Windows.Forms.CheckBox();
            this.driverList = new DriverDownloader.DriverList();
            this.downloadList = new DriverDownloader.DownloadList();
            this.tooltipTxt = new System.Windows.Forms.TextBox();
            this.linkLabelJira = new System.Windows.Forms.LinkLabel();
            this.waitControl = new MyDownloader.Core.WaitControl();
            this.menuBarStrip.SuspendLayout();
            this.modelBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBarStrip
            // 
            this.menuBarStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuBarStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuBarStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.menuBarStrip.Location = new System.Drawing.Point(0, 0);
            this.menuBarStrip.Name = "menuBarStrip";
            this.menuBarStrip.Size = new System.Drawing.Size(800, 27);
            this.menuBarStrip.TabIndex = 0;
            this.menuBarStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.optionsToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.optionsToolStripMenuItem.Text = "Tools";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(45, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.languageToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.languageToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(145, 23);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 1500;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // modelBox
            // 
            this.modelBox.Controls.Add(this.viewDriversBtn);
            this.modelBox.Controls.Add(this.osLabel);
            this.modelBox.Controls.Add(this.osCombox);
            this.modelBox.Controls.Add(this.markLabel);
            this.modelBox.Controls.Add(this.modelLabel);
            this.modelBox.Controls.Add(this.markCombox);
            this.modelBox.Controls.Add(this.modelCombox);
            this.modelBox.Location = new System.Drawing.Point(30, 29);
            this.modelBox.Name = "modelBox";
            this.modelBox.Size = new System.Drawing.Size(592, 64);
            this.modelBox.TabIndex = 20;
            this.modelBox.TabStop = false;
            // 
            // viewDriversBtn
            // 
            this.viewDriversBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.viewDriversBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.viewDriversBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewDriversBtn.Location = new System.Drawing.Point(483, 16);
            this.viewDriversBtn.Name = "viewDriversBtn";
            this.viewDriversBtn.Size = new System.Drawing.Size(100, 42);
            this.viewDriversBtn.TabIndex = 56;
            this.viewDriversBtn.Text = "View Drivers";
            this.viewDriversBtn.UseVisualStyleBackColor = false;
            this.viewDriversBtn.Click += new System.EventHandler(this.viewDriversBtn_Click);
            // 
            // osLabel
            // 
            this.osLabel.AutoSize = true;
            this.osLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osLabel.Location = new System.Drawing.Point(318, 14);
            this.osLabel.Name = "osLabel";
            this.osLabel.Size = new System.Drawing.Size(105, 15);
            this.osLabel.TabIndex = 23;
            this.osLabel.Text = "Operating System";
            // 
            // osCombox
            // 
            this.osCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.osCombox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.osCombox.FormattingEnabled = true;
            this.osCombox.Location = new System.Drawing.Point(316, 31);
            this.osCombox.Name = "osCombox";
            this.osCombox.Size = new System.Drawing.Size(161, 23);
            this.osCombox.TabIndex = 22;
            // 
            // markLabel
            // 
            this.markLabel.AutoSize = true;
            this.markLabel.BackColor = System.Drawing.SystemColors.Control;
            this.markLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.markLabel.Location = new System.Drawing.Point(158, 14);
            this.markLabel.Name = "markLabel";
            this.markLabel.Size = new System.Drawing.Size(84, 15);
            this.markLabel.TabIndex = 21;
            this.markLabel.Text = "Model Version";
            // 
            // modelLabel
            // 
            this.modelLabel.AutoSize = true;
            this.modelLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelLabel.Location = new System.Drawing.Point(18, 14);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(88, 15);
            this.modelLabel.TabIndex = 20;
            this.modelLabel.Text = "Model Number";
            // 
            // markCombox
            // 
            this.markCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.markCombox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.markCombox.FormattingEnabled = true;
            this.markCombox.Location = new System.Drawing.Point(154, 31);
            this.markCombox.Name = "markCombox";
            this.markCombox.Size = new System.Drawing.Size(151, 23);
            this.markCombox.TabIndex = 19;
            this.markCombox.SelectionChangeCommitted += new System.EventHandler(this.markCombox_SelectionChangeCommitted);
            this.markCombox.SelectedValueChanged += new System.EventHandler(this.markCombox_SelectedValueChanged);
            // 
            // modelCombox
            // 
            this.modelCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelCombox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelCombox.FormattingEnabled = true;
            this.modelCombox.Location = new System.Drawing.Point(13, 31);
            this.modelCombox.Name = "modelCombox";
            this.modelCombox.Size = new System.Drawing.Size(130, 23);
            this.modelCombox.TabIndex = 0;
            this.modelCombox.SelectionChangeCommitted += new System.EventHandler(this.modelCombox_SelectionChangeCommitted);
            this.modelCombox.SelectedValueChanged += new System.EventHandler(this.modelCombox_SelectedValueChanged);
            // 
            // dirLabel
            // 
            this.dirLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dirLabel.AutoSize = true;
            this.dirLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dirLabel.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.dirLabel.Location = new System.Drawing.Point(22, 561);
            this.dirLabel.Name = "dirLabel";
            this.dirLabel.Size = new System.Drawing.Size(159, 15);
            this.dirLabel.TabIndex = 36;
            this.dirLabel.Text = "Default Download Directory:";
            // 
            // driverwaitWorker
            // 
            this.driverwaitWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.driverwaitWorker_DoWork);
            this.driverwaitWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.driverwaitWorker_Completed);
            // 
            // dirLinkLabel
            // 
            this.dirLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dirLinkLabel.AutoSize = true;
            this.dirLinkLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dirLinkLabel.Location = new System.Drawing.Point(182, 561);
            this.dirLinkLabel.Name = "dirLinkLabel";
            this.dirLinkLabel.Size = new System.Drawing.Size(19, 15);
            this.dirLinkLabel.TabIndex = 39;
            this.dirLinkLabel.TabStop = true;
            this.dirLinkLabel.Text = "c:\\";
            this.dirLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.dirLinkLabel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 97);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.displayLabel);
            this.splitContainer1.Panel1.Controls.Add(this.countLabel);
            this.splitContainer1.Panel1.Controls.Add(this.downloadBtn);
            this.splitContainer1.Panel1.Controls.Add(this.selectAllCheckbox);
            this.splitContainer1.Panel1.Controls.Add(this.driverList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.downloadList);
            this.splitContainer1.Size = new System.Drawing.Size(776, 461);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 55;
            // 
            // displayLabel
            // 
            this.displayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.displayLabel.BackColor = System.Drawing.Color.LawnGreen;
            this.displayLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.displayLabel.Location = new System.Drawing.Point(624, 145);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Size = new System.Drawing.Size(132, 51);
            this.displayLabel.TabIndex = 57;
            this.displayLabel.Text = "Highlighted items are\r\nalready in the target directory";
            this.displayLabel.Visible = false;
            // 
            // countLabel
            // 
            this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.countLabel.AutoSize = true;
            this.countLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countLabel.ForeColor = System.Drawing.Color.MediumBlue;
            this.countLabel.Location = new System.Drawing.Point(625, 10);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(74, 18);
            this.countLabel.TabIndex = 56;
            this.countLabel.Text = "0 Found";
            this.countLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.countLabel.Visible = false;
            // 
            // downloadBtn
            // 
            this.downloadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.downloadBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.downloadBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadBtn.Location = new System.Drawing.Point(625, 79);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(93, 42);
            this.downloadBtn.TabIndex = 55;
            this.downloadBtn.Text = "Download";
            this.downloadBtn.UseVisualStyleBackColor = false;
            this.downloadBtn.Visible = false;
            this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
            // 
            // selectAllCheckbox
            // 
            this.selectAllCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectAllCheckbox.Checked = true;
            this.selectAllCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectAllCheckbox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAllCheckbox.Location = new System.Drawing.Point(626, 52);
            this.selectAllCheckbox.Name = "selectAllCheckbox";
            this.selectAllCheckbox.Size = new System.Drawing.Size(150, 23);
            this.selectAllCheckbox.TabIndex = 54;
            this.selectAllCheckbox.Text = "Select/UnSelect";
            this.selectAllCheckbox.UseVisualStyleBackColor = true;
            this.selectAllCheckbox.Visible = false;
            this.selectAllCheckbox.Click += new System.EventHandler(this.selectAllCheckbox_Click);
            // 
            // driverList
            // 
            this.driverList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driverList.BackColor = System.Drawing.SystemColors.Control;
            this.driverList.Location = new System.Drawing.Point(0, 0);
            this.driverList.Name = "driverList";
            this.driverList.Size = new System.Drawing.Size(610, 213);
            this.driverList.TabIndex = 52;
            // 
            // downloadList
            // 
            this.downloadList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadList.Location = new System.Drawing.Point(0, 0);
            this.downloadList.Name = "downloadList";
            this.downloadList.Size = new System.Drawing.Size(776, 218);
            this.downloadList.TabIndex = 0;
            // 
            // tooltipTxt
            // 
            this.tooltipTxt.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tooltipTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tooltipTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tooltipTxt.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tooltipTxt.Location = new System.Drawing.Point(16, 59);
            this.tooltipTxt.Multiline = true;
            this.tooltipTxt.Name = "tooltipTxt";
            this.tooltipTxt.ReadOnly = true;
            this.tooltipTxt.Size = new System.Drawing.Size(11, 25);
            this.tooltipTxt.TabIndex = 42;
            this.tooltipTxt.Text = "?\r\n";
            this.tooltipTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // linkLabelJira
            // 
            this.linkLabelJira.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelJira.AutoSize = true;
            this.linkLabelJira.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelJira.Location = new System.Drawing.Point(697, 561);
            this.linkLabelJira.Name = "linkLabelJira";
            this.linkLabelJira.Size = new System.Drawing.Size(95, 15);
            this.linkLabelJira.TabIndex = 59;
            this.linkLabelJira.TabStop = true;
            this.linkLabelJira.Text = "Report an Issue";
            this.linkLabelJira.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelJira_LinkClicked);
            // 
            // waitControl
            // 
            this.waitControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitControl.Location = new System.Drawing.Point(628, 55);
            this.waitControl.Name = "waitControl";
            this.waitControl.Size = new System.Drawing.Size(137, 25);
            this.waitControl.TabIndex = 58;
            this.waitControl.Text = "looking for drivers....";
            this.waitControl.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 578);
            this.Controls.Add(this.linkLabelJira);
            this.Controls.Add(this.tooltipTxt);
            this.Controls.Add(this.waitControl);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dirLinkLabel);
            this.Controls.Add(this.dirLabel);
            this.Controls.Add(this.modelBox);
            this.Controls.Add(this.menuBarStrip);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBarStrip;
            this.MinimumSize = new System.Drawing.Size(760, 416);
            this.Name = "MainForm";
            this.Text = "Driver Download Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuBarStrip.ResumeLayout(false);
            this.menuBarStrip.PerformLayout();
            this.modelBox.ResumeLayout(false);
            this.modelBox.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBarStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox modelBox;
        private System.Windows.Forms.Label osLabel;
        private System.Windows.Forms.ComboBox osCombox;
        private System.Windows.Forms.Label markLabel;
        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.ComboBox markCombox;
        private System.Windows.Forms.ComboBox modelCombox;
        private System.Windows.Forms.ToolStripComboBox languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.Label dirLabel;
        private System.ComponentModel.BackgroundWorker driverwaitWorker;
        private System.Windows.Forms.LinkLabel dirLinkLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DownloadList downloadList;
        private System.Windows.Forms.Button viewDriversBtn;
        private MyDownloader.Core.WaitControl waitControl;
        private System.Windows.Forms.TextBox tooltipTxt;
        private System.Windows.Forms.LinkLabel linkLabelJira;
        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.CheckBox selectAllCheckbox;
        private DriverList driverList;
    }
}

