using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
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
            userContext.Setup(c => c.Users.Add(newUser)).Throws(new DataAccessException($"User with email test@example.com already exists."));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            userRepository.CreateUser(newUser);
        }

        [TestMethod]
        public void DeleteUser()
        {
            Guid id = Guid.NewGuid();
            User deletingUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com",
                Id = id
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(
                new List<User>
                {
                new User
                    {
                        Name = "TestUser",
                        Email="test@example.com",
                        Id = id
                    }
                }
                );
            userContext.Setup(c => c.Users.Remove(deletingUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(deletingUser.Id);
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
                Email = "test@example.com",
                Id = Guid.NewGuid()
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());
            userContext.Setup(c => c.Users.Remove(deletingUser));
            userContext.Setup(c => c.SaveChanges());

            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(deletingUser.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void DeleteAdminUserThrowsException()
        {

            User deletingUser = new User
            {
                Name = "TestUser",
                Email = "test@example.com",
                Id = Guid.NewGuid(),
                Roles = new List<StringWrapper> { new StringWrapper() {Info="admin" } }
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User>() { deletingUser});
            userContext.Setup(c => c.Users.Remove(deletingUser));
            userContext.Setup(c => c.SaveChanges());
            IUserRepository userRepository = new UserRepository(userContext.Object);
            var expectedReturn = userRepository.DeleteUser(deletingUser.Id);
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
            Guid id = Guid.NewGuid();
            User existingUser = new User
            {
                Id = id,
                Name = "oldName",
                Email = "test@example.com",
                Address = "old street",
                Password = "old password",
            };

            User updatedUser = new User
            {
                Id = id,
                Name = "newName",
                Email = "test@example.com",
                Address = "new street",
                Password = "new password",
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { updatedUser });

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
            Guid id = Guid.NewGuid();
            User existingUser = new User
            {
                Name = "oldName",
                Email = "test@example.com",
                Address = "old address",
                Id = id
            };

            User updatedUser = new User
            {
                Email = "test@example.com",
                Address = "new street",
                Password = "new password",
                Id = id
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { existingUser });

            IUserRepository userRepository = new UserRepository(userContext.Object);

            var updatedResult = userRepository.UpdateUser(updatedUser);

            Assert.AreEqual(updatedResult.Name, updatedResult.Name);
            Assert.AreEqual(updatedResult.Address, updatedUser.Address);
            Assert.AreEqual(updatedResult.Password, updatedResult.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void UpdateNonExistingUserThrowsException()
        {
            Guid idUserExample = Guid.NewGuid();
            Guid idUpdate = Guid.NewGuid();
            User example = new User()
            {
                Id = idUserExample,
                Name = "ExampleUser"
            };
            User existingUser = new User
            {
                Id = idUpdate,
                Name = "oldName",
                Email = "test@example.com",
            };

            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { example });

            IUserRepository userRepository = new UserRepository(userContext.Object);

            var updatedResult = userRepository.UpdateUser(existingUser);
        }

    }
}

