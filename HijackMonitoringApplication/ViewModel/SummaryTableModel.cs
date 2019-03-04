using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.ViewModel
{
	public class SummaryTableModel
	{
		public string Server { get; set; }
		public List<string> AverageCount { get; set; }
	}
}