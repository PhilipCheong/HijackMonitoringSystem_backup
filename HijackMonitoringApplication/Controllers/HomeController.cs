using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace HijackMonitoringApplication.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		private readonly HijackingDomainService hijackingDomainService = new HijackingDomainService();
		private readonly UserService userService = new UserService();
		private readonly HijackingTestResultService hijackingTestResultService = new HijackingTestResultService();
		//Return Customer Id for dropdown

		public ActionResult Index()
		{
			dynamic returnData = new ExpandoObject();

			try
			{
				if (CurrentAccount.Type.Equals("Toffstech_Admin") && CurrentAccount.CustomerId.Equals("TOFFSTECH"))
				{
					returnData.CustomerId = userService.GetAllCustomerId().Where(s => !s.Equals(CurrentAccount.CustomerId));
					returnData.SelfId = CurrentAccount.CustomerId;
				}
				else
				{
					returnData.CustomerId = null;
					returnData.SelfId = CurrentAccount.CustomerId;
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return View(returnData);
		}

		//Return data for Map and Locations Hijacked

		public string MapPopulation(string customerId)
		{
			var returnData = new List<HijackingTestResultDto>();
			dynamic objectForChart = new ExpandoObject();
			if (string.IsNullOrEmpty(customerId)) return JsonConvert.SerializeObject(objectForChart);
			try
			{
				if (customerId == "TOFFSTECH" && CurrentAccount.Type.Equals("Toffstech_Admin"))
				{
					returnData = hijackingTestResultService.GetAllForAdminOnly();
				}
				else
				{
					var domainList = hijackingDomainService.GetAllDomainByCustomerId(customerId).Select(p => p.Domain).ToList();
					returnData = hijackingTestResultService.GetAllForUser(domainList);
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			var mapData = ConfigurationManager.AppSettings["MapLocations"];
				var mapLocation = mapData.Split(',').ToArray();
				var hijackedMapView = mapLocation.ToDictionary(location => location, location => returnData.Count(s => location.ToLower().Contains(s.Province.ToLower()) && s.Verified.StartsWith("Caution"))).OrderByDescending(p => p.Value);

				objectForChart.cols = new List<Object>();
				objectForChart.cols.Add(new { id = "", label = "Location", pattern = "", type = "string" });
				objectForChart.cols.Add(new { id = "", label = "Alert", pattern = "", type = "number" });


				objectForChart.rows = new List<Object>();

				foreach (var item in hijackedMapView)
				{
					var cItem = new List<Object>
				{
					new { v = item.Key, f = item.Key },
					new { v = item.Value, f = item.Value }
				};

					objectForChart.rows.Add(new { c = cItem });
				}

			return JsonConvert.SerializeObject(objectForChart);
		}

		//Return last test data base on customer id

		public string PercentageResult(string customerId)
		{
			var returnData = new List<Tuple<string, string, decimal, decimal, decimal, decimal>>();
			if (string.IsNullOrEmpty(customerId)) return JsonConvert.SerializeObject(returnData);
			try
			{
				if (customerId == "TOFFSTECH" && CurrentAccount.Type.Equals("Toffstech_Admin"))
				{
					var domainList = hijackingDomainService.GetAll().Select(p => (p.Protocol + p.Domain)).Distinct().ToList();
					var queryData = hijackingTestResultService.GetAllForAdminOnly();
					returnData = PercentagesCalculation(domainList, queryData).OrderByDescending(p => p.Item5).ToList();
				}
				else
				{
					var domainList = hijackingDomainService.GetAllDomainByCustomerId(customerId).Select(p => (p.Protocol + p.Domain)).Distinct().ToList();
					var queryData = hijackingTestResultService.GetAllForUser(domainList);

					returnData = PercentagesCalculation(domainList, queryData).OrderByDescending(p => p.Item5).ToList();
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(returnData);
		}
		public string IspPopulation(string customerId)
		{
			string[] ispArray = { "China Telecom", "China Mobile", "China Unicom" };
			var returnData = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("China Telecom", 0), new KeyValuePair<string, int>("China Unicom", 0), new KeyValuePair<string, int>("China Mobile", 0) };
			var queryData = new List<HijackingTestResultDto>();
			if(string.IsNullOrEmpty(customerId)) return JsonConvert.SerializeObject(returnData);
			try
			{
				if (customerId.Equals("TOFFSTECH") && CurrentAccount.Type.Equals("Toffstech_ADMIN"))
				{
					queryData = hijackingTestResultService.GetAllForAdminOnly();
				}
				else
				{
					var domainList = hijackingDomainService.GetAllDomainByCustomerId(customerId).Select(p => p.Domain.Substring(p.Domain.IndexOf("://", StringComparison.Ordinal) + 3)).ToList();
					queryData = hijackingTestResultService.GetAllForUser(domainList);

				}

				var calculated = queryData.Where(s => s.Verified.StartsWith("Caution")).GroupBy(p => p.Isp).ToDictionary(s => s.Key, s => s.Count());
				if (calculated.Count > 0)
				{
					returnData.Clear();
					foreach (var data in calculated)
					{
						if (ispArray.Any(p => p.Equals(data.Key)))
						{
							returnData.Add(data);
						}
					}
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(returnData.OrderByDescending(p => p.Value));
		}
		public List<Tuple<string, string, decimal, decimal, decimal, decimal>> PercentagesCalculation(List<string> domainList, List<HijackingTestResultDto> queryData)
		{
			var calculatedResult = new List<Tuple<string, string, decimal, decimal, decimal, decimal>>();
			if (!domainList.Any()) return calculatedResult;
			try
			{
				foreach (var domain in domainList)
				{
					var testDate = queryData.Select(p => p.CreateDate).LastOrDefault().ToString();
					decimal caution = queryData.Count(p => p.Domain.Equals(domain) && p.Verified.StartsWith("Caution"));
					decimal normal = queryData.Count(p => p.Domain.Equals(domain) && p.Verified.StartsWith("Normal"));
					decimal percentage = 0;
					if (normal + caution != 0)
					{
						percentage = System.Math.Round(caution / ((caution + normal) / 100), 2);
					}
					decimal percentageNormal = 100 - percentage;
					calculatedResult.Add(Tuple.Create(domain, testDate, caution, normal, percentage, percentageNormal));
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return calculatedResult;
		}
	}
}