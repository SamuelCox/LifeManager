﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using LifeManager.Models;
using NServiceBus;

namespace LifeManager.Messages.Calendar
{
    [ExcludeFromCodeCoverage]
    public class GetCalendarEventCommand : ICommand
    {
        public CalendarEventModel Model { get; set; }
    }
}
