using Performance_Monitoring.BusinessLayer.Dtos;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Performance_Monitoring.Repository;

namespace Performance_Monitoring.DataAccessLayer.Entities
{
	[Serializable]
	[DisplayName("PerformanceData")]
	public class PerformanceDataEntities : PersistenceEntities, IPersistenceEntities
	{
		[BsonElement("Url")]
		public string Url { get; set; }
		[BsonElement("Page_Total")]
		public int Page_Total { get; set; }
		[BsonElement("Response")]
		public decimal Response { get; set; }
		[BsonElement("PerformancesData")]
		public List<ResponseData> PerformancesData { get; set; }
	}
}
