using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.BookAsGuest
{
    public class BookAsGuestCommand : IRequest<Response<int?>>
    {
        public int DoctorId { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
