using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using MongoDB.Driver;

namespace LifeManager.Data.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly IMongoDatabase _db;        

        public CalendarRepository(MongoClient mongoClient)
        {            
            _db = mongoClient.GetDatabase("LifeManager");            
        }

        public async Task Add(CalendarEvent calendarEvent)
        {
            var calendarEvents = _db.GetCollection<CalendarEvent>("CalendarEvents");
            await calendarEvents.InsertOneAsync(calendarEvent);
        }

        public async Task Update(CalendarEvent calendarEvent)
        {
            var calendarEvents = _db.GetCollection<CalendarEvent>("CalendarEvents");
            var filter = Builders<CalendarEvent>.Filter.Eq(x => x.Id, calendarEvent.Id);
            await calendarEvents.UpdateOneAsync(filter, new ObjectUpdateDefinition<CalendarEvent>(calendarEvent));
        }

        public async Task Delete(CalendarEvent calendarEvent)
        {
            var calendarEvents = _db.GetCollection<CalendarEvent>("CalendarEvents");
            var filter = Builders<CalendarEvent>.Filter.Eq(x => x.Id, calendarEvent.Id);
            await calendarEvents.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<CalendarEvent>> Get(Guid? id, string name, string locationName, DateTime? startDate, DateTime? endDate)
        {
            var filter = Builders<CalendarEvent>.Filter.Empty;
            if (id.HasValue)
            {
                var idFilter = Builders<CalendarEvent>.Filter.Eq(x => x.Id, id);
                filter = Builders<CalendarEvent>.Filter.And(filter, idFilter);
            }
            if (!string.IsNullOrEmpty(name))
            {
                var nameFilter = Builders<CalendarEvent>.Filter.Eq(x => x.Name, name);
                filter = Builders<CalendarEvent>.Filter.And(filter, nameFilter);
            }
            if (!string.IsNullOrEmpty(locationName))
            {
                var locationFilter = Builders<CalendarEvent>.Filter.Eq(x => x.LocationName, locationName);
                filter = Builders<CalendarEvent>.Filter.And(filter, locationFilter);
            }
            if (startDate.HasValue)
            {
                var startDateFilter = Builders<CalendarEvent>.Filter.Eq(x => x.StartDate, startDate);
                filter = Builders<CalendarEvent>.Filter.And(filter, startDateFilter);
            }
            if (endDate.HasValue)
            {
                var endDateFilter = Builders<CalendarEvent>.Filter.Eq(x => x.EndDate, endDate);
                filter = Builders<CalendarEvent>.Filter.And(filter, endDateFilter);
            }

            var events = await _db.GetCollection<CalendarEvent>("CalendarEvents").FindAsync(filter);

            return events.ToList();
        }

        public async Task<IEnumerable<CalendarEvent>> GetAll()
        {
            var events = await _db.GetCollection<CalendarEvent>("CalendarEvents").FindAsync(Builders<CalendarEvent>.Filter.Empty);
            return events.ToList();
        }


    }
}
