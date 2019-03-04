using HijackMonitoringApplication.DataAccessLayer.Entities;
using HijackMonitoringApplication.DataAccessLayer.Entities.GeneralData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface
{
    public interface IDataForAnalysisRepository<T> : IRepository<T> where T : IPersistenceEntities
    {
        List<DataForAnalysisEntities> GetOneDayData(DateTime startDate, DateTime endDate);
    }
}