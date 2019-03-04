﻿using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
    public interface IDataForAnalysisService : IService<DataForAnalysisEntities, DataForAnalysisDto>
    {
        List<DataForAnalysisDto> GetOneDayData(DateTime startDate, DateTime endDate);
    }
}