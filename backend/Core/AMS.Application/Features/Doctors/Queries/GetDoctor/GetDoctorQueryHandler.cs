using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Queries.GetDoctor
{
    public class GetDoctorQueryHandler : ResponseHandler, IRequestHandler<GetDoctorQuery, Response<GetDoctorQueryResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Response<GetDoctorQueryResponse>> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetDetailsAsync(request.DoctorId);
            if (doctor == null)
            {
                return NotFound<GetDoctorQueryResponse>("The specified doctor does not exist.");
            }

            var response = new GetDoctorQueryResponse
            {
                DoctorId = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                SpecializationId = doctor.SpecializationId,
                SpecializationName = doctor.Specialization?.Name
            };

            return Success(response);
        }
    }
}