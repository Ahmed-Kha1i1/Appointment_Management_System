using AMS.Application.Contracts.Persistence;
using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using AMS.Persistence.Repositories.Base;
using Ecommerce.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Persistence.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Patient>> GetAll(short pageSize, int pageNumber, string searchQuery, enGender? gender, string orderDirection, string orderBy)
        {
            var query = _context.Patients
               .AsQueryable();
            if (gender != null)
            {
                query = query.Where(p => p.Gender == gender);
            }

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(d =>
                    (d.FirstName + " " + d.LastName).Contains(searchQuery)||
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
            var patients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Patient>
            (
                patients,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}
