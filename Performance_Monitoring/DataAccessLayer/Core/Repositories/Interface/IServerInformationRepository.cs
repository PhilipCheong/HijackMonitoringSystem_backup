using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface
{
	public interface IServerInformationRepository<T> : IRepository<T> where T : IPersistenceEntities
	{
	}
}
