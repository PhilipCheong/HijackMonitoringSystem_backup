using System;
using System.Collections.Generic;
using _17ceBackendFunction.BusinessLayer.Dtos;
using _17ceBackendFunction.BusinessLayer.Services.GeneralServices;
using _17ceBackendFunction.DataAccessLayer.Entities;

namespace _17ceBackendFunction.BusinessLayer.Services.IndependentServices.Interface
{
    public interface IHijackingTestResultService : IService<HijackingTestResultEntities, HijackingTestResultDto>
    {
        List<HijackingTestResultDto> GetAllForAdminOnly();
        List<HijackingTestResultDto> GetAllForUser(List<string> domainList);
	    List<HijackingTestResultDto> GetDataForSummarize();

    }
}
