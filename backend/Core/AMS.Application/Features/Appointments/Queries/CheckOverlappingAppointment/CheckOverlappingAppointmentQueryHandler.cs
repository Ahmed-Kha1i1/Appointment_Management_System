using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace AMS.Application.Features.Appointments.Queries.CheckOverlappingAppointment
{
    public class CheckOverlappingAppointmentQueryHandler :ResponseHandler, IRequestHandler<CheckOverlappingAppointmentQuery, Response<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly AppointmentSettings _appointmentSettings;
        public CheckOverlappingAppointmentQueryHandler(IAppointmentRepository appointmentRepository, IOptionsSnapshot<AppointmentSettings> appointmentSettings)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentSettings = appointmentSettings.Value;
        }

        public async Task<Response<bool>> Handle(CheckOverlappingAppointmentQuery request, CancellationToken cancellationToken)
        {
            TimeOnly EndTime = request.StartTime.AddMinutes(_appointmentSettings.TimeSlotDuration);
            var hasOverlap = await _appointmentRepository.HasOverlappingAppointment(request.DoctorId,request.PatientId, request.AppointmentDate, request.StartTime, EndTime);
            return Success(hasOverlap);
        }
    }
}