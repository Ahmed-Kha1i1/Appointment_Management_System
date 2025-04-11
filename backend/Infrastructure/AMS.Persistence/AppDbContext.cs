using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using AMS.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Persistence
{
    public class AppDbContext(IConfiguration configuration, IOptionsSnapshot<ConnectionStrings> optionsSnapshot) : DbContext
    {

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string cs = optionsSnapshot.Value.Connection;
            optionsBuilder.UseSqlServer(cs);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            #region Seed Mock Appointment Records for Development & Testing
            // equals to "Secure12#"
            string domeHashedPassowrd = "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710";

            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Doctor" },
                new Role { Id = 3, Name = "Patient" }
            );

            // Seed specializations
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Paediatrician" },
                new Specialization { Id = 2, Name = "Internal Medicine Physician" },
                new Specialization { Id = 3, Name = "Gynaecologist" },
                new Specialization { Id = 4, Name = "Cardiologist" },
                new Specialization { Id = 5, Name = "Dermatologist" },
                new Specialization { Id = 6, Name = "Neurologist" },
                new Specialization { Id = 7, Name = "Psychiatrist" },
                new Specialization { Id = 8, Name = "Gastroenterologist" },
                new Specialization { Id = 9, Name = "Ophthalmologist" },
                new Specialization { Id = 10, Name = "Pulmonologist" },
                new Specialization { Id = 11, Name = "Nephrologist" },
                new Specialization { Id = 12, Name = "Dentist" }
            );

            // Seed users (admins)
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Ahmed", LastName = "Magdy", Email = "admin1@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0) },
                new User { Id = 2, FirstName = "Mosa", LastName = "Ali", Email = "admin2@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0) }
            );

            // Seed doctors
            modelBuilder.Entity<Doctor>().HasData(
                // Paediatrician
                new Doctor { Id = 3, FirstName = "John", LastName = "Doe", Email = "johndoe1@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 1 },
                new Doctor { Id = 4, FirstName = "Jane", LastName = "Smith", Email = "janesmith1@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 1 },
                // Internal Medicine Physician
                new Doctor { Id = 5, FirstName = "Alice", LastName = "Johnson", Email = "alicejohnson@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 2 },
                // Gynaecologist
                new Doctor { Id = 6, FirstName = "Bob", LastName = "Brown", Email = "bobbrown@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 3 },
                new Doctor { Id = 7, FirstName = "Carol", LastName = "Davis", Email = "caroldavis@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 3 },
                new Doctor { Id = 8, FirstName = "David", LastName = "Wilson", Email = "davidwilson@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 3 },
                // Cardiologist
                new Doctor { Id = 9, FirstName = "Eve", LastName = "Martinez", Email = "evemartinez@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 4 },
                // Dermatologist
                new Doctor { Id = 10, FirstName = "Frank", LastName = "Garcia", Email = "frankgarcia@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 5 },
                new Doctor { Id = 11, FirstName = "Grace", LastName = "Rodriguez", Email = "gracerodriguez@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 5 },
                // Neurologist
                new Doctor { Id = 12, FirstName = "Hank", LastName = "Lee", Email = "hanklee@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 6 },
                // Psychiatrist
                new Doctor { Id = 13, FirstName = "Ivy", LastName = "Walker", Email = "ivywalker@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 7 },
                // Gastroenterologist
                new Doctor { Id = 14, FirstName = "Jack", LastName = "Hall", Email = "jackhall@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 8 },
                new Doctor { Id = 15, FirstName = "Kathy", LastName = "Allen", Email = "kathyallen@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 8 },
                // Ophthalmologist
                new Doctor { Id = 16, FirstName = "Leo", LastName = "Young", Email = "leoyoung@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 9 },
                // Pulmonologist
                new Doctor { Id = 17, FirstName = "Mia", LastName = "Hernandez", Email = "miahernandez@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 10 },
                // Nephrologist
                new Doctor { Id = 18, FirstName = "Nina", LastName = "King", Email = "ninaking@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 11 },
                new Doctor { Id = 19, FirstName = "Oscar", LastName = "Wright", Email = "oscarwright@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 11 },
                // Dentist
                new Doctor { Id = 20, FirstName = "Paul", LastName = "Lopez", Email = "paullopez@gmail.com", PasswordHash = domeHashedPassowrd, CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0), SpecializationId = 12 }
            );

            // Seed a patient
            modelBuilder.Entity<Patient>().HasData(
    new Patient
    {
        Id = 21,
        FirstName = "Ahmed",
        LastName = "Magdy",
        Email = "patient@gmail.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 7, 3, 10, 0),
        BirthDate = new DateOnly(2001, 10, 25),
        Gender = enGender.Male  // Assuming 0 = Male in your enum
    },
    new Patient
    {
        Id = 25,
        FirstName = "John",
        LastName = "Smith",
        Email = "john.smith@example.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 10, 5, 20, 0),
        BirthDate = new DateOnly(1985, 7, 15),
        Gender = enGender.Male
    },
    new Patient
    {
        Id = 27,
        FirstName = "Michael",
        LastName = "Williams",
        Email = "michael.w@example.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 10, 5, 20, 0),
        BirthDate = new DateOnly(1978, 11, 5),
        Gender = enGender.Male
    },
    new Patient
    {
        Id = 28,
        FirstName = "Sarah",
        LastName = "Brown",
        Email = "sarah.b@example.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 10, 5, 20, 0),
        BirthDate = new DateOnly(1995, 9, 18),
        Gender = enGender.Female  // Assuming 1 = Female in your enum
    },
    new Patient
    {
        Id = 29,
        FirstName = "David",
        LastName = "Millerz",
        Email = "davidz.m@example.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 10, 5, 20, 0),
        BirthDate = new DateOnly(2007, 4, 10),
        Gender = enGender.Male
    },
    new Patient
    {
        Id = 30,
        FirstName = "Ahmed",
        LastName = "Ali2",
        Email = "am234@gmail.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 10, 8, 22, 0),
        BirthDate = new DateOnly(2007, 4, 10),
        Gender = enGender.Female
    },
    new Patient
    {
        Id = 32,
        FirstName = "Davidz",
        LastName = "Ali",
        Email = "p2@gmail.com",
        PasswordHash = domeHashedPassowrd,
        CreatedDate = new DateTime(2025, 4, 10, 17, 2, 0),
        BirthDate = new DateOnly(2007, 4, 10),
        Gender = enGender.Male
    }
);

            // Seed user roles
            modelBuilder.Entity<UserRole>().HasData(
                // Admin roles
                new UserRole { Id = 1, UserId = 1, RoleId = 1 },
                new UserRole { Id = 2, UserId = 2, RoleId = 1 },
                // Doctor roles
                new UserRole { Id = 3, UserId = 3, RoleId = 2 },
                new UserRole { Id = 4, UserId = 4, RoleId = 2 },
                new UserRole { Id = 5, UserId = 5, RoleId = 2 },
                new UserRole { Id = 6, UserId = 6, RoleId = 2 },
                new UserRole { Id = 7, UserId = 7, RoleId = 2 },
                new UserRole { Id = 8, UserId = 8, RoleId = 2 },
                new UserRole { Id = 9, UserId = 9, RoleId = 2 },
                new UserRole { Id = 10, UserId = 10, RoleId = 2 },
                new UserRole { Id = 11, UserId = 11, RoleId = 2 },
                new UserRole { Id = 12, UserId = 12, RoleId = 2 },
                new UserRole { Id = 13, UserId = 13, RoleId = 2 },
                new UserRole { Id = 14, UserId = 14, RoleId = 2 },
                new UserRole { Id = 15, UserId = 15, RoleId = 2 },
                new UserRole { Id = 16, UserId = 16, RoleId = 2 },
                new UserRole { Id = 17, UserId = 17, RoleId = 2 },
                new UserRole { Id = 18, UserId = 18, RoleId = 2 },
                new UserRole { Id = 19, UserId = 19, RoleId = 2 },
                new UserRole { Id = 20, UserId = 20, RoleId = 2 },
                // Patient role
                new UserRole { Id = 21, UserId = 21, RoleId = 3 },
                new UserRole { Id = 25, UserId = 21, RoleId = 3 },
                new UserRole { Id = 27, UserId = 21, RoleId = 3 },
                new UserRole { Id = 28, UserId = 21, RoleId = 3 },
                new UserRole { Id = 29, UserId = 21, RoleId = 3 },
                new UserRole { Id = 30, UserId = 21, RoleId = 3 },
                new UserRole { Id = 32, UserId = 21, RoleId = 3 }

            );

            // Seed appointments
            modelBuilder.Entity<Appointment>().HasData(
    new Appointment { Id = 1, DoctorId = 3, PatientId = 21, CreatedDate = new DateTime(2023, 12, 15, 9, 0, 0), AppointmentDate = new DateOnly(2025, 1, 5), StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(10, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 2, DoctorId = 4, PatientId = 21, CreatedDate = new DateTime(2023, 12, 16, 10, 0, 0), AppointmentDate = new DateOnly(2025, 1, 10), StartTime = new TimeOnly(14, 0, 0), EndTime = new TimeOnly(15, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 3, DoctorId = 5, PatientId = 21, CreatedDate = new DateTime(2023, 12, 17, 11, 0, 0), AppointmentDate = new DateOnly(2025, 1, 15), StartTime = new TimeOnly(10, 0, 0), EndTime = new TimeOnly(11, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 4, DoctorId = 6, PatientId = 21, CreatedDate = new DateTime(2023, 12, 18, 12, 0, 0), AppointmentDate = new DateOnly(2025, 1, 20), StartTime = new TimeOnly(13, 0, 0), EndTime = new TimeOnly(14, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 5, DoctorId = 7, PatientId = 21, CreatedDate = new DateTime(2025, 1, 5, 9, 0, 0), AppointmentDate = new DateOnly(2025, 2, 2), StartTime = new TimeOnly(8, 0, 0), EndTime = new TimeOnly(9, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 6, DoctorId = 8, PatientId = 21, CreatedDate = new DateTime(2025, 1, 10, 10, 0, 0), AppointmentDate = new DateOnly(2025, 2, 7), StartTime = new TimeOnly(11, 0, 0), EndTime = new TimeOnly(12, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 7, DoctorId = 9, PatientId = 21, CreatedDate = new DateTime(2025, 1, 15, 11, 0, 0), AppointmentDate = new DateOnly(2025, 5, 14), StartTime = new TimeOnly(14, 0, 0), EndTime = new TimeOnly(15, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 8, DoctorId = 10, PatientId = 21, CreatedDate = new DateTime(2025, 1, 20, 12, 0, 0), AppointmentDate = new DateOnly(2025, 2, 21), StartTime = new TimeOnly(9, 0, 0), EndTime = new TimeOnly(10, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 9, DoctorId = 11, PatientId = 21, CreatedDate = new DateTime(2025, 2, 1, 9, 0, 0), AppointmentDate = new DateOnly(2025, 3, 1), StartTime = new TimeOnly(10, 0, 0), EndTime = new TimeOnly(11, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 10, DoctorId = 12, PatientId = 21, CreatedDate = new DateTime(2025, 2, 5, 10, 0, 0), AppointmentDate = new DateOnly(2025, 3, 8), StartTime = new TimeOnly(14, 0, 0), EndTime = new TimeOnly(15, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 11, DoctorId = 13, PatientId = 21, CreatedDate = new DateTime(2025, 2, 10, 11, 0, 0), AppointmentDate = new DateOnly(2025, 3, 15), StartTime = new TimeOnly(8, 0, 0), EndTime = new TimeOnly(9, 0, 0), Status = enAppointmentStatus.Completed, GuestEmail = null },
    new Appointment { Id = 12, DoctorId = 14, PatientId = 21, CreatedDate = new DateTime(2025, 2, 15, 12, 0, 0), AppointmentDate = new DateOnly(2025, 3, 22), StartTime = new TimeOnly(12, 0, 0), EndTime = new TimeOnly(13, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 15, DoctorId = 17, PatientId = 21, CreatedDate = new DateTime(2025, 3, 10, 11, 0, 0), AppointmentDate = new DateOnly(2025, 4, 15), StartTime = new TimeOnly(12, 0, 0), EndTime = new TimeOnly(13, 0, 0), Status = enAppointmentStatus.Cancelled, GuestEmail = null },
    new Appointment { Id = 16, DoctorId = 18, PatientId = 21, CreatedDate = new DateTime(2025, 3, 15, 12, 0, 0), AppointmentDate = new DateOnly(2025, 4, 20), StartTime = new TimeOnly(16, 0, 0), EndTime = new TimeOnly(17, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 17, DoctorId = 18, PatientId = 30, CreatedDate = new DateTime(2025, 4, 1, 9, 0, 0), AppointmentDate = new DateOnly(2025, 5, 3), StartTime = new TimeOnly(10, 0, 0), EndTime = new TimeOnly(11, 0, 0), Status = enAppointmentStatus.Confirmed, GuestEmail = null },
    new Appointment { Id = 18, DoctorId = 20, PatientId = 21, CreatedDate = new DateTime(2025, 4, 5, 10, 0, 0), AppointmentDate = new DateOnly(2025, 5, 10), StartTime = new TimeOnly(14, 0, 0), EndTime = new TimeOnly(15, 0, 0), Status = enAppointmentStatus.Confirmed, GuestEmail = null },
    new Appointment { Id = 19, DoctorId = 3, PatientId = 21, CreatedDate = new DateTime(2025, 4, 10, 11, 0, 0), AppointmentDate = new DateOnly(2025, 5, 20), StartTime = new TimeOnly(8, 0, 0), EndTime = new TimeOnly(9, 0, 0), Status = enAppointmentStatus.Confirmed, GuestEmail = null },
    new Appointment { Id = 20, DoctorId = 4, PatientId = 21, CreatedDate = new DateTime(2025, 4, 15, 12, 0, 0), AppointmentDate = new DateOnly(2025, 5, 24), StartTime = new TimeOnly(12, 0, 0), EndTime = new TimeOnly(13, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 21, DoctorId = 18, PatientId = 21, CreatedDate = new DateTime(2025, 3, 15, 12, 0, 0), AppointmentDate = new DateOnly(2025, 4, 20), StartTime = new TimeOnly(17, 0, 0), EndTime = new TimeOnly(18, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 22, DoctorId = 4, PatientId = 21, CreatedDate = new DateTime(2025, 4, 15, 12, 0, 0), AppointmentDate = new DateOnly(2025, 5, 24), StartTime = new TimeOnly(17, 0, 0), EndTime = new TimeOnly(18, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 23, DoctorId = 4, PatientId = 21, CreatedDate = new DateTime(2025, 4, 15, 12, 0, 0), AppointmentDate = new DateOnly(2025, 5, 24), StartTime = new TimeOnly(20, 0, 0), EndTime = new TimeOnly(21, 0, 0), Status = enAppointmentStatus.Confirmed, GuestEmail = null },
    new Appointment { Id = 24, DoctorId = 20, PatientId = 21, CreatedDate = new DateTime(2025, 4, 5, 10, 0, 0), AppointmentDate = new DateOnly(2025, 5, 10), StartTime = new TimeOnly(15, 0, 0), EndTime = new TimeOnly(16, 0, 0), Status = enAppointmentStatus.Cancelled, GuestEmail = null },
    new Appointment { Id = 25, DoctorId = 11, PatientId = 29, CreatedDate = new DateTime(2025, 4, 9, 22, 31, 0), AppointmentDate = new DateOnly(2025, 4, 13), StartTime = new TimeOnly(20, 0, 0), EndTime = new TimeOnly(21, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 26, DoctorId = 6, PatientId = 21, CreatedDate = new DateTime(2025, 4, 9, 22, 33, 0), AppointmentDate = new DateOnly(2025, 4, 12), StartTime = new TimeOnly(22, 0, 0), EndTime = new TimeOnly(23, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 28, DoctorId = 9, PatientId = 30, CreatedDate = new DateTime(2025, 4, 10, 0, 54, 0), AppointmentDate = new DateOnly(2025, 4, 13), StartTime = new TimeOnly(6, 0, 0), EndTime = new TimeOnly(7, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 29, DoctorId = 4, PatientId = 21, CreatedDate = new DateTime(2025, 4, 10, 0, 55, 0), AppointmentDate = new DateOnly(2025, 4, 12), StartTime = new TimeOnly(5, 0, 0), EndTime = new TimeOnly(6, 0, 0), Status = enAppointmentStatus.Confirmed, GuestEmail = null },
    new Appointment { Id = 30, DoctorId = 12, PatientId = 21, CreatedDate = new DateTime(2025, 4, 10, 11, 54, 0), AppointmentDate = new DateOnly(2025, 4, 11), StartTime = new TimeOnly(14, 0, 0), EndTime = new TimeOnly(15, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null },
    new Appointment { Id = 31, DoctorId = 4, PatientId = null, CreatedDate = new DateTime(2025, 4, 10, 14, 40, 0), AppointmentDate = new DateOnly(2025, 4, 13), StartTime = new TimeOnly(15, 0, 0), EndTime = new TimeOnly(16, 0, 0), Status = enAppointmentStatus.Confirmed, GuestEmail = "amdsf@gmai.lcom" },
    new Appointment { Id = 32, DoctorId = 3, PatientId = null, CreatedDate = new DateTime(2025, 4, 10, 14, 41, 0), AppointmentDate = new DateOnly(2025, 4, 12), StartTime = new TimeOnly(15, 0, 0), EndTime = new TimeOnly(16, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = "Ahmed123@gmail.com" },
    new Appointment { Id = 33, DoctorId = 3, PatientId = null, CreatedDate = new DateTime(2025, 4, 10, 17, 52, 0), AppointmentDate = new DateOnly(2025, 4, 11), StartTime = new TimeOnly(12, 0, 0), EndTime = new TimeOnly(13, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = "amdsf@gmai.lcom" },
    new Appointment { Id = 34, DoctorId = 3, PatientId = 28, CreatedDate = new DateTime(2025, 4, 10, 19, 4, 0), AppointmentDate = new DateOnly(2025, 4, 13), StartTime = new TimeOnly(12, 0, 0), EndTime = new TimeOnly(13, 0, 0), Status = enAppointmentStatus.Pending, GuestEmail = null }
);

            #endregion

        }
    }
}
