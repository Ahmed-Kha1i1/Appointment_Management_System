using AMS.API.Controllers.Base;
using AMS.API.DTO;
using AMS.Application.Common.Response;
using AMS.Application.Features.Appointments.Commands.AddAppointment;
using AMS.Application.Features.Appointments.Commands.BookAsGuest;
using AMS.Application.Features.Appointments.Commands.CancelAppointment;
using AMS.Application.Features.Appointments.Commands.CompleteAppointment;
using AMS.Application.Features.Appointments.Commands.ConfirmAppointment;
using AMS.Application.Features.Appointments.Commands.DeleteAppointment;
using AMS.Application.Features.Appointments.Commands.UpdateAppointment;
using AMS.Application.Features.Appointments.Queries.CheckOverlappingAppointment;
using AMS.Application.Features.Appointments.Queries.GetAppointment;
using AMS.Application.Features.Appointments.Queries.GetAppointments;
using Ecommerce.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace AMS.API.Controllers
{

    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{AppointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetAppointmentResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetAppointment([FromRoute] GetAppointmentQuery query)
        {
            var result = await _mediator.Send(query);

            return CreateResult(result);
        }

        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<PaginatedResult<GetAppointmentsQuery>>))]
        [Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<IActionResult> GetAppointments([FromBody] GetAppointmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }

        [HttpPost("BookAsGuest")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<int>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        public async Task<IActionResult> BookAsGuest([FromBody]  BookAsGuestCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<int>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> CreateAppointment([FromBody] AddAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpDelete("{AppointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAppointment([FromRoute] DeleteAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            return CreateResult(result);
        }

        [HttpPost("{AppointmentId}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> CancelAppointment([FromRoute] CancelAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            return CreateResult(result);
        }

        [HttpPost("{AppointmentId}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> CompleteAppointment([FromRoute] CompleteAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            return CreateResult(result);
        }

        [HttpPost("{AppointmentId}/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> ConfirmAppointment([FromRoute] ConfirmAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            return CreateResult(result);
        }
    }
}
