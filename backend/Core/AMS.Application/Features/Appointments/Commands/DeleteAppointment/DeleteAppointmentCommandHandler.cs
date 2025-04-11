using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;

namespace AMS.Application.Features.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : ResponseHandler, IRequestHandler<DeleteAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response<bool>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var IsExists = await _appointmentRepository.IsExists(request.AppointmentId);
            if (!IsExists)
            {
                return NotFound<bool>("The specified appointment does not exist.");
            }

            await _appointmentRepository.DeleteAsync(request.AppointmentId);

            return Success(true);
        }
    }
}