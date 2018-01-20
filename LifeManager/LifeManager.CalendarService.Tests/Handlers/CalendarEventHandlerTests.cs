using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.CalendarService.Handlers;
using LifeManager.CalendarService.Services;
using LifeManager.Data.Entities;
using LifeManager.Messages.Calendar;
using LifeManager.Models;
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
            _mockMessageHandlerContext = new Mock<IMessageHandlerContext>();
            _mockCalendarService = new Mock<ICalendarService>();
        }

        [Test]
        public async Task HandleAdd_ShouldCallAddOnService()
        {
            // Data
            var addCalendarEventCommand = new AddCalendarEventCommand{Model = new CalendarEventModel()};

            // Setup
            _mockCalendarService.Setup(x => x.CreateEvent(It.Is<CalendarEventModel>(y => y == addCalendarEventCommand.Model)))
                .Returns(Task.CompletedTask).Verifiable();

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
            var updateCalendarEventCommand = new UpdateCalendarEventCommand{Model = new CalendarEventModel()};

            // Setup
            _mockCalendarService.Setup(x => x.UpdateEvent(It.Is<CalendarEventModel>(y => y == updateCalendarEventCommand.Model)))
                .Returns(Task.CompletedTask).Verifiable();

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

        [Test]
        public async Task HandleGet_ShouldCallGetOnServiceAndReplyOnContext()
        {
            // Data
            var calendarEventModel = new CalendarEventModel {Name = "test_name"};
            var getCalendarEventCommand = new GetCalendarEventCommand{ Model = calendarEventModel};
            IEnumerable<CalendarEventModel> calendarEvents = new List<CalendarEventModel>
            {
                calendarEventModel
            };     
            
            //Setup
            _mockCalendarService.Setup(x => x.GetEvent(It.Is<CalendarEventModel>(y => y.Name == getCalendarEventCommand.Model.Name)))
                .Returns(Task.FromResult(calendarEvents)).Verifiable();
            _mockMessageHandlerContext.Setup(x => x.Reply(calendarEvents, It.IsAny<ReplyOptions>()))
                .Returns(Task.CompletedTask).Verifiable();

            //Test
            var handler = new CalendarEventHandler(_mockCalendarService.Object);
            await handler.Handle(getCalendarEventCommand, _mockMessageHandlerContext.Object);

            //Analysis
            _mockCalendarService.Verify();
            _mockMessageHandlerContext.Verify();
        }

        [Test]
        public async Task HandleGetAll_ShouldCallGetAll()
        {
            //Data
            var getAllCommand = new GetAllCalendarEventsCommand();
            IEnumerable<CalendarEventModel> calendarEvents = new List<CalendarEventModel>
            {
                new CalendarEventModel{ Name = "test_name"}
            };

            //Setup
            _mockCalendarService.Setup(x => x.GetAllEvents()).Returns(Task.FromResult(calendarEvents))
                .Verifiable();
            _mockMessageHandlerContext.Setup(x => x.Reply(calendarEvents, It.IsAny<ReplyOptions>()))
                .Returns(Task.CompletedTask).Verifiable();

            //Test
            var handler = new CalendarEventHandler(_mockCalendarService.Object);
            await handler.Handle(getAllCommand, _mockMessageHandlerContext.Object);

            //Analysis
            _mockCalendarService.Verify();
            _mockMessageHandlerContext.Verify();
        }


    }
}
