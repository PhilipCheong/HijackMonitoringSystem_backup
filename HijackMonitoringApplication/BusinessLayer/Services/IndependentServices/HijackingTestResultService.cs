using System;
using System.Collections.Generic;
using System.Linq;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using Microsoft.AspNet.SignalR;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
{
    public class HijackingTestResultService : BaseService<HijackingTestResultEntities, HijackingTestResultDto>,
        IHijackingTestResultService
    {
        public IUnitOfWork<HijackingTestResultEntities> _unitOfWork = new UnitOfWork<HijackingTestResultEntities>();

        private readonly IHijackingTestResultRespository<HijackingTestResultEntities> _hijackingTestResultRespository;

        public HijackingTestResultService()
        {
            _hijackingTestResultRespository = _unitOfWork.HijackingTestResultRespository;
            _repository = _hijackingTestResultRespository;
        }

        public List<HijackingTestResultDto> GetDataByDomain(string domain)
        {
            var data = _hijackingTestResultRespository.GetByDomain(domain);
            List<HijackingTestResultDto>
                returnData = new List<HijackingTestResultDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                HijackingTestResultDto temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;

        }

        public List<HijackingTestResultDto> GetAllCustomerDomain(List<string> domainList)
        {
            var data = _hijackingTestResultRespository.GetLastTestResult(domainList);
            List<HijackingTestResultDto>
                returnData = new List<HijackingTestResultDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                HijackingTestResultDto temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }

        public List<HijackingTestResultDto> GetResultByTime(string domain, DateTime Start, DateTime End)
        {
            var data = _hijackingTestResultRespository.GetResultByTime(domain, Start, End);
            List<HijackingTestResultDto>
                returnData = new List<HijackingTestResultDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                HijackingTestResultDto temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }

        //API For Mobile Version
        public List<HijackingTestResultDto> GetAllByDate()
        {
            //{bug} 
            var currentDayStart = new DateTime(2018, 04, 08);
            var currentDayEnds = currentDayStart.AddHours(24);
            var data = _hijackingTestResultRespository.GetAll().Where(p =>
                p.CreateDate.Value >= currentDayStart && p.CreateDate < currentDayEnds &&
                p.Verified.Contains("Hijacked")).ToList();
            List<HijackingTestResultDto>
                returnData = new List<HijackingTestResultDto>(); // new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                HijackingTestResultDto temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }

        public List<HijackingTestResultDto> PerDayEvents(List<string> domainList, DateTime choosenDate)
        {
            var data = _hijackingTestResultRespository.PerDayEvents(domainList, choosenDate);
            List<HijackingTestResultDto> returnData = new List<HijackingTestResultDto>();
            foreach (var d in data)
            {
                HijackingTestResultDto temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }

        [Authorize(Roles = "Toffstech_ADMIN")]
        public List<HijackingTestResultDto> GetAllForAdminOnly()
        {
            var data = _hijackingTestResultRespository.GetAllForAdminOnly();
            var returnData = new List<HijackingTestResultDto>();
            foreach (var d in data)
            {
                var temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;
        }

        public List<HijackingTestResultDto> GetAllForUser(List<string> domainList)
        {
            var data = _hijackingTestResultRespository.GetAllForUser(domainList);
            var returnData = new List<HijackingTestResultDto>();
            foreach (var d in data)
            {
                var temp = Activator.CreateInstance<HijackingTestResultDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }

            return returnData;

        }

	    public List<HijackingTestResultDto> GetDataForSummarize()
	    {
		    var data = _hijackingTestResultRespository.GetDataForSummarize();
		    var returnData = new List<HijackingTestResultDto>();
		    foreach (var d in data)
		    {
			    var temp = Activator.CreateInstance<HijackingTestResultDto>();
			    Mapping.MapProp(d, temp);
			    returnData.Add(temp);
		    }

		    return returnData;

	    }
	}
}