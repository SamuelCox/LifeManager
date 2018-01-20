using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class DeleteCalendarEventCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
