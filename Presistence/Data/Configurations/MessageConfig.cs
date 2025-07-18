using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            
            builder.Property(m => m.Text)
                   .IsRequired()
                   .HasMaxLength(1000); 

            builder.Property(m => m.SentAt)
                   .IsRequired();

            builder.Property(m => m.IsSeen)
                   .IsRequired();

           
            builder.HasOne(m => m.ApplicationUser)
                   .WithMany(m=>m.messages)
                   .HasForeignKey(m => m.ApplicationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction); 

          
        }

    }

}
