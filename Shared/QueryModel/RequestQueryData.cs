using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.QueryModel
{
    public class RequestQueryData
    {

        //private int? _take;
        //private int? _pageIndex;
        public bool? IsCompleted { get; set; }
        public RequestState? CallState { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? CategoryId { get; set; }
        public int? CarOwnerId { get; set; }
        public int? TechnicainId { get; set; }

    }
}
