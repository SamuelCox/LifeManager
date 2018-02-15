using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task Handle(AddCalendarEventCommand message, IMessageHandlerContext context)
        {            
            await _calendarService.CreateEvent(message.Model);
        }

        public async Task Handle(UpdateCalendarEventCommand message, IMessageHandlerContext context)
        {           
            await _calendarService.UpdateEvent(message.Model);
        }

        public async Task Handle(DeleteCalendarEventCommand message, IMessageHandlerContext context)
        {            
            await _calendarService.DeleteEvent(message.Id);
        }

        public async Task Handle(GetCalendarEventCommand message, IMessageHandlerContext context)
        {            
            var events = await _calendarService.GetEvent(message.Model);
            await context.Reply(new GetResponse { Models = events });
        }

        public async Task Handle(GetAllCalendarEventsCommand message, IMessageHandlerContext context)
        {            
            var events = await _calendarService.GetAllEvents(message.UserId);
            await context.Reply(new GetResponse{ Models = events });
        }
    }
}
