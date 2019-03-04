using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
    public class AlertInFiveRepository<T> : Repository<T>, IAlertInFiveRepository<T> where T : IPersistenceEntities
    {
        public AlertInFiveRepository(MongoDbContext context) : base(context)
        {
        }
    }
}