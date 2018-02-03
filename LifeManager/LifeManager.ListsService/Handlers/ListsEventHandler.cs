using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        public async Task Handle(AddListCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public async Task Handle(UpdateListCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public async Task Handle(DeleteListCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public async Task Handle(GetListCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public async Task Handle(GetAllListsCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
