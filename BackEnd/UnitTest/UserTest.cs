using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{

    [TestClass]
    public class UserTest
    {
        private User userSample;
        private const string userSampleName = "userSample";
        private const string userSampleEmail = "user@Sample.com";
        private const string userSamplePassword = "userPassword";
        private const string thisNameIsTooLong = "thisPasswordIsIncorrectEvenThoughItOnlyCointainsLetters";
        private const string thisNameIsTooShort = "a";
        private const string nonAlphanumericalName = "______";
        private const string thisPasswordIsTooLong = "ThisP44swordIsT00Long1234";
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

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name length must be between 3 and 20")]
        public void GivenTooLongNameThrowsBackEndException()
        {
            userSample.Name = thisNameIsTooLong;
        }
        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name length must be between 3 and 20")]
        public void GivenTooShortNameThrowsBackEndException()
        {
            userSample.Name = thisNameIsTooShort;
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name must be alphanumerical")]
        public void GivenNonAlphanumericalNameThrowsBackEndException()
        {
            userSample.Name = nonAlphanumericalName;
        }
        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Password length must be between 5 and 25")]
        public void GivenTooShortPasswordThrowsBackEndException()
        {
            userSample.Password = "1";
        }
        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Password length must be between 5 and 25")]
        public void GivenTooLongPasswordThrowsBackEndException()
        {
            userSample.Password = thisPasswordIsTooLong;
        }


    }
}
