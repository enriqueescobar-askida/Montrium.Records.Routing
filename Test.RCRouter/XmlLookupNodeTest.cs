// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlLookupNodeTest.cs" company="Montrium">
//   MIT License
// </copyright>
// <summary>
//   This is a test class for XmlLookupNodeTest and is intended to contain all XmlLookupNodeTest Unit Tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Test.RCRouter
{
    using DropOffEventReceiver;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Microsoft.SharePoint;

    /// <summary>This is a test class for XmlLookupNodeTest and is intended to contain all XmlLookupNodeTest Unit Tests.</summary>
    [TestClass()]
    public class XmlLookupNodeTest
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
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            string header = string.Empty; // TODO: Initialize to an appropriate value
            target.TraceLog(header);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>A test for XmlLookupNode Constructor.</summary>
        [TestMethod()]
        public void XmlLookupNodeConstructorTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>A test for GetSentenceName.</summary>
        [TestMethod()]
        public void GetSentenceNameTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetSentenceName();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for GetSpFieldType.</summary>
        [TestMethod()]
        public void GetSpFieldTypeTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            SPFieldType expected = new SPFieldType(); // TODO: Initialize to an appropriate value
            SPFieldType actual;
            actual = target.GetSpFieldType();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for XmlLookupNode Constructor.</summary>
        [TestMethod()]
        public void XmlLookupNodeConstructorTest1()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>A test for Value.</summary>
        [TestMethod()]
        public void ValueTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for Type.</summary>
        [TestMethod()]
        public void TypeTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for CamelCaseName.</summary>
        [TestMethod()]
        public void CamelCaseNameTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.CamelCaseName = expected;
            actual = target.CamelCaseName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>A test for ToString.</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string fieldName = string.Empty; // TODO: Initialize to an appropriate value
            string fieldValue = string.Empty; // TODO: Initialize to an appropriate value
            string fieldType = string.Empty; // TODO: Initialize to an appropriate value
            XmlLookupNode target = new XmlLookupNode(fieldName, fieldValue, fieldType); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
