using ApiModels.Out;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.Out
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class SessionResponseTest
    {
        [TestMethod]
        public void AssignsAllValues()
        {
            Guid guid = Guid.NewGuid();
            SessionResponse response = new SessionResponse();
            response.Token = guid;

            Assert.AreEqual(guid, response.Token);
        }
    }
}
