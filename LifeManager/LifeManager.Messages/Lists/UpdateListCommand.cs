using LifeManager.Models;
using NServiceBus;


namespace LifeManager.Messages.Lists
{
    public class UpdateListCommand : ICommand
    {
        public ListModel Model { get; set; }
    }
}
