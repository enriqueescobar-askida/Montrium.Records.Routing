// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlLookupManagerTest.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This is a test class for XmlLookupManagerTest and is intended to contain all XmlLookupManagerTest Unit Tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.RCRouter
{
    using System.Collections.Generic;
    using System.Xml;

    using DropOffEventReceiver;

    using Microsoft.SharePoint;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>This is a test class for XmlLookupManagerTest and is intended to contain all XmlLookupManagerTest Unit Tests.</summary>
    [TestClass()]
    public class XmlLookupManagerTest
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
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            string header = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceLog(header);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>A test for XmlLookupManager Constructor.</summary>
        [TestMethod()]
        public void XmlLookupManagerConstructorTest()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>A test for Contains.</summary>
        [TestMethod()]
        public void ContainsTest()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            SPField spField = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Contains(spField);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for FindXmlLookups.</summary>
        [TestMethod()]
        [DeploymentItem("DropOffEventReceiver.dll")]
        public void FindXmlLookupsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlLookupManager_Accessor target = new XmlLookupManager_Accessor(param0); // TODO: Initialize to an appropriate value
            XmlElement xmlDocumentElement = null; // TODO: Initialize to an appropriate value
            List<XmlLookupNode> expected = null; // TODO: Initialize to an appropriate value
            List<XmlLookupNode> actual;
            actual = target.FindXmlLookups(xmlDocumentElement);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for IsIdValid.</summary>
        [TestMethod()]
        [DeploymentItem("DropOffEventReceiver.dll")]
        public void IsIdValidTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlLookupManager_Accessor target = new XmlLookupManager_Accessor(param0); // TODO: Initialize to an appropriate value
            string guid = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsIdValid(guid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for IsInternalNameUse.
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DropOffEventReceiver.dll")]
        public void IsInternalNameUseTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlLookupManager_Accessor target = new XmlLookupManager_Accessor(param0); // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsInternalNameUse(name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for IsInternalNameValid.</summary>
        [TestMethod()]
        [DeploymentItem("DropOffEventReceiver.dll")]
        public void IsInternalNameValidTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlLookupManager_Accessor target = new XmlLookupManager_Accessor(param0); // TODO: Initialize to an appropriate value
            string spFieldInternalName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsInternalNameValid(spFieldInternalName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for IsTitleValid.</summary>
        [TestMethod()]
        [DeploymentItem("DropOffEventReceiver.dll")]
        public void IsTitleValidTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlLookupManager_Accessor target = new XmlLookupManager_Accessor(param0); // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsTitleValid(title);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for IsTypeValid.</summary>
        [TestMethod()]
        [DeploymentItem("DropOffEventReceiver.dll")]
        public void IsTypeValidTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            XmlLookupManager_Accessor target = new XmlLookupManager_Accessor(param0); // TODO: Initialize to an appropriate value
            string spFieldType = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsTypeValid(spFieldType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for ToString.</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for TraceLog.</summary>
        [TestMethod()]
        public void TraceLogTest1()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            string header = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceLog(header);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>A test for Value.</summary>
        [TestMethod()]
        public void ValueTest()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            SPField spField = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Value(spField);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for LookupNodeList.</summary>
        [TestMethod()]
        public void LookupNodeListTest()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            List<XmlLookupNode> expected = null; // TODO: Initialize to an appropriate value
            List<XmlLookupNode> actual;
            target.LookupNodeList = expected;
            actual = target.LookupNodeList;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for LookupNodeMatched.</summary>
        [TestMethod()]
        public void LookupNodeMatchedTest()
        {
            string xmlProperties = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupManager target = new XmlLookupManager(xmlProperties); // TODO: Initialize to an appropriate value
            XmlLookupNode expected = null; // TODO: Initialize to an appropriate value
            XmlLookupNode actual;
            target.LookupNodeMatched = expected;
            actual = target.LookupNodeMatched;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
