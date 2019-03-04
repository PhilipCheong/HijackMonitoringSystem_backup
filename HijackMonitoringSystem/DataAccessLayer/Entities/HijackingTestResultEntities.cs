using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson.Serialization.Attributes;

namespace HijackMonitoringSystem.DataAccessLayer.Entities
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
    }
}