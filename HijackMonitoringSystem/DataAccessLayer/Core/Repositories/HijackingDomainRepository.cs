using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories
{
    public class HijackingDomainRespository<T> : Repository<T>, IHijackingDomainRespository<T> where T : IPersistenceEntities
    {
        public HijackingDomainRespository(MongoDbContext context) : base(context)
        {
        }
    }
}