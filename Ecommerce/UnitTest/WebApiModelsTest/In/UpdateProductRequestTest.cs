﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using WebApi.Models.In;

namespace UnitTest.WebApiModelsTest.In
{

    [TestClass]
    public class UpdateProductRequestTest
    {
        private UpdateProductRequest updateProductRequestExample;
        private const string _nameSample = "name sample";
        private const int _priceSample = 100;
        private const string _descriptionSample = "description sample";
        private const string _brandSample = "brand sample";
        private const string _categorySample = "category sample";
        private List<string> _colorsSample = new List<string> { "Red", "Blue" };
      


        [TestInitialize]
        public void Init()
        {
            updateProductRequestExample = new UpdateProductRequest();
        }


        [TestMethod]
        public void GivenUpdateProductRequestReturnsItsName()
        {
            updateProductRequestExample.Name = _nameSample;
            Assert.AreEqual(_nameSample, updateProductRequestExample.Name);
        }

        [TestMethod]
        public void GivenUpdateProductRequestReturnsItsDescription()
        {
            updateProductRequestExample.Description = _descriptionSample;
            Assert.AreEqual(_descriptionSample, updateProductRequestExample.Description);
        }

        [TestMethod]
        public void GivenUpdateProductRequestReturnsItsPrice()
        {
            updateProductRequestExample.Price = _priceSample;
            Assert.AreEqual(_priceSample, updateProductRequestExample.Price);
        }

        [TestMethod]
        public void GivenUpdateProductRequestReturnsItsBrand()
        {
            updateProductRequestExample.Brand = _brandSample;
            Assert.AreEqual(_brandSample, updateProductRequestExample.Brand);
        }

        [TestMethod]
        public void GivenUpdateProductRequestReturnsItsCategory()
        {
            updateProductRequestExample.Category = _categorySample;
            Assert.AreEqual(_categorySample, updateProductRequestExample.Category);
        }

        [TestMethod]
        public void GivenUpdateProductRequestReturnsItsColor()
        {
            updateProductRequestExample.Color = _colorsSample;
            Assert.AreEqual(_colorsSample, updateProductRequestExample.Color);
        }



    }
}