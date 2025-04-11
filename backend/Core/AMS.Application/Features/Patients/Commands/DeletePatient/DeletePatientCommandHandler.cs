using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommandHandler : ResponseHandler, IRequestHandler<DeletePatientCommand, Response<bool>>
    {
        private readonly IPatientRepository _patientRepository;

        public DeletePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var Patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (Patient is null)
            {
                return NotFound<bool>("The specified patient does not exist.");
            }

            _patientRepository.Delete(Patient);
            await _patientRepository.SaveChangesAsync();

            return Success(true);
        }
    }
}