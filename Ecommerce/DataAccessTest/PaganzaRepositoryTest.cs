using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface.Exceptions;
using DataAccessInterface;
using Domain.PaymentMethodCategories;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PaganzaRepositoryTest
    {
        [TestMethod]
        public void GivenExistingFlagNameReturnsTrue()
        {
            Paganza paganza = new Paganza() { CategoryName = "Paganza"};
            string paganzaName = "Paganza";
            var paganzaContext = new Mock<ECommerceContext>();
            paganzaContext.Setup(ctx => ctx.Paganzas).ReturnsDbSet(new List<Paganza>() { paganza });
            IPaganzaRepository paganzaRepository = new PaganzaRepository(paganzaContext.Object);
            var expectedReturn = paganzaRepository.CheckForPaganza(paganzaName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingFlagNameThrowsException()
        {
            string paganzaName = "Paganza";
            var paganzaContext = new Mock<ECommerceContext>();
            paganzaContext.Setup(ctx => ctx.Paganzas).ReturnsDbSet(new List<Paganza>() { });
            IPaganzaRepository paganzaRepository = new PaganzaRepository(paganzaContext.Object);
            Exception catchedException = null;
            try
            {
                paganzaRepository.CheckForPaganza(paganzaName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"{paganzaName} does not exists"));


        }
    }
}
