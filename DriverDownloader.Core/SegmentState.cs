using System;
using System.Collections.Generic;
using System.Text;

namespace DriverDownloader.Core
{
    public enum SegmentState
    {
        Idle,
        Connecting,
        Downloading,
        Paused,
        Finished,
        Error,
    }
}
