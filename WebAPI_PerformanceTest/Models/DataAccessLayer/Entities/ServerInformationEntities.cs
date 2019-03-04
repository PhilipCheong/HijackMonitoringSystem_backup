using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Entities
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