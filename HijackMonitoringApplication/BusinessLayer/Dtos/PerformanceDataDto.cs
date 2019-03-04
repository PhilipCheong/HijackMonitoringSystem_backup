using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;
using HijackMonitoringApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
{
	public class PerformanceDataDto : PersistenceDtos, IPersistenceDtos
	{
		public string Url { get; set; }
		public int Page_Total { get; set; }
		public decimal Response { get; set; }
		public string Province { get; set; }
        public DateTime TestTime { get; set; }
        public string Image { get; set; }
		public List<ResponseData> PerformancesData { get; set; }
	}
}