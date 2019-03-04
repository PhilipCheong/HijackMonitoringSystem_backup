using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories
{
    public class SpeedTestRepository<T> : Repository<T>, ISpeedTestRespository<T> where T : IPersistenceEntities
    {
        public SpeedTestRepository(MongoDbContext context) : base(context)
        {

        }
    }
}