using AMS.Application.Contracts.Persistence;
using AMS.Doman.Entities;
using AMS.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using AMS.Doman.Common.Enum;


namespace AMS.Persistence.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(DateOnly StartDate, DateOnly EndDate, int? DoctorId = null, int? PatientId = null)
        {
            var query = _context.Appointments
                 .Include(a => a.Doctor)
                 .Include(a => a.Patient)
                 .Where(a => a.AppointmentDate >= StartDate && a.AppointmentDate <= EndDate);

            if(DoctorId != null )
                query = query.Where(a => a.DoctorId ==  DoctorId);

            if(PatientId != null)
                query = query.Where(a => a.PatientId == PatientId);

            return await query.ToListAsync();
        }

        public Task<Appointment?> GetDetailsByIdAsync(int id)
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialization)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> HasOverlappingAppointment(int doctorId,int? patientId, DateOnly appointmentDate, TimeOnly StartTime, TimeOnly EndTime, string? guestEmail = null)
        {
            return await _context.Appointments
                .Where(a => a.AppointmentDate == appointmentDate && (a.DoctorId == doctorId || 
                (patientId != null ? a.PatientId == patientId : a.GuestEmail == guestEmail)) && a.Status != enAppointmentStatus.Cancelled)
                .AnyAsync(a => 
                    ((a.StartTime >= StartTime && a.StartTime < EndTime) ||
                    (a.EndTime > StartTime && a.EndTime <= EndTime) ||
                    (a.StartTime <= StartTime && a.EndTime >= EndTime)));
        }
    }
}

