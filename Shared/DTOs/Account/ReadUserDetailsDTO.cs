using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.Account
{
    public class ReadUserDetailsDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string FaceImageUrl { get; set; }
    }
}
