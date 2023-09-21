using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace UnitTest.WebApiModelsTest.Controllers
{
    [TestClass]
    public class UserControllerTest
    {

        [TestMethod]
        public void GetAllUsersOk()
        {
            List<User> usersSample = new()
            {
                new User {Email= "mail1@sample.com",Name="name1",Password="password1" },
                new User {Email= "mail2@sample.com",Name="name2",Password="password2" },
                new User {Email= "mail3@sample.com",Name="name3",Password="password3" },
            };

            Mock<IUserLogic> mock = new();
            mock.Setup(u => u.GetUsers()).Returns(usersSample);

            UserController userController = new(mock.Object);
            var result = userController.GetAllUsers().Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(usersSample, result.Value);
        }
    }
}
