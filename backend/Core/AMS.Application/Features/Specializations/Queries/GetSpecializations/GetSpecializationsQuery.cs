using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Specializations.Queries.GetSpecializations
{
    public class GetSpecializationsQuery: IRequest<Response<List<GetSpecializationsQueryResponse>>>
    {
    }
}
