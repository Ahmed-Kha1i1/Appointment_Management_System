using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Doctors.Queries.GetDoctor
{
    public class GetDoctorQuery : IRequest<Response<GetDoctorQueryResponse>>
    {
        public int DoctorId { get; set; }
    }
}