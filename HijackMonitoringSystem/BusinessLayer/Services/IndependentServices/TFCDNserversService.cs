using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.BusinessLayer.Dtos;
using HijackMonitoringSystem.BusinessLayer.Services.GeneralServices;
using HijackMonitoringSystem.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities;

namespace HijackMonitoringSystem.BusinessLayer.Services.IndependentServices
{
    public class TFCDNserversService : BaseService<TFCDNserversEntities, TFCDNserverDto>, ITFCDNserversService
    {
        public IUnitOfWork<TFCDNserversEntities> _unitOfWork = new UnitOfWork<TFCDNserversEntities>();

        IServerRespository<TFCDNserversEntities> _serverRespository;
        public TFCDNserversService()
        {
            _serverRespository = _unitOfWork.ServerRespository;
            _repository = _serverRespository;
        }

    }
}