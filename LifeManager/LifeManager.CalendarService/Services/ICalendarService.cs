using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LifeManager.Models;

namespace LifeManager.CalendarService.Services
{
    public interface ICalendarService
    {
        Task CreateEvent(CalendarEventModel model);
        Task UpdateEvent(CalendarEventModel model);
        Task DeleteEvent(Guid id, string userId);
        Task<IEnumerable<CalendarEventModel>> GetEvent(CalendarEventModel model);
        Task<IEnumerable<CalendarEventModel>> GetAllEvents(string userId);
    }
}
