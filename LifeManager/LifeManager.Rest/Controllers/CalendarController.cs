using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeManager.Messages.Calendar;
using LifeManager.Models;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace LifeManager.Rest.Controllers
{
    
    public class CalendarController : Controller
    {
        private readonly IEndpointInstance _endpointInstance;
        private readonly IUserManagerWrapper _userManagerWrapper;        

        public CalendarController(IEndpointInstance endpointInstance, IUserManagerWrapper userManagerWrapper)
        {
            _endpointInstance = endpointInstance;            
            _userManagerWrapper = userManagerWrapper;
        }

        [Authorize]
        [HttpPost("api/Calendar/Add")]
        public async Task<IActionResult> Add([FromBody] CalendarEventModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var addCalendarEventCommand = new AddCalendarEventCommand { Model = model };
            await _endpointInstance.Send("LifeManager.Calendar", addCalendarEventCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/Calendar/Update")]
        public async Task<IActionResult> Update([FromBody] CalendarEventModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var updateCalendarEventCommand = new UpdateCalendarEventCommand {Model = model};
            await _endpointInstance.Send("LifeManager.Calendar", updateCalendarEventCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/Calendar/Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            //var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            //model.UserId = user.Id;
            var deleteCalendarEventCommand = new DeleteCalendarEventCommand{ Id = id};
            await _endpointInstance.Send("LifeManager.Calendar", deleteCalendarEventCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/Calendar/Get")]
        public async Task<IActionResult> Get([FromBody] CalendarEventModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var getCalendarEventCommand = new GetCalendarEventCommand {Model = model};
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("LifeManager.Calendar");            
            var response = await _endpointInstance.Request<GetResponse>(getCalendarEventCommand, sendOptions).ConfigureAwait(false);
            return Ok(new { Response = response.Models });
        }

        [Authorize]
        [HttpGet("api/Calendar/GetAll")]
        public async Task<IActionResult> GetAll()
        {            
            var getAllCalendarEventsCommand = new GetAllCalendarEventsCommand();
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            getAllCalendarEventsCommand.UserId = user.Id;
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("LifeManager.Calendar");            
            var response = await _endpointInstance.Request<GetResponse>(getAllCalendarEventsCommand, sendOptions).ConfigureAwait(false);
            return Ok(new { Response = response.Models });
        }
    }
}