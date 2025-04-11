using AMS.API.Controllers.Base;
using AMS.API.DTO;
using AMS.Application.Common.Enums;
using AMS.Application.Common.Response;
using AMS.Application.Features.Auth;
using AMS.Application.Features.Auth.Commands.LoginCommand;
using AMS.Application.Features.Auth.Commands.RefreshToken;
using AMS.Application.Features.Auth.Commands.RevokeToken;
using AMS.Application.Features.Auth.Queries.GetDetails;
using AMS.Doman.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AMS.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private const string _refreshTokenCookie = "RefreshToken";
        private const string _Role = "SignRole";


        [HttpGet("", Name = "GetDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<GetDetailsQueryResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        public async Task<IActionResult> GetDetails()
        {
            var result = await _mediator.Send(new GetDetailsQuery());

            return CreateResult(result);
        }

        [HttpPost("Login", Name = "login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);

            if (HasValidRefreshToken(result.Data))
            {
                SetAuthCookies(result.Data);
            }

            return CreateResult(result);
        }

        [HttpPost("RefreshToken", Name = "RefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        public async Task<IActionResult> RefreshToken()
        {
            if (!TryGetRefreshTokenAndRole(out var refreshToken, out var role))
            {
                ClearAuthCookies();
                return BadRequest(new Response<AuthDTO>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Invalid or missing authentication tokens"
                });
            }

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken, role));

            if (result.StatusCode == HttpStatusCode.OK && HasValidRefreshToken(result.Data))
            {
                SetAuthCookies(result.Data);
            }
            else
            {
                ClearAuthCookies();
            }

            return CreateResult(result);

        }

        [HttpPost("RevokeToken", Name = "RevokeToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<>))]
        [Authorize()]
        public async Task<IActionResult> RevokeToken()
        {
            HttpContext.Request.Cookies.TryGetValue(_refreshTokenCookie, out var refreshToken);
            var result = await _mediator.Send(new RevokeTokenCommand(refreshToken));

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ClearAuthCookies();
            }

            return CreateResult(result);
        }

        #region Helper Methods
        private bool TryGetRefreshTokenAndRole(out string refreshToken, out enRole role)
        {
            refreshToken = null;
            role = default;

            if (!HttpContext.Request.Cookies.TryGetValue(_refreshTokenCookie, out refreshToken))
                return false;

            if (!HttpContext.Request.Cookies.TryGetValue(_Role, out var roleValue))
                return false;

            if(int.TryParse(roleValue, out int roleId) && Enum.IsDefined(typeof(enRole), roleId))
            {
                role = (enRole)roleId;
            }
            return true;
        }
        private bool HasValidRefreshToken(AuthDTO authData)
        {
            return authData is not null && !string.IsNullOrEmpty(authData.RefreshToken);
        }
        private void SetAuthCookies(AuthDTO authData)
        {
            SetCookie(_refreshTokenCookie, authData.RefreshToken, authData.RefreshTokenExpiration);
            SetCookie(_Role, Convert.ToString((int)authData.RoleId), authData.RefreshTokenExpiration);
        }
        private void ClearAuthCookies()
        {
            RemoveRefreshTokenCookie();
            RemoveRole();
        }
        private void SetCookie(string cookieName, string cookieValue, DateTime ExpiresOn)
        {

            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = ExpiresOn,
            };

            HttpContext.Response.Cookies.Append(cookieName, cookieValue, CookieOptions);
        }
        private void RemoveRole()
        {
            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1),
            };
            HttpContext.Response.Cookies.Delete(_Role, CookieOptions);
        }
        private void RemoveRefreshTokenCookie()
        {
            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1),
            };
            HttpContext.Response.Cookies.Delete(_refreshTokenCookie, CookieOptions);
        }
        #endregion
    }
}
