using AMS.Application.Contracts.Persistence;
using AMS.Doman.Entities;
using AMS.Persistence.Repositories.Base;
using Ecommerce.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
namespace AMS.Persistence.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Doctor>> GetAll(
     short pageSize,
     int pageNumber,
     string searchQuery,
     int? specializationId,
     string orderDirection,
     string orderBy)
        {

            
            var query = _context.Doctors
                .Include(d => d.Specialization)
                .AsQueryable();

            if(specializationId is not null)
            {
                query = query.Where(s => s.SpecializationId == specializationId);
            }
            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(d =>
                    (d.FirstName + " " + d.LastName).Contains(searchQuery) ||
                    d.Email.Contains(searchQuery));
            }

            // Apply sorting
            query = orderBy.ToLower() switch
            {
                "name" => orderDirection.ToLower() == "desc"
                    ? query.OrderByDescending(d => d.FirstName)
                          .ThenByDescending(d => d.LastName)
                    : query.OrderBy(d => d.FirstName)
                          .ThenBy(d => d.LastName),

                "id" => orderDirection.ToLower() == "desc"
                    ? query.OrderByDescending(d => d.Id)
                    : query.OrderBy(d => d.Id),

                _ => orderDirection.ToLower() == "desc"
                    ? query.OrderByDescending(d => d.Id)
                    : query.OrderBy(d => d.Id)
            };

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var doctors = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Doctor>
            (
                doctors,
                pageNumber,
                pageSize,
                totalCount
            );
        }

        public Task<Doctor?> GetDetailsAsync(int id)
        {
            return _context.Doctors
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
