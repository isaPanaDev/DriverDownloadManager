using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace DriverDownloader
{
    /// <summary>
    /// The class will detect OS language and try to find a match in the driver languages.    
    /// </summary>

    public static class OSLang2DriverLang
    {      
        /// <summary>
        /// Predict a driver language that matches the OS language
        /// The driver languages supported as of 2012/07/02 is as below:    
        /*{
            "text": "English(UK) / MUI", 
            "value": "002"
        }, 
        {
            "text": "English(North America)", 
            "value": "005"
        }, 
        {
            "text": "German", 
            "value": "007"
        }, 
        {
            "text": "Italian", 
            "value": "008"
        }, 
        {
            "text": "French", 
            "value": "009"
        }, 
        {
            "text": "Spanish", 
            "value": "010"
        }, 
        {
            "text": "Multi(Hong Kong)", 
            "value": "011"
        }, 
        {
            "text": "Chinese  simplified", 
            "value": "012"
        }, 
        {
            "text": "Traditional Chinese", 
            "value": "062"
        }, 
        {
            "text": "Multi(China)", 
            "value": "013"
        }, 
        {
            "text": "Turkish", 
            "value": "014"
        }, 
        {
            "text": "Korean", 
            "value": "015"
        }, 
        {
            "text": "Japanese", 
            "value": "016"
        }, 
        {
            "text": "Russian / MUI", 
            "value": "061"
        }
        */
        /// </summary>
        /// <returns></returns> 
        public static string guessDriverLanguage()
        {
            string OSLang = getOSLanguage();
            if (OSLang == "en-GB")
                return "English(UK) / MUI";

            if (OSLang == "en-US")
                return "English(North America)";
            // use US English as defautl for other English speaking countries
            if (OSLang.StartsWith("en") )
                return "English(North America)";

            if (OSLang.StartsWith("de"))
                return "German";

            if (OSLang.StartsWith("it"))
                return "Italian";

            if (OSLang.StartsWith("fr"))
                return "French";

            if (OSLang.StartsWith("es"))
                return "Spanish";

            if (OSLang == "zh-HK")
                return "Multi(Hong Kong)";

            if (OSLang == "zh-CN" || OSLang == "zh-CHS")
                return "Chinese  simplified";

            if (OSLang == "zh-CHT" || OSLang == "zh-MO"  || OSLang == "zh-SG"  || OSLang == "zh-TW" )
                return "Traditional Chinese";
                       
            // Multi(China) ?
            // TBD: not known which OS language shall be mapped to Multi(China)

            if (OSLang.StartsWith("tr"))
                return "Turkish";

            if (OSLang == "ko" || OSLang == "ko-KR")
                return "Korean";

            if (OSLang.StartsWith("ja"))
                return "Japanese";

            if (OSLang.StartsWith("ru"))
                return "Russian / MUI";

            // US English by default
            return "English(North America)";
        }

        private static string getOSLanguage()
        {
            CultureInfo ci = CultureInfo.InstalledUICulture ;
            return ci.Name;           
            
        }

    }
}
