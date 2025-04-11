using AMS.API.Controllers.Base;
using AMS.Application.Common.Response;
using AMS.Application.Features.Specializations.Queries.GetSpecializations;
using AMS.Application.Features.Specializations.Queries.GetSpecializationsDetials;
using Microsoft.AspNetCore.Mvc;

namespace AMS.API.Controllers
{
    [Route("api/specializations")]
    [ApiController]
    public class SpecializationsController : AppControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<GetSpecializationsQueryResponse>>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetSpecializationsQuery());
            return CreateResult(result);
        }

        [HttpGet("AllDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<GetSpecializationsDetailsQueryResponse>>))]
        public async Task<IActionResult> GetAllDetails()
        {
            var result = await _mediator.Send(new GetSpecializationsDetailsQuery());
            return CreateResult(result);
        }
    }
}
