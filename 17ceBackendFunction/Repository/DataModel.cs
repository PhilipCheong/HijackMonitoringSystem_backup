using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17ceBackendFunction.Repository
{
	public class WebsocketData
	{
		public int txnid { get; set; }
		public int TimeOut { get; set; }
		public int[] nodetype { get; set; }
		public int type { get; set; }
		public string num { get; set; }
		public string Url { get; set; }
		public string TestType { get; set; }
		//public string Host { get; set; }
		public string UserAgent { get; set; }
		public bool GetMD5 { get; set; }
		public int MaxDown { get; set; }
		public bool AutoDecompress { get; set; }
		public string Request { get; set; }
		public int[] isps { get; set; }
		public string[] pro_ids { get; set; }
		public string[] city_ids { get; set; }
		public int[] areas { get; set; }
		public bool NoCache { get; set; }
		public int FollowLocation { get; set; }
		//public int PingCount { get; set; }
		//public int PingSize { get; set; }
	}

	public class ResultsReturned
	{
		public string DomainName { get; set; }
		public string ResolutionIp { get; set; }
		public string IpFrom { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Isp { get; set; }
		public string Redirection { get; set; }
		public string Verified { get; set; }
		public string ErrMsg { get; set; }
		public DateTime ExecutionTime { get; set; }
		public decimal NsLookupTime { get; set; }
		public decimal ConnectionTime { get; set; }
		public decimal TimeToFirstByte { get; set; }
		public decimal DownloadTime { get; set; }
		public decimal TotalTime { get; set; }

	}
}
