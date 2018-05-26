using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.People
{
    public class GetPersonCommand : ICommand
    {
        public PersonModel Person { get; set; }
    }
}
