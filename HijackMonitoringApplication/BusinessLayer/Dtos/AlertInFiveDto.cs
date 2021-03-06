﻿using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
{
    public class AlertInFiveDto : PersistenceDtos, IPersistenceDtos
    {
        public string ServerCName { get; set; }
        public string Province { get; set; }
        public string Isp { get; set; }
        public string PingResults { get; set; }
        public string PingAverage { get; set; }
        public string AlertInfo { get; set; }
    }
}