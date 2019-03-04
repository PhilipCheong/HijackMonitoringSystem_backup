using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.GeneralServices;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices
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