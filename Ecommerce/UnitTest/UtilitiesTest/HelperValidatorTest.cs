using Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.UtilitiesTest
{

    [TestClass]
    public class CreateProductResponseTest
    {
        private const int _minLength = 3;
        private const int _maxLength = 10;
        private const string _twoCharName = "Ab";
        private const string _elevenCharName = "Abcdefghijk";
        private const string _tenCharName = "Abcdefghij";
        private const string _nonAlphanumericalName = "_*";
        private const string _wrongEmailFormat = "wrongemail@";
        private const string _correctEmailFormat = "myEmail@email.com";

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
            bool result = HelperValidator.IsValidEmail(_wrongEmailFormat);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GivenCorrectEmailFormatReturnsTrue()
        {
            bool result = HelperValidator.IsValidEmail(_correctEmailFormat);
            Assert.IsTrue(result);
        }


    }
}
