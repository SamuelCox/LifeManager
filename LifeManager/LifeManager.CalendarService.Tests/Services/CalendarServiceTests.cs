using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.CalendarService.Models;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.Messages.Calendar;
using Moq;
using NUnit.Framework;

namespace LifeManager.CalendarService.Tests.Services
{
    [TestFixture]
    public class CalendarServiceTests
    {
        private Mock<ICalendarRepository> _mockCalendarRepository;

        [SetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalendarEvent, CalendarEventModel>().ReverseMap();
                cfg.CreateMap<PersonDTO, Person>().ReverseMap();
            });
            _mockCalendarRepository = new Mock<ICalendarRepository>();
        }

        [Test]
        public async Task CreateEvent_ShouldCallRepositoryAdd()
        {
            //Data
            var calendarEventModel = new CalendarEventModel { Id = Guid.NewGuid()};

            //Setup
            _mockCalendarRepository.Setup(x => x.Add(It.IsAny<CalendarEvent>())).Returns(Task.CompletedTask).Verifiable();

            //Test
            var calendarService = new CalendarService.Services.CalendarService(_mockCalendarRepository.Object);
            await calendarService.CreateEvent(calendarEventModel);

            //Analysis
            _mockCalendarRepository.Verify();
        }

        [Test]
        public async Task UpdateEvent_ShouldCallRepositoryUpdate()
        {
            //Data
            var calendarEventModel = new CalendarEventModel { Id = Guid.NewGuid() };

            //Setup
            _mockCalendarRepository.Setup(x => x.Update(It.IsAny<CalendarEvent>())).Returns(Task.CompletedTask).Verifiable();

            //Test
            var calendarService = new CalendarService.Services.CalendarService(_mockCalendarRepository.Object);
            await calendarService.UpdateEvent(calendarEventModel);

            //Analysis
            _mockCalendarRepository.Verify();
        }

        [Test]
        public async Task DeleteEvent_ShouldCallRepositoryDelete()
        {
            //Data
            var id = Guid.NewGuid();

            //Setup
            _mockCalendarRepository.Setup(x => x.Delete(It.Is<Guid>(y => y == id))).Returns(Task.CompletedTask).Verifiable();

            //Test
            var calendarService = new CalendarService.Services.CalendarService(_mockCalendarRepository.Object);
            await calendarService.DeleteEvent(id);

            //Analysis
            _mockCalendarRepository.Verify();
        }

        [Test]
        public async Task GetEvent_ShouldReturnEvent()
        {
            //Data
            var calendarEventModel = new CalendarEventModel { Id = Guid.NewGuid() };
            IEnumerable<CalendarEvent> calendarEvents = new[]
            {
                new CalendarEvent{ Id = calendarEventModel.Id}
            }.ToList();

            //Setup
            _mockCalendarRepository.Setup(x => x.Get(calendarEventModel.Id, null, null, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(calendarEvents)).Verifiable();

            //Test
            var calendarService = new CalendarService.Services.CalendarService(_mockCalendarRepository.Object);
            var result = await calendarService.GetEvent(calendarEventModel);

            //Analysis
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(calendarEventModel.Id, result.First().Id);
            _mockCalendarRepository.Verify();
        }

        [Test]
        public async Task GetAllEvents_ShouldReturnEvents()
        {
            //Data
            
            IEnumerable<CalendarEvent> calendarEvents = new[]
            {
                new CalendarEvent{ Id = Guid.NewGuid()},
                new CalendarEvent{ Id = Guid.NewGuid()}
            }.ToList();

            //Setup
            _mockCalendarRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult(calendarEvents)).Verifiable();

            //Test
            var calendarService = new CalendarService.Services.CalendarService(_mockCalendarRepository.Object);
            var result = await calendarService.GetAllEvents();

            //Analysis
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(calendarEvents.First().Id, result.First().Id);
            _mockCalendarRepository.Verify();
        }
    }
}
