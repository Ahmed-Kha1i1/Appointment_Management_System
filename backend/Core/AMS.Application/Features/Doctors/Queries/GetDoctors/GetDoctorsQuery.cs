using AMS.Application.Common.Response;
using Ecommerce.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQuery :PaginatedQueryBase, IRequest<Response<PaginatedResult<GetDoctorsQueryResponse>>>
    {
        public string SearchQuery { get; set; }
        public string OrderDirection { get; set; } = "asc";
        public int? SpecializationId { get; set; }
        public string OrderBy { get; set; } = "id"; //id - name
    }
}
