using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringSystem.BusinessLayer.Dtos.GeneralData
{
    public class PersistenceDtos :IPersistenceDtos
    {
        public string Id { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserId { get; set; }
        public virtual UserDto User { get; set; }

    }
}