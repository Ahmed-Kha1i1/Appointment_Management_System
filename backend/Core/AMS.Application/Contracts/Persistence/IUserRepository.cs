using AMS.Application.Contracts.Persistence.Base;
using AMS.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task<User?> GetAsync(string email, string password, int RoleId);
    }
}
