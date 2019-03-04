using Performance_Monitoring.BusinessLayer.Dtos;
using Performance_Monitoring.BusinessLayer.Services.GeneralServices;
using Performance_Monitoring.BusinessLayer.Services.IndependentServices.Interface;
using Performance_Monitoring.DataAccessLayer.Core.Context;
using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Monitoring.BusinessLayer.Services.IndependentServices
{
	public class ServerInformationService : BaseService<ServerInformationEntities, ServerInformationDto>, IServerInformationService
	{
		public IUnitOfWork<ServerInformationEntities> _unitOfWork = new UnitOfWork<ServerInformationEntities>();

		IServerInformationRepository<ServerInformationEntities> _serverInformationRepository;
		public ServerInformationService()
		{
			_serverInformationRepository = _unitOfWork.ServerInformationRepository;
			_repository = _serverInformationRepository;
		}
	}
}
