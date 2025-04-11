using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Doman.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler :ResponseHandler, IRequestHandler<UpdatePatientCommand, Response<bool>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository, IUserRepository userRepository)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                return NotFound<bool>("The specified patient does not exist.");
            }

            if (patient.Email != request.Email && await _userRepository.IsEmailExistsAsync(request.Email))
            {
                return BadRequest<bool>("The specified email is already in use.");
            }

            patient.FirstName = request.FirstName;
            patient.LastName = request.LastName;
            patient.Email = request.Email;
            patient.BirthDate = request.BirthDate;
            patient.Gender = request.Gender;

            await _patientRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}