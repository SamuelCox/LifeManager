using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class GetCalendarEventCommand : ICommand
    {
        public string Name { get; set; }
        public string LocationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<PersonDTO> People { get; set; }
    }
}
