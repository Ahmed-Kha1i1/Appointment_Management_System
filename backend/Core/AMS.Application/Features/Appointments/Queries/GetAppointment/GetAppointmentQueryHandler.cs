using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Appointments.Queries.GetAppointment
{
    public class GetAppointmentQueryHandler :ResponseHandler, IRequestHandler<GetAppointmentQuery, Response<GetAppointmentResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetAppointmentQueryHandler(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetAppointmentResponse>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetDetailsByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                return NotFound<GetAppointmentResponse>("The specified appointment does not exist.");
            }
            var response = _mapper.Map<GetAppointmentResponse>(appointment);
            return Success(response);
        }
    }
}