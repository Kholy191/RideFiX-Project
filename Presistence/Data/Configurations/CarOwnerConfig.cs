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
    internal class CarOwnerConfig : IEntityTypeConfiguration<CarOwner>
    {


        public void Configure(EntityTypeBuilder<CarOwner> builder)
        {
            builder.HasKey(co => co.Id);

            builder.HasOne(co => co.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(co => co.ApplicationUserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}