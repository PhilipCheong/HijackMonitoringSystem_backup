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
    [DisplayName("SpeedTestResult")]
    public class SpeedTestResultEntities : PersistenceEntities, IPersistenceEntities
    {
        [BsonElement("ExecutedDate")]
        public DateTime ExecutedDate { get; set; }
        [BsonElement("ServerCName")]
        public string ServerCName { get; set; }
        [BsonElement("Location")]
        public string Location { get; set; }
        [BsonElement("Isp")]
        public string Isp { get; set; }
        [BsonElement("TotalTime")]
        public float TotalTime { get; set; }
    }
}