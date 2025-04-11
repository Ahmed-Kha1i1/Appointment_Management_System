using AMS.Application.Contracts.Persistence;
using AMS.Doman.Entities;
using AMS.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> GetAsync(string email, string password, int roleId)
        {
            var user = await _context.Users
                .Where(x => x.Email == email && x.PasswordHash == password)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();

            if (user?.Roles?.Any(r => r.Id == roleId) == true)
            {
                return user;
            }

            return null;
        }
    }
}
