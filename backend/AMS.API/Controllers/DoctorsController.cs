using AMS.API.Controllers.Base;
using AMS.Application.Common.Response;
using AMS.Application.Features.Doctors.Commands.AddDoctor;
using AMS.Application.Features.Doctors.Commands.CreateDoctor;
using AMS.Application.Features.Doctors.Commands.DeleteDoctor;
using AMS.Application.Features.Doctors.Commands.UpdateDoctor;
using AMS.Application.Features.Doctors.Queries.GetDoctor;
using AMS.Application.Features.Doctors.Queries.GetDoctors;
using AMS.Application.Features.Patients.Queries.GetPatients;
using Ecommerce.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/doctors")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DoctorsController : AppControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{DoctorId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetDoctorQueryResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    public async Task<IActionResult> GetDoctor([FromRoute] GetDoctorQuery query)
    {
        var result = await _mediator.Send(query);
        return CreateResult(result);
    }

    [HttpPost("Search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<PaginatedResult<GetDoctorsQueryValidator>>))]
    public async Task<IActionResult> GetDoctors([FromBody] GetDoctorsQuery query)
    {
        var result = await _mediator.Send(query);
        return CreateResult(result);
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<int>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorCommand command)
    {
        var result = await _mediator.Send(command);
        return CreateResult(result);
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorCommand command)
    {
        var result = await _mediator.Send(command);
        return CreateResult(result);
    }

    [HttpDelete("{DoctorId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    public async Task<IActionResult> DeleteDoctor([FromRoute] DeleteDoctorCommand command)
    {
        var result = await _mediator.Send(command);
        return CreateResult(result);
    }
}