using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
	public class ServerPerformanceRepository<T> : Repository<T>, IServerPerformanceRepository<T> where T : IPersistenceEntities
	{
		public ServerPerformanceRepository(MongoDbContext context) : base(context)
		{
		}
    }
}