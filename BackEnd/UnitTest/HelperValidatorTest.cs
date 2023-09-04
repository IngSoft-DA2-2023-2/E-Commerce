using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{

    [TestClass]
    public class HelperValidatorTest
    {
        private const int _minLength = 3;
        private const int _maxLength = 10;
        private const string _twoCharName = "Ab";
        private const string _elevenCharName = "Abcdefghijk";
        private const string _tenCharName = "Abcdefghij";
        private const string _nonAlphanumericalName = "_*";
        private const string _wrongEmail = "wrongemail@";


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
        public void GivenNameShorterThanMinimumReturnsFalse()
        {
            bool result = HelperValidator.IsLengthBetween(_twoCharName, _minLength, _maxLength);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GivenNameLargerThanMaximumReturnsFalse()
        {
            bool result = HelperValidator.IsLengthBetween(_elevenCharName, _minLength, _maxLength);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GivenValidNameReturnsTrue()
        {
            bool result = HelperValidator.IsLengthBetween(_tenCharName, _minLength, _maxLength);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivenNameStartingWithSpacesReturnsTrue()
        {
            bool result = HelperValidator.IsTrimmable(" " + _tenCharName);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivenNameEndingWithSpacesReturnsTrue()
        {
            bool result = HelperValidator.IsTrimmable(_tenCharName + " ");
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivenNameWithoutSpacesReturnsFalse()
        {
            bool result = HelperValidator.IsTrimmable(_tenCharName);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GivenNonAlphanumericalNameReturnsFalse()
        {
            bool result = HelperValidator.IsAlphanumerical(_nonAlphanumericalName);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GivenAlphanumericalNameReturnsTrue()
        {
            bool result = HelperValidator.IsAlphanumerical(_tenCharName);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivenWrongEmailFormatReturnsFalse()
        {
            bool result = HelperValidator.IsValidEmail(_wrongEmail);
            Assert.IsFalse(result);
        }


    }
}
