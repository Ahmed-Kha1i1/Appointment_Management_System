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

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            // Properties
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.CreatedDate)
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.UseTptMappingStrategy();
            builder.HasMany(u => u.Roles)
                   .WithMany(r => r.Users)
                   .UsingEntity<UserRole>(
                       j => j.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId),
                       j => j.HasOne(ur => ur.User).WithMany().HasForeignKey(ur => ur.UserId),
                       j => j.ToTable("UserRoles")
                   );

        }
    }
}
