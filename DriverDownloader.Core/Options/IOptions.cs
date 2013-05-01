using System;
using System.Collections.Generic;
using System.Text;

namespace DriverDownloader.Core
{
    public interface IOptions
    {
        string Name { get; }

        ISubOptions UIExtension { get; }
    }
}
