namespace AMS.API.DTO
{
    public class BriefAuthDTO
    {
        public bool IsAuthenticated { get; set; }
        public string? UserId { get; set; }
        public BriefAuthDTO(bool isAuthenticated, string? userId)
        {
            IsAuthenticated = isAuthenticated;
            UserId = userId;
        }
    }
}
