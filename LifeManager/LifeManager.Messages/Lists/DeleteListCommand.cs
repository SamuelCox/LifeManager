using System;
using NServiceBus;

namespace LifeManager.Messages.Lists
{
    public class DeleteListCommand : ICommand
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
