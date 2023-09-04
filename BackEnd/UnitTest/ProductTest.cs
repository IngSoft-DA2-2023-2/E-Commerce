using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;

namespace UnitTest
{
    [TestClass]
    public class ProductTest
    {
        const string _nameSample = "name sample";
        const int _priceSample = 100;
        [TestMethod]
        public void GivenAProductReturnsItsName()
        {
            Product p = new Product();
            p.Name = _nameSample;
            
           Assert.AreEqual(_nameSample,p.Name);
        }

        [TestMethod]
        public void GivenAProductReturnsItsPrice()
        {
            Product p = new Product();
            p.Price = _priceSample;

            Assert.AreEqual(_priceSample, p.Price);
        }
    }
}
