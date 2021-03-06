﻿using NServiceBus;
using System.Diagnostics.CodeAnalysis;
using LifeManager.Models;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class AddCalendarEventCommand : ICommand
    {        
        public CalendarEventModel Model { get; set; }
    }
}
