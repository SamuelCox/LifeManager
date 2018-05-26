using NServiceBus;

namespace LifeManager.Messages.People
{
    public class GetAllPeopleCommand : ICommand
    {
        public string UserId { get; set; }
    }
}
