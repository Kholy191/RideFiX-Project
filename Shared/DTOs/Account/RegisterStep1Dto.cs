using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SharedData.DTOs.Account
{
    public class RegisterStep1Dto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(14, MinimumLength = 14)]
        public string SSN { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Gender { get; set; }

       

        [Required]
        [RegularExpression("^(CarOwner|Technician)$", ErrorMessage = "Role must be either 'CarOwner' or 'Technician'")]
        public string Role { get; set; }


        public TimeOnly? StartWorking { get; set; }

        public TimeOnly? EndWorking { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }
    }
}
