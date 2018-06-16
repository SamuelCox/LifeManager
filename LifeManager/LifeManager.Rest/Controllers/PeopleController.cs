using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeManager.Messages.People;
using LifeManager.Models;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using GetResponse = LifeManager.Messages.People.GetResponse;

namespace LifeManager.Rest.Controllers
{    
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IEndpointInstance _endpointInstance;
        private readonly IUserManagerWrapper _userManagerWrapper;

        public PeopleController(IEndpointInstance endpointInstance, IUserManagerWrapper userManagerWrapper)
        {
            _endpointInstance = endpointInstance;
            _userManagerWrapper = userManagerWrapper;
        }

        [Authorize]
        [HttpPost("api/People/Add")]
        public async Task<IActionResult> Add([FromBody] PersonModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var addPersonCommand = new AddPersonCommand { Person = model };
            await _endpointInstance.Send("LifeManager.People", addPersonCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/People/Update")]
        public async Task<IActionResult> Update([FromBody] PersonModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var updatePersonCommand = new UpdatePersonCommand { Person = model };
            await _endpointInstance.Send("LifeManager.People", updatePersonCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/People/Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            var deletePersonCommand = new DeletePersonCommand { Id = id, UserId = user.Id };
            await _endpointInstance.Send("LifeManager.People", deletePersonCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/People/Get")]
        public async Task<IActionResult> Get([FromBody] PersonModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var getPersonCommand = new GetPersonCommand { Person = model };
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("LifeManager.People");
            var response = await _endpointInstance.Request<GetResponse>(getPersonCommand, sendOptions).ConfigureAwait(false);
            return Ok(new { Response = response.People });
        }

        [Authorize]
        [HttpGet("api/People/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var getAllPeopleCommand = new GetAllPeopleCommand();
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            getAllPeopleCommand.UserId = user.Id;
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("LifeManager.People");
            var response = await _endpointInstance.Request<GetResponse>(getAllPeopleCommand, sendOptions).ConfigureAwait(false);
            return Ok(new { Response = response.People });
        }
    }
}