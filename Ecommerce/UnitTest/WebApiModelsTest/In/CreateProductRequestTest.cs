using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using WebApi.Models.In;

namespace UnitTest.WebApiModelsTest.In
{

    [TestClass]
    public class CreateProductRequestTest
    {
        private CreateProductRequest productRequestExample;
        private const string _nameSample = "name sample";
        private const int _priceSample = 100;
        private const string _descriptionSample = "description sample";
        private const string _brandSample = "brand sample";
        private const string _categorySample = "category sample";
        private List<string> _colorsSample = new List<string> { "Red", "Blue" };
      


        [TestInitialize]
        public void Init()
        {
            productRequestExample = new CreateProductRequest();
        }

        [TestMethod]
        public void GivenProductRequestReturnsItsName()
        {
            productRequestExample.Name = _nameSample;
            Assert.AreEqual(_nameSample, productRequestExample.Name);
        }

        [TestMethod]
        public void GivenProductRequestReturnsItsDescrition()
        {
            productRequestExample.Description = _descriptionSample;
            Assert.AreEqual(_descriptionSample, productRequestExample.Description);
        }

        [TestMethod]
        public void GivenProductRequestReturnsItsPrice()
        {
            productRequestExample.Price = _priceSample;
            Assert.AreEqual(_priceSample, productRequestExample.Price);
        }

        [TestMethod]
        public void GivenProductRequestReturnsItsBrand()
        {
            productRequestExample.Brand = _brandSample;
            Assert.AreEqual(_brandSample, productRequestExample.Brand);
        }

        [TestMethod]
        public void GivenProductRequestReturnsItsCategory()
        {
            productRequestExample.Category = _categorySample;
            Assert.AreEqual(_categorySample, productRequestExample.Category);
        }

        [TestMethod]
        public void GivenProductRequestReturnsItsColors()
        {
            productRequestExample.Color = _colorsSample;
            Assert.AreEqual(_colorsSample, productRequestExample.Color);
        }



    }
}
