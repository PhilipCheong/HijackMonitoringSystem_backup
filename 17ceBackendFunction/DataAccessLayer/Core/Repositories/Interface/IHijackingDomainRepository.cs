using System.Collections.Generic;
using _17ceBackendFunction.DataAccessLayer.Entities;
using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;

namespace _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface
{
    public interface IHijackingDomainRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
        List<HijackingDomainEntities> GetAllDomainWithCustomerId(string customerId);
    }
}