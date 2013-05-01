using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DriverDownloader.Core
{
    public interface IExtensionParameters
    {
        event PropertyChangedEventHandler ParameterChanged;
    }
}
