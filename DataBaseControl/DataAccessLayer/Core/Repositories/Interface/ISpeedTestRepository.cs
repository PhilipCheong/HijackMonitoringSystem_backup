using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
    public interface ISpeedTestRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
    }
}