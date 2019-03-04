using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
    public interface ITFCDNserversService : IService<TFCDNserversEntities, TFCDNserverDto>
    {
    }
}
