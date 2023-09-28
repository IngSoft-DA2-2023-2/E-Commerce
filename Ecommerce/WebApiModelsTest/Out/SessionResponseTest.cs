using ApiModels.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.Out
{
    [TestClass]
    public class SessionResponseTest
    {
        [TestMethod]
        public void AssignsAllValues()
        { 
            Guid guid = Guid.NewGuid();
            SessionResponse response = new SessionResponse();
            response.Token = guid;

            Assert.AreEqual(guid,response.Token);
        }
    }
}
