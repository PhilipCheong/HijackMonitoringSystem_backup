using System;

namespace _17ceBackendFunction.BusinessLayer.Dtos.GeneralData
{
    public class PersistenceDtos :IPersistenceDtos
    {
        public string Id { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserId { get; set; }
    }
}