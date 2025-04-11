using AMS.Application.Contracts.Persistence.Base;
using AMS.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Contracts.Persistence
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetActiveRefreshToken(int userId);
        Task<RefreshToken?> GetByToken(string token);
        Task<RefreshToken?> GetWithUserAsync(string token);
        RefreshToken GenerateRefreshToken(int userId);
    }
}
