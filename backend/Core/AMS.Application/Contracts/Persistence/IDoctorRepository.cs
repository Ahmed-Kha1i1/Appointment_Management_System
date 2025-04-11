using AMS.Application.Contracts.Persistence.Base;
using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using Ecommerce.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Contracts.Persistence
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<Doctor?> GetDetailsAsync(int id);
        Task<PaginatedResult<Doctor>> GetAll(short PageSize, int PageNumber, string SearchQuery,int? SpecializationId, string OrderDirection, string OrderBy);
    }
}
