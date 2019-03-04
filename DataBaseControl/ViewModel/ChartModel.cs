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
	}
	public class HttpHijackedDestinationModel
	{
		public string RedirectedDestination { get; set; }
		public int RedirectedCounts { get; set; }

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
	}
	public class NormalRedirectionModel
	{
		public int RedirectionCounts { get; set; }
		public List<NormalRedirectionDestinationModel> NormalResolutionDestinationModel { get; set; }
	}
	public class NormalRedirectionDestinationModel
	{
		public string RedirectToDestination { get; set; }
		public int RedirectedCounts { get; set; }
	}
}