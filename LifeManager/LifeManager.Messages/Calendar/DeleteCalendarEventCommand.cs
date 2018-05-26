using System;
using System.Diagnostics.CodeAnalysis;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class DeleteCalendarEventCommand : ICommand
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
