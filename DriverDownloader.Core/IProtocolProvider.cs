using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DriverDownloader.Core
{
    public interface IProtocolProvider
    {
        void Initialize(Downloader downloader);

        Stream CreateStream(ResourceLocation rl, long initialPosition, long endPosition);

        RemoteFileInfo GetFileInfo(ResourceLocation rl, out Stream stream);
        RemoteFileInfo GetFileInfoResume(ResourceLocation rl, long startByte, long endByte, out Stream stream);
    }
}
