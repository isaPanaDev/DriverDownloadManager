using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DriverDownloader.Core;

namespace DriverDownloader
{
    public partial class OptionsForm : Form
    {
        private MainForm mainForm;
        delegate void ProcessItemDelegate(IOptions extension, Control[] options);
        public event ChangedEventHandler DirectoryChanged;
        bool saveSettings = true;

        public OptionsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            try
            {
                treeOptions.BeginUpdate();

                for (int i = 0; i < mainForm.Extensions.Count; i++)
                {
                    IOptions option = mainForm.Extensions[i];
                    ISubOptions subOption = option.UIExtension;

                    if (subOption == null)
                    {
                        continue;
                    }

                    Control[] options = subOption.CreateSettingsView();
                    if (options == null || options.Length == 0)
                    {
                        continue;
                    }

                    TreeNode node = new TreeNode(option.Name);
                    node.Tag = option;
                    //if (option.Name != "Language")
                    //{
                        for (int j = 0; j < options.Length; j++)
                        {
                            TreeNode optioNd = new TreeNode(options[j].Text);
                            optioNd.Tag = options[j];
                            node.Nodes.Add(optioNd);
                        }
                        node.Expand();
                        treeOptions.Nodes.Add(node);
                    //}
                    //else
                    //{ 
                    //    node.Tag = options[0];
                    //    treeOptions.Nodes.Add(node);
                    //}
                }
            }
            finally
            {
                treeOptions.EndUpdate();
            }
        }
        protected virtual void OnDirectoryChanged(EventArgs e)
        {
            if (DirectoryChanged != null)
                DirectoryChanged(this, e);
        } 
        private void ProcessSettings(ProcessItemDelegate process)
        {
            saveSettings = true;
            for (int i = 0; i < treeOptions.Nodes.Count; i++)
            {
                if (i == 1) // Language
                { 
                    // Need to save the language selected here!
                    TreeNode node = treeOptions.Nodes[i];
                    IOptions extension = (IOptions)node.Tag;

                    Control[] options = new Control[node.Nodes.Count];
                    for (int j = 0; j < node.Nodes.Count; j++)
                    {                        
                        options[j] = (Control)node.Nodes[j].Tag; 
                    }
                    process(extension, options);

                }
                else
                {
                    TreeNode node = treeOptions.Nodes[i];
                    IOptions extension = (IOptions)node.Tag;

                    Control[] options = new Control[node.Nodes.Count];
                    int curr_ConcurrentThreads = Settings.Default.concurrentMaxJobs;
                    for (int j = 0; j < node.Nodes.Count; j++)
                    {
                        Control control = (Control)node.Nodes[j].Tag;
                        //Console.WriteLine("Tag: " + control.ToString());
                        if (control.ToString().Equals("DriverDownloader.Core.DownloadThreads"))
                        {
                            curr_ConcurrentThreads = ((DownloadThreads)control).ConcurrentThreads;
                        }
                        //options[j] = (Control)node.Nodes[j].Tag;
                        options[j] = control;
                    }

                    int activejobs = 0;
                    if (DownloadManager.Instance != null)
                    {
                        activejobs = DownloadManager.Instance.GetActiveJobsCount();
                    }

                    process(extension, options);
                }
            }
            if(saveSettings)
                treeOptions.Nodes.Clear();
        }

        private void treeOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pnlExtension.Controls.Clear();

            if (e.Node.Text == "Drivers")
            {
                // Do Nothing
            }
            else if (e.Node.Text == "System")
            {
                //ShowOptionFromNode(e.Node);
                //Control c = (Control)e.Node.Tag;
            }
            else
            {
                ShowOptionFromNode(e.Node);
            }
        }

        private void ShowOptionFromNode(TreeNode node)
        {
            Control ctrl = (Control)node.Tag;
            ctrl.Visible = true;
            ctrl.Dock = DockStyle.Fill;
            pnlExtension.Controls.Add(ctrl);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ProcessSettings(
                delegate(IOptions extension, Control[] options)
                {
                    if(saveSettings) 
                        extension.UIExtension.PersistSettings(options);

                    for (int i = 0; i < options.Length; i++)
                    {
                        options[i].Dispose();
                    }
                }
                );
            if (saveSettings)
            {
                OnDirectoryChanged(e);
                DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ProcessSettings(
                delegate(IOptions extension, Control[] options)
                {
                    for (int i = 0; i < options.Length; i++)
                    {
                        options[i].Dispose();
                    }
                }
            );

            DialogResult = DialogResult.Cancel;
        }
    }
}