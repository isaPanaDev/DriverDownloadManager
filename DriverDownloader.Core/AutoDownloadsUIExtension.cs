using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public class AutoDownloadsUIExtension: IUIExtension
    {
        #region IUIExtension Members

        public Control[] CreateSettingsView()
        {
            return new Control[] { new Jobs() };
        }

        public void PersistSettings(Control[] settingsView)
        {
            Jobs jobs = (Jobs)settingsView[0];

            Settings.Default.concurrentMaxJobs = jobs.MaxJobs;
            Settings.Default.WorkOnlyOnSpecifiedTimes = jobs.WorkOnlyOnSpecifiedTimes;
            Settings.Default.MaxRateOnTime = jobs.MaxRate;
            Settings.Default.Save();
        }

        #endregion
    }
}
