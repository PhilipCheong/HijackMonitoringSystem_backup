using System;
using System.Collections.Generic;
using System.Linq;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface;
using HijackMonitoringApplication.DataAccessLayer.Core.Context;
using HijackMonitoringApplication.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices
{
    public class UserService : BaseService<UserEntities, UserDto>, IUserService
    {
        public IUnitOfWork<UserEntities> _unitOfWork = new UnitOfWork<UserEntities>();

        readonly IUserRespository<UserEntities> _userRespository;
        public UserService()
        {
            _userRespository = _unitOfWork.UserRespository;
            _repository = _userRespository;
        }

        public UserDto LoginVerification(string userName, string passWord)
        {
            var data = _userRespository.LoginProcess(userName, passWord);
            var returnData = new UserDto();// new List<Activator.CreateInstance<Dto>> ();

            foreach (var d in data)
            {
                UserDto temp = Activator.CreateInstance<UserDto>();
                Mapping.MapProp(d, temp);
                returnData = temp;
            }
            return returnData;

        }
        public List<string> GetAllCustomerId()
        {
            var data = _userRespository.GetAllCustomerId();
            var returnData = data.Distinct().ToList();// new List<Activator.CreateInstance<Dto>> ();
            return returnData;
        }

    }
}