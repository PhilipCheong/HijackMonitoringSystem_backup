using System.Collections.Generic;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
    public interface IUserRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
        List<UserEntities> LoginProcess(string userName, string passWord);
        List<string> GetAllCustomerId();
    }
}