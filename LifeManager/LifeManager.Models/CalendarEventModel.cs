using System;
using System.Collections.Generic;

namespace LifeManager.Models
{
    public class CalendarEventModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<PersonModel> People { get; set; }
    }
}