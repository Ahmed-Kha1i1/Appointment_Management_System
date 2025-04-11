using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Doman.Common.Enum;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler :ResponseHandler, IRequestHandler<CancelAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return NotFound<bool>("The specified appointment does not exist.");
            }

            appointment.Status = enAppointmentStatus.Cancelled;
            await _appointmentRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}