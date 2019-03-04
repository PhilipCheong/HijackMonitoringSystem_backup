using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;

namespace Performance_Monitoring.DataAccessLayer.Core.Context
{
    public interface IUnitOfWork <T> where T : IPersistenceEntities
    {
		IPerformanceDataRepository<T> PerformanceDataRepository { get; }
		IDomainExaminationRepository<T> DomainExaminationRepository { get; }
		IServerInformationRepository<T> ServerInformationRepository { get; }
	}
}