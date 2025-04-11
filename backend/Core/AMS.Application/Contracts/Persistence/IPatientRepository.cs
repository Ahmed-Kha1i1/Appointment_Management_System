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
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<PaginatedResult<Patient>> GetAll(short PageSize, int PageNumber, string SearchQuery, enGender? Gender, string OrderDirection, string OrderBy);
    }
}
