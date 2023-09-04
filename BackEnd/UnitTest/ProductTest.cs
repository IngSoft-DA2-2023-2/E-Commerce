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
        Product productSample;

        [TestInitialize]
        public void Initialize() {
            productSample = new Product();
        }


        [TestMethod]
        public void GivenAProductReturnsItsName()
        {
            productSample.Name = _nameSample;
            Assert.AreEqual(_nameSample, productSample.Name);
        }

        [TestMethod]
        public void GivenAProductReturnsItsPrice()
        {
            productSample.Price = _priceSample;

            Assert.AreEqual(_priceSample, productSample.Price);
        }

        [TestMethod]
        public void GivenAProductReturnsItsDescription() {
            productSample.Description = _descriptionSample;
                
            Assert.AreEqual(_descriptionSample, productSample.Description);
        }

        [TestMethod]
        public void GivenAProductReturnsItsBrand()
        {
            productSample.Brand = _brandSample;

            Assert.AreEqual(_brandSample, productSample.Brand);
        }

        [TestMethod]
        public void GivenAProductReturnsItsCategory()
        {
            productSample.Category = _categorySample;

            Assert.AreEqual(_categorySample, productSample.Category);
        }

        [TestMethod]
        public void GivenAProductReturnsItsColor()
        {
            productSample.Color = _colorSample;

            Assert.AreEqual(_colorSample, productSample.Color);
        }

    }
}
