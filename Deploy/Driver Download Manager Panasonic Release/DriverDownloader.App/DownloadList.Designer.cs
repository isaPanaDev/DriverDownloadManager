namespace DriverDownloader
{
    partial class DownloadList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadList));
            this.logContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnCurrentTry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSegProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSegCompleted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSegSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStartPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEndPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSegRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSegLeft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSegState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCurrURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabSegmentsLogs = new System.Windows.Forms.TabControl();
            this.tabJobList = new System.Windows.Forms.TabPage();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolResumeStarted = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPause = new System.Windows.Forms.ToolStripButton();
            this.toolPauseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolRemove = new System.Windows.Forms.ToolStripButton();
            this.toolClearAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tabCompleted = new System.Windows.Forms.TabPage();
            this.toolStripCompleted = new System.Windows.Forms.ToolStrip();
            this.toolOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolClearCompleted = new System.Windows.Forms.ToolStripButton();
            this.completedListView = new System.Windows.Forms.ListView();
            this.columnCompletedFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCompletedSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCompletedState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabError = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolClearError = new System.Windows.Forms.ToolStripButton();
            this.errorListView = new System.Windows.Forms.ListView();
            this.columnErrorFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnErrorSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnErrorStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.contextMenuCompleted = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listViewColumn = new System.Windows.Forms.ListView();
            this.progressListView = new DriverDownloader.ListViewProgress();
            this.columnFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCompleted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProgressbar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logContextMenu.SuspendLayout();
            this.tabSegmentsLogs.SuspendLayout();
            this.tabJobList.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.tabCompleted.SuspendLayout();
            this.toolStripCompleted.SuspendLayout();
            this.tabError.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // logContextMenu
            // 
            this.logContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem});
            this.logContextMenu.Name = "logContextMenu";
            this.logContextMenu.Size = new System.Drawing.Size(125, 26);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.clearLogToolStripMenuItem.Text = global::DriverDownloader.Resources.common.DownloadList_UI_ClearLog;
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // columnCurrentTry
            // 
            this.columnCurrentTry.DisplayIndex = 0;
            this.columnCurrentTry.Text = global::DriverDownloader.Resources.common.DownloadList_UI_CurrentTry;
            this.columnCurrentTry.Width = 66;
            // 
            // columnSegProgress
            // 
            this.columnSegProgress.DisplayIndex = 1;
            this.columnSegProgress.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Progress;
            this.columnSegProgress.Width = 80;
            // 
            // columnSegCompleted
            // 
            this.columnSegCompleted.DisplayIndex = 2;
            this.columnSegCompleted.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Completed;
            this.columnSegCompleted.Width = 81;
            // 
            // columnSegSize
            // 
            this.columnSegSize.DisplayIndex = 3;
            this.columnSegSize.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Size;
            this.columnSegSize.Width = 77;
            // 
            // columnStartPosition
            // 
            this.columnStartPosition.DisplayIndex = 4;
            this.columnStartPosition.Text = global::DriverDownloader.Resources.common.DownloadList_UI_StartPos;
            this.columnStartPosition.Width = 97;
            // 
            // columnEndPosition
            // 
            this.columnEndPosition.DisplayIndex = 5;
            this.columnEndPosition.Text = global::DriverDownloader.Resources.common.DownloadList_UI_EndPos;
            this.columnEndPosition.Width = 100;
            // 
            // columnSegRate
            // 
            this.columnSegRate.DisplayIndex = 6;
            this.columnSegRate.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Rate;
            this.columnSegRate.Width = 55;
            // 
            // columnSegLeft
            // 
            this.columnSegLeft.DisplayIndex = 7;
            this.columnSegLeft.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Left;
            this.columnSegLeft.Width = 80;
            // 
            // columnSegState
            // 
            this.columnSegState.DisplayIndex = 8;
            this.columnSegState.Text = global::DriverDownloader.Resources.common.DownloadList_UI_State;
            this.columnSegState.Width = 133;
            // 
            // columnCurrURL
            // 
            this.columnCurrURL.DisplayIndex = 9;
            this.columnCurrURL.Text = global::DriverDownloader.Resources.common.DownloadList_UI_CurrentURL;
            this.columnCurrURL.Width = 100;
            // 
            // tabSegmentsLogs
            // 
            this.tabSegmentsLogs.Controls.Add(this.tabJobList);
            this.tabSegmentsLogs.Controls.Add(this.tabCompleted);
            this.tabSegmentsLogs.Controls.Add(this.tabError);
            this.tabSegmentsLogs.Controls.Add(this.tabLog);
            this.tabSegmentsLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSegmentsLogs.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabSegmentsLogs.Location = new System.Drawing.Point(0, 0);
            this.tabSegmentsLogs.Name = "tabSegmentsLogs";
            this.tabSegmentsLogs.SelectedIndex = 0;
            this.tabSegmentsLogs.Size = new System.Drawing.Size(784, 228);
            this.tabSegmentsLogs.TabIndex = 2;
            this.tabSegmentsLogs.SelectedIndexChanged += new System.EventHandler(this.tabSegmentsLogs_SelectedIndexChanged);
            // 
            // tabJobList
            // 
            this.tabJobList.Controls.Add(this.toolStripMain);
            this.tabJobList.Controls.Add(this.progressListView);
            this.tabJobList.Location = new System.Drawing.Point(4, 24);
            this.tabJobList.Name = "tabJobList";
            this.tabJobList.Padding = new System.Windows.Forms.Padding(3);
            this.tabJobList.Size = new System.Drawing.Size(776, 200);
            this.tabJobList.TabIndex = 2;
            this.tabJobList.Text = global::DriverDownloader.Resources.common.DownloadList_UI_JobList;
            this.tabJobList.UseVisualStyleBackColor = true;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolResumeStarted,
            this.toolStripSeparator1,
            this.toolPause,
            this.toolPauseAll,
            this.toolStripSeparator2,
            this.toolRemove,
            this.toolClearAll,
            this.toolStripSeparator3});
            this.toolStripMain.Location = new System.Drawing.Point(3, 172);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(770, 25);
            this.toolStripMain.TabIndex = 5;
            // 
            // toolResumeStarted
            // 
            this.toolResumeStarted.CheckOnClick = true;
            this.toolResumeStarted.Enabled = false;
            this.toolResumeStarted.Image = ((System.Drawing.Image)(resources.GetObject("toolResumeStarted.Image")));
            this.toolResumeStarted.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolResumeStarted.Name = "toolResumeStarted";
            this.toolResumeStarted.Size = new System.Drawing.Size(68, 22);
            this.toolResumeStarted.Text = global::DriverDownloader.Resources.common.DownloadList_UI_StartAll;
            this.toolResumeStarted.ToolTipText = global::DriverDownloader.Resources.common.DownloadList_Tooltip_Toggle;
            this.toolResumeStarted.Click += new System.EventHandler(this.toolResumeStarted_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolPause
            // 
            this.toolPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPause.Enabled = false;
            this.toolPause.Image = ((System.Drawing.Image)(resources.GetObject("toolPause.Image")));
            this.toolPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPause.Name = "toolPause";
            this.toolPause.Size = new System.Drawing.Size(23, 22);
            this.toolPause.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Pause;
            this.toolPause.Click += new System.EventHandler(this.toolPause_Click);
            // 
            // toolPauseAll
            // 
            this.toolPauseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPauseAll.Enabled = false;
            this.toolPauseAll.Image = ((System.Drawing.Image)(resources.GetObject("toolPauseAll.Image")));
            this.toolPauseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPauseAll.Name = "toolPauseAll";
            this.toolPauseAll.Size = new System.Drawing.Size(23, 22);
            this.toolPauseAll.Text = global::DriverDownloader.Resources.common.DownloadList_U_PauseAll;
            this.toolPauseAll.Click += new System.EventHandler(this.toolPauseAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolRemove
            // 
            this.toolRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolRemove.Enabled = false;
            this.toolRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolRemove.Image")));
            this.toolRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRemove.Name = "toolRemove";
            this.toolRemove.Size = new System.Drawing.Size(23, 22);
            this.toolRemove.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Remove;
            this.toolRemove.Click += new System.EventHandler(this.toolRemove_Click);
            // 
            // toolClearAll
            // 
            this.toolClearAll.Image = ((System.Drawing.Image)(resources.GetObject("toolClearAll.Image")));
            this.toolClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClearAll.Name = "toolClearAll";
            this.toolClearAll.Size = new System.Drawing.Size(71, 22);
            this.toolClearAll.Text = global::DriverDownloader.Resources.common.DownloadList_UI_ClearAll;
            this.toolClearAll.Click += new System.EventHandler(this.toolClearAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tabCompleted
            // 
            this.tabCompleted.Controls.Add(this.toolStripCompleted);
            this.tabCompleted.Controls.Add(this.completedListView);
            this.tabCompleted.Location = new System.Drawing.Point(4, 24);
            this.tabCompleted.Name = "tabCompleted";
            this.tabCompleted.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompleted.Size = new System.Drawing.Size(776, 200);
            this.tabCompleted.TabIndex = 0;
            this.tabCompleted.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Completed;
            this.tabCompleted.UseVisualStyleBackColor = true;
            // 
            // toolStripCompleted
            // 
            this.toolStripCompleted.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripCompleted.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpenFile,
            this.toolClearCompleted});
            this.toolStripCompleted.Location = new System.Drawing.Point(3, 172);
            this.toolStripCompleted.Name = "toolStripCompleted";
            this.toolStripCompleted.Size = new System.Drawing.Size(770, 25);
            this.toolStripCompleted.TabIndex = 6;
            // 
            // toolOpenFile
            // 
            this.toolOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("toolOpenFile.Image")));
            this.toolOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOpenFile.Name = "toolOpenFile";
            this.toolOpenFile.Size = new System.Drawing.Size(23, 22);
            this.toolOpenFile.Text = global::DriverDownloader.Resources.common.DownloadList_Tooltip_OpenFile;
            this.toolOpenFile.Click += new System.EventHandler(this.toolOpenFile_Click);
            // 
            // toolClearCompleted
            // 
            this.toolClearCompleted.Image = ((System.Drawing.Image)(resources.GetObject("toolClearCompleted.Image")));
            this.toolClearCompleted.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClearCompleted.Name = "toolClearCompleted";
            this.toolClearCompleted.Size = new System.Drawing.Size(71, 22);
            this.toolClearCompleted.Text = global::DriverDownloader.Resources.common.DownloadList_UI_ClearAll;
            this.toolClearCompleted.Click += new System.EventHandler(this.toolClearCompleted_Click);
            // 
            // completedListView
            // 
            this.completedListView.BackgroundImageTiled = true;
            this.completedListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.completedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCompletedFile,
            this.columnCompletedSize,
            this.columnCompletedState});
            this.completedListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.completedListView.FullRowSelect = true;
            this.completedListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.completedListView.HideSelection = false;
            this.completedListView.Location = new System.Drawing.Point(3, 3);
            this.completedListView.Name = "completedListView";
            this.completedListView.ShowGroups = false;
            this.completedListView.ShowItemToolTips = true;
            this.completedListView.Size = new System.Drawing.Size(770, 194);
            this.completedListView.TabIndex = 2;
            this.completedListView.UseCompatibleStateImageBehavior = false;
            this.completedListView.View = System.Windows.Forms.View.Details;
            this.completedListView.DoubleClick += new System.EventHandler(this.completedListView_DoubleClick);
            // 
            // columnCompletedFile
            // 
            this.columnCompletedFile.Text = global::DriverDownloader.Resources.common.DownloadList_UI_File;
            this.columnCompletedFile.Width = 400;
            // 
            // columnCompletedSize
            // 
            this.columnCompletedSize.Text = global::DriverDownloader.Resources.common.DownloadList_UI_SizeMB;
            this.columnCompletedSize.Width = 70;
            // 
            // columnCompletedState
            // 
            this.columnCompletedState.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Status;
            this.columnCompletedState.Width = 90;
            // 
            // tabError
            // 
            this.tabError.Controls.Add(this.toolStrip2);
            this.tabError.Controls.Add(this.errorListView);
            this.tabError.Controls.Add(this.toolStrip1);
            this.tabError.ForeColor = System.Drawing.Color.Red;
            this.tabError.Location = new System.Drawing.Point(4, 24);
            this.tabError.Name = "tabError";
            this.tabError.Padding = new System.Windows.Forms.Padding(3);
            this.tabError.Size = new System.Drawing.Size(776, 200);
            this.tabError.TabIndex = 3;
            this.tabError.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Error;
            this.tabError.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolClearError});
            this.toolStrip2.Location = new System.Drawing.Point(3, 172);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(770, 25);
            this.toolStrip2.TabIndex = 9;
            // 
            // toolClearError
            // 
            this.toolClearError.ForeColor = System.Drawing.Color.Black;
            this.toolClearError.Image = ((System.Drawing.Image)(resources.GetObject("toolClearError.Image")));
            this.toolClearError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClearError.Name = "toolClearError";
            this.toolClearError.Size = new System.Drawing.Size(71, 22);
            this.toolClearError.Text = global::DriverDownloader.Resources.common.DownloadList_UI_ClearAll;
            this.toolClearError.Click += new System.EventHandler(this.toolClearError_Click);
            // 
            // errorListView
            // 
            this.errorListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnErrorFile,
            this.columnErrorSize,
            this.columnErrorStatus});
            this.errorListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorListView.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorListView.ForeColor = System.Drawing.Color.Red;
            this.errorListView.Location = new System.Drawing.Point(3, 3);
            this.errorListView.Name = "errorListView";
            this.errorListView.Size = new System.Drawing.Size(770, 194);
            this.errorListView.TabIndex = 8;
            this.errorListView.UseCompatibleStateImageBehavior = false;
            this.errorListView.View = System.Windows.Forms.View.Details;
            // 
            // columnErrorFile
            // 
            this.columnErrorFile.Text = global::DriverDownloader.Resources.common.DownloadList_UI_File;
            this.columnErrorFile.Width = 330;
            // 
            // columnErrorSize
            // 
            this.columnErrorSize.Text = global::DriverDownloader.Resources.common.DownloadList_UI_SizeMB;
            this.columnErrorSize.Width = 80;
            // 
            // columnErrorStatus
            // 
            this.columnErrorStatus.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Status;
            this.columnErrorStatus.Width = 120;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 174);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton2.Text = global::DriverDownloader.Resources.common.DownloadList_UI_ClearAll;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.listViewColumn);
            this.tabLog.Location = new System.Drawing.Point(4, 24);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(776, 200);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Log;
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // contextMenuCompleted
            // 
            this.contextMenuCompleted.Name = "contextMenuCompleted";
            this.contextMenuCompleted.Size = new System.Drawing.Size(61, 4);
            // 
            // listViewColumn
            // 
            this.listViewColumn.Location = new System.Drawing.Point(0, 0);
            this.listViewColumn.Name = "listViewColumn";
            this.listViewColumn.Size = new System.Drawing.Size(776, 201);
            this.listViewColumn.TabIndex = 0;
            this.listViewColumn.UseCompatibleStateImageBehavior = false;
            this.listViewColumn.View = System.Windows.Forms.View.Details;
            // 
            // progressListView
            // 
            this.progressListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.progressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFile,
            this.columnSize,
            this.columnCompleted,
            this.columnState,
            this.columnProgressbar});
            this.progressListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressListView.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressListView.FullRowSelect = true;
            this.progressListView.Location = new System.Drawing.Point(3, 3);
            this.progressListView.Name = "progressListView";
            this.progressListView.Size = new System.Drawing.Size(770, 194);
            this.progressListView.TabIndex = 4;
            this.progressListView.UseCompatibleStateImageBehavior = false;
            this.progressListView.View = System.Windows.Forms.View.Details;
            this.progressListView.SelectedIndexChanged += new System.EventHandler(this.progressListView_SelectedIndexChanged);
            // 
            // columnFile
            // 
            this.columnFile.Text = global::DriverDownloader.Resources.common.DownloadList_UI_File;
            this.columnFile.Width = 270;
            // 
            // columnSize
            // 
            this.columnSize.Text = global::DriverDownloader.Resources.common.DownloadList_UI_SizeMB;
            this.columnSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnSize.Width = 75;
            // 
            // columnCompleted
            // 
            this.columnCompleted.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Done;
            this.columnCompleted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnCompleted.Width = 75;
            // 
            // columnState
            // 
            this.columnState.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Status;
            this.columnState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnState.Width = 95;
            // 
            // columnProgressbar
            // 
            this.columnProgressbar.Text = global::DriverDownloader.Resources.common.DownloadList_UI_Progress;
            this.columnProgressbar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnProgressbar.Width = 230;
            // 
            // DownloadList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabSegmentsLogs);
            this.Name = "DownloadList";
            this.Size = new System.Drawing.Size(784, 228);
            this.logContextMenu.ResumeLayout(false);
            this.tabSegmentsLogs.ResumeLayout(false);
            this.tabJobList.ResumeLayout(false);
            this.tabJobList.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.tabCompleted.ResumeLayout(false);
            this.tabCompleted.PerformLayout();
            this.toolStripCompleted.ResumeLayout(false);
            this.toolStripCompleted.PerformLayout();
            this.tabError.ResumeLayout(false);
            this.tabError.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip logContextMenu;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnCurrentTry;
        private System.Windows.Forms.ColumnHeader columnSegProgress;
        private System.Windows.Forms.ColumnHeader columnSegCompleted;
        private System.Windows.Forms.ColumnHeader columnSegSize;
        private System.Windows.Forms.ColumnHeader columnStartPosition;
        private System.Windows.Forms.ColumnHeader columnEndPosition;
        private System.Windows.Forms.ColumnHeader columnSegRate;
        private System.Windows.Forms.ColumnHeader columnSegLeft;
        private System.Windows.Forms.ColumnHeader columnSegState;
        private System.Windows.Forms.ColumnHeader columnCurrURL;
        private System.Windows.Forms.TabControl tabSegmentsLogs;
        private System.Windows.Forms.TabPage tabJobList;
        private System.Windows.Forms.TabPage tabCompleted;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.ListView completedListView;
        private System.Windows.Forms.ColumnHeader columnCompletedFile;
        private System.Windows.Forms.ColumnHeader columnCompletedSize;
        private System.Windows.Forms.ColumnHeader columnCompletedState;
        private System.Windows.Forms.ContextMenuStrip contextMenuCompleted;
        private ListViewProgress progressListView;
        private System.Windows.Forms.ColumnHeader columnFile;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.ColumnHeader columnCompleted;
        private System.Windows.Forms.ColumnHeader columnState;
        private System.Windows.Forms.ColumnHeader columnProgressbar;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolResumeStarted;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolPause;
        private System.Windows.Forms.ToolStripButton toolPauseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolRemove;
        private System.Windows.Forms.ToolStripButton toolClearAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage tabError;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ListView errorListView;
        private System.Windows.Forms.ColumnHeader columnErrorFile;
        private System.Windows.Forms.ColumnHeader columnErrorSize;
        private System.Windows.Forms.ColumnHeader columnErrorStatus;
        private System.Windows.Forms.ToolStrip toolStripCompleted;
        private System.Windows.Forms.ToolStripButton toolOpenFile;
        private System.Windows.Forms.ToolStripButton toolClearCompleted;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolClearError;
        private System.Windows.Forms.ListView listViewColumn;
    }
}
