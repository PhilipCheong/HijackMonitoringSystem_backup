using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.BusinessLayer.Dtos.GeneralData;

namespace HijackMonitoringSystem.BusinessLayer.Dtos
{
    public class TFCDNserverDto : PersistenceDtos, IPersistenceDtos
    {
        public string GroupName { get; set; }
        public string ServerCname { get; set; }
        public string BandwidthLimitation { get; set; }
        public string ServerIp { get; set; }

    }
}