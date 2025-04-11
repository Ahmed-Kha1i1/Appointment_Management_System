using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AutoMapper;
using Ecommerce.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
namespace AMS.Application.Features.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler : ResponseHandler,
        IRequestHandler<GetAppointmentsQuery, Response<List<GetAppointmentsQueryResponse>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentRepository = appointmentRepository;
            _contextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<List<GetAppointmentsQueryResponse>>> Handle(
            GetAppointmentsQuery request,
            CancellationToken cancellationToken)
        {
            var userIdClaim = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized<List<GetAppointmentsQueryResponse>> ();
            }

            string roleClaim = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized<List<GetAppointmentsQueryResponse>>();
            }

            int? doctorId = roleClaim == "Doctor" ? userId : null;
            int? patientId = roleClaim == "Patient" ? userId : null;

            var result = await _appointmentRepository.GetAppointmentsAsync(request.StartDate, request.EndDate, doctorId, patientId);

            var appointmentsResponse = _mapper.Map<List<GetAppointmentsQueryResponse>>(result);

            return Success(appointmentsResponse);
        }
    }
}