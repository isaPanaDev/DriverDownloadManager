using System;
using System.Collections.Generic;
using System.Text;

namespace DriverDownloader.Core
{
    public class VideoDownloadUIExtension: IUIExtension
    {
        #region IUIExtension Members

        public System.Windows.Forms.Control[] CreateSettingsView()
        {
            return null;
        }

        public void PersistSettings(System.Windows.Forms.Control[] settingsView)
        {
        }

        #endregion


    }
}
