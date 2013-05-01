using DriverDownloader.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JSON.test
{
    
    
    /// <summary>
    ///This is a test class for WebUtilityTest and is intended
    ///to contain all WebUtilityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WebUtilityTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetWebText
        ///</summary>
        [TestMethod()]
        public void GetWebTextTest()
        {
            string url = "http://pc-dl.panasonic.co.jp/dl/api/v1/langs?dc%5B%5D=002001&oc=";
            string ResponseText = "";
            string ResponseTextExpected = @"[{""value"":""002"",""text"":""English(UK) / MUI""},{""value"":""005"",""text"":""English(North America)""},{""value"":""007"",""text"":""German""},{""value"":""008"",""text"":""Italian""},{""value"":""009"",""text"":""French""},{""value"":""010"",""text"":""Spanish""},{""value"":""011"",""text"":""Multi(Hong Kong)""},{""value"":""012"",""text"":""Chinese  simplified""},{""value"":""062"",""text"":""Traditional Chinese""},{""value"":""013"",""text"":""Multi(China)""},{""value"":""014"",""text"":""Turkish""},{""value"":""015"",""text"":""Korean""},{""value"":""016"",""text"":""Japanese""},{""value"":""061"",""text"":""Russian / MUI""}]";
            bool expected = true; 
            bool actual;
            actual = WebUtility.GetWebText(url, ref ResponseText);
            Assert.AreEqual(ResponseTextExpected, ResponseText);
            Assert.AreEqual(expected, actual);            
        }


        /// <summary>
        ///A test for GetDynamicObject
        ///</summary>
        [TestMethod()]
        public void GetDynamicObjectTest()
        {

            const string jlang =
                "[" +
                "{" +
                "\"value\": \"002\"," +
                "\"text\": \"English(UK) / MUI\"" +
                "}, " +
                "{  " +
                "\"value\": \"005\",  " +
                "\"text\": \"English(North America)\" " +
                "} " +
                "]   ";
            dynamic actual = WebUtility.GetDynamicObject(jlang);

            Assert.AreEqual("002", actual[0].value);
            Assert.AreEqual("English(UK) / MUI", actual[0].text);

            Assert.AreEqual("005", actual[1].value);
            Assert.AreEqual("English(North America)", actual[1].text);
        }

        /// <summary>
        ///A test for GetWebTextWithWebClient
        ///</summary>
        [TestMethod()]
        public void GetWebTextWithWebClientTest()
        {
            string URL = "http://msdn.microsoft.com/en-us/library/system.net.webclient(v=vs.80).aspx";
            string ResponseText = string.Empty; 
            string ResponseTextExpected = string.Empty;
            bool expected = true;
            bool actual;
            actual = WebUtility.GetWebTextWithWebClient(URL, ref ResponseText);
            Console.Write(ResponseText);
        }

        /// <summary>
        ///A test for GetHttpQueryForArray
        ///</summary>
        [TestMethod()]
        public void GetHttpQueryForArrayTest()
        {
            string key = "dc";
            string[] list = null;
            string expected = "dc%5B%5D=";
            string actual;
            actual = WebUtility.GetHttpQueryForArray(key, list);
            Assert.AreEqual(expected, actual);
            
            key = "dc";
            list = new string[] {};
            expected = "dc%5B%5D=";            
            actual = WebUtility.GetHttpQueryForArray(key, list);
            Assert.AreEqual(expected, actual);


            key = "dc";
            list = new string[] {"02001"}; 
            expected = "dc%5B%5D=02001";            
            actual = WebUtility.GetHttpQueryForArray(key, list);
            Assert.AreEqual(expected, actual);

            key = "dc";
            list = new string[] { "02001", "02002" };
            expected = "dc%5B%5D=02001&dc%5B%5D=02002";
            actual = WebUtility.GetHttpQueryForArray(key, list);
            Assert.AreEqual(expected, actual);
        }
    }
}
