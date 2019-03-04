using System;
using System.ComponentModel;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;

namespace HijackMonitoringApplication.DataAccessLayer.Entities
{
    [Serializable]
    [DisplayName("HijackingDomain")]
    public class HijackingDomainEntities : PersistenceEntities, IPersistenceEntities
    {
		[BsonElement("Protocol")]
		public string Protocol { get; set; }
		[BsonElement("Domain")]
        public string Domain { get; set; }
        [BsonElement("DestinationIp")]
        public string DestinationIp { get; set; }
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
        [BsonElement("ProvinceToTest")]
        public string Province { get; set; }
        [BsonElement("Isp")]
        public string Isp { get; set; }
	    [BsonElement("Redirection")]
	    public string Redirection { get; set; }
		[BsonElement("LastExecuted")]
		public DateTime LastExecuted { get; set; }


	}
}