namespace DriverDownloader.Core
{
    partial class FilterControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioShowAll = new System.Windows.Forms.RadioButton();
            this.radioShowUpdated = new System.Windows.Forms.RadioButton();
            this.radioShowLatest = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioShowAll);
            this.groupBox1.Controls.Add(this.radioShowUpdated);
            this.groupBox1.Controls.Add(this.radioShowLatest);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 173);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Filters";
            // 
            // radioShowAll
            // 
            this.radioShowAll.AutoSize = true;
            this.radioShowAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioShowAll.Location = new System.Drawing.Point(7, 112);
            this.radioShowAll.Name = "radioShowAll";
            this.radioShowAll.Size = new System.Drawing.Size(71, 19);
            this.radioShowAll.TabIndex = 14;
            this.radioShowAll.TabStop = true;
            this.radioShowAll.Text = "Show All";
            this.radioShowAll.UseVisualStyleBackColor = true;
            // 
            // radioShowUpdated
            // 
            this.radioShowUpdated.AutoSize = true;
            this.radioShowUpdated.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioShowUpdated.Location = new System.Drawing.Point(7, 67);
            this.radioShowUpdated.Name = "radioShowUpdated";
            this.radioShowUpdated.Size = new System.Drawing.Size(175, 19);
            this.radioShowUpdated.TabIndex = 13;
            this.radioShowUpdated.TabStop = true;
            this.radioShowUpdated.Text = "Only Show Updated Drivers";
            this.radioShowUpdated.UseVisualStyleBackColor = true;
            // 
            // radioShowLatest
            // 
            this.radioShowLatest.AutoSize = true;
            this.radioShowLatest.Checked = true;
            this.radioShowLatest.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioShowLatest.Location = new System.Drawing.Point(7, 20);
            this.radioShowLatest.Name = "radioShowLatest";
            this.radioShowLatest.Size = new System.Drawing.Size(101, 19);
            this.radioShowLatest.TabIndex = 12;
            this.radioShowLatest.TabStop = true;
            this.radioShowLatest.Text = "Latest Drivers";
            this.radioShowLatest.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(20, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Drivers that have been Updated only";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(20, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Drivers with latest Version numbers";
            // 
            // FilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FilterControl";
            this.Size = new System.Drawing.Size(269, 176);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioShowAll;
        private System.Windows.Forms.RadioButton radioShowUpdated;
        private System.Windows.Forms.RadioButton radioShowLatest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}
