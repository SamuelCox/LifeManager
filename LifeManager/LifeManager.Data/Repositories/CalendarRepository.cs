﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeManager.Data.Entities;
using MongoDB.Driver;

namespace LifeManager.Data.Repositories
{
    public class CalendarRepository : MongoRepository<CalendarEvent>, ICalendarRepository
    {               

        public CalendarRepository(IMongoDatabase database) : base(database, "calendarevents")
        {
        }        

        public async Task<IEnumerable<CalendarEvent>> Get(Guid? id, string userId, string name, string locationName, DateTime? startDate, DateTime? endDate)
        {
            var filter = Builders<CalendarEvent>.Filter.Empty;
            if (id.HasValue)
            {
                var idFilter = Builders<CalendarEvent>.Filter.Eq(x => x.Id, id);
                filter = Builders<CalendarEvent>.Filter.And(filter, idFilter);
            }

            var userIdFilter = Builders<CalendarEvent>.Filter.Eq(x => x.UserId, userId);
            filter = Builders<CalendarEvent>.Filter.And(filter, userIdFilter);

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

            var events = await _db.GetCollection<CalendarEvent>("calendarevents").FindAsync(filter);

            return events.ToList();
        }       
    }
}
