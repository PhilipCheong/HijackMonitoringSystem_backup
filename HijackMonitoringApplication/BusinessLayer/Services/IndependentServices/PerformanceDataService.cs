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
    public class PerformanceDataService : BaseService<PerformanceDataEntities, PerformanceDataDto>, IPerformanceDataService
    {
        public IUnitOfWork<PerformanceDataEntities> _unitOfWork = new UnitOfWork<PerformanceDataEntities>();

        IPerformanceDataRepository<PerformanceDataEntities> _performanceDataRepository;

        public PerformanceDataService()
        {
            _performanceDataRepository = _unitOfWork.PerformanceDataRepository;
            _repository = _performanceDataRepository;
        }
        public List<PerformanceDataDto> FindWithoutImage(string domainName, DateTime startDate, DateTime endDate)
        {
            var data = _performanceDataRepository.FindWithoutImageAsync(domainName, startDate, endDate);
            List<PerformanceDataDto> returnData = new List<PerformanceDataDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                PerformanceDataDto temp = Activator.CreateInstance<PerformanceDataDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }
        public List<PerformanceDataDto> FindWithoutImage(string domainName, DateTime startDate)
        {
            var data = _performanceDataRepository.FindWithoutImageAsync(domainName, startDate);
            List<PerformanceDataDto> returnData = new List<PerformanceDataDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                PerformanceDataDto temp = Activator.CreateInstance<PerformanceDataDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }
        public List<PerformanceDataDto> FindWithoutImage(DateTime startDate)
        {
            var data = _performanceDataRepository.FindWithoutImageAsync(startDate);
            List<PerformanceDataDto> returnData = new List<PerformanceDataDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                PerformanceDataDto temp = Activator.CreateInstance<PerformanceDataDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }

    }
}