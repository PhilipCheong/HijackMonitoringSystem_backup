using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;

namespace WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context
{
    public interface IUnitOfWork <T> where T : IPersistenceEntities
    {
		IPerformanceDataRepository<T> PerformanceDataRepository { get; }
		IDomainExaminationRepository<T> DomainExaminationRepository { get; }
		IServerInformationRepository<T> ServerInformationRepository { get; }
	}
}