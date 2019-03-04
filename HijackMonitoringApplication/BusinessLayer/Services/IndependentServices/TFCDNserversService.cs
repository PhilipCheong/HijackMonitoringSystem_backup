using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
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