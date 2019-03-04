using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.GeneralServices;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices.Interface
{
	public interface IPerformanceDataService : IService<PerformanceDataEntities, PerformanceDataDto>
	{
	}
}