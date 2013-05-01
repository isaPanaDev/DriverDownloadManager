using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using DriverDownloader.Core;

namespace DriverDownloader.JSON
{
  
    /// <summary>
    /// Get the response text from a specified URL
    /// </summary>
    class WebUtility
    {

        /// <summary>
        /// Get Web Text using Async WebRequests
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="ResponseText"></param>
        /// <returns></returns>
        public static  bool GetWebTextAsync(string URL, ref string ResponseText)
        {            
            HttpWebRequest objRequest = null;
            IAsyncResult ar = null;
            HttpWebResponse objResponse = null;
            StreamReader objs = null;
            try
            {

                objRequest = (HttpWebRequest)WebRequest.Create(URL);
                SetProxy(objRequest);
                ar = objRequest.BeginGetResponse(new AsyncCallback(GetScrapingResponse), objRequest);

                //// Wait for request to complete
                ar.AsyncWaitHandle.WaitOne(1000 * 10, true);
                if (objRequest.HaveResponse == false)
                {
                    return false;                        
                }

                objResponse = (HttpWebResponse)objRequest.EndGetResponse(ar);
                objs = new StreamReader(objResponse.GetResponseStream());
                ResponseText = objs.ReadToEnd();
            }
            catch (Exception exp)
            {
                return false;
            }
            finally
            {
                if (objResponse != null)
                    objResponse.Close();
                objRequest = null;
                ar = null;
                objResponse = null;
                objs = null;
            }

            return true;
        }

       /// <summary>
       /// Dummy method
       /// </summary>
       /// <param name="result"></param>
        protected static void GetScrapingResponse(IAsyncResult result)
        {

        }


        /// <summary>
        /// Create a Http query string for array values
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="list">value array</param>
        /// <returns>A http query string</returns>
        public static string GetHttpQueryForArray(string key, string[] list)
        {
            if (list == null || list.Length ==0)
            {
                return key + "%5B%5D=";
            }

            string strRet = "";
            foreach (string x in list)
            {
                strRet += key + "%5B%5D=" + HttpUtility.HtmlEncode(x)+ "&";
            }           
            strRet = strRet.TrimEnd('&');
           
            return strRet;

        }

        /// <summary>
        /// using WebClient instead of Httprequest to get web text (internal user for testing only)
        /// </summary>
        /// <param name="URL"></param>
        /// <param name="ResponseText"></param>
        /// <returns></returns>
        public static bool GetWebTextWithWebClient(string URL, ref string ResponseText)
        {
            WebClient client = new WebClient();
            // Add a user agent header in case the             
            client.Headers.Add("user-agent", "Panasonic Download Driver Manager");            
            Stream data = client.OpenRead(URL);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();            
            data.Close();
            reader.Close();

            return true;
        }

        /// <summary>
        /// Gets the response text for a given url.
        /// </summary>
        /// <param name="url">The url whose text needs to be fetched.</param>
        /// <returns>The text of the response.</returns>
        public static bool GetWebText(string url, ref string ResponseText)
        {   
            
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
            

                request = (HttpWebRequest)WebRequest.Create(url);                
                //SetProxy(request);
                request.Method = "GET";
                request.KeepAlive = false;
                request.UserAgent = "Panasonic Driver Download Manager";
                request.ServicePoint.UseNagleAlgorithm = false;
                request.ServicePoint.ConnectionLimit = 100; 
                //request.Proxy = null;                
                response = (HttpWebResponse)request.GetResponse();
                //request.Timeout = 500;
                stream = response.GetResponseStream();
                reader = new StreamReader(stream);
                ResponseText = reader.ReadToEnd();             
                
                return true;
            }
            catch (Exception ex)
            {
                // Error handling
                throw ex;                
            }
            finally
            {                
                if (reader != null)
                {
                    reader.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }                
            }            
        }

        private static void SetProxy(WebRequest request)
        {

            WebProxy proxy = new WebProxy("sec-proxy.meca.panasonic.com", 8080);
            proxy.BypassProxyOnLocal = true;
            request.Proxy = proxy;

            
            request.Proxy.Credentials = new NetworkCredential(
                    "zz09092",
                    "Welcome7",
                    "America");
            

            /*
            if (HttpFtpProtocolExtension.parameters.UseProxy)
            {
                WebProxy proxy = new WebProxy(HttpFtpProtocolExtension.parameters.ProxyAddress, HttpFtpProtocolExtension.parameters.ProxyPort);
                proxy.BypassProxyOnLocal = HttpFtpProtocolExtension.parameters.ProxyByPassOnLocal;
                request.Proxy = proxy;

                if (!String.IsNullOrEmpty(HttpFtpProtocolExtension.parameters.ProxyUserName))
                {
                    request.Proxy.Credentials = new NetworkCredential(
                        HttpFtpProtocolExtension.parameters.ProxyUserName,
                        HttpFtpProtocolExtension.parameters.ProxyPassword,
                        HttpFtpProtocolExtension.parameters.ProxyDomain);
                }
            }
             * */
        }

        /// <summary>
        /// Parse a JSON string and construct a dynamic object
        /// </summary>
        /// <param name="Json">A JOSON format string</param>
        /// <returns></returns>
        public static dynamic GetDynamicObject(string Json)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            return serializer.Deserialize<object>(Json);
        }
       
    }

    /// <summary>
    /// Using WEB API to get Toughbook Models, Model Versions, Supported OS and drivers list
    /// </summary>

    public enum URLCategory { PRODUCTS, MODELS, CATEGORY, OS, LANGUAGE, PRODUCT_LANGUAGE, SEARCH, DRIVERDETAILS };    
    
      
# region "WEB API 6th Edition"
    /// <summary>
    /// Impletmention of WEB API Specification 6th Editon 
    /// </summary>
    public class WebAPIAccessV6
    {
       
        private const string mCategoryCode = "002001"; // Category code for "Driver"
        private const int mnPerPage = 100; // Number of items listed per page, must <=100

        /// <summary>
        /// Get the list of all products (Toughbook CF-19, Toughbook CF-31, etc.)
        /// </summary>
        /// <returns></returns>
        public DataTable getProducts()
        {           
            string strJson = "";            
            if (WebUtility.GetWebText(getURL(URLCategory.PRODUCTS, null), ref strJson))
            {
                dynamic jLang = WebUtility.GetDynamicObject(strJson);
                return getTableWithCodeAndName(jLang, URLCategory.PRODUCTS.ToString());
            }

            return getTableWithCodeAndName(null, URLCategory.PRODUCTS.ToString());            
        }

        /// <summary>
        /// Get the list of models (CF-19C/D/E (mk1), etc.)
        /// </summary>
        /// <returns></returns>
        public DataTable getModelNumbers(string ProductCode)
        {           
            string strJson = "";
            Dictionary<string, string[]> map = new Dictionary<string, string[]>();
            map.Add("p1", new string[] {ProductCode});

            if (WebUtility.GetWebText(getURL(URLCategory.MODELS, map), ref strJson))
            {                
                dynamic jLang = WebUtility.GetDynamicObject(strJson);
                return getTableWithCodeAndName(jLang, URLCategory.MODELS.ToString());
            }

            return getTableWithCodeAndName(null, URLCategory.MODELS.ToString());
        }

        /// <summary>
        /// Get the list of Operation System
        /// </summary>
        /// <returns></returns>
        public DataTable getOSs(string ProductCode, string ModelCode)
        {
            string strJson = "";
            Dictionary<string, string[]> map = new Dictionary<string, string[]>();
            map.Add("p1", new string[] { ProductCode });
            map.Add("p2", new string[] { ModelCode });
            if (WebUtility.GetWebText(getURL(URLCategory.OS, map), ref strJson))
            {
                dynamic jLang = WebUtility.GetDynamicObject(strJson);
                return getTableWithCodeAndName(jLang, URLCategory.OS.ToString());
            }

            return getTableWithCodeAndName(null, URLCategory.OS.ToString());
        }

        /// <summary>
        /// Get the list of supported languages
        /// </summary>
        /// <returns></returns>
        public DataTable getLanguages()
        {         
            string strJson = "";            
            if (WebUtility.GetWebText(getURL(URLCategory.LANGUAGE, null), ref strJson))
            {
                dynamic jLang = WebUtility.GetDynamicObject(strJson);
                return getTableWithCodeAndName(jLang, URLCategory.LANGUAGE.ToString());
            }

            return getTableWithCodeAndName(null, URLCategory.LANGUAGE.ToString());
        }

        /// <summary>
        /// Get the list of supported languages
        /// </summary>
        /// <returns></returns>
        public DataTable getLanguages(string ProductCode, string ModelNumber, string OSCode)
        {
            string strJson = "";            
            Dictionary<string, string[]> map = new Dictionary<string, string[]>();
            map.Add("p1", new string[] { ProductCode });
            map.Add("p2", new string[] { ModelNumber });
            map.Add("oc", new string[] { OSCode });
            if (WebUtility.GetWebText(getURL(URLCategory.PRODUCT_LANGUAGE, map), ref strJson))
            {
                dynamic jLang = WebUtility.GetDynamicObject(strJson);
                return getTableWithCodeAndName(jLang, URLCategory.PRODUCT_LANGUAGE.ToString());
            }

            return getTableWithCodeAndName(null, URLCategory.PRODUCT_LANGUAGE.ToString());
        }

        /// <summary>
        /// Get the list of supported category
        /// For drivers, the category is fixed as "all drivers" and category code is 002001
        /// This function is not used by driver downloader manager
        /// </summary>
        /// <returns></returns>
        public string getCategories()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get driver list for specified product, model, os and language.
        /// </summary>
        /// <returns></returns>
        public DataTable getDriverSearchResults(string ProductCode, string ModelCode, string OSCode, string LanguageCode)
        {
            // Initialize table
            DataTable dt = new DataTable(URLCategory.SEARCH.ToString());
            dt.Columns.Add("DriverName", typeof(string));            
            dt.Columns.Add("Category", typeof(string));            
            dt.Columns.Add("Date Released", typeof(string));
            
            dt.Columns.Add("Version", typeof(string));
            dt.Columns.Add("Size", typeof(string));                        
            dt.Columns.Add("FilesList", typeof(DataTable));

            dt.Columns.Add("InstallationInstructions", typeof(Boolean));
            dt.Columns.Add("IsUpdatedDriver", typeof(Boolean));
         
            try
            {
                // Get Web Response
                int nPage = 1; // Init Page number= 1          
                bool lastPage = false;
                string strJson = "";                                           
                Dictionary<string, string[]> map = new Dictionary<string, string[]>();
                map.Add("p1", new string[] { ProductCode });
                map.Add("p2", new string[] { ModelCode });
                map.Add("oc", new string[] { OSCode });
                map.Add("lang", new string[] { LanguageCode });
                map.Add("page", new string[] { nPage.ToString() });
                while (!lastPage)
                {
                    if (!WebUtility.GetWebText(getURL(URLCategory.SEARCH, map), ref strJson))
                    {
                        return dt;
                    }

                    // Parse Json response
                    dynamic jsonDrivers = WebUtility.GetDynamicObject(strJson);

                    // get the driver list
                    foreach (var x in jsonDrivers.search_results)
                    {
                        DataRow row = dt.NewRow();
                        if (x.installation_instructions_flag)
                        {
                            // installation instructions
                            Boolean blUpdated = false;
                            row.SetField("DriverName", getDriverName("Installation Instructions", ref blUpdated));
                            row.SetField("Category", HttpUtility.HtmlDecode((string)x.categories[0].name));
                            row.SetField("Date Released", (string)x.doc_updated_on);
                            row.SetField("IsUpdatedDriver", blUpdated);

                            // size and version are not availabe
                            row.SetField("Version", "");
                            row.SetField("Size", "");

                            // Set File List
                            DataTable files = new DataTable("FILES");
                            files.Columns.Add("file_id", typeof(string));
                            files.Columns.Add("file_name", typeof(string));
                            files.Columns.Add("name", typeof(string));
                            files.Columns.Add("path", typeof(string));
                            files.Columns.Add("size", typeof(string));

                            string strLink = (string)x.installation_instructions_url;
                            // get file name
                            string strFileName = strLink.Substring(strLink.LastIndexOf("/") + 1);

                            files.Rows.Add("", strFileName, "Installation Instructions", strLink, "unknown");
                            row.SetField("FilesList", files);

                            row.SetField("InstallationInstructions", true);
                        }
                        else
                        {
                            // drivers
                            Boolean blUpdated = false;
                            row.SetField("DriverName", getDriverName((string)x.title, ref blUpdated));
                            row.SetField("Category", HttpUtility.HtmlDecode((string)x.categories[0].name));
                            row.SetField("Date Released", (string)x.doc_updated_on);
                            row.SetField("IsUpdatedDriver", blUpdated);
                            
                            // Set File List
                            DataTable files = new DataTable("FILES");
                            files.Columns.Add("file_id", typeof(string));
                            files.Columns.Add("file_name", typeof(string));
                            files.Columns.Add("name", typeof(string));
                            files.Columns.Add("path", typeof(string));
                            files.Columns.Add("size", typeof(string));

                            // Get detailed driver information      
                            string strVersion = "";
                            int nFileSize = 0; 
                            string strJsonFiles = "";
                            if (WebUtility.GetWebText(x.detail_url, ref strJsonFiles))
                            {
                                dynamic jsonFiles = WebUtility.GetDynamicObject(strJsonFiles);
                                foreach (var y in jsonFiles.files)
                                {
                                    files.Rows.Add(y.file_id, y.file_name, y.name, y.path, y.size);
                                    nFileSize += y.size;
                                }


                                // search version 
                                /* sample JSON format for version
                                "doc_details": [
                                    {
                                        "body_text": "Stability of \"Wireless WAN Communication\" and \r\n \"Resume from sleep or hibernation\" will improve.\r\n", 
                                        "title": "Reason for Update"
                                    }, 
                                    {
                                        "body_text": "V1.00L10M00", 
                                        "title": "Version"
                                    }, 
                                    {
                                        "body_text": "No", 
                                        "title": "Microsoft Signed"
                                    }
                                ], 
                                */

                                foreach (var y in jsonFiles.doc_details)
                                {
                                    if (string.Equals( (string)y.title, "VERSION", StringComparison.OrdinalIgnoreCase ))
                                    {
                                        strVersion = y.body_text;
                                    }
                                }

                            }

                            // size and version 
                            row.SetField("Version", strVersion);
                            row.SetField("Size", nFileSize.ToString());
                            row.SetField("FilesList", files);

                            // Set Installation Instructions flag
                            row.SetField("InstallationInstructions", false);
                        }

                        dt.Rows.Add(row);
                    }

                    // if current page is the last page
                    int nHitCount = jsonDrivers.hit_count;
                    if ((nHitCount - nPage * jsonDrivers.per_page) > 0)
                    {
                        nPage += 1;
                    }
                    else
                    {
                        lastPage = true;
                        nPage = 1;
                    }

                }
            }

            catch (Exception ex)
            {
                //TODO: handle errors
                dt.Clear();
                throw ex;
            }

            return dt;
        }


        private DataTable getTableWithCodeAndName(dynamic DynamicJsonObject, string TableName)
        {
            DataTable dt = new DataTable(TableName);
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));

            // Null reference
            if (DynamicJsonObject == null)
            {
                return dt;
            }

            // Emumerate the dynamic Json object
            foreach (var x in DynamicJsonObject)
            {
                dt.Rows.Add(x.value, x.text);
            }

            return dt;
        }


        private string getURL(URLCategory URLType, Dictionary<string, string[]> Map)
        {
            const string strBase = "http://pc-dl.panasonic.co.jp/dl/api/v1/";
            string strExtension = "";
            switch (URLType)
            {
                case URLCategory.CATEGORY:
                    strExtension = "categories";
                    break;

                case URLCategory.PRODUCTS:
                    strExtension = string.Format("products?dc%5B%5D={0}", mCategoryCode);
                    break;
                case URLCategory.LANGUAGE:
                    strExtension = string.Format("langs?dc%5B%5D={0}&oc=", mCategoryCode);
                    break;
                case URLCategory.MODELS:         
                    // product code and only one product code is allowed
                    if (!Map.ContainsKey("p1") && Map["p1"].Length != 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");

                    }
                    strExtension = string.Format("models?dc%5B%5D={0}&p={1}", mCategoryCode, Map["p1"][0]);
                    break;

                case URLCategory.OS:
                    // product code and only one product code is allowed
                    if (Map.ContainsKey("p1") && Map["p1"].Length != 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");

                    }
                    // Model number
                    if (Map.ContainsKey("p2") && Map["p2"].Length != 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");

                    }
                    strExtension = string.Format("oss?dc%5B%5D={0}&p1={1}&p2={2}", mCategoryCode, Map.ContainsKey("p1")? Map["p1"][0]:"", Map.ContainsKey("p2")? Map["p2"][0]:"");
                    break;
                case URLCategory.SEARCH:
                    // at least a key word or a category shall be specified.
                    /*
                    if ( ! (Map.ContainsKey("q") ||  Map.ContainsKey("dc") ) )
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }*/
                    if (Map.ContainsKey("q") && Map["q"].Length != 1) 
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }
                    if (Map.ContainsKey("dc") && Map["dc"].Length < 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }
                    string keyWord = Map.ContainsKey("q")? Map["q"][0]:"";
                    string p1= Map.ContainsKey("p1")? Map["p1"][0]:"";
                    string p2 = Map.ContainsKey("p2")? Map["p2"][0]:"";
                    string oc = Map.ContainsKey("oc") ? Map["oc"][0] : "";
                    string lang = Map.ContainsKey("lang") ? Map["lang"][0] : "";
                    string page = Map.ContainsKey("page") ? Map["page"][0] : "1";
                    // TBD, add more query parameters    
                
                    // if category is spefied, use specified category instead of mCategoryCode
                    if (Map.ContainsKey("dc") )
                    {
                        // Category
                        string dc = WebUtility.GetHttpQueryForArray("dc", Map["dc"]);
                        strExtension = string.Format("search?q={0}&{1}&per_page={2}&p1={3}&p2={4}&oc={5}&lang={6}&page={7}", keyWord, dc, mnPerPage, p1, p2, oc, lang,  page);
                    }
                    else
                    {
                        strExtension = string.Format("search?q={0}&dc%5B%5D={1}&per_page={2}&p1={3}&p2={4}&oc={5}&lang={6}&page={7}", keyWord, mCategoryCode, mnPerPage, p1, p2, oc, lang,  page);
                    }
                    break;

                case URLCategory.DRIVERDETAILS:
                    if (!Map.ContainsKey("docNo") && Map["docNo"].Length != 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }
                    strExtension = string.Format("docs/{0}", Map["docNo"][0]);
                    break;

                case URLCategory.PRODUCT_LANGUAGE:
                    if (! Map.ContainsKey("oc"))
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }
                    p1= Map.ContainsKey("p1")? Map["p1"][0]:"";
                    p2 = Map.ContainsKey("p2")? Map["p2"][0]:"";
                    // OS can be array
                    oc = WebUtility.GetHttpQueryForArray("oc", Map.ContainsKey("oc")? Map["oc"]:null);
                    strExtension = string.Format("langs?dc%5B%5D={0}&p1={1}&p2={2}&{3}", mCategoryCode, p1, p2, oc);
                    break;
                default:
                    throw new Exception("[ERROR] URL category is not supported in getURL call.");
            }

            return strBase + strExtension;
        }

        
        private string getURL_deprecated(URLCategory URLType, params string[] list)
        {
            const string strBase = "http://pc-dl.panasonic.co.jp/dl/api/v1/";
            string strExtension = "";
            switch (URLType)
            {
                case URLCategory.CATEGORY:
                    strExtension = "categories";
                    break;

                case URLCategory.PRODUCTS:
                    strExtension = string.Format("products?dc%5B%5D={0}", mCategoryCode);
                    break;
                case URLCategory.LANGUAGE:
                    strExtension = string.Format("langs?dc%5B%5D={0}&oc=", mCategoryCode);
                    break;
                case URLCategory.MODELS:
                    if (list.Length < 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");

                    }
                    strExtension = string.Format("models?dc%5B%5D={0}&p={1}", mCategoryCode, list[0]);
                    break;

                case URLCategory.OS:
                    if (list.Length < 2)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");

                    }
                    strExtension = string.Format("oss?dc%5B%5D={0}&p1={1}&p2={2}", mCategoryCode, list[0], list[1]);
                    break;
                case URLCategory.SEARCH:
                    if (list.Length < 5)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");

                    }
                    strExtension = string.Format("search?dc%5B%5D={0}&per_page={1}&p1={2}&p2={3}&oc={4}&lang={5}&page={6}", mCategoryCode, mnPerPage, list[0], list[1], list[2], list[3], list[4]);
                    break;

                case URLCategory.DRIVERDETAILS:
                    if (list.Length < 1)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }
                    strExtension = string.Format("docs/{0}", list[0]);

                    break;

                case URLCategory.PRODUCT_LANGUAGE:
                    if (list.Length < 3)
                    {
                        throw new Exception("[ERROR] number of parameters are not expected in getURL call.");
                    }
                    strExtension = string.Format("langs?dc%5B%5D={0}&p1={1}&p2={2}&oc={3}", mCategoryCode, list[0], list[1], list[2]);

                    break;
                default:
                    throw new Exception("[ERROR] URL category is not supported in getURL call.");                    
            }

            return strBase + strExtension;
        }
        
        /// <summary>
        /// Get Driver name from the tilte of JSON response. 
        /// Sometimes in the title encoded HTML text such as "<font color=red><B>Update</B></font>" is included.
        /// This method will trim any text after "<".
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        private string getDriverName(string Title, ref Boolean UpdatedDriverFlag)
        {
            // decode HTML text into plain text
            string strPlain = HttpUtility.HtmlDecode(Title);

            // check updated flag
            // <font color=red><B>Latest Update</B></font>
            // <font color=red><B>Update</B></font>
            string strPattern = @"<\s*font[\s\S]*update[\s\S]*font\s*>";
            UpdatedDriverFlag = Regex.IsMatch(strPlain, strPattern, RegexOptions.IgnoreCase);                

            int nPos = strPlain.IndexOf('<');
            if ( nPos>= 0 )
            {
                return strPlain.Substring(0, nPos).Trim();
            }                       

            return strPlain.Trim();
        }

    }
# endregion "WEB API 6th Edition"
        
 
}
