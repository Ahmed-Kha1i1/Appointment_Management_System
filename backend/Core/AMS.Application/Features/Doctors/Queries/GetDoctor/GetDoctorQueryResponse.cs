using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Queries.GetDoctor
{
    public class GetDoctorQueryResponse
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
    }
}
