using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer
{
	public class ServerPerformanceDto : PersistenceDtos, IPersistenceDtos
	{
		public string ServerCName { get; set; }
		public string Province { get; set; }
		public string Isp { get; set; }
		public string PingResults { get; set; }
		public string PingAverage { get; set; }
		public float DownloadTime { get; set; }
        public int CountOnOne { get; set; }
        public int CountOnTwo { get; set; }
        public int CountOnThree { get; set; }
        public int ContinuosFive { get; set; }
        public string AlertDetail { get; set; }

    }
}