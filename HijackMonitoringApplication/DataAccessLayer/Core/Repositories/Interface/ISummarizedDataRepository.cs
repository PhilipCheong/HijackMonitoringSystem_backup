using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
	public interface ISummarizedDataRepository<T> : IRepository<T> where T : IPersistenceEntities
	{
		List<SummarizedDataEntities> SingleDomainLineChart(string serverName, DateTime startDate, DateTime endDate);
	}
}