using Domain;
using Domain.Exceptions;
using Domain.ProductParts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.DomainTest
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
            productSample.Brand = new Brand() {Name = _brandSample};

            Assert.AreEqual(_brandSample, productSample.Brand.Name);
        }

        [TestMethod]
        public void GivenProductReturnsItsCategory()
        {
            productSample.Category =new Category() {Name= _categorySample };

            Assert.AreEqual(_categorySample, productSample.Category.Name);
        }

        [TestMethod]
        public void GivenSingleColoredProductReturnsItsColor()
        {
            productSample.Colors = new List<Colour>()
            {
                new Colour() {Name = _colorSample},
            };
            Assert.AreEqual(1, productSample.Colors.Count());
            Assert.AreEqual(_colorSample, productSample.Colors.First().Name);
        }

        [TestMethod]
        public void GivenMultipleColoredProductReturnsTheColors()
        {
            productSample.Colors = new List<Colour>()
            {
                new Colour() {Name = _colorSample},
                new Colour() {Name = _anotherColorSample}
            };
            Assert.AreEqual(2, productSample.Colors.Count);
            Assert.AreEqual(_colorSample, productSample.Colors[0].Name);
            Assert.AreEqual(_anotherColorSample, productSample.Colors[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Name must not be null")]
        public void GivenEmptyNameThrowsBackEndException()
        {
            productSample.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Price must not be negative")]
        public void GivenNegativePriceThrowsBackEndException()
        {
            productSample.Price = _negativePriceSample;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Description must not be null")]
        public void GivenEmptyDescriptionThrowsBackEndException()
        {
            productSample.Description = null;
        }
    }
}

