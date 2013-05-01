using DriverDownloader.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace JSON.test
{
    
    
    /// <summary>
    ///This is a test class for DriverListManagerTest and is intended
    ///to contain all DriverListManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DriverListManagerTest
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
        ///A test for initDriverList
        ///</summary>
        [TestMethod()]
        public void initDriverListTest()
        {
            DriverListManager target = new DriverListManager(); 
            target.initDriverList();

            DataTable dt = target.getProducts();
            for (int i = 0; i > dt.Rows.Count; i++)
            {
                Console.WriteLine("Code:" + dt.Rows[i].Field<string>("code"));
            }

        }

        /// <summary>
        ///A test for getDriverSearchResults
        ///</summary>
        [TestMethod()]
        public void getDriverSearchResultsTest()
        {
            DriverListManager target = new DriverListManager(); 
            string ProductCode = "111";
            string ModelCode = "1110016";
            string OSCode = "001016";
            string LanguageCode = "005";
            DriverSearchFilter sf = DriverSearchFilter.ALL;
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.getDriverSearchResults(ProductCode, ModelCode, OSCode, LanguageCode, sf);
            
            // compare actual and expected data
            // TBD
            
        }
    }
}
