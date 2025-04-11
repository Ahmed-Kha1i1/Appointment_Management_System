using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQueryResponse
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public enAppointmentStatus Status { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; init; }
        public TimeOnly EndTime { get; init; }
    }
}


