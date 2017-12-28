using System;
using System.Collections.Generic;
using System.Text;
using LifeManager.Data.Entities;

namespace LifeManager.Data.Repositories
{
    public interface ICalendarRepository
    {
        void Add(CalendarEvent calendarEvent);
        void Update(CalendarEvent calendarEvent);
        void Delete(CalendarEvent calendarEvent);
        CalendarEvent Get(Guid? id, string name, string locationName, DateTime? startDate, DateTime? endDate,
            IEnumerable<Person> people);
        CalendarEvent GetAll();
    }
}