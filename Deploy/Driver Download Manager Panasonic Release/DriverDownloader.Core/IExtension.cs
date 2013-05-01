using System;
using System.Collections.Generic;
using System.Text;

namespace DriverDownloader.Core
{
    public interface IExtension
    {
        string Name { get; }

        IUIExtension UIExtension { get; }
    }
}
