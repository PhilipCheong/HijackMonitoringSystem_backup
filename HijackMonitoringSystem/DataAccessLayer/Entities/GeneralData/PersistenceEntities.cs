using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData
{
    public class PersistenceEntities : IPersistenceEntities
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonElement("ModifyDate")]
        public DateTime? ModifyDate { get; set; }
        [BsonElement("DeleteDate")]
        public DateTime? DeleteDate { get; set; }
        [BsonElement("CreateDate")]
        public DateTime? CreateDate { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }

    }
}