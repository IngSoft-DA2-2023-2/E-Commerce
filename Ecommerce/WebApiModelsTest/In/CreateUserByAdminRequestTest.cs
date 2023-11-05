using ApiModels.In;
using Domain;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreateUserByAdminRequestTest
    {
        private readonly string nameSample = "nameSample";
        private readonly string addressSample = "address sample";
        private readonly string emailSample = "email@sample.com";
        private readonly string passwordSample = "passwordSample";
        private readonly List<string> roles = new();
        private CreateUserByAdminRequest request;


        [TestInitialize]
        public void Init()
        {
            request = new CreateUserByAdminRequest()
            {
                Name = nameSample,
                Address = addressSample,
                Email = emailSample,
                Password = passwordSample,
                Roles = roles,
            };

        }


        [TestMethod]
        public void AssignsProperties()
        {
            Assert.AreEqual(request.Name, nameSample);
            Assert.AreEqual(request.Address, addressSample);
            Assert.AreEqual(request.Email, emailSample);
            Assert.AreEqual(request.Password, passwordSample);
            Assert.AreEqual(request.Roles.Count, 0);
        }

        [TestMethod]
        public void UserRequestToUserEntity()
        {

            User entity = request.ToEntity();

            Assert.AreEqual(entity.Name, nameSample);
            Assert.AreEqual(entity.Address, addressSample);
            Assert.AreEqual(entity.Email, emailSample);
            Assert.AreEqual(entity.Password, passwordSample);
            Assert.AreEqual(entity.Roles.Count, 0);

        }
    }
}
