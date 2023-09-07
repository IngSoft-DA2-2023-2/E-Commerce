using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ProductTest
    {
        private const string _nameSample = "name sample";
        private const int _priceSample = 100;
        private const string _descriptionSample = "description sample";
        private const string _brandSample = "brand sample";
        private const string _categorySample = "category sample";
        private const string _colorSample = "color sample";
        private const string _anotherColorSample = "another color sample";
        private Product productSample;
        private const int _negativePriceSample = -10;

        [TestInitialize]
        public void Init()
        {
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
        public void GivenProductReturnsItsDescription()
        {
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

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Name must not be null")]
        public void GivenEmptyNameThrowsBackEndException()
        {
            productSample.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Price must not be negative")]
        public void GivenNegativePriceThrowsBackEndException()
        {
            productSample.Price = _negativePriceSample;
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Description must not be null")]
        public void GivenEmptyDescriptionThrowsBackEndException()
        {
            productSample.Description = null;
        }
    }
}

