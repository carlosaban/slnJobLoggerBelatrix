using JobLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestLogger
{
    
    
    /// <summary>
    ///Only include the test for the 2 principal methods. The test case is based in a bad configuration in the appconfig file(Level). 
    ///</summary>
    [TestClass()]
    public class JobLoggerTest
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
        ///A test for LogMessage
        ///</summary>
        [TestMethod()]
        public void LogMessageLevelExceptionTest()
        {
            JobLogger_Accessor target = new JobLogger_Accessor(); // TODO: Initialize to an appropriate value
            target.InitLogger();
            string message = "test message"; // TODO: Initialize to an appropriate value
            bool bmessage = false; // TODO: Initialize to an appropriate value
            bool warning = true; // TODO: Initialize to an appropriate value
            bool error = false; // TODO: Initialize to an appropriate value

            try
            {
                target.LogMessage(message, bmessage, warning, error);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {

                Assert.IsTrue(ex is LoggerExceptionLevel);

            }
           
        }

        /// <summary>
        ///A test for LogMessage
        ///</summary>
        [TestMethod()]
        public void LogMessageLevelExceptionMethod2Test()
        {
            JobLogger_Accessor target = new JobLogger_Accessor(); // TODO: Initialize to an appropriate value
            string message = "test message"; // TODO: Initialize to an appropriate value
            LoggerLevelEnum level = LoggerLevelEnum.WARN; // TODO: Initialize to an appropriate value
            try
            {
                target.LogMessage(message, level);
                Assert.Fail("no exception thrown");

            }
            catch (Exception ex)
            {

                Assert.IsTrue(ex is LoggerExceptionLevel);
            }
            
            
           // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
