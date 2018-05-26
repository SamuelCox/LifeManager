using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    public class GetAllCalendarEventsCommand : ICommand
    {
        public string UserId { get; set; }
    }
}
