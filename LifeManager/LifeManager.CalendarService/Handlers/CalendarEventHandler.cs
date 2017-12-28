using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.CalendarService.Services;
using LifeManager.Messages;
using LifeManager.Messages.Calendar;
using NServiceBus;

namespace LifeManager.CalendarService.Handlers
{
    public class CalendarEventHandler : IHandleMessages<AddCalendarEventCommand>, 
        IHandleMessages<UpdateCalendarEventCommand>,
        IHandleMessages<DeleteCalendarEventCommand>,
        IHandleMessages<GetCalendarEventCommand>,
        IHandleMessages<GetAllCalendarEventsCommand>
    {
        private readonly ICalendarService _calendarService;

        public CalendarEventHandler(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public Task Handle(AddCalendarEventCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(UpdateCalendarEventCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(DeleteCalendarEventCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(GetCalendarEventCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }

        public Task Handle(GetAllCalendarEventsCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
