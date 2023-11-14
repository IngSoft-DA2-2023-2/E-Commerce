using BusinessLogic;
using LogicInterface;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ReflectionPromotionsTest
    {
        private const int _minPromotions = 0;
        [TestMethod]
        public void ReturnListPromotionsTest()
        {
            ReflectionPromotions reflectionPromotions = new ReflectionPromotions();
            List<IPromotionable> listPromotions = reflectionPromotions.ReturnListPromotions();
            Assert.IsTrue(listPromotions.Count > _minPromotions);
            Assert.AreEqual(4, listPromotions.Count);
        }
    }
}