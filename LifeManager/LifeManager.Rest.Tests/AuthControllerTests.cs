using System;
using System.Threading;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using LifeManager.Rest.Controllers;
using LifeManager.Rest.Models;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LifeManager.Rest.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IUserManagerWrapper> _mockUserManager;        

        [SetUp]
        public void SetUp()
        {
            //Data
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserManager = new Mock<IUserManagerWrapper>();

            //Setup
            _mockConfiguration.Setup(x => x["Jwt:Key"]).Returns("VeryLongKeyVeryLongKey");
            


            
        }

        [Test]
        public async Task Register_ShouldReturnBadRequestIfEmptyUsername()
        {
            //Data
            var userModel = new UserModel {Password = "blah", UserName = string.Empty};

            //Setup            

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
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
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
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
            _mockUserManager.Setup(x => x.CreateAsync(It.Is<User>(y => y.UserName == userModel.UserName), It.Is<string>(y => y == userModel.Password)))
                .Returns(Task.FromResult(IdentityResult.Success))
                .Verifiable();

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Register(userModel) as OkObjectResult;

            //Analysis            
            _mockUserManager.Verify();
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var innerObject = result.Value;
            var token = innerObject.GetType().GetProperty("token").GetValue(innerObject);
            Assert.IsNotEmpty((string)token);
            
        }

        [Test]
        public async Task Register_ShouldReturnBadRequestIfUserCreationFailed()
        {
            //Data
            var userModel = new UserModel { Password = "blah", UserName = "password" };

            //Setup            
            _mockUserManager.Setup(x => x.FindByNameAsync(userModel.UserName)).Returns(Task.FromResult<User>(null)).Verifiable();
            _mockUserManager.Setup(x => x.CreateAsync(It.Is<User>(y => y.UserName == userModel.UserName), It.Is<string>(y => y == userModel.Password)))
                .Returns(Task.FromResult(IdentityResult.Failed()))
                .Verifiable();

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Register(userModel) as BadRequestResult;

            //Analysis            
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);            
        }

        [Test]
        public async Task Register_ShouldReturnBadRequestIfUserExists()
        {
            //Data
            var userModel = new UserModel { Password = "blah", UserName = "password" };

            //Setup
            _mockUserManager.Setup(x => x.FindByNameAsync(userModel.UserName)).Returns(Task.FromResult(new User())).Verifiable();            

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Register(userModel) as BadRequestResult;

            //Analysis
            _mockUserManager.Verify();
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Authenticate_ShouldReturnBadRequestIfEmptyUserName()
        {
            //Data
            var userModel = new UserModel { Password = "blah", UserName = string.Empty };

            //Setup            
            

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Authenticate(userModel) as BadRequestResult;

            //Analysis            
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Authenticate_ShouldReturnBadRequestIfFindByNameAsyncFails()
        {
            //Data
            var userModel = new UserModel { Password = "blah", UserName = "user" };

            //Setup            
            _mockUserManager.Setup(x => x.FindByNameAsync(userModel.UserName)).Returns(Task.FromResult<User>(null)).Verifiable();

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Authenticate(userModel) as BadRequestResult;

            //Analysis
            _mockUserManager.Verify();
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Authenticate_ShouldReturnBadRequestIfCheckPasswordAsyncFails()
        {
            //Data
            var userModel = new UserModel { Password = "blah", UserName = "user" };

            //Setup            
            _mockUserManager.Setup(x => x.FindByNameAsync(userModel.UserName))
                .Returns(Task.FromResult<User>(new User{ UserName = userModel.UserName})).Verifiable();
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.Is<User>(y => y.UserName == userModel.UserName), It.Is<string>(y => y == userModel.Password)))
                .Returns(Task.FromResult(false)).Verifiable();

            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Authenticate(userModel) as BadRequestResult;

            //Analysis
            _mockUserManager.Verify();
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public async Task Authenticate_ShouldReturnOkIfGoodCredentials()
        {
            //Data
            var userModel = new UserModel { Password = "password", UserName = "user" };                                  


            //Setup                        
            _mockUserManager.Setup(x => x.FindByNameAsync(userModel.UserName))
                .Returns(Task.FromResult<User>(new User { UserName = userModel.UserName })).Verifiable();
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.Is<User>(y => y.UserName == userModel.UserName), It.Is<string>(y => y == userModel.Password)))
                .Returns(Task.FromResult(true)).Verifiable();


            //Test
            var authController = new AuthController(_mockConfiguration.Object, _mockUserManager.Object);
            var result = await authController.Authenticate(userModel) as OkObjectResult;

            //Analysis            
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var innerObject = result.Value;
            var token = innerObject.GetType().GetProperty("token").GetValue(innerObject);
            Assert.IsNotEmpty((string)token);
        }
    }
}
