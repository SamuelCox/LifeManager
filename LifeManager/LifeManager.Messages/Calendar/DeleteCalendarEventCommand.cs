using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    public class DeleteCalendarEventCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
