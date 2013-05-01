using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace DriverDownloader.JSON
{
    /// <summary>
    /// Driver Seach Enum
    /// UPDATEDONLY - only search updated drivers
    /// LATESTONLY - only search latest drivers
    /// ALL - all the drivers
    /// </summary>
    public enum DriverSearchFilter { UPDATEDONLY, LATESTONLY, ALL };
    
    /// <summary>
    /// Manage list of drivers. Currently the list is created by directly WEB API and will cache the drive list. 
    /// 
    /// TBD: Serialize/Deserialize of driver list to physical files to accelarate loading speed.
    /// </summary>
    public class DriverListManager
    {
        private WebAPIAccessV6 _mObjWebAPI = new WebAPIAccessV6();
        private DataTable _mProductsTable = null; 
        private Dictionary<string, DataTable> _mModelsMap= null;
        private Dictionary<string, DataTable> _mOSsMap = null;
        private Dictionary<string, DataTable> _mLanguagesMap = null;
        private DataTable _mLanguages = null;

        // If it's true, it will cach the items at the first time of request
        // If it's false, it will try to cach all at once
        private bool mCachOnTheFly = true;

        private void initProducts()
        {
            _mProductsTable = _mObjWebAPI.getProducts();
        }

        private void initModels()
        {
            _mModelsMap = new Dictionary<string,DataTable>();

            if (!mCachOnTheFly)
            {
                for (int i = 0; i < _mProductsTable.Rows.Count; i++)
                {
                    DataRow r = _mProductsTable.Rows[i];
                    string strCode = r.Field<string>("code");
                    _mModelsMap.Add(strCode, _mObjWebAPI.getModelNumbers(strCode));
                }
            }
        }
        private void initOSs()
        {
            _mOSsMap = new Dictionary<string, DataTable>();
            if (!mCachOnTheFly)
            {
                for (int i = 0; i < _mProductsTable.Rows.Count; i++)
                {
                    DataRow r = _mProductsTable.Rows[i];
                    string strCode = r.Field<string>("code");

                    DataTable dtModels = getModelNumbers(strCode);
                    for (int j = 0; j < dtModels.Rows.Count; j++)
                    {
                        string strModelCode = dtModels.Rows[j].Field<string>("code");
                        _mOSsMap.Add(strCode + strModelCode, _mObjWebAPI.getOSs(strCode, strModelCode));
                    }
                }
            }
        }

        private void initLanguages()
        {
            _mLanguagesMap = new Dictionary<string, DataTable>();
            if (!mCachOnTheFly)
            {
                for (int i = 0; i < _mProductsTable.Rows.Count; i++)
                {
                    DataRow r = _mProductsTable.Rows[i];
                    string strCode = r.Field<string>("code");

                    DataTable dtModels = getModelNumbers(strCode);
                    for (int j = 0; j < dtModels.Rows.Count; j++)
                    {
                        string strModelCode = dtModels.Rows[j].Field<string>("code");

                        DataTable dtOSs = getOSs(strCode, strModelCode);
                        for (int k = 0; k < dtOSs.Rows.Count; k++)
                        {
                            string strOS = dtOSs.Rows[k].Field<string>("code");
                            _mLanguagesMap.Add(strCode + strModelCode + strOS, _mObjWebAPI.getLanguages(strCode, strModelCode, strOS));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Initialize the list of products, models, OSs and languages
        /// </summary>
        public void initDriverList()
        {
            // initialization orders matter
            initProducts();
            //initModels();
            //initOSs();
            //initLanguages();
        }

        /// <summary>
        /// Get the list of all products (Toughbook CF-19, Toughbook CF-31, etc.)
        /// </summary>
        /// <returns></returns>
        public DataTable getProducts()
        {
            if (_mProductsTable == null)
            {
                initProducts();
            }
            
            return _mProductsTable;
        }

         /// <summary>
        /// Get the list of models (CF-19C/D/E (mk1), etc.)
        /// </summary>
        /// <returns></returns>
        public DataTable getModelNumbers(string ProductCode)
        {
            if (_mModelsMap == null)
            {
                initModels();
            }

            // Query if it's not cached
            if (!_mModelsMap.ContainsKey(ProductCode))            
            {
                _mModelsMap.Add(ProductCode, _mObjWebAPI.getModelNumbers(ProductCode));
            }

            return _mModelsMap[ProductCode];
        }


        /// <summary>
        /// Get the list of Operation System
        /// </summary>
        /// <returns></returns>
        public DataTable getOSs(string ProductCode, string ModelCode)
        {
            if (_mOSsMap == null)
            {
                initOSs();
            }

            // Query if it's not cached
            String strKey = ProductCode + ModelCode;
            if (!_mOSsMap.ContainsKey(strKey))
            {
                _mOSsMap.Add(strKey, _mObjWebAPI.getOSs(ProductCode, ModelCode));
            }

            return _mOSsMap[strKey];
        }

        /// <summary>
        /// Get the list of supported languages
        /// </summary>
        /// <returns></returns>
        public DataTable getLanguages()
        {
            if (_mLanguages == null || _mLanguages.Rows.Count == 0)
            {
                _mLanguages = _mObjWebAPI.getLanguages();
            }

            return _mLanguages;   
        }
        
        /// <summary>
        /// Get the list of supported languages
        /// </summary>
        /// <returns></returns>
        public DataTable getLanguages(string ProductCode, string ModelNumber, string OSCode)
        {
            if (_mLanguagesMap == null)
            {
                initLanguages();
            }

            // Query if it's not cached
            String strKey = ProductCode + ModelNumber + OSCode;
            if (!_mOSsMap.ContainsKey(strKey))
            {
                _mLanguagesMap.Add(strKey, _mObjWebAPI.getLanguages(ProductCode, ModelNumber, OSCode));
            }

            return _mLanguagesMap[strKey];
        }


        /// <summary>
        /// Get driver list for specified product, model, os and language.
        /// </summary>
        /// <returns></returns>
        public DataTable getDriverSearchResults(string ProductCode, string ModelCode, string OSCode, string LanguageCode, DriverSearchFilter SearchFilter = DriverSearchFilter.ALL)
        {
            DataTable dt = _mObjWebAPI.getDriverSearchResults(ProductCode, ModelCode, OSCode, LanguageCode);
            if (DriverSearchFilter.ALL == SearchFilter)
            {
                return dt;
            }

            if (DriverSearchFilter.LATESTONLY == SearchFilter)
            {
                // remove old drivers by using driver name and release date
                dt.DefaultView.Sort = "drivername, Date Released DESC";
                DataTable dtLatest = dt.DefaultView.ToTable();
                for (int i = 1; i < dtLatest.Rows.Count; i++)
                {
                    // remove the row if driver name is the same with previous row
                    if (string.Compare(dtLatest.Rows[i].Field<string>("drivername"), dtLatest.Rows[i - 1].Field<string>("drivername").ToUpper(), true) == 0)
                    {
                        dtLatest.Rows.RemoveAt(i);
                        i--;
                    }
                }

                return dtLatest;
            }


            // Updated Drivers Only            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // remove the driver if it doesn't have "updated" flag
                if (!dt.Rows[i].Field<Boolean>("IsUpdatedDriver"))
                {
                    dt.Rows.RemoveAt(i);
                    i--;
                }
                
            }

            return dt;

        }

        /// <summary>
        /// /// Get driver list for specified product, model, os and language.
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <param name="ModelCode"></param>
        /// <param name="OSCode"></param>
        /// <param name="LanguageCode"></param>
        /// <param name="LatestOnly"></param>
        /// <returns></returns>
        public List<Driver> getDriverSearchResultsList(string ProductCode, string ProductName, string ModelCode, string ModelName, string OSCode, string OSName, string LanguageCode, string LanguageName, DriverSearchFilter SearchFilter = DriverSearchFilter.ALL)
        {
            DataTable dt = getDriverSearchResults(ProductCode, ModelCode, OSCode, LanguageCode, SearchFilter);
            List<Driver> liRet = new List<Driver>();

            for (int i=0; i<dt.Rows.Count; i++)
            {
                Driver d = new Driver();
                d.Name = dt.Rows[i].Field<string>("DriverName");
                d.Category = dt.Rows[i].Field<string>("Category");                
                d.Version = dt.Rows[i].Field<string>("Version");
                d.Size = dt.Rows[i].Field<string>("Size");
                d.Date = dt.Rows[i].Field<string>("Date Released");
                // Driver files
                DataTable file = dt.Rows[i].Field<DataTable>("FilesList");

                // Only download EXE files, sometimes the driver FAQ only contains an Installation Instruction PDF file.
                // Example: https://pc-dl.panasonic.co.jp/dl/docs/060140?dc%5B%5D=002001&lang=005&no=2&oc=001020&p1=111&p2=1110375&per_page=100&score=1.0&sri=3543222&trn_org=2
                Boolean blFoundDownloadableFile = false;
                for (int j = 0; j < file.Rows.Count; j++)
                {
                    // the first EXE found
                    string strFN = file.Rows[j].Field<string>("file_name");
                    if (strFN.EndsWith("exe", StringComparison.CurrentCultureIgnoreCase) || dt.Rows[i].Field<Boolean>("InstallationInstructions")) 
                    {
                        d.Link =  file.Rows[j].Field<string>("path");
                        d.ExeName = d.Link;
                        blFoundDownloadableFile = true;
                        break;
                    }                    
                }

                if (!blFoundDownloadableFile)
                {
                    continue;
                }

                // Full name is the full product name (Mark Name)
                d.FullName = ModelName;
                d.OperatingSystem = OSName;
                
                // Create directory/folder name where models are being stored
                string fullRegex = @"(?<model>CF-[0-9]*)(?<version>[\s*-/\w*/]*)(?<mark>[\\(\\)a-zA-Z0-9]*)";
                MatchCollection fullMatch = Regex.Matches(ModelName, fullRegex);
                string name = fullMatch[0].Groups["model"].Value.Replace("-","");
                string version = fullMatch[0].Groups["version"].Value.Replace("/","");
                string mark = fullMatch[0].Groups["mark"].Value.Replace("(mk","M").Replace(")","");

                // Shorten the Windows name
                OSName = OSName.Replace("Windows ", "Win");
                d.Directory = name + version + mark + " " + OSName; // Adding OS to the directory name

                liRet.Add(d);
            }

            return liRet;
        }
       
    }
}
