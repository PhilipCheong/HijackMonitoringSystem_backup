using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
	public class NormalResolutionModel
	{
		public int NormalCount { get; set; } 
		public List<NormalResolutionDestinationModel> NormalResolutionDestinationModel { get; set; }
	}
	public class NormalResolutionDestinationModel
	{
		public string Ip { get; set; }
		public int IpCounts { get; set; }
		public string Destination { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class HttpHijackedDestinationModel
	{
		public string RedirectedDestination { get; set; }
		public int RedirectedCounts { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class HttpHijackedModel
	{
		public int HttpHijackedCounts { get; set; }
		public List<HttpHijackedDestinationModel> HttpHijackedDestinationModel { get; set; }
	}
	public class DnsHijackedDestinationModel
	{
		public string HijackedToIps { get; set; }
		public string Destination { get; set; }
		public int DestinationCount { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class DnsHijackedModel
	{
		public int DnsHijackedCount { get; set; }
		public List<DnsHijackedDestinationModel> HijackedToDestinationModel { get; set; }
	}
	public class ProvinceHijackedModel
	{
		public List<ProvinceHijackedDestinationModel> ProvinceHijackedDestinationModel { get; set; }
	}
	public class ProvinceHijackedDestinationModel
	{
		public string Province { get; set; }
		public int DnsCount { get; set; }
		public int HttpCount { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class NormalRedirectionModel
	{
		public int RedirectionCounts { get; set; }
		public List<NormalRedirectionDestinationModel> NormalRedirectionDestinationModel { get; set; }
	}
	public class NormalRedirectionDestinationModel
	{
		public string RedirectToDestination { get; set; }
		public int RedirectedCounts { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class ErrorModel
	{
		public int ErrorCounts { get; set; }
		public List<ErrorDestinationModel> ErrorMessange { get; set; }
	}
	public class ErrorDestinationModel
	{
		public string ErrMsg { get; set; }
		public int ErrCount { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class ResolutionTimeModel
	{
		public List<ResolutionTimeDestinationModel> ResolutionTimeDestination {get;set;}
	}
	public class ResolutionTimeDestinationModel
	{
		public decimal NsLookupTime { get; set; }
		public decimal ConnectionTime { get; set; }
		public decimal TimeToFirstByte { get; set; }
		public decimal DownloadTime { get; set; }
		public decimal TotalTime { get; set; }
		public DateTime ExecutionTime { get; set; }

	}
	public class WaterfallTitle
	{
		public string Url { get; set; }
		public string Response { get; set; }
		public string FailedCount { get; set; }
	}
}