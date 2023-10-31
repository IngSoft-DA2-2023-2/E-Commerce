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
    public class PaymentMethodRepositoryTest
    {
        [TestMethod]
        public void GivenExistingPaymentMethodReturnsTrue()
        {
            PaymentMethodEntity paymentMethods = new PaymentMethodEntity() { CategoryName = "name"};
            string paymentMethodName = "name";
            var context = new Mock<ECommerceContext>();
            context.Setup(ctx => ctx.PaymentMethods).ReturnsDbSet(new List<PaymentMethodEntity>() { paymentMethods });
            IPaymentMethodRepository bankDebitRepository = new PaymentMethodRepository(context.Object);
            var expectedReturn = bankDebitRepository.CheckForPaymentMethod(paymentMethodName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingPaymentMethodThrowsException()
        {
            PaymentMethodEntity paymentMethods = new PaymentMethodEntity() { CategoryName = "name" };
            string paymentMethodName = "name";
            var context = new Mock<ECommerceContext>();
            context.Setup(ctx => ctx.PaymentMethods).ReturnsDbSet(new List<PaymentMethodEntity>() { });
            IPaymentMethodRepository bankDebitRepository = new PaymentMethodRepository(context.Object);
            Exception? catchedException = null;
            try
            {
                bankDebitRepository.CheckForPaymentMethod(paymentMethodName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.AreEqual(catchedException.Message, $"{paymentMethodName} does not exists");
        }
    }
}
