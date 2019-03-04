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
	[DisplayName("SummarizedData")]
	public class SummarizedDataEntities : PersistenceEntities, IPersistenceEntities
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
		[BsonElement("CountOver3")]
		public int CountOver3 { get; set; }
		[BsonElement("CountOver5")]
		public int CountOver5 { get; set; }
		[BsonElement("TotalTest")]
		public int TotalTest { get; set; }
		[BsonElement("CountOver10")]
		public int CountOver10 { get; set; }
		[BsonElement("HighestResponse")]
		public float HighestResponse { get; set; }
		[BsonElement("HourCategory")]
		public DateTime HourCategory { get; set; }
	}

}