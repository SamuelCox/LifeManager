using System;
using System.Threading.Tasks;
using LifeManager.Messages.Lists;
using LifeManager.Models;
using LifeManager.Rest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using GetResponse = LifeManager.Messages.Lists.GetResponse;

namespace LifeManager.Rest.Controllers
{
    [Produces("application/json")]    
    public class ListController : Controller
    {
        private readonly IEndpointInstance _endpointInstance;
        private readonly IUserManagerWrapper _userManagerWrapper;

        public ListController(IEndpointInstance endpointInstance, IUserManagerWrapper userManagerWrapper)
        {
            _endpointInstance = endpointInstance;
            _userManagerWrapper = userManagerWrapper;
        }

        [Authorize]
        [HttpPost("api/Lists/Add")]
        public async Task<IActionResult> Add([FromBody] ListModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var addlistCommand = new AddListCommand { Model = model };
            await _endpointInstance.Send("LifeManager.Lists", addlistCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/Lists/Update")]
        public async Task<IActionResult> Update([FromBody] ListModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var updatelistCommand = new UpdateListCommand { Model = model };
            await _endpointInstance.Send("LifeManager.Lists", updatelistCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/Lists/Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            var deletelistCommand = new DeleteListCommand { Id = id, UserId = user.Id };
            await _endpointInstance.Send("LifeManager.Lists", deletelistCommand).ConfigureAwait(false);
            return Ok();
        }

        [Authorize]
        [HttpPost("api/Lists/Get")]
        public async Task<IActionResult> Get([FromBody] ListModel model)
        {
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            model.UserId = user.Id;
            var getlistCommand = new GetListCommand { Model = model };
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("LifeManager.Lists");
            var response = await _endpointInstance.Request<GetResponse>(getlistCommand, sendOptions).ConfigureAwait(false);
            return Ok(new { Response = response.Models });
        }

        [Authorize]
        [HttpGet("api/Lists/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var getAlllistsCommand = new GetAllListsCommand();
            var user = await _userManagerWrapper.FindByNameAsync(User.Identity.Name);
            getAlllistsCommand.UserId = user.Id;
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("LifeManager.Lists");
            var response = await _endpointInstance.Request<GetResponse>(getAlllistsCommand, sendOptions).ConfigureAwait(false);
            return Ok(new { Response = response.Models });
        }
    }
}