using NServiceBus;

namespace LifeManager.Messages.Lists
{
    public class GetAllListsCommand : ICommand
    {
        public string UserId { get; set; }
    }
}
