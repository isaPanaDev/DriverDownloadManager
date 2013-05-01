namespace DriverDownloader.Core
{
    partial class DownloadThreads
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
            this.concurrentMaxJobs = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maxSleep = new System.Windows.Forms.NumericUpDown();
            this.maxSleepLabel = new System.Windows.Forms.Label();
            this.maxRetries = new System.Windows.Forms.NumericUpDown();
            this.maxRetriesLabel = new System.Windows.Forms.Label();
            this.uptoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.concurrentMaxJobs)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRetries)).BeginInit();
            this.SuspendLayout();
            // 
            // concurrentMaxJobs
            // 
            this.concurrentMaxJobs.Location = new System.Drawing.Point(112, 24);
            this.concurrentMaxJobs.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.concurrentMaxJobs.Name = "concurrentMaxJobs";
            this.concurrentMaxJobs.Size = new System.Drawing.Size(47, 21);
            this.concurrentMaxJobs.TabIndex = 14;
            this.concurrentMaxJobs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.concurrentMaxJobs.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maxSleep);
            this.groupBox1.Controls.Add(this.maxSleepLabel);
            this.groupBox1.Controls.Add(this.maxRetries);
            this.groupBox1.Controls.Add(this.maxRetriesLabel);
            this.groupBox1.Controls.Add(this.concurrentMaxJobs);
            this.groupBox1.Controls.Add(this.uptoLabel);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 173);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Concurrent Downloads1";
            // 
            // maxSleep
            // 
            this.maxSleep.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maxSleep.Location = new System.Drawing.Point(112, 86);
            this.maxSleep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.maxSleep.Name = "maxSleep";
            this.maxSleep.Size = new System.Drawing.Size(50, 21);
            this.maxSleep.TabIndex = 22;
            this.maxSleep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maxSleep.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // maxSleepLabel
            // 
            this.maxSleepLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxSleepLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.maxSleepLabel.Location = new System.Drawing.Point(7, 88);
            this.maxSleepLabel.Name = "maxSleepLabel";
            this.maxSleepLabel.Size = new System.Drawing.Size(98, 20);
            this.maxSleepLabel.TabIndex = 21;
            this.maxSleepLabel.Text = "Max Sleep Time";
            // 
            // maxRetries
            // 
            this.maxRetries.Location = new System.Drawing.Point(112, 55);
            this.maxRetries.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxRetries.Name = "maxRetries";
            this.maxRetries.Size = new System.Drawing.Size(50, 21);
            this.maxRetries.TabIndex = 20;
            this.maxRetries.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maxRetries.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // maxRetriesLabel
            // 
            this.maxRetriesLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxRetriesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.maxRetriesLabel.Location = new System.Drawing.Point(7, 57);
            this.maxRetriesLabel.Name = "maxRetriesLabel";
            this.maxRetriesLabel.Size = new System.Drawing.Size(76, 20);
            this.maxRetriesLabel.TabIndex = 19;
            this.maxRetriesLabel.Text = "Max Retries";
            // 
            // uptoLabel
            // 
            this.uptoLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uptoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.uptoLabel.Location = new System.Drawing.Point(7, 26);
            this.uptoLabel.Name = "uptoLabel";
            this.uptoLabel.Size = new System.Drawing.Size(101, 20);
            this.uptoLabel.TabIndex = 18;
            this.uptoLabel.Text = "Up to (Max jobs)";
            // 
            // DownloadThreads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DownloadThreads";
            this.Size = new System.Drawing.Size(236, 179);
            ((System.ComponentModel.ISupportInitialize)(this.concurrentMaxJobs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxSleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRetries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown concurrentMaxJobs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label maxSleepLabel;
        private System.Windows.Forms.NumericUpDown maxRetries;
        private System.Windows.Forms.Label maxRetriesLabel;
        private System.Windows.Forms.Label uptoLabel;
        private System.Windows.Forms.NumericUpDown maxSleep;
    }
}
