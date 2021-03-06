﻿using System;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData
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