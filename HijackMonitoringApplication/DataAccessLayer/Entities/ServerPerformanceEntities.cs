using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Entities
{
	[Serializable]
	[DisplayName("PerformanceResults")]
	public class ServerPerformanceEntities : PersistenceEntities, IPersistenceEntities
	{
		[BsonElement("ServerCName")]
		public string ServerCName { get; set; }
		[BsonElement("Province")]
		public string Province { get; set; }
		[BsonElement("Isp")]
		public string Isp { get; set; }
		[BsonElement("PingResults")]
		public string PingResults { get; set; }
		[BsonElement("PingAverage")]
		public string PingAverage { get; set; }
		[BsonElement("DownloadTime")]
		public float DownloadTime { get; set; }
        [BsonElement("CountOnOne")]
        public int CountOnOne { get; set; }
        [BsonElement("CountOnTwo")]
        public int CountOnTwo { get; set; }
        [BsonElement("CountOnThree")]
        public int CountOnThree { get; set; }
        [BsonElement("ContinuosFive")]
        public int ContinuosFive { get; set; }
        [BsonElement("AlertDetail")]
        public string AlertDetail { get; set; }

    }
}