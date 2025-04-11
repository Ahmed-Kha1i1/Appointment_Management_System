using AMS.Application.Features.Doctors.Queries.GetDoctors;
using AMS.Application.Features.Patients.Queries.GetPatients;
using AMS.Doman.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, GetPatientsQueryResponse>();
        }
    }
}
