using BusinessLogic.PaymentMethod;
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
        private List<Product> _cartSample;

        private readonly Product _fiftyDollarProduct = new() { Price = 50 };


        [TestInitialize]
        public void Init()
        {
            _cartSample = new List<Product>();
            _paganzaLogic = new PaganzaLogic();
        }


    }
}
