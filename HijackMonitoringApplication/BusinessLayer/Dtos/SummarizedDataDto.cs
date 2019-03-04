using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;
using System;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
{
	public class SummarizedDataDto : PersistenceDtos, IPersistenceDtos
	{
		public string ServerCName { get; set; }
		public string Province { get; set; }
		public string Isp { get; set; }
		public string PingResults { get; set; }
		public string PingAverage { get; set; }
		public float DownloadTime { get; set; }
		public int CountOver3 { get; set; }
		public int CountOver5 { get; set; }
		public int CountOver10 { get; set; }
		public int TotalTest { get; set; }
		public float HighestResponse { get; set; }
		public DateTime HourCategory { get; set; }
	}
}