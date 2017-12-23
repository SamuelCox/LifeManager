using System;
using System.Threading;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using LifeManager.Rest.Controllers;
using LifeManager.Rest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LifeManager.Rest.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private UserManager<User> _mockUserManager;

        [SetUp]
        public void SetUp()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            var userStore = new Mock<IUserStore<User>>();
            var mockUser = new User {UserName = "user"};
            userStore.As<IUserPasswordStore<User>>().Setup(x => x.FindByNameAsync("user", It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockUser));
            userStore.As<IUserPasswordStore<User>>().Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new IdentityResult()));

            _mockUserManager = new UserManager<User>(userStore.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task Register_ShouldReturnBadRequestIfEmptyUsername()
        {
            //Data
            var userModel = new UserModel {Password = "blah", UserName = string.Empty};

            //Setup            

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager);
            var result =  await authController.Register(userModel) as BadRequestResult;

            //Analysis            
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Register_ShouldReturnBadRequestIfEmptyPassword()
        {
            //Data
            var userModel = new UserModel { Password = string.Empty, UserName = "user" };

            //Setup            

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager);
            var result = await authController.Register(userModel) as BadRequestResult;

            //Analysis            
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Register_ShouldReturnOkIfUserCreationSucceeded()
        {
            //Data
            var userModel = new UserModel { Password = "blah", UserName = "password" };

            //Setup            


            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager);
            var result = await authController.Register(userModel) as OkResult;

            //Analysis            
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void Register_ShouldReturnBadRequestIfUserCreationFailed()
        {

        }
    }
}
