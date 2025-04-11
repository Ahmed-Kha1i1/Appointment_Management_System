using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Patients.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<Response<GetPatientResponse>>
    {
        public int PatientId { get; set; }
    }
}