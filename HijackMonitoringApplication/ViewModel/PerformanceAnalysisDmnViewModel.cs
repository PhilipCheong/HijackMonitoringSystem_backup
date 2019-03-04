using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
	public class PerformanceAnalysisDmnViewModel
	{
		public string Id { get; set; }
		public string Protocol { get; set; }
		public string Domain { get; set; }
		public string CustomerId { get; set; }
		public int Status { get; set; }
		public DateTime ToStartTime { get; set; }
		public DateTime ToEndTime { get; set; }
		public int Interval { get; set; }
		public string BrowserType { get; set; }
		public string TestType { get; set; }
	}
}