﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using WebApi.Models.Out;

namespace UnitTest.WebApiModelsTest.Out
{

    [TestClass]
    public class UpdateProductResponseTest
    {
        private UpdateProductResponse updateProductResponseExample;
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
            updateProductResponseExample = new UpdateProductResponse();
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsGUID()
        {
            updateProductResponseExample.GUID = _guidSample;
            Assert.AreEqual(_guidSample, updateProductResponseExample.GUID);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsName()
        {
            updateProductResponseExample.Name = _nameSample;
            Assert.AreEqual(_nameSample, updateProductResponseExample.Name);
        }
    }
}