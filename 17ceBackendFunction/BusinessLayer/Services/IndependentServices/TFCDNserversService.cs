using _17ceBackendFunction.BusinessLayer.Dtos;
using _17ceBackendFunction.BusinessLayer.Services.GeneralServices;
using _17ceBackendFunction.BusinessLayer.Services.IndependentServices.Interface;
using _17ceBackendFunction.DataAccessLayer.Core.Context;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities;

namespace _17ceBackendFunction.BusinessLayer.Services.IndependentServices
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