// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordFieldManagerTest.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This is a test class for RecordFieldManagerTest and is intended to contain all RecordFieldManagerTest Unit Tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.RCRouter
{
    using DropOffEventReceiver;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    using Microsoft.SharePoint;

    /// <summary>This is a test class for RecordFieldManagerTest and is intended to contain all RecordFieldManagerTest Unit Tests.</summary>
    [TestClass()]
    public class RecordFieldManagerTest
    {
        /// <summary>Gets or sets the test context which provides information about and functionality for the current test run.</summary>
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
            SPListItem fileSpListItem = null; // TODO: Initialize to an appropriate value
            SPList contextLibraryList = null; // TODO: Initialize to an appropriate value
            string url = string.Empty; // TODO: Initialize to an appropriate value
            string xmlLookup = string.Empty; // TODO: Initialize to an appropriate value
            bool changeVersion = false; // TODO: Initialize to an appropriate value
            RecordFieldManager target = new RecordFieldManager(fileSpListItem, contextLibraryList, url, xmlLookup, changeVersion); // TODO: Initialize to an appropriate value
            string header = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceLog(header);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
