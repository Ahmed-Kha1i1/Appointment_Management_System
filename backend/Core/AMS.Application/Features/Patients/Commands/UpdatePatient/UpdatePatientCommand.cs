using AMS.Application.Common.Response;
using AMS.Doman.Common.Enum;
using MediatR;

namespace AMS.Application.Features.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommand : IRequest<Response<bool>>
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public enGender Gender { get; set; }
    }
}