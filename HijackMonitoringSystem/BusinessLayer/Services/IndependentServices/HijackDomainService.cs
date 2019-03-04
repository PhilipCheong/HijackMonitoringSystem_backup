using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.BusinessLayer.Dtos;
using HijackMonitoringSystem.BusinessLayer.Services.GeneralServices;
using HijackMonitoringSystem.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities;

namespace HijackMonitoringSystem.BusinessLayer.Services.IndependentServices
{
    public class HijackDomainService : BaseService<HijackDomainEntities, HijackDomainDto>, IHijackDomainService
    {
        public IUnitOfWork<HijackDomainEntities> _unitOfWork = new UnitOfWork<HijackDomainEntities>();

        IHijackingDomainRespository<HijackDomainEntities> _hijackDomainRespository;

        public HijackDomainService()
        {
            _hijackDomainRespository = _unitOfWork.HijackingDomainRespository;
            _repository = _hijackDomainRespository;
        }
        public List<HijackDomainDto> GetAllDomainByCustomerId(string customerId)
        {

            var data = GetAll().Where(p => p.CustomerID == customerId).ToList();
            List<HijackDomainDto> returnData = new List<HijackDomainDto>();// new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                HijackDomainDto temp = Activator.CreateInstance<HijackDomainDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }
            return returnData;
        }

    }
}