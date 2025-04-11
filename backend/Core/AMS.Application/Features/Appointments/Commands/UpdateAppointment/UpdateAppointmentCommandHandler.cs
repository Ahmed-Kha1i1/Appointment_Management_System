using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler :ResponseHandler, IRequestHandler<UpdateAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly AppointmentSettings _appointmentSettings;

        public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository,IOptionsSnapshot<AppointmentSettings> appointmentSettings)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _appointmentSettings = appointmentSettings.Value;
        }

        public async Task<Response<bool>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return NotFound<bool>("The specified appointment does not exist.");
            }

            var doctorExists = await _doctorRepository.IsExists(request.DoctorId);
            if (!doctorExists)
            {
                return NotFound<bool>("The specified doctor does not exist.");
            }
            //Check for guest users
            if(appointment.PatientId != null && request.PatientId != null)
            {
                var patientExists = await _patientRepository.IsExists(request.PatientId.Value);
                if (!patientExists)
                {
                    return NotFound<bool>("The specified patient does not exist.");
                }
            }

            TimeOnly EndTime = request.StartTime.AddMinutes(_appointmentSettings.TimeSlotDuration);

            if (!(appointment.AppointmentDate == request.AppointmentDate && appointment.StartTime == request.StartTime && appointment.EndTime == EndTime))
            {
                bool hasOverlap = await _appointmentRepository.HasOverlappingAppointment(request.DoctorId, request.PatientId, request.AppointmentDate, request.StartTime, EndTime);
                if (hasOverlap)
                {
                    return BadRequest<bool>("This time slot is already booked. Please choose a different time.");
                }
            }
            // Check for overlapping appointments
            
            TimeOnly UpdateStartTime = new TimeOnly(request.StartTime.Hour, 0);
            appointment.DoctorId = request.DoctorId;
            appointment.PatientId = appointment.PatientId is null ? null : request.PatientId;
            appointment.AppointmentDate = request.AppointmentDate;
            appointment.StartTime = UpdateStartTime;
            appointment.EndTime = EndTime;

            await _appointmentRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}