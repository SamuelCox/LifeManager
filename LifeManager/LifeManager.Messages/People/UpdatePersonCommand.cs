using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.People
{
    public class UpdatePersonCommand : ICommand
    {
        public PersonModel Person { get; set; }
    }
}
