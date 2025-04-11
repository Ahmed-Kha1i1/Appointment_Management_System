using AMS.Application.Common.Response;
using AMS.Doman.Common.Enum;
using MediatR;

namespace AMS.Application.Features.Patients.Commands.CreatePatient
{
    public class CreatePatientCommand : IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BirthDate { get; set; }
        public enGender Gender { get; set; }
    }
}