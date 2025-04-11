using AMS.Doman.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMS.Doman.Common.Enum;

namespace AMS.Persistence.Configurations
{

    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(a => a.Gender)
                .HasColumnType("int")
                .HasComment("0-Male, 1-Female")
                .HasConversion(
                    s => (int)s, // Convert enum to int
                    s => (enGender)s // Convert int to enum
                )
                .IsRequired();
            // Table name
            builder.ToTable("Patients");
        }
    }
}
