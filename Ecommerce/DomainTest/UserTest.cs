using Domain;
using Domain.Exceptions;
using Domain.ProductParts;
using System.Diagnostics.CodeAnalysis;

namespace UnitTest.DomainTest
{

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserTest
    {
        private User userSample;
        private const string _userSampleName = "userSample";
        private const string _userSampleEmail = "user@Sample.com";
        private const string _userSamplePassword = "userPassword";
        private const string _userSampleAddress = "user street";
        private readonly List<StringWrapper> _userSampleRoles = new() { new StringWrapper() { Info = "role sample 1" } };
        private readonly string _nameRoleSample = "role sample 2";
        private const string _thisNameIsTooLong = "thisPasswordIsIncorrectEvenThoughItOnlyCointainsLetters";
        private const string _thisNameIsTooShort = "a";
        private const string _thisPasswordIsTooShort = "1";
        private const string _nonAlphanumericalName = "______";
        private const string _thisPasswordIsTooLong = "ThisP44swordIsT00Long1234";
        private const string _wrongEmailFormat = "format.com";
        [TestInitialize]
        public void Init()
        {
            userSample = new User();
        }

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
        [ExpectedException(typeof(DomainException), "Name length must be between 3 and 20")]
        public void GivenTooLongNameThrowsBackEndException()
        {
            userSample.Name = _thisNameIsTooLong;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Name length must be between 3 and 20")]
        public void GivenTooShortNameThrowsBackEndException()
        {
            userSample.Name = _thisNameIsTooShort;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Name must be alphanumerical")]
        public void GivenNonAlphanumericalNameThrowsBackEndException()
        {
            userSample.Name = _nonAlphanumericalName;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Password length must be between 5 and 25")]
        public void GivenTooShortPasswordThrowsBackEndException()
        {
            userSample.Password = _thisPasswordIsTooShort;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Password length must be between 5 and 25")]
        public void GivenTooLongPasswordThrowsBackEndException()
        {
            userSample.Password = _thisPasswordIsTooLong;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "email format is not valid")]
        public void GivenWrongEmailFormatThrowsBackEndException()
        {
            userSample.Email = _wrongEmailFormat;
        }

        [TestMethod]
        public void GivenAddressAssignsIt()
        {
            userSample.Address = _userSampleAddress;
            Assert.AreEqual(_userSampleAddress, userSample.Address);
        }

        [TestMethod]
        public void GivenListOfRolesAssignThem()
        {
            userSample.Roles = _userSampleRoles;
            Assert.AreEqual(1, userSample.Roles.Count);
            Assert.AreEqual(_userSampleRoles, userSample.Roles);
        }

        [TestMethod]
        public void GivenRoleAddsItToCurrentOnes()
        {
            userSample.Roles.Add(new StringWrapper() { Info = _nameRoleSample });

            Assert.IsTrue(_userSampleRoles.Count == 1);
            Assert.AreEqual(_nameRoleSample, userSample.Roles.First().Info);
        }
    }
}
