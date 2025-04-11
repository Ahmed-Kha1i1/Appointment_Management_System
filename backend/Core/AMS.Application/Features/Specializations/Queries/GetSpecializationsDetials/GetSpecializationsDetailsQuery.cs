using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Specializations.Queries.GetSpecializationsDetials
{
    public class GetSpecializationsDetailsQuery : IRequest<Response<List<GetSpecializationsDetailsQueryResponse>>>
    {
    }
}
