namespace DriverDownloader
{
    partial class DriverList
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
            this.columnDownload = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDriverName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.driverListView = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.driverListView)).BeginInit();
            this.SuspendLayout();
            // 
            // columnDownload
            // 
            this.columnDownload.Text = "";
            this.columnDownload.Width = 40;
            // 
            // columnDriverName
            // 
            this.columnDriverName.Text = "Driver Name";
            this.columnDriverName.Width = 140;
            // 
            // columnVersion
            // 
            this.columnVersion.Text = "Version";
            // 
            // columnCategory
            // 
            this.columnCategory.Text = "Category";
            this.columnCategory.Width = 160;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Size";
            // 
            // columnDate
            // 
            this.columnDate.Text = "Date";
            this.columnDate.Width = 80;
            // 
            // driverListView
            // 
            this.driverListView.AllColumns.Add(this.olvColumn1);
            this.driverListView.AllColumns.Add(this.olvColumn2);
            this.driverListView.AllColumns.Add(this.olvColumn3);
            this.driverListView.AllColumns.Add(this.olvColumn4);
            this.driverListView.AllColumns.Add(this.olvColumn5);
            this.driverListView.AllColumns.Add(this.olvColumn6);
            this.driverListView.AllowColumnReorder = true;
            this.driverListView.AllowDrop = true;
            this.driverListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driverListView.CheckBoxes = true;
            this.driverListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6});
            this.driverListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.driverListView.EmptyListMsg = "No Driver Found!";
            this.driverListView.FullRowSelect = true;
            this.driverListView.HeaderUsesThemes = false;
            this.driverListView.HeaderWordWrap = true;
            this.driverListView.HideSelection = false;
            this.driverListView.Location = new System.Drawing.Point(0, 3);
            this.driverListView.Name = "driverListView";
            this.driverListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.driverListView.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.driverListView.ShowCommandMenuOnRightClick = true;
            this.driverListView.ShowGroups = false;
            this.driverListView.ShowImagesOnSubItems = true;
            this.driverListView.Size = new System.Drawing.Size(604, 170);
            this.driverListView.SortGroupItemsByPrimaryColumn = false;
            this.driverListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.driverListView.TabIndex = 0;
            this.driverListView.TriStateCheckBoxes = true;
            this.driverListView.UseCellFormatEvents = true;
            this.driverListView.UseCompatibleStateImageBehavior = false;
            this.driverListView.UseFiltering = true;
            this.driverListView.View = System.Windows.Forms.View.Details;
            this.driverListView.VirtualMode = true;
            this.driverListView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.driverListView_FormatRow);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Checkbox";
            this.olvColumn1.CheckBoxes = true;
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn1.MaximumWidth = 30;
            this.olvColumn1.MinimumWidth = 30;
            this.olvColumn1.Sortable = false;
            this.olvColumn1.Text = "";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Width = 30;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.MinimumWidth = 150;
            this.olvColumn2.Text = "Driver Name";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Width = 150;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Version";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.MinimumWidth = 60;
            this.olvColumn3.Text = "Version";
            this.olvColumn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Category";
            this.olvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.MinimumWidth = 120;
            this.olvColumn4.Text = "Category";
            this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Width = 120;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Size";
            this.olvColumn5.FillsFreeSpace = true;
            this.olvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn5.MinimumWidth = 60;
            this.olvColumn5.Text = "Size";
            this.olvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Date";
            this.olvColumn6.FillsFreeSpace = true;
            this.olvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn6.MinimumWidth = 80;
            this.olvColumn6.Text = "Date";
            this.olvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn6.Width = 80;
            // 
            // DriverList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.driverListView);
            this.Name = "DriverList";
            this.Size = new System.Drawing.Size(615, 185);
            ((System.ComponentModel.ISupportInitialize)(this.driverListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnDownload;
        private System.Windows.Forms.ColumnHeader columnDriverName;
        private System.Windows.Forms.ColumnHeader columnVersion;
        private System.Windows.Forms.ColumnHeader columnCategory;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.ColumnHeader columnDate;
        private BrightIdeasSoftware.FastObjectListView driverListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;

    }
}
