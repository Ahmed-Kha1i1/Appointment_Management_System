using AMS.Doman.Common.Enum;

namespace AMS.Doman.Entities
{
    public class Patient : User
    {
        public DateOnly BirthDate { get; set; }
        public enGender Gender { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
