using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Repository;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Entities
{
	[Serializable]
	[DisplayName("PerformanceData")]
	public class PerformanceDataEntities : PersistenceEntities, IPersistenceEntities
	{
		[BsonElement("Url")]
		public string Url { get; set; }
		[BsonElement("Page_Total")]
		public int Page_Total { get; set; }
		[BsonElement("Province")]
		public string Province { get; set; }
		[BsonElement("Response")]
		public decimal Response { get; set; }
        [BsonElement("TestTime")]
        public DateTime TestTime { get; set; }
        [BsonElement("Image")]
        public string Image { get; set; }
        [BsonElement("PerformancesData")]
		public List<ResponseData> PerformancesData { get; set; }
	}
}
