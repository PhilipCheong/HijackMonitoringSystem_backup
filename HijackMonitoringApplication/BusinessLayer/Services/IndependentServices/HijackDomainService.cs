using System;
using System.Collections.Generic;
using System.Linq;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
{
    public class HijackingDomainService : BaseService<HijackingDomainEntities, HijackingDomainDto>, IHijackingDomainService
    {
        public IUnitOfWork<HijackingDomainEntities> _unitOfWork = new UnitOfWork<HijackingDomainEntities>();

        private readonly IHijackingDomainRespository<HijackingDomainEntities> hijackingDomainRespository;

        public HijackingDomainService()
        {
            hijackingDomainRespository = _unitOfWork.HijackingDomainRespository;
            _repository = hijackingDomainRespository;
        }
        public List<HijackingDomainDto> GetAllDomainByCustomerId(string customerId)
        {
            var data = hijackingDomainRespository.GetAllDomainWithCustomerId(customerId);
            List<HijackingDomainDto> returnData = new List<HijackingDomainDto>();// new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                HijackingDomainDto temp = Activator.CreateInstance<HijackingDomainDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }
            return returnData;
        }
    }
}