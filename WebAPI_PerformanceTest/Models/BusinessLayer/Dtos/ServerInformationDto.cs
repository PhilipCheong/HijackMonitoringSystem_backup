using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos.GeneralData;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Dtos
{
	public class ServerInformationDto : PersistenceDtos, IPersistenceDtos
	{
		public string ServerIp { get; set; }
		public string Province { get; set; }
		public string Isp { get; set; }
	}
}