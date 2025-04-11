using AMS.Application.Features.Appointments.Queries.GetAppointment;
using AMS.Application.Features.Appointments.Queries.GetAppointments;
using AMS.Doman.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetAppointmentResponse>()
              .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
              .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.Doctor.SpecializationId))
              .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
              .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
              .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
              .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
              .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
              .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));


            CreateMap<Appointment, GetAppointmentsQueryResponse>()
              .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
              .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
              .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PatientId))
              .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
              .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
              .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
              .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));
        }
    }
}
