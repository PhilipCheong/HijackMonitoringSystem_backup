using System;
using System.Collections.Generic;
using System.ComponentModel;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace HijackMonitoringApplication.DataAccessLayer.Entities
{
    [Serializable]
    [DisplayName("User")]
    public class UserEntities : PersistenceEntities, IPersistenceEntities
    {
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Account")]
        public string Account { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("Type")]
        public string Type { get; set; }
        [BsonElement("Remark")]
        public string Remark { get; set; }
        [BsonElement("Roles")]
        public List<string> Roles { get; set; }
        [BsonElement("CustomerId")]
        public string CustomerId { get; set; }
        [BsonElement("DomainList")]
        public virtual HijackingDomainEntities DomainList { get; set; }

    }

}