using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace DriverDownloader.Core
{
    public static class ProtocolProviderFactory
    {
        private static Hashtable protocolHandlers = new Hashtable();

        public static event EventHandler<ResolvingProtocolProviderEventArgs> ResolvingProtocolProvider;

        public static void RegisterProtocolHandler(string prefix, Type protocolProvider)
        {
            protocolHandlers[prefix] = protocolProvider;
        }

        public static IProtocolProvider CreateProvider(string uri, Downloader downloader)
        {
            IProtocolProvider provider = InternalGetProvider(uri);

            if (downloader != null)
            {
                provider.Initialize(downloader);
            }

            return provider;
        }

        public static IProtocolProvider GetProvider(string uri)
        {
            return InternalGetProvider(uri);
        }

        public static Type GetProviderType(string uri)
        {
            int index = uri.IndexOf("://");

            if (index > 0)
            {
                string prefix = uri.Substring(0, index);
                Type type = protocolHandlers[prefix] as Type;
                return type;
            }
            else
            {
                return null;
            }
        }

        public static IProtocolProvider CreateProvider(Type providerType, Downloader downloader)
        {
            IProtocolProvider provider = CreateFromType(providerType);

            if (ResolvingProtocolProvider != null)
            {
                ResolvingProtocolProviderEventArgs e = new ResolvingProtocolProviderEventArgs(provider, null);
                ResolvingProtocolProvider(null, e);
                provider = e.ProtocolProvider;
            }

            if (downloader != null)
            {
                provider.Initialize(downloader);
            }

            return provider;
        }

        private static IProtocolProvider InternalGetProvider(string uri)
        {
            Type type = GetProviderType(uri);

            IProtocolProvider provider = CreateFromType(type);

            if (ResolvingProtocolProvider != null)
            {
                ResolvingProtocolProviderEventArgs e = new ResolvingProtocolProviderEventArgs(provider, uri);
                ResolvingProtocolProvider(null, e);
                provider = e.ProtocolProvider;
            }

            return provider;
        }

        private static IProtocolProvider CreateFromType(Type type)
        {
            IProtocolProvider provider = (IProtocolProvider)Activator.CreateInstance(type);
            return provider;
        }
    }

    public class ProtocolProviderProxy : IProtocolProvider
    {
        private IProtocolProvider proxy;
        private SpeedLimitExtension speedLimit;

        #region IProtocolProvider Members

        public void Initialize(Downloader downloader)
        {
            proxy.Initialize(downloader);
        }

        public System.IO.Stream CreateStream(ResourceLocation rl, long initialPosition, long endPosition)
        {
            return new LimitedRateStreamProxy(proxy.CreateStream(rl, initialPosition, endPosition), speedLimit);
        }

        public RemoteFileInfo GetFileInfo(ResourceLocation rl, out System.IO.Stream stream)
        {
            RemoteFileInfo result = proxy.GetFileInfo(rl, out stream);

            if (stream != null)
            {
                stream = new LimitedRateStreamProxy(stream, speedLimit);
            }

            return result;
        }
        public virtual RemoteFileInfo GetFileInfoResume(ResourceLocation rl, long startByte, long endByte, out Stream stream)
        {
            RemoteFileInfo result = proxy.GetFileInfoResume(rl, startByte, endByte, out stream);
            if (stream != null)
            {
                stream = new LimitedRateStreamProxy(stream, speedLimit);
            }
            return result;
        }
        #endregion

        public ProtocolProviderProxy(IProtocolProvider proxy, SpeedLimitExtension speedLimit)
        {
            this.proxy = proxy;
            this.speedLimit = speedLimit;
        }
    }

    public class LimitedRateStreamProxy : Stream
    {
        private Stream proxy;
        private SpeedLimitExtension speedLimit;

        #region Stream

        public override bool CanRead
        {
            get { return proxy.CanRead; }
        }

        public override bool CanSeek
        {
            get { return proxy.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return proxy.CanWrite; }
        }

        public override void Flush()
        {
            proxy.Flush();
        }

        public override long Length
        {
            get { return proxy.Length; }
        }

        public override long Position
        {
            get
            {
                return proxy.Position;
            }
            set
            {
                proxy.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            speedLimit.WaitFor();

            return proxy.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return proxy.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            proxy.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            proxy.Write(buffer, offset, count);
        }

        #endregion

        public LimitedRateStreamProxy(Stream proxy, SpeedLimitExtension speedLimit)
        {
            this.speedLimit = speedLimit;
            this.proxy = proxy;
        }
    }
}
