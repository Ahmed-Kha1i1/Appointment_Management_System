using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AMS.Application.Features.Auth.Queries.GetDetails
{
    public class GetDetailsQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : ResponseHandler, IRequestHandler<GetDetailsQuery, Response<GetDetailsQueryResponse>>
    {
        public async Task<Response<GetDetailsQueryResponse>> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized<GetDetailsQueryResponse>();
            }

            var user = await userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return BadRequest<GetDetailsQueryResponse>("User not found");
            }

            return Success(new GetDetailsQueryResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
            });
        }
    }
}
