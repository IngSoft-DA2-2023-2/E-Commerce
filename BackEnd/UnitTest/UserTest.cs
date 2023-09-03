using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for UserTest
    /// </summary>
    [TestClass]
    public class UserTest
    {
        private User userSample;
        private const string userSampleName = "userSample";
        private const string userSampleEmail = "user@Sample.com";
        private const string userSamplePassword = "userPassword";
        [TestInitialize]
        public void Initialize()
        {
            userSample = new User();
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GivenValidNameAssignsToUser()
        {
            userSample.Name = userSampleName;
            Assert.AreEqual(userSampleName, userSample.Name);
        }
        [TestMethod]
        public void GivenValidEmailAssignsToUser()
        {
            userSample.Email = userSampleEmail;
            Assert.AreEqual(userSampleEmail, userSample.Email);
        }
        [TestMethod]
        public void GivenValidPasswordAssignsToUser()
        {
            userSample.Password = userSamplePassword;
            Assert.AreEqual(userSamplePassword, userSample.Password);
        }

    }
}
