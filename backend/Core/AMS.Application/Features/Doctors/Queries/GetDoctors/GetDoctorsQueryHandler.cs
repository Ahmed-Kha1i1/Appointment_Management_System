using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Application.Features.Patients.Queries.GetPatients;
using AutoMapper;
using Ecommerce.Application.Common.Models;
using MediatR;

namespace AMS.Application.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryHandler(IDoctorRepository doctorRepository, IMapper mapper) : ResponseHandler, IRequestHandler<GetDoctorsQuery, Response<PaginatedResult<GetDoctorsQueryResponse>>>
    {

        public async Task<Response<PaginatedResult<GetDoctorsQueryResponse>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = await doctorRepository.GetAll(request.PageSize, request.PageNumber, request.SearchQuery,request.SpecializationId, request.OrderDirection, request.OrderBy);

            var data = mapper.Map<List<GetDoctorsQueryResponse>>(result.Data);

            var response = new PaginatedResult<GetDoctorsQueryResponse>(data, result.CurrentPage, result.PageSize, result.TotalCount);

            return Success(response);
        }
    }
}
