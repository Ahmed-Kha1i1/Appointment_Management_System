using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AutoMapper;
using Ecommerce.Application.Common.Models;
using MediatR;

namespace AMS.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler(IPatientRepository patientRepository, IMapper mapper) : ResponseHandler, IRequestHandler<GetPatientsQuery, Response<PaginatedResult<GetPatientsQueryResponse>>>
    {
        public async Task<Response<PaginatedResult<GetPatientsQueryResponse>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var result = await patientRepository.GetAll(request.PageSize, request.PageNumber, request.SearchQuery, request.Gender, request.OrderDirection, request.OrderBy);

            var data = mapper.Map<List<GetPatientsQueryResponse>>(result.Data);

            var response = new PaginatedResult<GetPatientsQueryResponse>(data, result.CurrentPage, result.PageSize, result.TotalCount);

            return Success(response);
        }
    }
}
