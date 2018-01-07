using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.CalendarService.Models;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace LifeManager.CalendarService.Tests
{
    [TestFixture]
    public class CalendarServiceTests
    {
        private Mock<ICalendarRepository> _mockCalendarRepository;

        [SetUp]
        public void SetUp()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CalendarEvent, CalendarEventModel>().ReverseMap());
            _mockCalendarRepository = new Mock<ICalendarRepository>();
        }

        [Test]
        public async Task CreateEvent_ShouldCallRepositoryAdd()
        {
            //Data
            var calendarEventModel = new CalendarEventModel { Id = Guid.NewGuid() };

            //Setup
            _mockCalendarRepository.Setup(x => x.Add(It.IsAny<CalendarEvent>())).Verifiable();

            //Test
            var calendarService = new CalendarService.Services.CalendarService(_mockCalendarRepository.Object);
            await calendarService.CreateEvent(calendarEventModel);

            //Analysis
            _mockCalendarRepository.Verify();
        }
    }
}
