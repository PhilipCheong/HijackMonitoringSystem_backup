using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories
{
	public class DomainExaminationRepository<T> : Repository<T>, IDomainExaminationRepository<T> where T : IPersistenceEntities
	{
		public DomainExaminationRepository(MongoDbContext context) : base(context)
		{
		}
	}
}