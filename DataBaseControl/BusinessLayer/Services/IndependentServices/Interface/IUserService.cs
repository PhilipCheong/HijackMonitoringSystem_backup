using System.Collections.Generic;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.GeneralServices;
using HijackMonitoringApplication.DataAccessLayer.Entities;

namespace HijackMonitoringApplication.BusinessLayer.Services.IndependentServices.Interface
{
    public interface IUserService : IService<UserEntities, UserDto>
    {
        UserDto LoginVerification(string userName, string passWord);
        List<string> GetAllCustomerId();
    }
}