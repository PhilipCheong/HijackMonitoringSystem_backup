using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using HijackMonitoringSystem.BusinessLayer.Dtos;using HijackMonitoringSystem.BusinessLayer.Services.GeneralServices;using HijackMonitoringSystem.DataAccessLayer.Entities;

namespace HijackMonitoringSystem.BusinessLayer.Services.IndependentServices.Interface
{
    public interface IHijackingTestResultService : IService<HijackingTestResultEntities, HijackingTestResultDto>
    {
    }
}
