using System;

namespace Performance_Monitoring.BusinessLayer.Dtos.GeneralData
{
    public interface IPersistenceDtos
    {
        string Id { get; set; }
        DateTime? ModifyDate { get; set; }
        DateTime? DeleteDate { get; set; }
        DateTime? CreateDate { get; set; }
        string UserId { get; set; }

    }
}