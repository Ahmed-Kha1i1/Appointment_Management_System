

namespace AMS.Doman.Entities
{
    public class Doctor : User
    {
        public int SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
