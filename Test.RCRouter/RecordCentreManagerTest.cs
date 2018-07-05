// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordCentreManagerTest.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This is a test class for RecordCentreManagerTest and is intended
//   to contain all RecordCentreManagerTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.RCRouter
{
    using System;

    using DropOffEventReceiver;

    using Microsoft.SharePoint;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for RecordCentreManagerTest and is intended
    /// to contain all RecordCentreManagerTest Unit Tests
    /// </summary>
    [TestClass()]
    public class RecordCentreManagerTest
    {
        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

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

        /// <summary>A test for TraceLog.</summary>
        [TestMethod()]
        public void TraceLogTest()
        {
            SPListItem dropOffListItem = null; // TODO: Initialize to an appropriate value
            string url = string.Empty; // TODO: Initialize to an appropriate value
            RecordCentreManager target = new RecordCentreManager(dropOffListItem, url); // TODO: Initialize to an appropriate value
            string header = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceLog(header);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
