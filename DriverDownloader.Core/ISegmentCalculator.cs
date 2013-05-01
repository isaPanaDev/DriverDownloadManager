using System;
using System.Collections.Generic;
using System.Text;

namespace DriverDownloader.Core
{
    public interface ISegmentCalculator
    {
        CalculatedSegment[] GetSegments(int segmentCount, RemoteFileInfo fileSize);
    }
}
