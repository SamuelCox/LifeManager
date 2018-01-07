using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.CalendarService.Models;
using LifeManager.Data.Entities;

namespace LifeManager.CalendarService.Services
{
    public interface ICalendarService
    {
        Task CreateEvent(CalendarEventModel model);
        Task UpdateEvent(CalendarEventModel model);
        Task DeleteEvent(CalendarEventModel model);
        Task<IEnumerable<CalendarEvent>> GetEvent(CalendarEventModel model);
        Task<IEnumerable<CalendarEvent>> GetAllEvents();
    }
}
