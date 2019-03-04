using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
{
	public class DomainExaminationService : BaseService<DomainExaminationEntities, DomainExaminationDto>, IDomainExaminationService
	{
		public IUnitOfWork<DomainExaminationEntities> _unitOfWork = new UnitOfWork<DomainExaminationEntities>();

		private readonly IDomainExaminationRepository<DomainExaminationEntities> hijackingDomainRespository;

		public DomainExaminationService()
		{
			hijackingDomainRespository = _unitOfWork.DomainExaminationRepository;
			_repository = hijackingDomainRespository;
		}
	}
}