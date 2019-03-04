using HijackMonitoringApplication.BusinessLayer.Dtos;
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
	public class SummarizedDataService : BaseService<SummarizedDataEntities, SummarizedDataDto>, ISummarizedDataService
	{
		public IUnitOfWork<SummarizedDataEntities> _unitOfWork = new UnitOfWork<SummarizedDataEntities>();

		ISummarizedDataRepository<SummarizedDataEntities> _summarizedDataRepository;
		public SummarizedDataService()
		{
			_summarizedDataRepository = _unitOfWork.SummarizedDataRepository;
			_repository = _summarizedDataRepository;
		}

		public List<SummarizedDataDto> GetQueryResult(string serverCName, DateTime start, DateTime end)
		{
			var data = _summarizedDataRepository.SingleDomainLineChart(serverCName, start, end);
			List<SummarizedDataDto>
				returnData = new List<SummarizedDataDto>(); // new List<Activator.CreateInstance<Dto>> ();
			foreach (var d in data)
			{
				SummarizedDataDto temp = Activator.CreateInstance<SummarizedDataDto>();
				Mapping.MapProp(d, temp);
				returnData.Add(temp);
			}

			return returnData;
		}

	}
}