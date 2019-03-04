using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories
{
    public class UserRespository<T> : Repository<T>, IUserRespository<T> where T : IPersistenceEntities
    {
        public UserRespository(MongoDbContext context) : base(context)
        {
        }
    }
}