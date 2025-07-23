using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SharedData.DTOs.Account
{
    public class RegisterStep2Dto
    {
        public string Email { get; set; }
        [Required]
        public IFormFile IdentityImage { get; set; }

        [Required]
        public IFormFile FaceImage { get; set; }
    }
}
