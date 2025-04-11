using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Specializations.Queries.GetSpecializations
{
    internal class GetSpecializationsQueryHandler : ResponseHandler, IRequestHandler<GetSpecializationsQuery, Response<List<GetSpecializationsQueryResponse>>>
    {
        private readonly ISpecializationRepository _specializationRepository;
        public GetSpecializationsQueryHandler(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        public async Task<Response<List<GetSpecializationsQueryResponse>>> Handle(GetSpecializationsQuery request, CancellationToken cancellationToken)
        {   
            var result = await _specializationRepository.GetAllAsNoTracking();

            var response = result.Select(s => new GetSpecializationsQueryResponse
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return Success(response);
        }
    }
   
}
