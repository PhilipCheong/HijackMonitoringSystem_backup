using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringSystem.BusinessLayer.Dtos.GeneralData
{
    public interface IPersistenceDtos
    {
        string Id { get; set; }
        DateTime? ModifyDate { get; set; }
        DateTime? DeleteDate { get; set; }
        DateTime? CreateDate { get; set; }
        string UserId { get; set; }
        UserDto User { get; set; }

    }
}