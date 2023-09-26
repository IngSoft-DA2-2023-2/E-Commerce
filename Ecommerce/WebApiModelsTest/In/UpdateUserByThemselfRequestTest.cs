using ApiModels.In;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.In
{

        [TestClass]
        public class UpdateUserByThemselfRequestTest
        {
            private string nameSample = "nameSample";
            private string passwordSample = "passwordSample";
            private string addressSample = "addressSample";
            private List<string> buyerRole = new List<string> { "buyer" };
            private UpdateUserRequestByThemself user;

            [TestInitialize]
            public void Init()
            {
                user = new UpdateUserRequestByThemself()
                {
                    Name = nameSample,
                    Password = passwordSample,
                    Address = addressSample,
                };
            }

            [TestMethod]
            public void SetValues()
            {
                Assert.AreEqual(user.Name, nameSample);
                Assert.AreEqual(user.Password, passwordSample);
                Assert.AreEqual(user.Address, addressSample);
            }


        }
}
