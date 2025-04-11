using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Queries.CheckOverlappingAppointment
{
    public class CheckOverlappingAppointmentQuery : IRequest<Response<bool>>
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
