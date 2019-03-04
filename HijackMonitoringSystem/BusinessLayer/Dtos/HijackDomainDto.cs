using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.BusinessLayer.Dtos.GeneralData;

namespace HijackMonitoringSystem.BusinessLayer.Dtos
{
    public class HijackDomainDto : PersistenceDtos, IPersistenceDtos
    {
        public HijackDomainDto()
        {
            DestinationIp = new List<string>();
        }
        public string Domain { get; set; }
        public List<string> DestinationIp { get; set; }
        public string CustomerID { get; set; }
        public string status { get; set; }
        public DateTime ToStartTime { get; set; }
        public DateTime ToEndTime { get; set; }
        public int Interval { get; set; }
        public string Province { get; set; }
        public string Isp { get; set; }

    }
}