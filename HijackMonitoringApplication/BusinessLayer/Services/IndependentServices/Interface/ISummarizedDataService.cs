using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
	public interface ISummarizedDataService : IService<SummarizedDataEntities, SummarizedDataDto>
	{
		List<SummarizedDataDto> GetQueryResult(string serverCName, DateTime start, DateTime end);
	}
}