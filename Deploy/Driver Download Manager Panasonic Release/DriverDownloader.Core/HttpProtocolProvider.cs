using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DriverDownloader.Core
{
    public class HttpProtocolProvider : BaseProtocolProvider, IProtocolProvider
    {
        static HttpProtocolProvider()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(certificateCallBack);
        }

        static bool certificateCallBack(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void FillCredentials(HttpWebRequest request, ResourceLocation rl)
        {

            if (rl.Authenticate)
            {
                string login = rl.Login;
                string domain = string.Empty;

                int slashIndex = login.IndexOf('\\');

                if (slashIndex >= 0)
                {
                    domain = login.Substring(0, slashIndex);
                    login = login.Substring(slashIndex + 1);
                }

                NetworkCredential myCred = new NetworkCredential(login, rl.Password);
                myCred.Domain = domain;

                request.Credentials = myCred;
            }
        }

        #region IProtocolProvider Members

        public virtual void Initialize(Downloader downloader)
        {
        }

        public virtual Stream CreateStream(ResourceLocation rl, long initialPosition, long endPosition)
        {
            HttpWebRequest request = (HttpWebRequest)GetRequest(rl);
            //request.UserAgent = "Panasonic Driver Download Manager";

            FillCredentials(request, rl);

            if (initialPosition != 0)
            {
                if (endPosition == 0)
                {
                    request.AddRange((int)initialPosition);
                }
                else
                {
                    request.AddRange((int)initialPosition, (int)endPosition);
                }
            }

            WebResponse response = request.GetResponse();

            return response.GetResponseStream();
        }

        public virtual RemoteFileInfo GetFileInfo(ResourceLocation rl, out Stream stream)
        {
            HttpWebRequest request = (HttpWebRequest)GetRequest(rl);
            //request.UserAgent = "Panasonic Driver Download Manager";

            //@modified
            Console.WriteLine("----------------------HTTP PROTOCOL PROVIDER----------------------");
            Console.WriteLine("Request Header");
            Console.WriteLine(request.Headers);

            FillCredentials(request, rl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //@modified
            //Console.WriteLine("Response Header");
            //Console.WriteLine(response.Headers);

            RemoteFileInfo result = new RemoteFileInfo();
            result.MimeType = response.ContentType;
            result.LastModified = response.LastModified;
            result.FileSize = response.ContentLength;

            //@modified
            //Console.WriteLine("ORGINAL STATE: " + (String.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0));
            //Console.WriteLine("Accept-Ranges: "+response.GetResponseHeader("Accept-Ranges"));
            //Console.WriteLine("Status: "+response.GetResponseHeader("Status"));

            //Setting RemoteFileInfo explicitly
            string value = "";
            value = response.GetResponseHeader("Accept-Ranges");
            //Console.WriteLine("Server Accept-Ranges? " + value);
            //Console.WriteLine("Condition1: " + (value == "bytes") + " Condition2: " + (value != ""));
            if (value=="bytes" && value!="")
                result.AcceptRanges = true;
            else
                result.AcceptRanges = false;

            //result.AcceptRanges = String.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0;
            //Console.WriteLine("result accepts ranges: " + result.AcceptRanges);
            //Console.WriteLine("----------------------HTTP PROTOCOL PROVIDER----------------------");

            stream = response.GetResponseStream();

            return result;
        }
        public virtual RemoteFileInfo GetFileInfoResume(ResourceLocation rl, long startByte, long endByte, out Stream stream)
        {
            HttpWebRequest request = (HttpWebRequest)GetRequest(rl);
            //request.UserAgent = "Panasonic Driver Download Manager";
            
            //request.Method = "HEAD";
            request.AddRange(startByte, endByte - 1);
            FillCredentials(request, rl);

            //@modified
            Console.WriteLine("RESUMING Request Header");
            Console.WriteLine(request.Headers);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            RemoteFileInfo result = new RemoteFileInfo();
            result.MimeType = response.ContentType;
            result.LastModified = response.LastModified;
            result.FileSize = response.ContentLength;

            //@modified
            Console.WriteLine("ORGINAL STATE: " + (String.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0));
            Console.WriteLine("Accept-Ranges: " + response.GetResponseHeader("Accept-Ranges"));
            Console.WriteLine("Status: " + response.GetResponseHeader("Status"));

            result.AcceptRanges = String.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0;

            //@modified
            Console.WriteLine("RESUMING Response Header");
            Console.WriteLine(response.Headers);

            stream = response.GetResponseStream();

            return result;
        }
        #endregion
    }
}
