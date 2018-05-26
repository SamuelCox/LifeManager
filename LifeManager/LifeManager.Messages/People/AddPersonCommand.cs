using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.People
{
    public class AddPersonCommand : ICommand
    {
        public PersonModel Person { get; set; }
    }
}
