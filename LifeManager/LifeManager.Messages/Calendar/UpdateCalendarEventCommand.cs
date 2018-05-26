using System.Diagnostics.CodeAnalysis;
using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class UpdateCalendarEventCommand : ICommand
    {
        public CalendarEventModel Model { get; set; }
    }
}
