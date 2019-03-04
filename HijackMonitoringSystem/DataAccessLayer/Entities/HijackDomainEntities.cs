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
    [DisplayName("HijackingDomain")]
    public class HijackDomainEntities : PersistenceEntities, IPersistenceEntities
    {
        public HijackDomainEntities()
        {
            DestinationIp = new List<string>();
        }
        [BsonElement("Domain")]
        public string Domain { get; set; }
        [BsonElement("DestinationIp")]
        public List<string> DestinationIp { get; set; }
        [BsonElement("CustomerID")]
        public string CustomerID { get; set; }
        [BsonElement("Status")]
        public string status { get; set; }
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

    }
}