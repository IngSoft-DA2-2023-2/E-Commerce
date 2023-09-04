using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;
using System.Security;

namespace UnitTest
{
    [TestClass]
    public class ProductTest
    {
        const string _nameSample = "name sample";
        const int _priceSample = 100;
        const string _descriptionSample = "description sample";
        const string _brandSample = "brand sample";
        const string _categorySample = "category sample";
        const string _colorSample = "color sample";

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

        [TestMethod]
        public void GivenAProductReturnsItsDescription() {
            Product p = new Product();
            p.Description = _descriptionSample;

            Assert.AreEqual(_descriptionSample, p.Description);
        }

        [TestMethod]
        public void GivenAProductReturnsItsBrand()
        {
            Product p = new Product();
            p.Brand = _brandSample;

            Assert.AreEqual(_brandSample, p.Brand);
        }

        [TestMethod]
        public void GivenAProductReturnsItsCategory()
        {
            Product p = new Product();
            p.Category = _categorySample;

            Assert.AreEqual(_categorySample, p.Category);
        }

        [TestMethod]
        public void GivenAProductReturnsItsColor()
        {
            Product p = new Product();
            p.Color = _colorSample;

            Assert.AreEqual(_colorSample, p.Color);
        }

    }
}
