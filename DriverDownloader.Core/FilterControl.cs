using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public partial class FilterControl : UserControl
    {
        public FilterControl()
        {
            InitializeComponent();

            this.Text = "Driver Filters";
            
            radioShowAll.Checked = Settings.Default.AllChecked;
            radioShowUpdated.Checked = Settings.Default.UpdatedOnlyChecked;
            radioShowLatest.Checked = Settings.Default.LatestDriversChecked;
        }
        public bool ShowLatestOnly
        {
            get { return radioShowLatest.Checked; }
        }
        public bool ShowAllChecked
        {
            get { return radioShowAll.Checked; }
        }
        public bool ShowUpdatedOnly
        {
            get { return radioShowUpdated.Checked; }
        }
    }
}
