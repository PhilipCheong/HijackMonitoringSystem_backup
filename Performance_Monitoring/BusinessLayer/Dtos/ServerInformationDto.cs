using Performance_Monitoring.BusinessLayer.Dtos.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Monitoring.BusinessLayer.Dtos
{
	public class ServerInformationDto : PersistenceDtos, IPersistenceDtos
	{
		public string ServerIp { get; set; }
		public string Province { get; set; }
		public string Isp { get; set; }
	}
}
