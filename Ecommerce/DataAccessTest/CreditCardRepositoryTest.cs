using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface.Exceptions;
using DataAccessInterface;
using Domain.ProductParts;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PaymentMethodCategories;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreditCardRepositoryTest
    {
        [TestMethod]
        public void GivenExistingFlagNameReturnsTrue()
        {
            CreditCard creditCard = new CreditCard() { CategoryName = "CreditCard", Flag = "Visa" };
            string flagName = "Visa";
            var creditCardContext = new Mock<ECommerceContext>();
            creditCardContext.Setup(ctx => ctx.CreditCards).ReturnsDbSet(new List<CreditCard>() { creditCard });
            ICreditCardRepository creditCardRepository = new CreditCardRepository(creditCardContext.Object);
            var expectedReturn = creditCardRepository.CheckForCreditCard(flagName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingFlagNameThrowsException()
        {

            string flagName = "flagName";
            var creditCardContext = new Mock<ECommerceContext>();
            creditCardContext.Setup(ctx => ctx.CreditCards).ReturnsDbSet(new List<CreditCard>() { });
            ICreditCardRepository creditCardRepository = new CreditCardRepository(creditCardContext.Object);
            Exception catchedException = null;
            try
            {
                creditCardRepository.CheckForCreditCard(flagName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"CreditCard {flagName} does not exists"));


        }
    }
}
