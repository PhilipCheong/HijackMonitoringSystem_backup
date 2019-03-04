using System;
using System.ComponentModel;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;

namespace HijackMonitoringApplication.DataAccessLayer.Entities
{
    [Serializable]
    [DisplayName("HijackingTestResult")]

    public class HijackingTestResultEntities : PersistenceEntities , IPersistenceEntities
    {
        [BsonElement("Domain")]
        public string Domain { get; set; }
        [BsonElement("DestinationIp")]
        public string DestinationIp { get; set; }
        [BsonElement("Location")]
        public string Location { get; set; }
        [BsonElement("Province")]
        public string Province { get; set; }
        [BsonElement("Isp")]
        public string Isp { get; set; }
        [BsonElement("Destination")]
        public string Destination { get; set; }
        [BsonElement("Redirection")]
        public string Redirection { get; set; }
        [BsonElement("Verified")]
        public string Verified { get; set; }
		[BsonElement("Summarized")]
		public bool Summarized { get; set; }
    }
}