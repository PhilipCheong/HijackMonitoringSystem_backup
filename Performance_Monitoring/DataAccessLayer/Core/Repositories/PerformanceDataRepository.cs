using Performance_Monitoring.DataAccessLayer.Core.Context;
using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.DataAccessLayer.Core.Repositories
{
	public class PerformanceDataRepository<T> : Repository<T>, IPerformanceDataRepository<T> where T : IPersistenceEntities
	{
		public PerformanceDataRepository(MongoDbContext context) : base(context)
		{
		}
	}
}