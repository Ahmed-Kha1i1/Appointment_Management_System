using AMS.Doman.Common.Enum;
using System;

namespace AMS.Application.Features.Appointments.Queries.GetAppointment
{
    public class GetAppointmentResponse
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int SpecializationId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public enAppointmentStatus Status { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; init; }
        public TimeOnly EndTime { get; init; }
    }
}