using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using DriverDownloader.Core;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;
using DriverDownloader.JSON;
using System.Resources;
using System.Net;
using System.Net.Mail;

namespace DriverDownloader
{
    public partial class MainForm : Form, ISingleInstanceEnforcer
    {
        private List<IOptions> extensions;
        SpeedLimitExtension speedLimit;
        private DataSet currentDataset;
        private DataTable currentTable = new DataTable("Table");
        private List<Model> modelList = new List<Model>();
        // private int currentModelIndex = 0;
        private Model currentModel;
        private JSON.DriverListManager mDriverListManager = new JSON.DriverListManager(); // Manager class to get driver list using WEB API
        private List<JSON.Driver> mDriverList;  // The result dirver list 
        private List<Driver> selectedDriverList;  // The selected driver list to send to queue list

        public List<IOptions> Extensions
        {
            get
            {
                return extensions;
            }
        }

        public MainForm()
        {
            InitializeComponent();        

            currentModel = new Model();
            // ParseModels();
            loadUIModels();
            loadMarks();
            loadOSModels();
            loadLanguages();
            
            SetupOptions();
            
            ToolTipWithImage tooltip = new ToolTipWithImage();
            tooltip.ImageFile = SampleImages.ModelIcon;
            tooltip.SetToolTip(this.tooltipTxt);

            selectedDriverList = new List<Driver>();
            this.driverList.Visible = false;
            this.driverList.CheckmarkChanged += new ChangedEventHandler(softwareList_CheckmarkChanged);

            this.dirLinkLabel.Text = getDownloadFolder();            
                       
            speedLimit = (SpeedLimitExtension)App.Instance.GetExtensionByType(typeof(SpeedLimitExtension));

            // Event to catch the download updates
            this.downloadList.ExtractionCompleted+=new DownloadList.ChangedEventHandler(downloadList_ExtractionCompleted); 
            
            downloadList.UpdateUI();           
       }

        void downloadList_ExtractionCompleted(int completedIndex)
        {
            if (completedIndex < 0) return;
            //MessageBox.Show(completedIndex.ToString() + " - " + selectedDriverList[completedIndex].Name);

            // If the list is 0, we have reached here by restarting the application and no driverlist exists yet!
            if (selectedDriverList.Count == 0) return;

            // Look up the driver that was completed from the selectedDriverList
            selectedDriverList[completedIndex].Downloaded = true;
            selectedDriverList[completedIndex].Checkbox = false;

            // Important to refresh the colors when download and extraction of files is completed!
            this.driverList.Refresh();
        }
     
        void softwareList_CheckmarkChanged(object sender, EventArgs e)
        {
            this.selectAllCheckbox.Checked = this.driverList.selectAll;
        }
         public void ShowHideForm()
         {
             if (this.Visible)
             {
                 HideForm();
             }
             else
             {
                 ShowForm();
                 LoadViewSettings();
             }
         }
         public void ShowForm()
         {
             this.ShowInTaskbar = true;
             this.Visible = true;
             this.WindowState = FormWindowState.Normal;
         }
         private void LoadViewSettings()
         {
             // Reload the settings you like to see
             downloadList.LoadSettingsView();            
         }
         public void HideForm()
         {
             this.ShowInTaskbar = false;
             this.Visible = false;
         }
        private void SetupOptions()
        {
            DataTable lang = null;
            try
            {
                lang = mDriverListManager.getLanguages();
            }
            catch (Exception ex)
            {                
            }            
            
            extensions = new List<IOptions>();
            extensions.Add(new DriverOptions());
            extensions.Add(new LanguageOptions(lang, languageToolStripMenuItem.ComboBox.SelectedText));
        }
        private void LoadMarkTable(Model model)
        {
            if (model.Name == null) return;
            DataTable markTable = new DataTable("CF-" + model.Name);
            markTable.Columns.Add("Mark", Type.GetType("System.String"));

            for (int m = 0; m < model.AllMarks.Count; m++)
            {
                DataRow dr = markTable.NewRow();
                dr["Mark"] = model.AllMarks[m];
                markTable.Rows.Add(dr);
            }
            currentDataset.Tables.Add(markTable);
        }
        private string ConvertDataTableToXML(DataTable dataTable)
        {
            DataSet dsBuildSQL = new DataSet();
            StringBuilder sbSQL;
            StringWriter swSQL;
            string XMLformat;
            try
            {
                sbSQL = new StringBuilder();
                swSQL = new StringWriter(sbSQL);
                dsBuildSQL.Merge(dataTable, true, MissingSchemaAction.AddWithKey);
                dsBuildSQL.Tables[0].TableName = "SampleDataTable";
                foreach (DataColumn col in dsBuildSQL.Tables[0].Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dsBuildSQL.WriteXml(swSQL, XmlWriteMode.WriteSchema);
                XMLformat = sbSQL.ToString();
                return XMLformat;
            }
            catch (Exception sysException)
            {
                throw sysException;
            }
        }
        private void loadLanguages()
        {
            try
            {
                DataTable selectedTable = mDriverListManager.getLanguages();
                languageToolStripMenuItem.ComboBox.DataSource = selectedTable;
                languageToolStripMenuItem.ComboBox.DisplayMember = "Name";
                languageToolStripMenuItem.ComboBox.ValueMember = "Code";

                if (Settings.Default.DefaultLanguage.Length > 0)
                {
                    //int i = languageToolStripMenuItem.ComboBox.FindStringExact(Settings.Default.DefaultLanguage);
                    //languageToolStripMenuItem.ComboBox.SelectedIndex = i;
                    languageToolStripMenuItem.ComboBox.SelectedValue = Settings.Default.DefaultLanguage;
                }
                else 
                { // First time when no default language 
                    if (languageToolStripMenuItem.ComboBox.Items.Count > 0)
                    {
                        string defaultLang = OSLang2DriverLang.guessDriverLanguage();
                        int i = languageToolStripMenuItem.ComboBox.FindStringExact(defaultLang);
                        languageToolStripMenuItem.ComboBox.SelectedIndex = i;
                        Settings.Default.DefaultLanguage = languageToolStripMenuItem.ComboBox.SelectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
            }

        }
        private void loadUIModels()
        {
            try
            {
                DataTable selectedTable = mDriverListManager.getProducts();
                modelCombox.DataSource = selectedTable;
                modelCombox.DisplayMember = "Name";
                modelCombox.ValueMember = "Code";

                if (modelCombox.Items.Count > 0)
                {
                    modelCombox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // error handling
            }
        }
        private void loadMarks()
        {
            markCombox.DataSource = null;
            markCombox.Items.Clear();
            osCombox.DataSource = null;
            osCombox.Items.Clear();

            if (modelCombox.SelectedValue != null)
            {
                try
                {
                    DataTable selectedTable = mDriverListManager.getModelNumbers(modelCombox.SelectedValue.ToString());
                    markCombox.DataSource = selectedTable;
                    markCombox.DisplayMember = "Name";
                    markCombox.ValueMember = "Code";
                }
                catch (Exception ex)
                {
                    // error handling
                }
            }
        }
        private void loadOSModels()
        {
            /*
            Model selected = modelList[currentModelIndex];
            Mark selectedMarkObj = selected.AllMarksObj[currentMarkIndex];

            // Get the corresponding OS table
            DataTable osTable = selectedMarkObj.OSTable;
            osCombox.DataSource = osTable;
            osCombox.DisplayMember = "OS";
            */

            osCombox.DataSource = null;
            osCombox.Items.Clear();
            // Get the corresponding OS table
            if (modelCombox.SelectedValue != null && markCombox.SelectedValue != null)
            {
                try
                {
                    DataTable osTable = mDriverListManager.getOSs(modelCombox.SelectedValue.ToString(), markCombox.SelectedValue.ToString());
                    osCombox.DataSource = osTable;
                    osCombox.DisplayMember = "Name";
                    osCombox.ValueMember = "Code";
                }
                catch (Exception ex)
                {
                    // error handling
                }
            }
        }
        private void modelCombox_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            currentModelIndex = modelCombox.SelectedIndex;
            loadUIModels();
            */
         
        }
        private void markCombox_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            currentMarkIndex = markCombox.SelectedIndex;
            if (currentMarkIndex >= 0)
                loadOSModels();
            */
         
        }
        private void modelCombox_SelectionChangeCommitted(object sender, EventArgs e)
        {           
            loadMarks();
            if (markCombox.Items.Count > 0)
            {
                markCombox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(this, Resources.common.MainForm_Messages_EmptyProductList);
                return;
            }
            loadOSModels();
        }
        private void markCombox_SelectionChangeCommitted(object sender, EventArgs e)
        {          
            loadOSModels();
            if (osCombox.Items.Count > 0)
            {
                osCombox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(this, Resources.common.MainForm_Messages_EmptyProductList);
                return;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void viewDriversBtn_Click(object sender, EventArgs e)
        {
            // Input Check.
            // Product, Model, OS must be selected. 
            if (modelCombox.SelectedValue == null || markCombox.SelectedValue == null || osCombox.SelectedValue == null)
            {
                MessageBox.Show(this, Resources.common.MainForm_Messages_ModelMarkOSNotAllSpecified, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            waitControl.Visible = true;
            // disable Button
            viewDriversBtn.Enabled = false;
            // change display label
           // displayLabel.Text = modelBox.Text;


            string strProductCode = modelCombox.SelectedValue.ToString();
            string strModelCode = markCombox.SelectedValue.ToString();
            string strOSCode = osCombox.SelectedValue.ToString();
            //string strLangCode = "005";  // North America (English)
            string strLangCode = languageToolStripMenuItem.ComboBox.SelectedValue.ToString();

            string strProductName = modelCombox.Text;
            string strModelName = markCombox.Text;
            string strOSName = osCombox.Text;
            //string strLangName = "English(North America)";  // North America (English)
            string strLangName = languageToolStripMenuItem.ComboBox.SelectedValue.ToString();

            Dictionary<string, string> productInfo = new Dictionary<string, string>();
            productInfo.Add("ProductCode", strProductCode);
            productInfo.Add("ModelCode", strModelCode);
            productInfo.Add("OSCode", strOSCode);
            productInfo.Add("LangCode", strLangCode);

            productInfo.Add("ProductName", strProductName);
            productInfo.Add("ModelName", strModelName);
            productInfo.Add("OSName", strOSName);
            productInfo.Add("LangName", strLangName);

            // Run the search
            driverwaitWorker.RunWorkerAsync(productInfo);
        }

        private void driverwaitWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary<string, string> info =  e.Argument as Dictionary<string, string>;

            string strProductCode = info["ProductCode"];
            string strModelCode = info["ModelCode"];
            string strOSCode = info["OSCode"];
            string strLangCode = info["LangCode"];

            string strProductName = info["ProductName"];
            string strModelName = info["ModelName"];
            string strOSName = info["OSName"];
            string strLangName = info["LangName"];

            DriverSearchFilter dsf = DriverSearchFilter.ALL;
            if (Settings.Default.LatestDriversChecked)
            {
                dsf = DriverSearchFilter.LATESTONLY;
            }
            else if (Settings.Default.UpdatedOnlyChecked)
            {
                dsf = DriverSearchFilter.UPDATEDONLY;
            }
            else
            {
                dsf = DriverSearchFilter.ALL;
            }                    
            mDriverList = mDriverListManager.getDriverSearchResultsList(strProductCode, strProductName, strModelCode, strModelName, strOSCode, strOSName, strLangCode, strLangName, dsf);                    
        }

        private void driverwaitWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {         
            viewDriversBtn.Enabled = true;
            waitControl.Visible = false;

            if (e.Error != null)
            {
                mDriverList = new List<Driver>();
                MessageBox.Show(this, Resources.common.MainForm_Messages_FailedToSearchDrivers);                
            }

            UpdateDriverList();
        }

        private void UpdateDriverList()
        {
            this.driverList.CleanDriverList();
            /* commented out by George to fix the checkbox issue
             * the sort will be done in the driverList.CreateDriverList method instead. 
            if (driverList.sortOrder == "")
            {
                Console.WriteLine("OLD DRIVERS -> " + driverList.sortOrder.ToUpper());
                foreach (Driver d in mDriverList)
                    Console.WriteLine(d.Checkbox.ToString() + " - " + d.Name.ToString());

                // we changed the driverlist and need to rearrange the order of the list
               // mDriverList.Sort(Driver.SortNameDescending); // Make sure the drivers are in name sorted column 
             
               // this.driverList.InitializeOrder();
              //  this.driverList.Refresh();
            }
            Console.WriteLine("SORTED DRIVERS -> " + driverList.sortOrder.ToUpper());
            foreach (Driver d in mDriverList)
                Console.WriteLine(d.Checkbox.ToString() + " - " + d.Name.ToString());
            */
            CheckPreviousDownloads(mDriverList);
            bool driversExists = this.driverList.CreateDriverList(mDriverList);

            UpdateUIText(driversExists);
        }

        private void UpdateUIText(bool driversExists)
        {
            int driverCount = mDriverList.Count;
            countLabel.Text = driverCount.ToString() + " Found";
            if (driverCount == 0)
                this.driverList.Visible = false;
            else
                this.driverList.Visible = true;
            this.dirLinkLabel.Text = getDownloadFolder();
            this.countLabel.Visible = true;
            this.downloadBtn.Visible = true;
            this.selectAllCheckbox.Visible = true;

            // If driver already exists, then show the corner message
            if (driversExists)
            {
                this.displayLabel.Visible = true;
                this.selectAllCheckbox.Checked = false; // make sure also the checkAll is cleared
            }
            else
            {
                this.displayLabel.Visible = false;
                this.selectAllCheckbox.Checked = true;
            }
        }

        private void CheckPreviousDownloads(List<Driver> drivers)
        {
            // sometimes the search result may be zero drivers
            if (drivers.Count > 0)
            {
                // Using model's subfolder Directory now to check for file existense
                String fullDirectory = Path.Combine(Settings.Default.DownloadFolder, drivers[0].Directory);
                //MessageBox.Show(fullDirectory);
                // Check each driver to see if it has been downloaded
                foreach (Driver driver in drivers)
                {
                    // Pull out the .exe file name
                    string exeName = driver.ExeName;
                    string fileName = exeName.Substring(exeName.LastIndexOf("/") + 1);
                    // Combine to get the complete path
                    string completePath = Path.Combine(fullDirectory, fileName);
                    string extractDir = FileManager.trimString(completePath, @".exe");
                    driver.Downloaded = (File.Exists(completePath) || Directory.Exists(extractDir));
                    driver.Checkbox = !driver.Downloaded;
                    Console.WriteLine("-------- " + driver.Checkbox + " is downloaded: " + driver.Downloaded);
                }
            }
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            //downloadList.ClearList();
            if(!downloadList.Visible)
                downloadList.Visible = true;

            selectedDriverList = this.driverList.SelectedDrivers();
            
            // check if any drivers are selected
            if (selectedDriverList.Count == 0)
            {
                MessageBox.Show(this, Resources.common.MainForm_Messages_NoDriverSelected);
                return;
            }

            AddDriversToJobList(selectedDriverList);
        }
        // Gets the drivers that are selected and adds them to the download list!
        private void AddDriversToJobList(List<Driver> selectedDrivers)
        {
            try
            { 
                for (int i = 0; i < selectedDrivers.Count; i++)
                {
                    Driver d = selectedDrivers[i];

                    ResourceLocation rl = new ResourceLocation();
                    rl.URL = d.ExeName;
                    rl.BindProtocolProviderType();
                    if (rl.ProtocolProviderType == null)
                    {
                        MessageBox.Show("Invalid URL format, please check the location field.");
                        DialogResult = DialogResult.None;
                        return;
                    }                    

                    String fullDirectory = Path.Combine(Settings.Default.DownloadFolder, d.Directory);
                    String downloadPath = Path.Combine(fullDirectory, Path.GetFileName(d.ExeName));

                    //MessageBox.Show("Adding to jobs: " + d.Name);
                    
                    // Note: Pass "false" at this point so the concurrent download can be applied later.  
                    if(DownloadManager.Instance.isDownloaderUnique(rl))
                        DownloadManager.Instance.Add(i, rl, downloadPath, false);
                }
            }
            catch (Exception)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Unknow error, please check your input data.", "adsfasfd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OptionsForm options = new OptionsForm(this))
            {
                // Event handlers to monitor when "directory" changes
                options.DirectoryChanged += new ChangedEventHandler(options_DirectoryChanged);
                options.ShowDialog();
            }

            // update Language dropdown list
            loadLanguages();

        }
        // Update the label bottom of the page to affect Options menu changes
        void options_DirectoryChanged(object sender, EventArgs e)
        {
            this.dirLinkLabel.Text = getDownloadFolder();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (About about = new About())
            {
                about.ShowDialog();
            }
        }

        private void selectAllCheckbox_Click(object sender, EventArgs e)
        {
            this.driverList.CheckAll(selectAllCheckbox.Checked);
        }

        private void dirLinkLabel_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", this.dirLinkLabel.Text);
        }

        /// <summary>
        /// Get download folder setting
        /// </summary>
        /// <returns>
        /// return download folder if download folder is not empty
        /// return executable's folder if download folder is empty
        /// </returns>
        private string getDownloadFolder()
        {
            string strDwnPath = Settings.Default.DownloadFolder;

            if (strDwnPath.Length == 0)
            {
                //strDwnPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                // change default directory to current user's desktop
                strDwnPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Settings.Default.DownloadFolder = strDwnPath;
            }

            return strDwnPath;
        }

        // Main Form closing and Cleaning up
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            downloadList.PauseAll();
            this.CleanupFilesInProcess();
        }

        // Delete the files that are left or paused in the download list from the Windows Explorer
        private void CleanupFilesInProcess()
        {
            List<String> leftoverFiles = new List<string>();
            for (int i = 0; i < DownloadManager.Instance.Downloads.Count; i++)
            {
                // Get the name of the file that is still to be processed
                Downloader d = DownloadManager.Instance.Downloads[i];
                leftoverFiles.Add(d.LocalFile);
            }

            for (int f = 0; f < leftoverFiles.Count; f++)
            {
                string fullName = leftoverFiles[f];
                try
                {
                    FileManager.DeleteFile(fullName);
                    
                    string directoryPath = FileManager.trimString(fullName, @".exe");
                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath, true);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    //MessageBox.Show(e.Message);
                    //return;
                }
            }
        }

        public void OnMessageReceived(MessageEventArgs e)
        {
            string[] args = (string[])e.Message;

            if (args.Length == 2 && args[0] == "/sw")
            {
                //this.BeginInvoke((MethodInvoker)delegate { downloadList.NewDownloadFromData(args[1]); });
            }
            else
            {
            }
        }

        public void OnNewInstanceCreated(EventArgs e)
        {
            this.Focus();
        }

        // Refreshes the main queue list every few seconds to update the download status!
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            downloadList.UpdateList();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (modelCombox.Items.Count == 0 || markCombox.Items.Count == 0 || osCombox.Items.Count == 0)
            {
                MessageBox.Show(this, Resources.common.MainForm_Messages_EmptyProductList);
            }
        }

        private void linkLabelJira_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Provide a link to Jira!
           // Process.Start("https://panaconsulting.atlassian.net/secure/CreateIssue.jspa?pid=10000&issuetype=1");
          
            Process.Start("mailto:softwaresolutions@us.panasonic.com");

           /* string email = "pscnaSoftware@gmail.com";
            // Gmail Address from where you send the mail
            var fromAddress = "test@gmail.com";
            // any address where the email will be sending
            var toAddress = email;
            //Password of your gmail address
            const string fromPassword = "pscn@Software1";
            // Passing the values and make a email formate to display
            string subject = email;
            string body = "From: " + email + "\n";
            body += "Email: " + email + "\n";
            body += "Subject: " + "my subject" + "\n";
            body += "Question: \n" + "my comments" + "\n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
            */
           
        }
    }
}
