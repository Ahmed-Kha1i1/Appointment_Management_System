using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace AMS.Application.Features.Appointments.Commands.AddAppointment
{

    public class AddAppointmentCommandHandler :ResponseHandler, IRequestHandler<AddAppointmentCommand, Response<int?>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly AppointmentSettings _appointmentSettings;

        public AddAppointmentCommandHandler(IAppointmentRepository appointmentRepository,IDoctorRepository doctorRepository,IPatientRepository patientRepository,IOptionsSnapshot<AppointmentSettings> appointmentSettings)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _appointmentSettings = appointmentSettings.Value;
        }


        public async Task<Response<int?>> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            var doctorExists = await _doctorRepository.IsExists(request.DoctorId);
            if (!doctorExists)
            {
                return NotFound<int?>("The specified doctor does not exist.");
            }

            var patientExists = await _patientRepository.IsExists(request.PatientId);
            if (!patientExists)
            {
                return NotFound<int?>("The specified patient does not exist.");
            }

            TimeOnly EndTime = request.StartTime.AddMinutes(_appointmentSettings.TimeSlotDuration);
            // Check for overlapping appointments
            bool hasOverlap = await _appointmentRepository.HasOverlappingAppointment(request.DoctorId, request.PatientId, request.AppointmentDate, request.StartTime, EndTime);

            if (hasOverlap)
            {
                return BadRequest<int?>("This time slot is already booked. Please choose a different time.");
            }   
            TimeOnly UpdateStartTime = new TimeOnly(request.StartTime.Hour,0);

            var appointment = new Appointment
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                AppointmentDate = request.AppointmentDate,
                StartTime = UpdateStartTime,
                EndTime = EndTime,
                CreatedDate = DateTime.UtcNow,
                Status = enAppointmentStatus.Pending
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return Created<int?>(appointment.Id);
        }
    }
}
