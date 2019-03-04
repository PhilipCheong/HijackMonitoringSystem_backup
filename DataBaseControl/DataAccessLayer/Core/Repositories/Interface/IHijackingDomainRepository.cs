using System.Collections.Generic;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
    public interface IHijackingDomainRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
        List<HijackingDomainEntities> GetAllDomainWithCustomerId(string customerId);
    }
}