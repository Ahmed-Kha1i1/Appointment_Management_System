using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Queries.GetAppointment
{
    public class GetAppointmentQuery : IRequest<Response<GetAppointmentResponse>>
    {
        public int AppointmentId { get; set; }
    }
}
