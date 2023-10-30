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
    public class BankDebitRepositoryTest
    {
        [TestMethod]
        public void GivenExistingFlagNameReturnsTrue()
        {
            BankDebit bankDebit = new BankDebit() { Bank = "BankDebit"};
            string bankDebitName = "BankDebit";
            var bankDebitContext = new Mock<ECommerceContext>();
            bankDebitContext.Setup(ctx => ctx.BankDebits).ReturnsDbSet(new List<BankDebit>() { bankDebit });
            IBankDebitRepository bankDebitRepository = new BankDebitRepository(bankDebitContext.Object);
            var expectedReturn = bankDebitRepository.CheckForBankDebit(bankDebitName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingFlagNameThrowsException()
        {
            string bankDebitName = "BankDebit";
            var bankDebitContext = new Mock<ECommerceContext>();
            bankDebitContext.Setup(ctx => ctx.BankDebits).ReturnsDbSet(new List<BankDebit>() { });
            IBankDebitRepository bankDebitRepository = new BankDebitRepository(bankDebitContext.Object);
            Exception catchedException = null;
            try
            {
                bankDebitRepository.CheckForBankDebit(bankDebitName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"{bankDebitName} does not exists"));
        }
    }
}
