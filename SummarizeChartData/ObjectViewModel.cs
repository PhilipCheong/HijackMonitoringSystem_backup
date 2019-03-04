using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummarizeChartData
{
	public class ObjectViewModel
	{
	}
	public class DnsObject
	{
		public string DomainName { get; set; }
		public string Isp { get; set; }
		public string DestinationIp { get; set; }
		public string Destination { get; set; }
		public int Count { get; set; }
		public DateTime ExecutionTime { get; set; }
	}

	public class HttpObject
	{
		public string DomainName { get; set; }
		public string Isp { get; set; }
		public string Redirection { get; set; }
		public int Count { get; set; }
		public DateTime ExecutionTime { get; set; }
	}

	public class NormalObject
	{
		public string DomainName { get; set; }
		public string Isp { get; set; }
		public string DestinationIp { get; set; }
		public string Destination { get; set; }
		public int Count { get; set; }
		public DateTime ExecutionTime { get; set; }
	}

	public class RedirectionObject
	{
		public string DomainName { get; set; }
		public string Isp { get; set; }
		public string Redirection { get; set; }
		public int Count { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class ProvinceObject
	{
		public string Domain { get; set; }
		public string Isp { get; set; }
		public string Province { get; set; }
		public int DnsCounts { get; set; }
		public int HttpCount { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class ErrorObject
	{
		public string Domain { get; set; }
		public string Isp { get; set; }
		public int ErrorCount { get; set; }
		public string ErrMsg { get; set; }
		public DateTime ExecutionTime { get; set; }
	}
	public class ResolutionTimeObject
	{
		public string Domain { get; set; }
		public string Isp { get; set; }
		public decimal NsLookupTime { get; set; }
		public decimal ConnectionTime { get; set; }
		public decimal TimeToFirstByte { get; set; }
		public decimal DownloadTime { get; set; }
		public decimal TotalTime { get; set; }
		public int ResolutionCount { get; set; }
		public DateTime ExecutionTime { get; set; }
	}

}
