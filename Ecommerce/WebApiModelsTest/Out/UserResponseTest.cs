using ApiModels.Out;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.Out
{
    [TestClass]
    public class UserResponseTest
    {

        private readonly string nameSample = "nameSample";
        private readonly string addressSample = "address sample";
        private readonly Guid guidSample = Guid.NewGuid();
        private readonly string emailSample = "email@sample.com";
        private readonly string passwordSample = "passwordSample";

        [TestMethod]
        public void AssignValues()
        {
            User user = new User()
            {
                Name = nameSample,
                Address = addressSample,
                Guid = guidSample,
                Email = emailSample,
                Password = passwordSample
            };

            UserResponse response = new UserResponse(user);

            Assert.AreEqual(user.Name, response.Name);
            Assert.AreEqual(user.Address, response.Address);
            Assert.AreEqual(user.Guid, response.Guid);
            Assert.AreEqual(user.Email, response.Email);
            Assert.AreEqual(user.Password, response.Password);
        }
    }
}
