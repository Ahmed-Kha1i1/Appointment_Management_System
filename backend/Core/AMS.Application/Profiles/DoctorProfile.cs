using AMS.Application.Features.Appointments.Queries.GetAppointment;
using AMS.Application.Features.Doctors.Queries.GetDoctors;
using AMS.Application.Features.Specializations.Queries.GetSpecializationsDetials;
using AMS.Doman.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {

            CreateMap<Doctor, GetDoctorsQueryResponse>()
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.Specialization.Id))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name));

            CreateMap<Specialization, GetSpecializationsDetailsQueryResponse>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.Doctors.Select(
                  d => new DoctorDTO { Id = d.Id, FirstName = d.FirstName, LastName = d.LastName})));
        }
    }
}
