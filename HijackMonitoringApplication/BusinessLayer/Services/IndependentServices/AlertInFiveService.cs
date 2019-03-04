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
    public class AlertInFiveService : BaseService<AlertInFiveEntities, AlertInFiveDto>, IAlertInFiveService
    {
        public IUnitOfWork<AlertInFiveEntities> _unitOfWork = new UnitOfWork<AlertInFiveEntities>();

        IAlertInFiveRepository<AlertInFiveEntities> _alertInFiveRepository;
        public AlertInFiveService()
        {
            _alertInFiveRepository = _unitOfWork.AlertInFiveRepository;
            _repository = _alertInFiveRepository;
        }
    }
}