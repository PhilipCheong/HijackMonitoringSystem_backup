using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.BusinessLayer.Dtos.GeneralData;

namespace HijackMonitoringSystem.BusinessLayer.Dtos
{
    public class HijackingTestResultDto : PersistenceDtos, IPersistenceDtos
    {
        public string Domain { get; set; }
        public string DestinationIp { get; set; }
        public string Location { get; set; }
        public string Province { get; set; }
        public string Isp { get; set; }
        public string Destination { get; set; }
        public string Redirection { get; set; }
        public string Verified { get; set; }

    }
}