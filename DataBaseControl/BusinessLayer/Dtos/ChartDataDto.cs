using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;
using HijackMonitoringApplication.ViewModel;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
{
	public class ChartDataDto : PersistenceDtos, IPersistenceDtos
	{
		public string Domain { get; set; }
		public NormalResolutionModel DestinationIpInfo { get; set; }
		public ProvinceHijackedModel Province { get; set; }
		public string Isp { get; set; }
		public NormalRedirectionModel RedirectionInfo { get; set; }
		public DnsHijackedModel DnsHijackedCounts { get; set; }
		public HttpHijackedModel HttpHijackedCounts { get; set; }
		public int ConnectionFailed { get; set; }
	}
}