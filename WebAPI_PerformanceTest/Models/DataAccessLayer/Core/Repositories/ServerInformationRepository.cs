using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories
{
	public class ServerInformationRepository<T> : Repository<T>, IServerInformationRepository<T> where T : IPersistenceEntities
	{
		public ServerInformationRepository(MongoDbContext context) : base(context)
		{
		}
	}
}