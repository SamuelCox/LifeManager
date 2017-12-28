using System;
using System.Collections.Generic;
using System.Text;

namespace LifeManager.Data.Entities
{
    public class CalendarEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
    
}
