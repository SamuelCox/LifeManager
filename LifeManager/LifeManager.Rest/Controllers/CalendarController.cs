using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //public async Task<IActionResult> Add()
        //{

        //}

        //public async Task<IActionResult> Update()
        //{

        //}

        //public async Task<IActionResult> Delete()
        //{

        //}

        //public async Task<IActionResult> Get()
        //{

        //}

        //public async Task<IActionResult> GetAll()
        //{

        //}
    }
}