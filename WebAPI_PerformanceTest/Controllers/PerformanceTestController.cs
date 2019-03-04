using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos;
using WebAPI_PerformanceTest.Repository;

namespace WebAPI_PerformanceTest.Controllers
{
    public class PerformanceTestController : ApiController
	{		public IEnumerable<string> Get()
		{
			return new string[] { "Performance Test", "Successful" };
		}

		public string Post([FromBody] PostData postData)
		{
			if (postData.passwd.Equals("Toffstech_Performance-Monitoring@48969487"))
			{
				var domainList = JsonConvert.DeserializeObject<List<string>>(postData.jsonDomain);
				
				MainProcess mainProcess = new MainProcess(domainList);
				mainProcess.Run();

				return "Successful";

			}
			else
			{
				return "Failed";
			}
		}
	}
}
