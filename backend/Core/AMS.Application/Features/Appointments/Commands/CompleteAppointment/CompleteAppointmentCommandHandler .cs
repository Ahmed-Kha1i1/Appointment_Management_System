using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Doman.Common.Enum;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.CompleteAppointment
{
    public class CompleteAppointmentCommandHandler :ResponseHandler, IRequestHandler<CompleteAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CompleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(CompleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);

            if (appointment == null)
            {
                return NotFound<bool>("The specified appointment does not exist.");
            }

            if (appointment.Status == enAppointmentStatus.Cancelled)
            {
                return BadRequest<bool>("Cannot complete an appointment that has been cancelled");
            }

            appointment.Status = enAppointmentStatus.Completed;
            await _appointmentRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}