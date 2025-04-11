using AMS.Application.Common.Response;
using AMS.Doman.Common.Enum;
using Ecommerce.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Patients.Queries.GetPatients
{
    public class GetPatientsQuery : PaginatedQueryBase, IRequest<Response<PaginatedResult<GetPatientsQueryResponse>>>
    {
        public string SearchQuery { get; set; }
        public enGender? Gender { get; set; }
        public string OrderDirection { get; set; } = "asc";
        public string OrderBy { get; set; } = "id"; //id - name
    }
}
