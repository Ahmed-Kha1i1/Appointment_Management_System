using AMS.Application.Common.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Commands.DeleteDoctor
{
    public class DeleteDoctorCommand : IRequest<Response<bool>>
    {
        public int DoctorId { get; set; }
    }
}
