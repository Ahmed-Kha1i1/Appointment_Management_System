using AMS.Application.Common.Response;
using MediatR;

namespace AMS.Application.Features.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQuery :  IRequest<Response<List<GetAppointmentsQueryResponse>>>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
