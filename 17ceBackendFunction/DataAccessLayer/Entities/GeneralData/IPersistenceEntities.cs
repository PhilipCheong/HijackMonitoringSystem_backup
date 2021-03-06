﻿using System;

namespace _17ceBackendFunction.DataAccessLayer.Entities.GeneralData
{
    public interface IPersistenceEntities
    {
        string Id { get; set; }
        DateTime? ModifyDate { get; set; }
        DateTime? DeleteDate { get; set; }
        DateTime? CreateDate { get; set; }
        string UserId { get; set; }

    }
}