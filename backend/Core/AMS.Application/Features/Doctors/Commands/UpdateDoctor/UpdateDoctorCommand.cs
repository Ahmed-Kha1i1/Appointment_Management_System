using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Doctors.Commands.UpdateDoctor
{
    public class UpdateDoctorCommand : IRequest<Response<bool>>
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int SpecializationId { get; set; }
    }
}