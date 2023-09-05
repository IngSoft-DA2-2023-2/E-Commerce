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
        const string _anotherColorSample = "another color sample";
        Product productSample;

        [TestInitialize]
        public void Initialize() {
            productSample = new Product();
        }


        [TestMethod]
        public void GivenProductReturnsItsName()
        {
            productSample.Name = _nameSample;
            Assert.AreEqual(_nameSample, productSample.Name);
        }

        [TestMethod]
        public void GivenProductReturnsItsPrice()
        {
            productSample.Price = _priceSample;

            Assert.AreEqual(_priceSample, productSample.Price);
        }

        [TestMethod]
        public void GivenProductReturnsItsDescription() {
            productSample.Description = _descriptionSample;
                
            Assert.AreEqual(_descriptionSample, productSample.Description);
        }

        [TestMethod]
        public void GivenProductReturnsItsBrand()
        {
            productSample.Brand = _brandSample;

            Assert.AreEqual(_brandSample, productSample.Brand);
        }

        [TestMethod]
        public void GivenProductReturnsItsCategory()
        {
            productSample.Category = _categorySample;

            Assert.AreEqual(_categorySample, productSample.Category);
        }

        [TestMethod]
        public void GivenSingleColoredProductReturnsItsColor()
        {
            productSample.Color.Add(_colorSample);

            Assert.AreEqual(1, productSample.Color.Count);
            Assert.AreEqual(_colorSample, productSample.Color[0]);
        }

        [TestMethod]
        public void GivenMultipleColoredProductReturnsTheColors()
        {
            productSample.Color.Add(_colorSample);
            productSample.Color.Add(_anotherColorSample);

            Assert.AreEqual(2, productSample.Color.Count);
            Assert.AreEqual(_colorSample, productSample.Color[0]);
            Assert.AreEqual(_anotherColorSample, productSample.Color[1]);

        }


    }
}
