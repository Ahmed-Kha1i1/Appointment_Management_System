using AMS.Doman.Common.Enum;
using AMS.Doman.Entities.Base;

namespace AMS.Doman.Entities
{
    public class Appointment : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public int? PatientId { get; set; }
        public string? GuestEmail { get; set; }
        public Patient? Patient { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public enAppointmentStatus Status { get; set; } = enAppointmentStatus.Pending;
    }
}
