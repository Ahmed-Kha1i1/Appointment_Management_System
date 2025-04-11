using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommandHandler :ResponseHandler, IRequestHandler<DeleteDoctorCommand, Response<bool>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Response<bool>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor is null)
            {
                return NotFound<bool>("The specified doctor does not exist.");
            }

            _doctorRepository.Delete(doctor);
            await _doctorRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}