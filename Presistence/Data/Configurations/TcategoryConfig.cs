using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Data.Configurations
{
    internal class TcategoreyConfig : IEntityTypeConfiguration<TCategory>
    {
        public void Configure(EntityTypeBuilder<TCategory> builder)
        {
            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Many-to-Many with Technician
            builder
                .HasMany(tc => tc.Technicians)
                .WithMany(t => t.TCategories)
                .UsingEntity<Dictionary<string, object>>(
                    "TechnicianCategory",
                    j => j
                        .HasOne<Technician>()
                        .WithMany()
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.NoAction),
                    j => j
                        .HasOne<TCategory>()
                        .WithMany()
                        .HasForeignKey("TCategoryId")
                        .OnDelete(DeleteBehavior.NoAction),
                    j =>
                    {
                        j.HasKey("TechnicianId", "TCategoryId");
                        j.ToTable("TechnicianCategory");
                    });
        }

    }
    }
