using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Queries.GetDoctors
{
    public class GetDoctorsQueryResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
