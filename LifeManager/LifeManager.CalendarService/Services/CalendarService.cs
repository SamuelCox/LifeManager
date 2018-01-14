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
            var calendarEvent = Mapper.Map<CalendarEvent>(model);
            await _repository.Update(calendarEvent);
        }

        public async Task DeleteEvent(Guid id)
        {            
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<CalendarEvent>> GetEvent(CalendarEventModel model)
        {
            return await _repository.Get(model.Id, model.Name, model.LocationName,
                model.StartDate, model.EndDate);
        }

        public async Task<IEnumerable<CalendarEvent>> GetAllEvents()
        {
            return await _repository.GetAll();
        }
    }
}
