using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DriverDownloader.Core
{
    public class LanguageSubOptions : ISubOptions
    {
        private DataTable _mLanguages;
        private string _mSelectedLanguage;
       
        #region ISubOptions Members
        public LanguageSubOptions(DataTable Languages, string SelectedLanguage)
        {
            _mLanguages = Languages;
            _mSelectedLanguage = SelectedLanguage;
        }
       
        public Control[] CreateSettingsView()
        {
            return new Control[] { new Languages(_mLanguages,  _mSelectedLanguage) };

        }

        public void PersistSettings(System.Windows.Forms.Control[] settingsView)
        {
            Languages lang = (Languages)settingsView[0];

            Settings.Default.DefaultLanguage = lang.DefaultLanuage;
        }

        #endregion
    }
}
