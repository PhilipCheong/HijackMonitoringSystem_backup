using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface
{
	public interface IServerInformationRepository<T> : IRepository<T> where T : IPersistenceEntities
	{
	}
}