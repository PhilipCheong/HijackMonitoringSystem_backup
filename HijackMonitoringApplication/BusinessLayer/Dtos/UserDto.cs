using System.Collections.Generic;
using HijackMonitoringApplication.BusinessLayer.Dtos.GeneralData;

namespace HijackMonitoringApplication.BusinessLayer.Dtos
{
    public class UserDto : PersistenceDtos, IPersistenceDtos
    {
        public string Email { get; set; }
        public string Account { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public List<string> Roles { get; set; }
        public string CustomerId { get; set; }
        public virtual HijackingDomainDto DomainList { get; set; }
    }
}