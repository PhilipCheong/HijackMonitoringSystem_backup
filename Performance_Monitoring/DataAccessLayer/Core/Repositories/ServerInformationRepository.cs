using Performance_Monitoring.DataAccessLayer.Core.Context;
using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Monitoring.DataAccessLayer.Core.Repositories
{
	public class ServerInformationRepository<T> : Repository<T>, IServerInformationRepository<T> where T : IPersistenceEntities
	{
		public ServerInformationRepository(MongoDbContext context) : base(context)
		{
		}
	}
}
