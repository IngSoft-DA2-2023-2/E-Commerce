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
        private Guid _guidSample = new Guid();


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


    }
}
