using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public partial class Jobs : UserControl
    {
        public Jobs()
        {
            InitializeComponent();

            Text = "Auto-Downloads";

            numMaxJobs.Value = Settings.Default.concurrentMaxJobs;
            chkUseTime.Checked = Settings.Default.WorkOnlyOnSpecifiedTimes;
            numMaxRate.Value = (decimal)(Math.Max(Settings.Default.MaxRateOnTime, 1024) / 1024.0);
           
            UpdateUI();
        }

        private void chkUseTime_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            pnlTime.Enabled = chkUseTime.Checked;
        }

        public int MaxJobs
        {
            get { return (int)numMaxJobs.Value; }
        }

        public double MaxRate
        {
            get { return ((double)numMaxRate.Value) * 1024; }
        }

        public bool WorkOnlyOnSpecifiedTimes
        {
            get { return chkUseTime.Checked; }
        }
    }
}
