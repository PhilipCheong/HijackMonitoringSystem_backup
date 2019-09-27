using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using HijackMonitoringApplication.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Entities
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
		[BsonElement("Province")]
		public string Province { get; set; }
        [BsonElement("TestTime")]
        public DateTime TestTime { get; set; }
        [BsonElement("Image")]
        public string Image { get; set; }
        [BsonElement("PerformancesData")]
		public List<ResponseData> PerformancesData { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }

    }
}
