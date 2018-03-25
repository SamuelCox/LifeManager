using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace LifeManager.Messages.Lists
{
    public class GetAllListsCommand : ICommand
    {
        public string UserId { get; set; }
    }
}
