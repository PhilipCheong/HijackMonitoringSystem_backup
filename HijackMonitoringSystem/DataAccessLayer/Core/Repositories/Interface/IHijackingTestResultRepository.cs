﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Entities;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface
{
    public interface IHijackingTestResultRespository<T> : IRepository<T> where T : IPersistenceEntities
    {
        List<HijackingTestResultEntities> GetByDomain(string domain);
        List<HijackingTestResultEntities> GetLastTestResult(List<string> domainList);
        List<HijackingTestResultEntities> GetResultByTime(string domain, DateTime Start, DateTime End);
        List<HijackingTestResultEntities> PerDayEvents(List<string> domainList, DateTime choosenDate);
    }
}