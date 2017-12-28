using System;
using System.Collections.Generic;
using System.Text;
using LifeManager.Data.Entities;
using MongoDB.Driver;

namespace LifeManager.Data.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly MongoClient _mongoClient;

        public CalendarRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public void Add(CalendarEvent calendarEvent)
        {
            throw new NotImplementedException();
        }

        public void Update(CalendarEvent calendarEvent)
        {
            throw new NotImplementedException();
        }

        public void Delete(CalendarEvent calendarEvent)
        {
            throw new NotImplementedException();
        }

        public CalendarEvent Get(Guid? id, string name, string locationName, DateTime? startDate, DateTime? endDate,
            IEnumerable<Person> people)
        {            
            throw new NotImplementedException();
        }

        public CalendarEvent GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
