﻿using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest<Response<bool>>
    {
        public int AppointmentId { get; set; }
    }
}
