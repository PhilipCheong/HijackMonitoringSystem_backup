using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
{
	public class ChartDataService : BaseService<ChartDataEntities, ChartDataDto>, IChartDataService
	{
		public IUnitOfWork<ChartDataEntities> _unitOfWork = new UnitOfWork<ChartDataEntities>();

		IChartDataRepository<ChartDataEntities> _chartDataRepository;
		public ChartDataService()
		{
			_chartDataRepository = _unitOfWork.ChartDataRepository;
			_repository = _chartDataRepository;
		}
		public List<ChartDataDto> GetResultByHour(DateTime lastQuery)
		{
			var data = _chartDataRepository.GetResultByHour(lastQuery);
			List<ChartDataDto>
				returnData = new List<ChartDataDto>(); // new List<Activator.CreateInstance<Dto>> ();
			foreach (var d in data)
			{
				ChartDataDto temp = Activator.CreateInstance<ChartDataDto>();
				Mapping.MapProp(d, temp);
				returnData.Add(temp);
			}

			return returnData;
		}
		public List<ChartDataDto> SingleDomainLineChart(string domain, DateTime start, DateTime end)
		{
			var data = _chartDataRepository.SingleDomainLineChart(domain, start, end);
			List<ChartDataDto>
				returnData = new List<ChartDataDto>(); // new List<Activator.CreateInstance<Dto>> ();
			foreach (var d in data)
			{
				ChartDataDto temp = Activator.CreateInstance<ChartDataDto>();
				Mapping.MapProp(d, temp);
				returnData.Add(temp);
			}

			return returnData;
		}


	}
}