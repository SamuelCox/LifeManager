using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.CalendarService.Handlers;
using LifeManager.CalendarService.Models;
using LifeManager.CalendarService.Services;
using LifeManager.Messages.Calendar;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace LifeManager.CalendarService.Tests.Handlers
{
    public class CalendarEventHandlerTests
    {
        private Mock<IMessageHandlerContext> _mockMessageHandlerContext;
        private Mock<ICalendarService> _mockCalendarService;

        [SetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AddCalendarEventCommand, CalendarEventModel>().ReverseMap();
                cfg.CreateMap<UpdateCalendarEventCommand, CalendarEventModel>().ReverseMap();
            });
            _mockMessageHandlerContext = new Mock<IMessageHandlerContext>();
            _mockCalendarService = new Mock<ICalendarService>();
        }

        [Test]
        public async Task HandleAdd_ShouldCallAddOnService()
        {
            // Data
            var addCalendarEventCommand = new AddCalendarEventCommand();

            // Setup
            _mockCalendarService.Setup(x => x.CreateEvent(It.IsAny<CalendarEventModel>())).Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new CalendarEventHandler(_mockCalendarService.Object);
            await handler.Handle(addCalendarEventCommand, _mockMessageHandlerContext.Object);

            // Analysis
            _mockCalendarService.Verify();
        }

        [Test]
        public async Task HandleUpdate_ShouldCallUpdateOnService()
        {
            // Data
            var updateCalendarEventCommand = new UpdateCalendarEventCommand();

            // Setup
            _mockCalendarService.Setup(x => x.UpdateEvent(It.IsAny<CalendarEventModel>())).Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new CalendarEventHandler(_mockCalendarService.Object);
            await handler.Handle(updateCalendarEventCommand, _mockMessageHandlerContext.Object);

            // Analysis
            _mockCalendarService.Verify();
        }

        [Test]
        public async Task HandleDelete_ShouldCallDeleteOnService()
        {
            // Data
            var deleteCalendarEventCommand = new DeleteCalendarEventCommand { Id = Guid.NewGuid() };

            // Setup
            _mockCalendarService.Setup(x => x.DeleteEvent(It.Is<Guid>(y => y == deleteCalendarEventCommand.Id)))
                .Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new CalendarEventHandler(_mockCalendarService.Object);
            await handler.Handle(deleteCalendarEventCommand, _mockMessageHandlerContext.Object);

            // Analysis
            _mockCalendarService.Verify();
        }
    }
}
