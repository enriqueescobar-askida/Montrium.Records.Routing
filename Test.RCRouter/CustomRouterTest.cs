// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomRouterTest.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This is a test class for CustomRouterTest and is intended to contain all CustomRouterTest Unit Tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.RCRouter
{
    using DropOffEventReceiver;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    using Microsoft.SharePoint;

    /// <summary>This is a test class for CustomRouterTest and is intended to contain all CustomRouterTest Unit Tests.
    ///</summary>
    [TestClass()]
    public class CustomRouterTest
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

        /// <summary>A test for Initialize.</summary>
        [TestMethod()]
        public void InitializeTest()
        {
            CustomRouter target = new CustomRouter(); // TODO: Initialize to an appropriate value
            string absoluteSiteUrl = string.Empty; // TODO: Initialize to an appropriate value
            SPFile recordFile = null; // TODO: Initialize to an appropriate value
            string contentTypeName = string.Empty; // TODO: Initialize to an appropriate value
            target.Initialize(absoluteSiteUrl, recordFile, contentTypeName);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
