
using AMS.Doman.Entities.Base;

namespace AMS.Doman.Entities
{
    public class Specialization : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }

    }
}
