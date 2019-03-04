using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
	public class DomainExaminationRepository<T> : Repository<T>, IDomainExaminationRepository<T> where T : IPersistenceEntities
	{
		public DomainExaminationRepository(MongoDbContext context) : base(context)
		{
		}
	}
}