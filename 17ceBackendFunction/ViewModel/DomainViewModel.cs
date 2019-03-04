using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _17ceBackendFunction.ViewModel
{
    public class DomainViewModel
    {
		public string Id { get; set; }
        public string Domain { get; set; }
		public string Protocol { get; set; }
        public string DestinationIp { get; set; }
        public string CustomerID { get; set; }
        public string Status { get; set; }
        public string ToStartTime { get; set; }
        public string ToEndTime { get; set; }
        public int Interval { get; set; }
        public string CheckedProvince { get; set; }
        public string CheckedIsp { get; set; }

    }
}