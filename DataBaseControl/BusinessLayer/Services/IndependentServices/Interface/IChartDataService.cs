using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
	public interface IChartDataService : IService<ChartDataEntities, ChartDataDto>
	{
		List<ChartDataDto> GetResultByHour(DateTime lastQuery);
		List<ChartDataDto> SingleDomainLineChart(string domain, DateTime start, DateTime end);
	}
}