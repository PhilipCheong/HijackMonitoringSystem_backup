using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.BusinessLayer.Dtos.GeneralData;

namespace HijackMonitoringSystem.BusinessLayer.Dtos
{
    public class SpeedTestResultDto : PersistenceDtos, IPersistenceDtos
    {
        public DateTime ExecutedDate { get; set; }
        public string ServerCName { get; set; }
        public string Location { get; set; }
        public string Isp { get; set; }
        public float TotalTime { get; set; }
    }
}