using System;
using System.Collections.Generic;
using System.Linq;
using _17ceBackendFunction.BusinessLayer.Dtos;
using _17ceBackendFunction.BusinessLayer.Services.GeneralServices;
using _17ceBackendFunction.BusinessLayer.Services.IndependentServices.Interface;
using _17ceBackendFunction.DataAccessLayer.Core.Context;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities;

namespace _17ceBackendFunction.BusinessLayer.Services.IndependentServices
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