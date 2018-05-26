using System;
using NServiceBus;

namespace LifeManager.Messages.People
{
    public class DeletePersonCommand : ICommand
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
