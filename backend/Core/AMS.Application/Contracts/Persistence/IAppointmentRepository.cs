using AMS.Application.Contracts.Persistence.Base;
using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using Ecommerce.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Application.Contracts.Persistence
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<Appointment?> GetDetailsByIdAsync(int id);
        Task<bool> HasOverlappingAppointment(int doctorId, int? patientId, DateOnly appointmentDate, TimeOnly StartTime, TimeOnly EndTime,string? GuestEmail = null);
        Task<List<Appointment>> GetAppointmentsAsync(DateOnly StartDate, DateOnly EndDate, int? DoctorId = null, int?  PatientId = null);
    }
}
