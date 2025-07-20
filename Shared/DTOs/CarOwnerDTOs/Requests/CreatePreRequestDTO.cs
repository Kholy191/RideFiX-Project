using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.CarOwnerDTOs.Requests
{
    public class CreatePreRequestDTO
    {
        public int categoryId { get; set; }
        public int CarOwnerId { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }

        }
    }


