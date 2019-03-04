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
    public class SpeedTestService : BaseService<SpeedTestResultEntities, SpeedTestResultDto>, ISpeedTestService
    {
        public IUnitOfWork<SpeedTestResultEntities> _unitOfWork = new UnitOfWork<SpeedTestResultEntities>();

        ISpeedTestRespository<SpeedTestResultEntities> _speedTestRespository;

        public SpeedTestService()
        {
            _speedTestRespository = _unitOfWork.SpeedTestRespository;
            _repository = _speedTestRespository;
        }
    }
}