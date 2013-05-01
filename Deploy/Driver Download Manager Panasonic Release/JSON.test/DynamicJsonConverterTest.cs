using DriverDownloader.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace JSON.test
{
    
    
    /// <summary>
    ///This is a test class for DynamicJsonConverterTest and is intended
    ///to contain all DynamicJsonConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DynamicJsonConverterTest
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
        ///A test for Deserialize
        ///</summary>
        [TestMethod()]
        public void DeserializeTest()
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
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic lang = serializer.Deserialize<object>(jlang);

            Assert.AreEqual("002", lang[0].value);
            Assert.AreEqual("English(UK) / MUI", lang[0].text);
            
            Assert.AreEqual("005", lang[1].value);
            Assert.AreEqual("English(North America)", lang[1].text);            
        }
    }
}
