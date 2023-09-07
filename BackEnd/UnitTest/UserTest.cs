using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{

    [TestClass]
    public class UserTest
    {
        private User userSample;
        private const string _userSampleName = "userSample";
        private const string _userSampleEmail = "user@Sample.com";
        private const string _userSamplePassword = "userPassword";
        private const string _thisNameIsTooLong = "thisPasswordIsIncorrectEvenThoughItOnlyCointainsLetters";
        private const string _thisNameIsTooShort = "a";
        private const string _nonAlphanumericalName = "______";
        private const string _thisPasswordIsTooLong = "ThisP44swordIsT00Long1234";
        private const string _wrongEmailFormat = "format.com";
        [TestInitialize]
        public void Init()
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
            userSample.Name = _userSampleName;
            Assert.AreEqual(_userSampleName, userSample.Name);
        }
        [TestMethod]
        public void GivenValidEmailAssignsToUser()
        {
            userSample.Email = _userSampleEmail;
            Assert.AreEqual(_userSampleEmail, userSample.Email);
        }
        [TestMethod]
        public void GivenValidPasswordAssignsToUser()
        {
            userSample.Password = _userSamplePassword;
            Assert.AreEqual(_userSamplePassword, userSample.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name length must be between 3 and 20")]
        public void GivenTooLongNameThrowsBackEndException()
        {
            userSample.Name = _thisNameIsTooLong;
        }
        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name length must be between 3 and 20")]
        public void GivenTooShortNameThrowsBackEndException()
        {
            userSample.Name = _thisNameIsTooShort;
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name must be alphanumerical")]
        public void GivenNonAlphanumericalNameThrowsBackEndException()
        {
            userSample.Name = _nonAlphanumericalName;
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
            userSample.Password = _thisPasswordIsTooLong;
        }
        [TestMethod]
        [ExpectedException(typeof(BackEndException), "email format is not valid")]
        public void GivenWrongEmailFormatThrowsBackEndException()
        {
            userSample.Email = _wrongEmailFormat;
        }


    }
}
