using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;
using System;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
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
		public bool Summarized { get; set; }
		public DateTime ExecutionTime { get; set; }
		public string ErrMsg { get; set; }
		public decimal NsLookupTime { get; set; }
		public decimal ConnectionTime { get; set; }
		public decimal TimeToFirstByte { get; set; }
		public decimal DownloadTime { get; set; }
		public decimal TotalTime { get; set; }

	}
}