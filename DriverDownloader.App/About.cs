using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DriverDownloader
{
    public partial class About : Form
    {

        private UpdateManager uman;

        public About()
        {
            InitializeComponent();
            updateInit();
        }

        private void updateInit()
        {
            uman = new UpdateManager();
            checkBox1.Checked = DriverDownloader.Core.Settings.Default.CheckUpdates;
        }


        private void okBtn_Click(object sender, EventArgs e)
        {
            DriverDownloader.Core.Settings.Default.CheckUpdates = checkBox1.Checked;
            DriverDownloader.Core.Settings.Default.Save();
            if (checkBox1.Checked)
                uman.InstallUpdateSyncWithInfo();
            DialogResult = DialogResult.OK;
        }

        private void About_Load(object sender, EventArgs e)
        {
            //lblVersion.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblVersion.Text = uman.GetRunningVersion();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
