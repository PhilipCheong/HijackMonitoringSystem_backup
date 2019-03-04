using Performance_Monitoring.BusinessLayer.Dtos;
using Performance_Monitoring.BusinessLayer.Services.GeneralServices;
using Performance_Monitoring.BusinessLayer.Services.IndependentServices.Interface;
using Performance_Monitoring.DataAccessLayer.Core.Context;
using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.BusinessLayer.Services.IndependentServices
{
	public class PerformanceDataService : BaseService<PerformanceDataEntities, PerformanceDataDto>, IPerformanceDataService
	{
		public IUnitOfWork<PerformanceDataEntities> _unitOfWork = new UnitOfWork<PerformanceDataEntities>();

		IPerformanceDataRepository<PerformanceDataEntities> _performanceDataRepository;

		public PerformanceDataService()
		{
			_performanceDataRepository = _unitOfWork.PerformanceDataRepository;
			_repository = _performanceDataRepository;
		}
	}
}