using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface
{
	public interface IDomainExaminationRepository<T> : IRepository<T> where T : IPersistenceEntities
	{
	}
}