using JobLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestLoggerConsoleProject
{
    
    
    /// <summary>
    ///This is a test class for JobLoggerTest and is intended
    ///to contain all JobLoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JobLoggerConsoleTest
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
        public void LogMessageLevels()
        {
            JobLogger_Accessor target = new JobLogger_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            LoggerLevelEnum level = new LoggerLevelEnum(); // TODO: Initialize to an appropriate value
            target.LogMessage(message, level);

           bool bWarn =  target.LogMessage("prueba error level warn In Console", JobLogger.LoggerLevelEnum.WARN);
           bool bError = target.LogMessage("prueba error level error In Console", JobLogger.LoggerLevelEnum.ERROR);
           bool bMessage = target.LogMessage("prueba error level message In Console", JobLogger.LoggerLevelEnum.MESSAGE);
           Assert.IsTrue(bWarn && bError && bMessage);
        }

        /// <summary>
        ///A test for LogMessage
        ///</summary>
        [TestMethod()]
        public void LogMessageEmpty()
        {
            JobLogger_Accessor target = new JobLogger_Accessor(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            bool bmessage = false; // TODO: Initialize to an appropriate value
            bool warning = false; // TODO: Initialize to an appropriate value
            bool error = false; // TODO: Initialize to an appropriate value
            
            Assert.IsFalse( target.LogMessage(message, bmessage, warning, error) ); //should return false because the message is null
        }
    }
}
