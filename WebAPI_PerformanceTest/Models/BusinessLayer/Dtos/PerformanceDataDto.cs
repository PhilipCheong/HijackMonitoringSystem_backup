using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos.GeneralData;
using WebAPI_PerformanceTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Dtos
{
	public class PerformanceDataDto : PersistenceDtos, IPersistenceDtos
	{
		public string Url { get; set; }
		public int Page_Total { get; set; }
		public string Province { get; set; }
		public decimal Response { get; set; }
        public DateTime TestTime { get; set; }
        public string Image { get; set; }
		public List<ResponseData> PerformancesData { get; set; }
	}
}