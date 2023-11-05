using BusinessLogic;
using LogicInterface;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ReflectionPromotionsTest
    {
        [TestMethod]
        public void ReturnListPromotionsTest()
        {
            ReflectionPromotions reflectionPromotions = new ReflectionPromotions();
            List<IPromotionable> listPromotions = reflectionPromotions.ReturnListPromotions();
            Assert.IsTrue(listPromotions.Count > 0);
            Assert.AreEqual(4, listPromotions.Count);
        }

    }
}
