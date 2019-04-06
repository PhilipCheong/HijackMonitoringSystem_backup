using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
	public interface IServerPerformanceService : IService<ServerPerformanceEntities, ServerPerformanceDto>
	{
    }
}