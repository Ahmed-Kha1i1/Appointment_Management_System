using AMS.API.Controllers.Base;
using AMS.Application.Common.Response;
using AMS.Application.Features.Patients.Commands.CreatePatient;
using AMS.Application.Features.Patients.Commands.DeletePatient;
using AMS.Application.Features.Patients.Commands.UpdatePatient;
using AMS.Application.Features.Patients.Queries.GetPatient;
using AMS.Application.Features.Patients.Queries.GetPatients;
using Ecommerce.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/patients")]
[ApiController]

public class PatientsController : AppControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{PatientId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetPatientResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPatient([FromRoute]GetPatientQuery query)
    {
        var result = await _mediator.Send(query);
        return CreateResult(result);
    }

    [HttpPost("Search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<PaginatedResult<GetPatientsQueryResponse>>))]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPatients([FromBody] GetPatientsQuery query)
    {
        var result = await _mediator.Send(query);
        return CreateResult(result);
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<int>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand command)
    {
        var result = await _mediator.Send(command);
        return CreateResult(result);
    }

    [HttpPut("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientCommand command)
    {

        var result = await _mediator.Send(command);
        return CreateResult(result);
    }

    [HttpDelete("{PatientId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePatient([FromRoute] DeletePatientCommand command)
    {
        var result = await _mediator.Send(command);
        return CreateResult(result);
    }
}