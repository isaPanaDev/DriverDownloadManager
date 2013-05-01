using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DriverDownloader.Core
{
    public interface IUIExtension
    {
        Control[] CreateSettingsView();

        void PersistSettings(Control[] settingsView);
    }
}
