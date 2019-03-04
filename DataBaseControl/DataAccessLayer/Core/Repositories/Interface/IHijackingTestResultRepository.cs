using System;
using System.Collections.Generic;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
    public interface IHijackingTestResultRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
        List<HijackingTestResultEntities> GetByDomain(string domain);
        List<HijackingTestResultEntities> GetLastTestResult(List<string> domainList);
        List<HijackingTestResultEntities> GetResultByTime(string domain, DateTime Start, DateTime End);
        List<HijackingTestResultEntities> PerDayEvents(List<string> domainList, DateTime choosenDate);
        List<HijackingTestResultEntities> GetAllForAdminOnly();
        List<HijackingTestResultEntities> GetAllForUser(List<string> domainList);
	    List<HijackingTestResultEntities> GetDataForSummarize();
    }
}