using System.Collections.Generic;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
    public interface IHijackingDomainService : IService<HijackingDomainEntities, HijackingDomainDto>
    {
        List<HijackingDomainDto> GetAllDomainByCustomerId(string customerId);
    }
}