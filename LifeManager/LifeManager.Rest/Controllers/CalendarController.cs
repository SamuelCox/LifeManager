using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace LifeManager.Rest.Controllers
{
    [Produces("application/json")]    
    public class CalendarController : Controller
    {
        private readonly IEndpointInstance _endpointInstance;

        public CalendarController(IEndpointInstance endpointInstance)
        {
            _endpointInstance = endpointInstance;
        }

        [Authorize]
        [HttpPost("api/Calendar/Add")]
        public async Task<IActionResult> Add(CalendarEventModel model)
        {
            return null;
        }

        [Authorize]
        [HttpPost("api/Calendar/Update")]
        public async Task<IActionResult> Update(CalendarEventModel model)
        {
            return null;
        }

        [Authorize]
        [HttpPost("api/Calendar/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return null;
        }

        [Authorize]
        [HttpGet("api/Calendar/Get")]
        public async Task<IActionResult> Get(CalendarEventModel model)
        {
            return null;
        }

        [Authorize]
        [HttpGet("api/Calendar/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return null;
        }
    }
}