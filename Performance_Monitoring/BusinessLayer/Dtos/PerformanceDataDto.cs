using Performance_Monitoring.BusinessLayer.Dtos.GeneralData;
using Performance_Monitoring.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.BusinessLayer.Dtos
{
	public class PerformanceDataDto : PersistenceDtos, IPersistenceDtos
	{
		public string Url { get; set; }
		public int Page_Total { get; set; }
		public decimal Response { get; set; }
		public List<ResponseData> PerformancesData { get; set; }
	}
}