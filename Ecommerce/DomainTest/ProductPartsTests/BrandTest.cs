﻿using Domain.ProductParts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.ProductPartsTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BrandTest
    {
        [TestMethod]
        public void GetBrandId()
        {
            Guid guid = Guid.NewGuid();
            Brand brand = new Brand()
            {
                Id = guid,
            };
            Assert.AreEqual(guid, brand.Id);
        }
        [TestMethod]
        public void BrandEqualsReturnsTrue()
        {
            string name = "brand";
            Brand brand1 = new Brand()
            {
                Name = name,
            };
            Brand brand2 = new Brand()
            {
                Name = name,
            };
            Assert.IsTrue(brand1.Equals(brand2));
        }
    }
}