using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
{
	public class ServerPerformanceService : BaseService<ServerPerformanceEntities, ServerPerformanceDto>, IServerPerformanceService
	{
		public IUnitOfWork<ServerPerformanceEntities> _unitOfWork = new UnitOfWork<ServerPerformanceEntities>();

		IServerPerformanceRepository<ServerPerformanceEntities> _serverPerformanceRepository;
		public ServerPerformanceService()
		{
			_serverPerformanceRepository = _unitOfWork.ServerPerformanceRepository;
			_repository = _serverPerformanceRepository;
		}
	}
}