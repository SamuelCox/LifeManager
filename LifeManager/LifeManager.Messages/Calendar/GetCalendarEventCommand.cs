using System.Diagnostics.CodeAnalysis;
using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class GetCalendarEventCommand : ICommand
    {
        public CalendarEventModel Model { get; set; }
    }
}
