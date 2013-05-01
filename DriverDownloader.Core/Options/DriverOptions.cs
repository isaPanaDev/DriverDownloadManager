using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DriverDownloader.Core
{
    public class DriverOptions : IOptions
    {
        #region IExtension Members
        public DriverOptions()
        {
        }

        public string Name
        {
            get { return "Drivers"; }
        }

        public ISubOptions UIExtension
        {
            get { return new DriverSubOptions(); }
        }

        #endregion
    }
}
