using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
	public class ResponseData
	{
		public decimal RequestTime { get; set; }
		public decimal ResponseTime { get; set; }
		public string Url { get; set; }
		public string MimeType { get; set; }
		public string RemoteIPAddress { get; set; }
		public string Protocol { get; set; }
		public string Status { get; set; }
		public decimal Dns { get; set; }
		public decimal Connect { get; set; }
		public decimal SSL { get; set; }
		public decimal Send { get; set; }
		public decimal TTFB { get; set; }
		public decimal Download { get; set; }

	}
}