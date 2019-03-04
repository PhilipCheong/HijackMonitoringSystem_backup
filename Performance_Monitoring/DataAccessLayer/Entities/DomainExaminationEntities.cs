using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.DataAccessLayer.Entities
{
	[Serializable]
	[DisplayName("DomainExamination")]
	public class DomainExaminationEntities : PersistenceEntities, IPersistenceEntities
	{
		[BsonElement("Protocol")]
		public string Protocol { get; set; }
		[BsonElement("Domain")]
		public string Domain { get; set; }
		[BsonElement("CustomerID")]
		public string CustomerId { get; set; }
		[BsonElement("Status")]
		public int Status { get; set; }
		[BsonElement("ToStartTime")]
		public DateTime ToStartTime { get; set; }
		[BsonElement("ToEndTime")]
		public DateTime ToEndTime { get; set; }
		[BsonElement("Interval")]
		public int Interval { get; set; }
		[BsonElement("BrowserType")]
		public string BrowserType { get; set; }
		[BsonElement("TestType")]
		public string TestType { get; set; }
		[BsonElement("LastExecuted")]
		public DateTime LastExecuted { get; set; }
	}
}