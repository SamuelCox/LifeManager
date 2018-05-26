using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Lists
{
    public class GetListCommand : ICommand
    {
        public ListModel Model { get; set; }
    }
}
