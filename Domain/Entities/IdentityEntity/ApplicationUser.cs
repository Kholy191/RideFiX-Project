using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string IdentityImageUrl { get; set; }
        public string FaceImageUrl { get; set; }
    }
}
