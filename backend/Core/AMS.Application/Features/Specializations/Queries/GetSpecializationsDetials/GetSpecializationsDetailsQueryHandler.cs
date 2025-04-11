using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Specializations.Queries.GetSpecializationsDetials
{
    public class GetSpecializationsDetailsQueryHandler : ResponseHandler, IRequestHandler<GetSpecializationsDetailsQuery, Response<List<GetSpecializationsDetailsQueryResponse>>>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;
        public GetSpecializationsDetailsQueryHandler(ISpecializationRepository specializationRepository,IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<GetSpecializationsDetailsQueryResponse>>> Handle(GetSpecializationsDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _specializationRepository.GetAllAsync();

            return Success(_mapper.Map<List<GetSpecializationsDetailsQueryResponse>>(result));
        }
    }
}
