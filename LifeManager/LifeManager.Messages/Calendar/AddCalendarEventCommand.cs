using NServiceBus;
using System;
using System.Collections.Generic;

namespace LifeManager.Messages.Calendar
{
    public class AddCalendarEventCommand : ICommand
    {        
        public string Name { get; set; }
        public string LocationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<PersonDTO> People { get; set; }
    }
}
