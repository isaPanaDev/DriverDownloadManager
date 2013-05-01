using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public partial class DownloadThreads : UserControl
    {
        public DownloadThreads()
        {
            InitializeComponent();
            this.Text = "Downloads";

            concurrentMaxJobs.Value = Settings.Default.concurrentMaxJobs;
        }
        public int ConcurrentThreads
        {
            get { return int.Parse(concurrentMaxJobs.Value.ToString()); }
        }
    }
}
