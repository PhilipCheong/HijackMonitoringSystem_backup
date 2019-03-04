using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface
{
	public interface IPerformanceDataRepository<T> : IRepository<T> where T : IPersistenceEntities
	{
	}
}