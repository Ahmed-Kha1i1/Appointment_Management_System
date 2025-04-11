using AMS.Application.Features.Auth;
using AMS.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Contracts
{
    public interface IAuthRespository
    {
        TokenResult GenerateAccessToken(User user, string roleName);
    }
}
