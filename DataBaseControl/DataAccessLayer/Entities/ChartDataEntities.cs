using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using HijackMonitoringApplication.ViewModel;
using MongoDB.Bson.Serialization.Attributes;

namespace HijackMonitoringApplication.DataAccessLayer.Entities
{
	[Serializable]
	[DisplayName("ChartsData")]
	public class ChartDataEntities : PersistenceEntities, IPersistenceEntities
	{
		[BsonElement("Domain")]
		public string Domain { get; set; }
		[BsonElement("DestinationIpInfo")]
		public NormalResolutionModel DestinationIpInfo { get; set; }
		[BsonElement("Isp")]
		public string Isp { get; set; }
		[BsonElement("Province")]
		public ProvinceHijackedModel Province { get; set; }
		[BsonElement("RedirectionInfo")]
		public NormalRedirectionModel RedirectionInfo { get; set; }
		[BsonElement("DnsCounts")]
		public DnsHijackedModel DnsHijackedCounts { get; set; }
		[BsonElement("HttpCounts")]
		public HttpHijackedModel HttpHijackedCounts { get; set; }
		[BsonElement("FailedCounts")]
		public int ConnectionFailed { get; set; }
	}
}