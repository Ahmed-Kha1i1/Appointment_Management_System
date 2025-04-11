using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Specializations.Queries.GetSpecializationsDetials
{
    public class GetSpecializationsDetailsQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DoctorDTO> Doctors { get; set; }
    }

    public class DoctorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
