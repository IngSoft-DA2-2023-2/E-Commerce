using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;

namespace UnitTest
{
    [TestClass]
    public class ProductTest
    {
        const string _nameSample = "name sample";
        [TestMethod]
        public void GivenAProductReturrnsItsName()
        {
            Product p = new Product();
            p.Name = _nameSample;
            
           Assert.AreEqual(_nameSample,p.Name);
        }
    }
}
