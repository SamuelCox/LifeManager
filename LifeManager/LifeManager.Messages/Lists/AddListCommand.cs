using System;
using System.Collections.Generic;
using System.Text;
using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Lists
{
    public class AddListCommand : ICommand
    {
        public ListModel Model { get; set; }
    }
}
