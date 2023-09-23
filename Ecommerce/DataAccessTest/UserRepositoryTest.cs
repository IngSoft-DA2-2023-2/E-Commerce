using DataAccess.Context;
using DataAccess.Exceptions;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

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

        [TestMethod]
        public void DeleteUser()
        {

            User newUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com"
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(
                new List<User>
                {
                new User 
                    {
                        Name = "TestUser",
                        Email="test@example.com"
                    }
                }
                );
            userContext.Setup(c => c.Users.Remove(newUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(newUser);
            Assert.AreEqual(expectedReturn.Name, newUser.Name);
            Assert.AreEqual(expectedReturn.Email, newUser.Email);
        }


        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void DeleteNonExistingUser()
        {

            User newUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com"
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());
            userContext.Setup(c => c.Users.Remove(newUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(newUser);

        }


    }
}
