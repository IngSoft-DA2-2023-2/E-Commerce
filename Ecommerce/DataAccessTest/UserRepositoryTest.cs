using DataAccess.Context;
using DataAccess.Exceptions;
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
        public void CreateUser()
        {

            User newUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com"
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());
            userContext.Setup(c => c.Users.Add(newUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.CreateUser(newUser);
            Assert.AreEqual(expectedReturn, newUser);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void CreateUserThrowsException()
        {
            User newUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com"
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());
            userContext.Setup(c => c.Users.Add(newUser)).Throws(new DataAccessException());
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            userRepository.CreateUser(newUser);   
        }



    }
}
