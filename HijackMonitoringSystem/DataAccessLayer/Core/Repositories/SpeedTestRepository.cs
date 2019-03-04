using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories
{
    public class SpeedTestRepository<T> : Repository<T>, ISpeedTestRespository<T> where T : IPersistenceEntities
    {
        public SpeedTestRepository(MongoDbContext context) : base(context)
        {

        }
    }
}