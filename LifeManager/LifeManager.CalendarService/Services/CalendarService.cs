using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.Data.Entities;
using LifeManager.Data.Repositories;
using LifeManager.Models;

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
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
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

        public async Task<IEnumerable<CalendarEventModel>> GetEvent(CalendarEventModel model)
        {
            var entities =  await _repository.Get(model.Id, model.UserId, model.Name, model.LocationName,
                model.StartDate, model.EndDate);
            return Mapper.Map<IEnumerable<CalendarEventModel>>(entities);
        }

        public async Task<IEnumerable<CalendarEventModel>> GetAllEvents(string userId)
        {
            var entities = await _repository.GetAll(userId);
            return Mapper.Map<IEnumerable<CalendarEventModel>>(entities);
        }
    }
}
