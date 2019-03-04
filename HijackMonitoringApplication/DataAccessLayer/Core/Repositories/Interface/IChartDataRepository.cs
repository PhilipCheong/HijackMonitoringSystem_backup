using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
	public interface IChartDataRepository<T> : IRepository<T> where T : IPersistenceEntities
	{
		List<ChartDataEntities> GetResultByHour(DateTime lastQuery);
		List<ChartDataEntities> SingleDomainLineChart(string domainName, DateTime startDate, DateTime endDate);
	}
}