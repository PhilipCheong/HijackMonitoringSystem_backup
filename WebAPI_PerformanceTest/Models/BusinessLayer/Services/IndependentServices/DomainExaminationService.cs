using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.GeneralServices;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Context;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Core.Repositories.Interface;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices
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