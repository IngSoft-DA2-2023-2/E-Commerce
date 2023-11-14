using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UpdateUserByAdminRequestTest
    {
        private readonly string nameSample = "nameSample";
        private readonly string passwordSample = "passwordSample";
        private readonly string addressSample = "addressSample";
        private readonly List<string> buyerRole = new List<string> { "buyer" };
        private UpdateUserRequestByAdmin user;

        [TestInitialize]
        public void Init()
        {
            user = new UpdateUserRequestByAdmin()
            {
                Name = nameSample,
                Password = passwordSample,
                Address = addressSample,
                Roles = buyerRole,
            };
        }

        [TestMethod]
        public void SetValues()
        {
            Assert.AreEqual(user.Name, nameSample);
            Assert.AreEqual(user.Password, passwordSample);
            Assert.AreEqual(user.Address, addressSample);
            Assert.AreEqual(user.Roles, buyerRole);
        }
    }
}