using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Patients.Commands.DeletePatient
{
    public class DeletePatientCommand : IRequest<Response<bool>>
    {
        public int PatientId { get; set; }
    }
}