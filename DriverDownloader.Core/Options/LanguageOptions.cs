using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DriverDownloader.Core
{
    public class LanguageOptions : IOptions
    {
        private DataTable _mLanguages;
        private string _mSelectedLanguage;

        #region IExtension Members
        public LanguageOptions(DataTable Languages, string SelectedLanguage)
        {
            _mLanguages = Languages;
            _mSelectedLanguage = SelectedLanguage;
        }

        public string Name
        {
            get { return "System"; }
        }

        public ISubOptions UIExtension
        {
            get { return new LanguageSubOptions(_mLanguages,  _mSelectedLanguage); }
        }

        #endregion
    }
}
