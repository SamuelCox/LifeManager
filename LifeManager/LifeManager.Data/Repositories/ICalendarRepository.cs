﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeManager.Data.Entities;

namespace LifeManager.Data.Repositories
{
    public interface ICalendarRepository
    {
        Task Add(CalendarEvent calendarEvent);
        Task Update(CalendarEvent calendarEvent);
        Task Delete(Guid id, string userId);
        Task<IEnumerable<CalendarEvent>> Get(Guid? id, string userId, string name, string locationName, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<CalendarEvent>> GetAll(string userId);
    }
}