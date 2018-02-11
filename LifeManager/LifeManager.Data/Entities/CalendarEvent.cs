using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace LifeManager.Data.Entities
{
    [ExcludeFromCodeCoverage]
    public class CalendarEvent : IMongoEntity
    {
        [BsonElement("Id")]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("LocationName")]
        public string LocationName { get; set; }

        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("EndDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("People")]
        public IEnumerable<Person> People { get; set; }
    }
    
}
