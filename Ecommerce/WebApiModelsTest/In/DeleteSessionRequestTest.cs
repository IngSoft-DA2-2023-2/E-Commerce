using ApiModels.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.In
{
    [TestClass]
    public class DeleteSessionRequestTest
    {
        [TestMethod]
        public void AssignsGuid()
        {
            Guid guid = Guid.NewGuid();
            DeleteSessionRequest request = new DeleteSessionRequest { Token = guid };

            Assert.AreEqual(guid, request.Token);
        }
    }
}
