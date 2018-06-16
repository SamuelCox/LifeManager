using System;
using System.Security.Principal;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using LifeManager.Messages.Lists;
using LifeManager.Models;
using LifeManager.Rest.Controllers;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace LifeManager.Rest.Tests
{
    [TestFixture]
    public class ListControllerTests
    {
        private Mock<IEndpointInstance> _mockEndpointInstance;
        private Mock<IUserManagerWrapper> _mockUserManagerWrapper;
        private Mock<HttpContext> _httpContext;

        [SetUp]
        public void SetUp()
        {
            _mockEndpointInstance = new Mock<IEndpointInstance>();
            _mockUserManagerWrapper = new Mock<IUserManagerWrapper>();
            _httpContext = new Mock<HttpContext>();
            var user = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            _httpContext.SetupGet(x => x.User).Returns(user);
        }

        [Test]
        public async Task Add_ShouldSendToEndpointAndReturnOk()
        {
            //Data
            var model = new ListModel
            {
                Id = Guid.Empty,
                Name = "test"
            };

            //Setup
            _mockEndpointInstance.Setup(x => x.Send(It.Is<AddListCommand>(y => y.Model.Id == model.Id 
                                        && y.Model.Name == model.Name && y.Model.Items == model.Items), 
                                        It.IsAny<SendOptions>())).Returns(Task.CompletedTask).Verifiable();
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(new User()));
            var listController = new ListController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            listController.ControllerContext = controllerContext;

            //Test
            var result = await listController.Add(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Add_ShouldGetUserNameAndSetIdOnModel()
        {
            //Data
            var model = new ListModel
            {
                Id = Guid.Empty,
                Name = "test"
            };

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { Id = "test" })).Verifiable();
            _mockEndpointInstance.Setup(x => x.Send(It.Is<AddListCommand>(y => y.Model.UserId == "test"), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            var listController = new ListController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            listController.ControllerContext = controllerContext;

            //Test
            var result = await listController.Add(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockUserManagerWrapper.Verify();
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Update_ShouldSendToEndpointAndReturnOk()
        {
            //Data
            var model = new ListModel
            {
                Id = Guid.Empty,
                Name = "test"
            };

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User {Id = "test"}));
            _mockEndpointInstance.Setup(x => x.Send(It.Is<UpdateListCommand>(y => y.Model.Id == model.Id), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            var listController = new ListController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            listController.ControllerContext = controllerContext;

            //Test
            var result = await listController.Update(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis            
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Update_ShouldGetUserNameAndSetIdOnModel()
        {
            //Data
            var model = new ListModel
            {
                Id = Guid.Empty,
                Name = "test"
            };

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { Id = "test" })).Verifiable();
            _mockEndpointInstance.Setup(x => x.Send(It.Is<UpdateListCommand>(y => y.Model.UserId == "test"), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            var listController = new ListController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            listController.ControllerContext = controllerContext;

            //Test
            var result = await listController.Update(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockUserManagerWrapper.Verify();
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Delete_ShouldSendToEndpointAndReturnOk()
        {
            //Data
            var guid = Guid.NewGuid();

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { Id = "test" }));
            _mockEndpointInstance.Setup(x => x.Send(It.Is<DeleteListCommand>(y => y.Id == guid), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            var listController = new ListController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            listController.ControllerContext = controllerContext;

            //Test
            var result = await listController.Delete(guid) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis            
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Delete_ShouldGetUserNameAndIncludeInMessage()
        {
            //Data
            var guid = Guid.NewGuid();

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { Id = "test" })).Verifiable();
            _mockEndpointInstance.Setup(x => x.Send(It.Is<DeleteListCommand>(y => y.Id == guid && y.UserId == "test"), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            var listController = new ListController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            listController.ControllerContext = controllerContext;

            //Test
            var result = await listController.Delete(guid) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockUserManagerWrapper.Verify();
            _mockEndpointInstance.Verify();
        }
    }
}
