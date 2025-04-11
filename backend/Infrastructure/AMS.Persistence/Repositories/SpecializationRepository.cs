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
    public class SpecializationRepository : GenericRepository<Specialization>, ISpecializationRepository
    {
        private readonly AppDbContext _context;

        public SpecializationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Specialization>> GetAllAsync()
        {
            return await _context.Specializations.Include(s => s.Doctors).ToListAsync();
        }
    }
}
