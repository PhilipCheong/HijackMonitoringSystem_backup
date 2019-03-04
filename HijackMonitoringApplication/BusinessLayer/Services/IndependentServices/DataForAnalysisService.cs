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
    public class DataForAnalysisService : BaseService<DataForAnalysisEntities, DataForAnalysisDto>, IDataForAnalysisService
    {
        public IUnitOfWork<DataForAnalysisEntities> _unitOfWork = new UnitOfWork<DataForAnalysisEntities>();

        IDataForAnalysisRepository<DataForAnalysisEntities> _dataForAnalysisRepository;
        public DataForAnalysisService()
        {
            _dataForAnalysisRepository = _unitOfWork.DataForAnalysisRepository;
            _repository = _dataForAnalysisRepository;
        }
        public List<DataForAnalysisDto> GetOneDayData(DateTime startDate, DateTime endDate)
        {
            var data = _dataForAnalysisRepository.GetOneDayData(startDate, endDate);
            List<DataForAnalysisDto> returnData = new List<DataForAnalysisDto>(); // new List<Activator.CreateInstance<Dto>> ();

            foreach (var d in data)
            {
                DataForAnalysisDto temp = Activator.CreateInstance<DataForAnalysisDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }
    }
}