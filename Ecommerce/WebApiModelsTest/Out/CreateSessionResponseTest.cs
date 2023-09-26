using ApiModels.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.Out
{
    [TestClass]
    public class CreateSessionResponseTest
    {
        [TestMethod]
        public void AssignsAllValues()
        { 
            Guid guid = Guid.NewGuid();
            CreateSessionResponse response = new CreateSessionResponse();
            response.Token = guid;

            Assert.AreEqual(guid,response.Token);
        }
    }
}
