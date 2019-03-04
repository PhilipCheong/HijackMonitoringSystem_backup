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
    [DisplayName("Server")]
    public class TFCDNserversEntities :PersistenceEntities, IPersistenceEntities
    {
        [BsonElement("GroupName")]
        public string GroupName { get; set; }
        [BsonElement("ServerCname")]
        public string ServerCname { get; set; }
        [BsonElement("BandwidthLimitation")]
        public string BandwidthLimitation { get; set; }
        [BsonElement("ServerIp")]
        public string ServerIp { get; set; }

    }
}