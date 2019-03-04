using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;

namespace _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface
{
    public interface IServerRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
    }
}