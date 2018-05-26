using System;
using System.Threading.Tasks;
using LifeManager.Messages.People;
using LifeManager.PeopleService.Services;
using NServiceBus;

namespace LifeManager.PeopleService.Handlers
{
    public class PeopleEventHandler : IHandleMessages<AddPersonCommand>, 
        IHandleMessages<UpdatePersonCommand>,
        IHandleMessages<DeletePersonCommand>, 
        IHandleMessages<GetPersonCommand>, 
        IHandleMessages<GetAllPeopleCommand>
    {
        private readonly IPeopleService _peopleService;

        public PeopleEventHandler(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task Handle(AddPersonCommand message, IMessageHandlerContext context)
        {
            await _peopleService.CreatePerson(message.Person);
        }

        public async Task Handle(UpdatePersonCommand message, IMessageHandlerContext context)
        {
            await _peopleService.UpdatePerson(message.Person);
        }

        public async Task Handle(DeletePersonCommand message, IMessageHandlerContext context)
        {
            await _peopleService.DeletePerson(message.Id, message.UserId);
        }

        public async Task Handle(GetPersonCommand message, IMessageHandlerContext context)
        {
            var people = await _peopleService.GetPerson(message.Person);
            await context.Reply(new GetResponse { People = people });
        }

        public async Task Handle(GetAllPeopleCommand message, IMessageHandlerContext context)
        {
            var people = await _peopleService.GetAllPeople(message.UserId);
            await context.Reply(new GetResponse { People = people });
        }
    }
}
