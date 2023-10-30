using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.PaymentMethodCategories;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PaypalRepositoryTest
    {
        [TestMethod]
        public void GivenExistingFlagNameReturnsTrue()
        {
            Paypal paypal = new Paypal() { CategoryName = "Paypal"};
            string paypalName = "Paypal";
            var paypalContext = new Mock<ECommerceContext>();
            paypalContext.Setup(ctx => ctx.Paypal).ReturnsDbSet(new List<Paypal>() { paypal });
            IPaypalRepository paypalRepository = new PaypalRepository(paypalContext.Object);
            var expectedReturn = paypalRepository.CheckForPaypal(paypalName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingFlagNameThrowsException()
        {
            string paypalName = "Paypal";
            var paypalContext = new Mock<ECommerceContext>();
            paypalContext.Setup(ctx => ctx.Paypal).ReturnsDbSet(new List<Paypal>() { });
            IPaypalRepository paypalRepository = new PaypalRepository(paypalContext.Object);
            Exception catchedException = null;
            try
            {
                paypalRepository.CheckForPaypal(paypalName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"{paypalName} does not exists"));
        }
    }
}
