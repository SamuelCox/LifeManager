using System;
using System.Collections.Generic;
using System.Text;
using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    public class GetResponse : IMessage
    {
        public IEnumerable<CalendarEventModel> Models { get; set; }
    }
}
