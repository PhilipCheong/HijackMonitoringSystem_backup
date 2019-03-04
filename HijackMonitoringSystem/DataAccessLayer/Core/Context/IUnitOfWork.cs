using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Context
{
    public interface IUnitOfWork <T> where T : IPersistenceEntities
    {
        ISpeedTestRespository<T> SpeedTestRespository { get; }
        IHijackingTestResultRespository<T> HijackingTestResultRespository { get; }
        IHijackingDomainRespository<T> HijackingDomainRespository { get; }
        IUserRespository<T> UserRespository { get; }
        IServerRespository<T> ServerRespository { get; }

    }
}