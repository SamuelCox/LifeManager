using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using LifeManager.Messages.Calendar;
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
    public class CalendarControllerTests
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
            var model = new CalendarEventModel
            {
                Id = Guid.Empty,
                Name = "test",
                LocationName = "testname",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            //Setup
            _mockEndpointInstance.Setup(x => x.Send(It.Is<AddCalendarEventCommand>(y => 
                    y.Model.Name == model.Name && y.Model.LocationName == model.LocationName &&
                    y.Model.StartDate == model.StartDate && y.Model.EndDate == model.EndDate), It.IsAny<SendOptions>()))
                    .Returns(Task.CompletedTask).Verifiable();
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(new User()));            
            var calendarController = new CalendarController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            calendarController.ControllerContext = controllerContext;

            //Test
            var result = await calendarController.Add(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Add_ShouldGetUserByNameAndSetIdOnModel()
        {
            //Data
            var model = new CalendarEventModel
            {
                Id = Guid.Empty,
                Name = "test",
                LocationName = "testname",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User{ Id = "test"})).Verifiable();
            _mockEndpointInstance.Setup(x => x.Send(It.Is<AddCalendarEventCommand>(y => y.Model.UserId == "test"), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();            
            var calendarController = new CalendarController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            calendarController.ControllerContext = controllerContext;

            //Test
            var result = await calendarController.Add(model) as OkResult;
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
            var model = new CalendarEventModel
            {
                Id = Guid.Empty,
                Name = "test",
                LocationName = "testname",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            //Setup
            _mockEndpointInstance.Setup(x => x.Send(It.Is<UpdateCalendarEventCommand>(y =>
                    y.Model.Name == model.Name && y.Model.LocationName == model.LocationName &&
                    y.Model.StartDate == model.StartDate && y.Model.EndDate == model.EndDate), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(new User()));
            var calendarController = new CalendarController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            calendarController.ControllerContext = controllerContext;

            //Test
            var result = await calendarController.Update(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockEndpointInstance.Verify();
        }

        [Test]
        public async Task Update_ShouldGetUserByNameAndSetIdOnModel()
        {
            //Data
            var model = new CalendarEventModel
            {
                Id = Guid.Empty,
                Name = "test",
                LocationName = "testname",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            //Setup
            _mockUserManagerWrapper.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { Id = "test" })).Verifiable();
            _mockEndpointInstance.Setup(x => x.Send(It.Is<UpdateCalendarEventCommand>(y => y.Model.UserId == "test"), It.IsAny<SendOptions>()))
                .Returns(Task.CompletedTask).Verifiable();
            var calendarController = new CalendarController(_mockEndpointInstance.Object, _mockUserManagerWrapper.Object);
            var controllerContext = new ControllerContext
            {
                HttpContext = _httpContext.Object
            };
            calendarController.ControllerContext = controllerContext;

            //Test
            var result = await calendarController.Update(model) as OkResult;
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            //Analysis
            _mockUserManagerWrapper.Verify();
            _mockEndpointInstance.Verify();
        }
        
    }
}
