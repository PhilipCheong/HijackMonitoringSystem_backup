using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
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