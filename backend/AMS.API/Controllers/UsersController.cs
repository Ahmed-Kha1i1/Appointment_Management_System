using AMS.API.Controllers.Base;
using AMS.Application.Common.Response;
using AMS.Application.Features.Users.Queries.CheckEmailExists;
using Microsoft.AspNetCore.Mvc;

namespace AMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : AppControllerBase
    {
        [HttpGet("CheckEmail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        public async Task<IActionResult> CheckEmailExists([FromQuery] CheckEmailExistsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }
    }
}
