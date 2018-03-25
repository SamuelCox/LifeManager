using System;
using System.Collections.Generic;
using System.Text;
using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Lists
{
    public class GetResponse : IMessage
    {
        public IEnumerable<ListModel> Models { get; set; }
    }
}
