using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Context
{
    public interface IUnitOfWork <T> where T : IPersistenceEntities
    {
        ISpeedTestRespository<T> SpeedTestRespository { get; }
        IHijackingTestResultRespository<T> HijackingTestResultRespository { get; }
        IHijackingDomainRespository<T> HijackingDomainRespository { get; }
        IUserRespository<T> UserRespository { get; }
        IServerRespository<T> ServerRespository { get; }
	    IChartDataRepository<T> ChartDataRepository { get; }

	}
}