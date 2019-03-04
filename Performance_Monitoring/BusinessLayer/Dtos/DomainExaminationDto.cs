using Performance_Monitoring.BusinessLayer.Dtos.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.BusinessLayer.Dtos
{
	public class DomainExaminationDto : PersistenceDtos, IPersistenceDtos
	{
		public string Protocol { get; set; }
		public string Domain { get; set; }
		public string CustomerId { get; set; }
		public int Status { get; set; }
		public DateTime ToStartTime { get; set; }
		public DateTime ToEndTime { get; set; }
		public int Interval { get; set; }
		public string BrowserType { get; set; }
		public string TestType { get; set; }
		public DateTime LastExecuted { get; set; }
	}
}
