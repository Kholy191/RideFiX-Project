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
    internal class ChatSessionConfig : IEntityTypeConfiguration<ChatSession>
    {
        public void Configure(EntityTypeBuilder<ChatSession> builder)
        {
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.StartAt)
                   .IsRequired();

            builder.Property(cs => cs.IsClosed)
                   .IsRequired();

            builder.HasOne(cs => cs.CarOwner)
                   .WithMany(co => co.ChatSessions)
                   .HasForeignKey(cs => cs.CarOwnerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(cs => cs.Technician)
                   .WithMany(t => t.ChatSessions)
                   .HasForeignKey(cs => cs.TechnicianId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
