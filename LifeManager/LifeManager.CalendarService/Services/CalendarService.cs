using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.CalendarService.Models;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;

namespace LifeManager.CalendarService.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _repository;

        public CalendarService(ICalendarRepository repo)
        {
            _repository = repo;
        }

        public async Task CreateEvent(CalendarEventModel model)
        {
            var calendarEvent = Mapper.Map<CalendarEvent>(model);
            await _repository.Add(calendarEvent);
        }

        public async Task UpdateEvent(CalendarEventModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEvent(CalendarEventModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalendarEvent>> GetEvent(CalendarEventModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CalendarEvent>> GetAllEvents()
        {
            throw new NotImplementedException();
        }
    }
}
