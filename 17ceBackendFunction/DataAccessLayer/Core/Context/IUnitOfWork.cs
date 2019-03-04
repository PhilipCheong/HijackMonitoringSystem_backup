using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;

namespace _17ceBackendFunction.DataAccessLayer.Core.Context
{
    public interface IUnitOfWork <T> where T : IPersistenceEntities
    {
        IHijackingTestResultRespository<T> HijackingTestResultRespository { get; }
        IHijackingDomainRespository<T> HijackingDomainRespository { get; }
        IServerRespository<T> ServerRespository { get; }
	}
}