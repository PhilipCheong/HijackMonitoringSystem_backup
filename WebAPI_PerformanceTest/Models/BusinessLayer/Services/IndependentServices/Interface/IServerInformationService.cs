using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Models.BusinessLayer.Services.GeneralServices;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Services.IndependentServices.Interface
{
	public interface IServerInformationService : IService<ServerInformationEntities, ServerInformationDto>
	{
	}
}