﻿using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [TestClass]
    public class BrandRepositoryTest
    {
        [TestMethod]
        public void GivenExistingBrandNameReturnsTrue()
        {
            Brand brand = new Brand() { Name = "brand" };
            string brandName = "brand";
            var brandContext = new Mock<ECommerceContext>();
            brandContext.Setup(ctx => ctx.Brands).ReturnsDbSet(new List<Brand>() {brand });
            IBrandRepository brandRepository = new BrandRepository(brandContext.Object);
            var expectedReturn = brandRepository.CheckForBrand(brandName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenExistingBrandNameReturnsFalse()
        {
            string brandName = "brand";
            var brandContext = new Mock<ECommerceContext>();
            brandContext.Setup(ctx => ctx.Brands).ReturnsDbSet(new List<Brand>() { });
            IBrandRepository brandRepository = new BrandRepository(brandContext.Object);
            var expectedReturn = brandRepository.CheckForBrand(brandName);
            Assert.IsFalse(expectedReturn);
        }
    }
}