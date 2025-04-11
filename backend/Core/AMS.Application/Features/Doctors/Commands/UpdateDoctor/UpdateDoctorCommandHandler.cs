using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;

namespace AMS.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandHandler :ResponseHandler, IRequestHandler<UpdateDoctorCommand, Response<bool>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IUserRepository userRepository)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                return NotFound<bool>("The specified doctor does not exist.");
            }

            if(doctor.Email != request.Email && await _userRepository.IsEmailExistsAsync(request.Email))
            {
                return BadRequest<bool>("The specified email is already in use.");
            }

            doctor.FirstName = request.FirstName;
            doctor.LastName = request.LastName;

            doctor.Email = request.Email;
            doctor.SpecializationId = request.SpecializationId;

            await _doctorRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}