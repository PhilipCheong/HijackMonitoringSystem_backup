using MongoDB.Bson.Serialization.Attributes;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Monitoring.DataAccessLayer.Entities
{
	[Serializable]
	[DisplayName("ServerInformation")]
	public class ServerInformationEntities : PersistenceEntities, IPersistenceEntities
	{
		[BsonElement("ServerIp")]
		public string ServerIp { get; set; }
		[BsonElement("Province")]
		public string Province { get; set; }
		[BsonElement("Isp")]
		public string Isp { get; set; }
	}
}
