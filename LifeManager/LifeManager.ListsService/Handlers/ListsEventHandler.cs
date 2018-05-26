using System.Threading.Tasks;
using LifeManager.ListsService.Services;
using LifeManager.Messages.Lists;
using NServiceBus;

namespace LifeManager.ListsService.Handlers
{
    public class ListsEventHandler : IHandleMessages<AddListCommand>,
        IHandleMessages<UpdateListCommand>,
        IHandleMessages<DeleteListCommand>,
        IHandleMessages<GetListCommand>,
        IHandleMessages<GetAllListsCommand>
    {
        private readonly IListService _listService;

        public ListsEventHandler(IListService listService)
        {
            _listService = listService;
        }

        public async Task Handle(AddListCommand message, IMessageHandlerContext context)
        {
            await _listService.CreateList(message.Model);
        }

        public async Task Handle(UpdateListCommand message, IMessageHandlerContext context)
        {
            await _listService.UpdateList(message.Model);
        }

        public async Task Handle(DeleteListCommand message, IMessageHandlerContext context)
        {
            await _listService.DeleteList(message.Id, message.UserId);
        }

        public async Task Handle(GetListCommand message, IMessageHandlerContext context)
        {
            var lists = await _listService.GetList(message.Model);
            await context.Reply(new GetResponse { Models = lists});
        }

        public async Task Handle(GetAllListsCommand message, IMessageHandlerContext context)
        {
            var lists = await _listService.GetAllLists(message.UserId);
            await context.Reply(new GetResponse { Models = lists});
        }
    }
}
