using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DriverDownloader.Core.Common;

namespace DriverDownloader.Core
{
    public partial class DownloadFolder : UserControl
    {
        public DownloadFolder()
        {
            InitializeComponent();

            Text = "Directory";

            // set download folder to executable's folder if it's empty (by default)
            if (Settings.Default.DownloadFolder.Length > 0)
            {
                txtSaveTo.Text = Settings.Default.DownloadFolder;
            }
            else
            {
                txtSaveTo.Text = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            }
        }

        public string LabelText
        {
            get
            {
                return lblText.Text;
            }
            set
            {
                lblText.Text = value;
            }
        }

        public string Folder
        {
            get { return PathHelper.GetWithBackslash(txtSaveTo.Text); }
        }

        private void btnSelAV_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSaveTo.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
