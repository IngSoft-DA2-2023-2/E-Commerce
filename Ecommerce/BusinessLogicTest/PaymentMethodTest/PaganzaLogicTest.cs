﻿using BusinessLogic.PaymentMethod;
using BusinessLogic.Promotions;
using Domain;
using LogicInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest.PaymentMethodTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PaganzaLogicTest
    {
        private IPaymentMethod _paganzaLogic;
        private int totalCart = 100;
        private string categoryName = "Paganza";

        [TestInitialize]
        public void Init()
        {
            _paganzaLogic = new PaganzaLogic();
        }

        [TestMethod]
        public void GivenEmptyCartReturnsZero()
        {
            Assert.AreEqual(0, _paganzaLogic.CalculateDiscount(0, categoryName));
        }


    }
}