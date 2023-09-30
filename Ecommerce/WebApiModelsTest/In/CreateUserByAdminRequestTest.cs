using ApiModels.In;
using Domain;
using Domain.ProductParts;

namespace WebApiModelsTest.In
{
    [TestClass]
    public class CreateUserByAdminRequestTest
    {
        private string nameSample = "nameSample";
        private string addressSample = "address sample";
        private string emailSample = "email@sample.com";
        private string passwordSample = "passwordSample";
        private List<StringWrapper> roles = new();
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
        public void UserRequestToUserEntity() {

            User entity = request.ToEntity();

            Assert.AreEqual(entity.Name, nameSample);
            Assert.AreEqual(entity.Address, addressSample);
            Assert.AreEqual(entity.Email, emailSample);
            Assert.AreEqual(entity.Password, passwordSample);
            Assert.AreEqual(entity.Roles.Count, 0);

        }
    }
}
