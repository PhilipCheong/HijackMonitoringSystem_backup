using System;
using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
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