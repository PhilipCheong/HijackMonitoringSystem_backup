using Performance_Monitoring.BusinessLayer.Dtos;
using Performance_Monitoring.BusinessLayer.Services.GeneralServices;
using Performance_Monitoring.BusinessLayer.Services.IndependentServices.Interface;
using Performance_Monitoring.DataAccessLayer.Core.Context;
using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performance_Monitoring.BusinessLayer.Services.IndependentServices
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