using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Users.Queries.CheckEmailExists
{
    public class CheckEmailExistsQueryHandler :ResponseHandler, IRequestHandler<CheckEmailExistsQuery, Response<bool>>
    {
        private readonly IUserRepository _userRepository;

        public CheckEmailExistsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<bool>> Handle(CheckEmailExistsQuery request, CancellationToken cancellationToken)
        {
            var emailExists = await _userRepository.IsEmailExistsAsync(request.Email);
            return Success(emailExists);
        }
    }

}
