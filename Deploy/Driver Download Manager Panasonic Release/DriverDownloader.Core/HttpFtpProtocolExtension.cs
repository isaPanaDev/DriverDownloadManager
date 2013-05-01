using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DriverDownloader.Core
{
    public interface IHttpFtpProtocolParameters
    {
        string ProxyAddress { get; set; }

        string ProxyUserName { get; set; }

        string ProxyPassword { get; set; }

        string ProxyDomain { get; set; }

        bool UseProxy { get; set; }

        bool ProxyByPassOnLocal { get; set; }

        int ProxyPort { get; set; }
    }

    class HttpFtpProtocolParametersSettingsProxy : IHttpFtpProtocolParameters
    {
        private string proxyAddress;
        private string proxyUserName;
        private string proxyPassword;
        private string proxyDomain;
        private bool useProxy;
        private bool proxyByPassOnLocal;
        private int proxyPort;

        #region IHttpFtpProtocolParameters Members

        public string ProxyAddress
        {
            get
            {
                return proxyAddress;
            }
            set
            {
                proxyAddress = value;
            }
        }

        public string ProxyUserName
        {
            get
            {
                return proxyUserName;
            }
            set
            {
                proxyUserName = value;
            }
        }

        public string ProxyPassword
        {
            get
            {
                return proxyPassword;
            }
            set
            {
                proxyPassword = value;
            }
        }

        public string ProxyDomain
        {
            get
            {
               return proxyDomain;
            }
            set
            {
                proxyDomain = value;
            }
        }

        public bool UseProxy
        {
            get
            {
                return useProxy;
            }
            set
            {
                useProxy = value;
            }
        }

        public bool ProxyByPassOnLocal
        {
            get
            {
                return proxyByPassOnLocal;
            }
            set
            {
                proxyByPassOnLocal = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return proxyPort;
            }
            set
            {
                proxyPort = value;
            }
        }

        #endregion
    }
   public class HttpFtpProtocolExtension: IExtension
    {
        internal static IHttpFtpProtocolParameters parameters;

        #region IExtension Members

        public string Name
        {
            get { return "HTTP/FTP"; }
        }
        public HttpFtpProtocolExtension() :
            this(new HttpFtpProtocolParametersSettingsProxy())
        {
        }
        public HttpFtpProtocolExtension(IHttpFtpProtocolParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (HttpFtpProtocolExtension.parameters != null)
            {
                throw new InvalidOperationException("The type HttpFtpProtocolExtension is already initialized.");
            }

            HttpFtpProtocolExtension.parameters = parameters;

            ProtocolProviderFactory.RegisterProtocolHandler("http", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("https", typeof(HttpProtocolProvider));
         //   ProtocolProviderFactory.RegisterProtocolHandler("ftp", typeof(FtpProtocolProvider));
        }

        #endregion


        public IUIExtension UIExtension
        {
            get { throw new NotImplementedException(); }
        }
    }
}
