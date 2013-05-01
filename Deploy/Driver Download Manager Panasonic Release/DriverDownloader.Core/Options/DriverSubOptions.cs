using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DriverDownloader.Core
{
    public class DriverSubOptions : ISubOptions
    {
        public DriverSubOptions()
        {
        }

        #region ISubOptions Members

        public Control[] CreateSettingsView()
        {
            return new Control[] { new DownloadFolder(), new FilterControl(), new DownloadThreads() };
        }

        public void PersistSettings(System.Windows.Forms.Control[] settingsView)
        {
            // Save settings
            DownloadFolder downloadFolder = (DownloadFolder)settingsView[0];
            FilterControl filter = (FilterControl)settingsView[1];
            DownloadThreads threads = (DownloadThreads)settingsView[2];            

            // Directory Settings
            Settings.Default.DownloadFolder = downloadFolder.Folder;

            // Filter Settings
            Settings.Default.AllChecked = filter.ShowAllChecked;
            Settings.Default.UpdatedOnlyChecked = filter.ShowUpdatedOnly;
            Settings.Default.LatestDriversChecked = filter.ShowLatestOnly;

            // Thread Settings
            Settings.Default.concurrentMaxJobs = threads.ConcurrentThreads;                
            
        }

        #endregion
    }
}
