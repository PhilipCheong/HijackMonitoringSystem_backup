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
    public class UserService : BaseService<UserEntities, UserDto>, IUserService
    {
        public IUnitOfWork<UserEntities> _unitOfWork = new UnitOfWork<UserEntities>();

        IUserRespository<UserEntities> _userRespository;
        public UserService()
        {
            _userRespository = _unitOfWork.UserRespository;
            _repository = _userRespository;
        }

    }
}