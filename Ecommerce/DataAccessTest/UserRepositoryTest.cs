using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void AddUser()
        {
            User user = new User() { Email = "sample@sample.com" };
            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>() { });
            IUserRepository userRepository = new UserRepository(userContext.Object);
        }

    }
}
