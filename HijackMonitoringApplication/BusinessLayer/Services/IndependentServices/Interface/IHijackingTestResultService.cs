using System;
using System.Collections.Generic;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
    public interface IHijackingTestResultService : IService<HijackingTestResultEntities, HijackingTestResultDto>
    {
        List<HijackingTestResultDto> GetAllForAdminOnly();
        List<HijackingTestResultDto> GetAllForUser(List<string> domainList);
	    List<HijackingTestResultDto> GetDataForSummarize();

    }
}
