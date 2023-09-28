using ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest
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
