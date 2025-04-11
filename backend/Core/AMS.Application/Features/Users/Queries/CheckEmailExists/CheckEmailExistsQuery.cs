using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Users.Queries.CheckEmailExists
{
    public class CheckEmailExistsQuery : IRequest<Response<bool>>
    {
        public string Email { get; set; }
    }
}
