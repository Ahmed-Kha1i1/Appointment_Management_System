using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Auth.Queries.GetDetails
{
    public class GetDetailsQuery : IRequest<Response<GetDetailsQueryResponse>>
    {
    }
}
