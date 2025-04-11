using AMS.Application.Common.Enums;
using System.Text.Json.Serialization;

namespace AMS.Application.Features.Auth
{
    public class AuthDTO
    {
        public int UserId { get; set; }
        public enRole RoleId { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresOn { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
