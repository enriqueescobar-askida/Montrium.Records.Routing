﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecordDocumentTest.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This is a test class for RecordDocumentTest and is intended to contain all RecordDocumentTest Unit Tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.RCRouter
{
    using DropOffEventReceiver;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    using Microsoft.SharePoint;

    using System.Collections.Generic;

    /// <summary>This is a test class for RecordDocumentTest and is intended to contain all RecordDocumentTest Unit Tests.</summary>
    [TestClass()]
    public class RecordDocumentTest
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
            List<SPList> enabledLibraries = null; // TODO: Initialize to an appropriate value
            SPListItem dropOffListItem = null; // TODO: Initialize to an appropriate value
            SPListItemCollection routingRules = null; // TODO: Initialize to an appropriate value
            RecordDocument target = new RecordDocument(enabledLibraries, dropOffListItem, routingRules); // TODO: Initialize to an appropriate value
            string header = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceLog(header);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
