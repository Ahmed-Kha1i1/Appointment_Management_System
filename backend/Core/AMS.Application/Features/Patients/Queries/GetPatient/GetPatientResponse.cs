using AMS.Doman.Common.Enum;

namespace AMS.Application.Features.Patients.Queries.GetPatient
{
    public class GetPatientResponse
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public enGender Gender { get; set; }
    }
}