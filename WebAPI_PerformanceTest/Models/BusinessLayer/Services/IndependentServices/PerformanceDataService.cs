using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.GeneralServices;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices
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