using DataAccess.Context;
using DataAccessInterface.Exceptions;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
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

            User deletingUser = new User
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
            userContext.Setup(c => c.Users.Remove(deletingUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(deletingUser);
            Assert.AreEqual(expectedReturn.Name, deletingUser.Name);
            Assert.AreEqual(expectedReturn.Email, deletingUser.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void DeleteNonExistingUser()
        {

            User deletingUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com"
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());
            userContext.Setup(c => c.Users.Remove(deletingUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(deletingUser);
        }

        [TestMethod]
        public void GetExistingUser()
        {
            User existingUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com"
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { existingUser });

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.GetAllUsers(u => u.Name == "TestUser").ToList();

            Assert.AreEqual(expectedReturn.Count, 1);
            Assert.AreEqual(expectedReturn[0], existingUser);
        }


        [TestMethod]
        public void GetNoUsers()
        {
            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { });

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.GetAllUsers(u => true).ToList();

            Assert.AreEqual(expectedReturn.Count, 0);
        }


        [TestMethod]
        public void UpdateAllUserProperties()
        {
            User existingUser = new User
            {
                Name = "oldName",
                Email = "test@example.com",
                Address = "old street",
                Password = "old password",
            };

            User updatedUser = new User
            {
                Name = "newName",
                Email = "test@example.com",
                Address = "new street",
                Password = "new password",
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { existingUser });
            
            IUserRepository userRepository = new UserRepository(userContext.Object);

            var updatedResult = userRepository.UpdateUser(updatedUser);

            Assert.AreEqual(updatedResult.Name, updatedUser.Name);
            Assert.AreEqual(updatedResult.Address, updatedUser.Address);
            Assert.AreEqual(updatedResult.Password, updatedUser.Password);
            Assert.AreEqual(updatedResult.Email, updatedUser.Email);
        }

        [TestMethod]
        public void UpdateUserName()
        {
            User existingUser = new User
            {
                Name = "oldName",
                Email = "test@example.com",
            };

            User updatedUser = new User
            {
                Email = "test@example.com",
                Address = "new street",
                Password = "new password",
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { existingUser });

            IUserRepository userRepository = new UserRepository(userContext.Object);

            var updatedResult = userRepository.UpdateUser(updatedUser);

            Assert.AreEqual(updatedResult.Name, existingUser.Name);
            Assert.AreEqual(updatedResult.Address, updatedUser.Address);
            Assert.AreEqual(updatedResult.Password, updatedUser.Password);
            Assert.AreEqual(updatedResult.Email, updatedUser.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void UpdateNonExistingUserThrowsException()
        {
            User existingUser = new User
            {
                Name = "oldName",
                Email = "test@example.com",
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> {});

            IUserRepository userRepository = new UserRepository(userContext.Object);

            var updatedResult = userRepository.UpdateUser(existingUser);
        }

    }
}

