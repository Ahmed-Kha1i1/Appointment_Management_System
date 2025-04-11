using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Patients.Queries.GetPatient
{
    public class GetPatientQueryHandler :ResponseHandler, IRequestHandler<GetPatientQuery, Response<GetPatientResponse>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response<GetPatientResponse>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                return NotFound<GetPatientResponse>("The specified patient does not exist.");
            }

            var response = new GetPatientResponse
            {
                PatientId = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender,
            };

            return Success(response);
        }
    }
}