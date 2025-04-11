using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Doman.Common.Enum;
using MediatR;

namespace AMS.Application.Features.Appointments.Commands.ConfirmAppointment
{
    public class ConfirmAppointmentCommandHandler :ResponseHandler, IRequestHandler<ConfirmAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ConfirmAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return NotFound<bool>("The specified appointment does not exist.");
            }

            if (appointment.Status == enAppointmentStatus.Cancelled)
            {
                return BadRequest<bool>("Cannot comfirm an appointment that has been cancelled");
            }

            appointment.Status = enAppointmentStatus.Confirmed;
            
            await _appointmentRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}