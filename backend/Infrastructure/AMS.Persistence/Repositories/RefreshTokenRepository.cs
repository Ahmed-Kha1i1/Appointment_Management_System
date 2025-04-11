using AMS.Application.Contracts.Persistence;
using AMS.Doman.Entities;
using AMS.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace AMS.Persistence.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        private readonly int _refreshTokenLifetime;
        public RefreshTokenRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _refreshTokenLifetime = configuration.GetValue<int>("RefreshTokenLifetime");
        }

        public async Task<RefreshToken?> GetActiveRefreshToken(int userId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.RevokedOn == null && r.ExpiresOn > DateTime.UtcNow && r.userId == userId);
        }

        public RefreshToken GenerateRefreshToken(int userId)
        {
            var randomNumber = new byte[32];
            string Token;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                Token = Convert.ToBase64String(randomNumber);
            }

            return new RefreshToken
            {
                Token = Token,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenLifetime),
                userId = userId
            };
        }

        public async Task<RefreshToken?> GetWithUserAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .ThenInclude(u => u.Roles)
                .FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
