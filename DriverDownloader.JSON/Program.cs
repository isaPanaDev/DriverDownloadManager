using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Data;

using System.Web;
using System.Net;
using System.Threading;


namespace DriverDownloader.JSON
{
    class Program
    {
        static void PoolFunc(object state)
        {

            int workerThreads, completionPortThreads;

            ThreadPool.GetAvailableThreads(out workerThreads,

                out completionPortThreads);

            Console.WriteLine("WorkerThreads: {0}, CompletionPortThreads: {1}",

                workerThreads, completionPortThreads);

            

            string url = "http://pc-dl.panasonic.co.jp/dl/api/v1/products?dc%5B%5D=002001";

           // DriverListManager target = new DriverListManager();
           // target.initDriverList();
            /*
            
            HttpWebRequest myHttpWebRequest;

            HttpWebResponse myHttpWebResponse = null;

            // Creates an HttpWebRequest for the specified URL. 

            myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            // Sends the HttpWebRequest, and waits for a response. 

            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            myHttpWebResponse.Close();
            */

             
            string txtRes = "";
            bool b = WebUtility.GetWebTextAsync(url, ref txtRes);

            Console.WriteLine("done.");
            if (b)
            {
                Console.WriteLine(txtRes.Substring(0, 30));
            }
            else
            {
                 Console.WriteLine("failed.");
            }
            Thread.Sleep(100000);
        } 


        static void Main(string[] args)
        {
            Console.WriteLine("Testing JSON Response.");
           /* const string json =
           "{" +
           "     \"firstName\": \"John\"," +
           "     \"lastName\" : \"Smith\"," +
           "     \"age\"      : 25," +
           "     \"address\"  :" +
           "     {" +
           "         \"streetAddress\": \"21 2nd Street\"," +
           "         \"city\"         : \"New York\"," +
           "         \"state\"        : \"NY\"," +
           "         \"postalCode\"   : \"11229\"" +
           "     }," +
           "     \"phoneNumber\":" +
           "     [" +
           "         {" +
           "           \"type\"  : \"home\"," +
           "           \"number\": \"212 555-1234\"" +
           "         }," +
           "         {" +
           "           \"type\"  : \"fax\"," +
           "           \"number\": \"646 555-4567\"" +
           "         }" +
           "     ]" +
           " }";
            
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize<object>(json);

            Console.WriteLine(data.firstName); // John
            Console.WriteLine(data.lastName); // Smith
            Console.WriteLine(data.age); // 25
            Console.WriteLine(data.address.postalCode); // 11229

            Console.WriteLine(data.phoneNumber.Count); // 2

            Console.WriteLine(data.phoneNumber[0].type); // home
            Console.WriteLine(data.phoneNumber[1].type); // fax

            foreach (var pn in data.phoneNumber)
            {
                Console.WriteLine(pn.number); // 212 555-1234, 646 555-4567
            }

            Console.WriteLine(data.ToString());

            */

            /*
            WebAPIAccessV6 v6 = new WebAPIAccessV6();
            DataTable dt = v6.getLanguages();
            dt.WriteXml("languages.xml");

            dt = v6.getProducts();
            dt.WriteXml("products.xml");

            dt = v6.getModelNumbers("111"); //CF19
            dt.WriteXml("models.xml");

            dt = v6.getOSs("111", "1110391"); //CF19X
            dt.WriteXml("OS.xml");

            dt = v6.getDriverSearchResults("111", "1110391", "001021", "002"); //CF19X
            dt.WriteXml("driverlists.xml");

            Console.ReadLine();
            */

            DriverListManager target = new DriverListManager();

            for (int i = 0; i < 10000; i++)
            {
                //target.initDriverList();
            }



            // Set number of threads to be created for testing. 
            int testThreads = 300;
            ThreadPool.SetMaxThreads(1000,1000) ;
            string url = "http://pc-dl.panasonic.co.jp/dl/api/v1/products?dc%5B%5D=002001";
            for (int i = 0; i < testThreads; i++)
            {

                ThreadPool.QueueUserWorkItem(new WaitCallback(PoolFunc));
                
            }

            Console.ReadLine(); 


        }
    }
}
