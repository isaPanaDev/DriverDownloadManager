using DriverDownloader.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace JSON.test
{
    
    
    /// <summary>
    ///This is a test class for WebAPIAccessV6Test and is intended
    ///to contain all WebAPIAccessV6Test Unit Tests
    ///</summary>
    [TestClass()]
    public class WebAPIAccessV6Test
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
        ///A test for getProducts
        ///</summary>
        [TestMethod()]
        public void getProductsTest()
        {
            WebAPIAccessV6 target = new WebAPIAccessV6(); 
            DataTable expected = new DataTable(URLCategory.PRODUCTS.ToString()); 

            DataTable actual;
            actual = target.getProducts();
            // TODO compare
        }

        /// <summary>
        ///A test for getOSs
        ///</summary>
        [TestMethod()]
        public void getOSsTest()
        {
            WebAPIAccessV6 target = new WebAPIAccessV6(); // TODO: Initialize to an appropriate value
            string ProductCode = string.Empty; // TODO: Initialize to an appropriate value
            string ModelCode = string.Empty; // TODO: Initialize to an appropriate value
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.getOSs(ProductCode, ModelCode);

            // TODO compare

        }

        /// <summary>
        ///A test for getModelNumbers
        ///</summary>
        [TestMethod()]
        public void getModelNumbersTest()
        {
            WebAPIAccessV6 target = new WebAPIAccessV6(); // TODO: Initialize to an appropriate value
            string ProductCode = string.Empty; // TODO: Initialize to an appropriate value
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.getModelNumbers(ProductCode);

            // TODO compare

        }

        /// <summary>
        ///A test for getLanguages
        ///</summary>
        [TestMethod()]
        public void getLanguagesTest()
        {
            WebAPIAccessV6 target = new WebAPIAccessV6(); // TODO: Initialize to an appropriate value
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.getLanguages();

            // TODO compare
        }

        /// <summary>
        ///A test for getDriverSearchResults
        ///</summary>
        [TestMethod()]
        public void getDriverSearchResultsTest()
        {
            WebAPIAccessV6 target = new WebAPIAccessV6();
            string ProductCode = "111";
            string ModelCode = "1110016";
            string OSCode = "001016";
            string LanguageCode = "005";
            DataTable expected = null; // TODO: Initialize to an appropriate value
            DataTable actual;
            actual = target.getDriverSearchResults(ProductCode, ModelCode, OSCode, LanguageCode);

            // TODO compare
        }

       
    }
}
