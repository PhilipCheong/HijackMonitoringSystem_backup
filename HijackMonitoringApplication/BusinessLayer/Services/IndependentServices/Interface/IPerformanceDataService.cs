using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
	public interface IPerformanceDataService : IService<PerformanceDataEntities, PerformanceDataDto>
	{
        List<PerformanceDataDto> FindWithoutImage(string domainName, DateTime startDate, DateTime endDate);
        List<PerformanceDataDto> FindWithoutImage(string domainName, DateTime startDate);
        List<PerformanceDataDto> FindWithoutImage(DateTime startDate);
    }
}