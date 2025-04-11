using AMS.Doman.Common.Enum;
using AMS.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Doman.Configurations
{

    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // Properties
            builder.Property(a => a.CreatedDate)
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.Property(a => a.GuestEmail)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.EndTime)
                .IsRequired();

            builder.HasIndex(a => a.AppointmentDate)
                .HasDatabaseName("IX_Appointment_Date");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasColumnType("int")
                .HasComment("0-Pending, 1-Confirmed, 2-Completed, 3-Cancelled")
                .HasConversion(
                    s => (int)s, // Convert enum to int
                    s => (enAppointmentStatus)s // Convert int to enum
                )
                .IsRequired();

            // Relationships
            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
