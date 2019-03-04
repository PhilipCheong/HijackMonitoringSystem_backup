﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface
{
    public interface IHijackingDomainRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
    }
}