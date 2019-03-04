using Performance_Monitoring.BusinessLayer.Dtos;
using Performance_Monitoring.BusinessLayer.Services.GeneralServices;
using Performance_Monitoring.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.BusinessLayer.Services.IndependentServices.Interface
{
	public interface IPerformanceDataService : IService<PerformanceDataEntities, PerformanceDataDto>
	{
	}
}