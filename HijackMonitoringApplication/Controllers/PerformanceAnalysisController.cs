using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using System.Dynamic;
using Newtonsoft.Json;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.ViewModel;
using HijackMonitoringApplication.Resources;

namespace HijackMonitoringApplication.Controllers
{
    [Authorize(Roles = "Toffstech_Admin")]
	public class PerformanceAnalysisController : BaseController
	{
		readonly DomainExaminationService domainExaminationService = new DomainExaminationService();
		readonly UserService userService = new UserService();
		readonly PerformanceDataService performanceDataService = new PerformanceDataService();

		// GET: PerformanceAnalysis
		public ActionResult Index()
		{
			dynamic returnData = new ExpandoObject();
			try
			{
				var refaceDomain = domainExaminationService.GetAll();

				foreach (var dmn in refaceDomain)
				{
					switch (dmn.BrowserType)
					{
						case "1":
							dmn.BrowserType = "Chrome";
							break;
						case "2":
							dmn.BrowserType = "Firefox";
							break;
						case "1,2":
							dmn.BrowserType = "Chrome/Firefox";
							break;
					}
					switch (dmn.TestType)
					{
						case "1":
							dmn.TestType = "Performance";
							break;
						case "2":
							dmn.TestType = "Monitoring";
							break;
						case "1,2":
							dmn.TestType = "Performance/Monitoring";
							break;
					}
				}
				returnData.DomainName = refaceDomain;
				returnData.customerId = userService.GetAllCustomerId().Where(p => !p.Equals(CurrentAccount.CustomerId));
				returnData.selfId = CurrentAccount.CustomerId;
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}

			return View(returnData);
		}

		public ActionResult NewDomain()
		{
			dynamic returnData = new ExpandoObject();
			ProvinceValue provinceValue = new ProvinceValue();
			try
			{
				returnData.CustomerId = userService.GetAllCustomerId();
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return View(returnData);
		}

		[HttpPost]
		public string AddOrEditForDomain(PerformanceAnalysisDmnViewModel domainViewModel)
		{
			var domainDto = new DomainExaminationDto();

			try
			{
				if (domainViewModel.Id != null)
				{
					domainDto = domainExaminationService.GetById(domainViewModel.Id);
				}
				domainDto.Protocol = domainViewModel.Protocol;
				domainDto.Domain = domainViewModel.Domain;
				domainDto.CustomerId = domainViewModel.CustomerId;
				domainDto.Status = (int)StatusEnum.enabled;
				domainDto.ToStartTime = Convert.ToDateTime(domainViewModel.ToStartTime).ToLocalTime();
				domainDto.ToEndTime = Convert.ToDateTime(domainViewModel.ToEndTime).ToLocalTime();
				domainDto.Interval = domainViewModel.Interval;
				domainDto.BrowserType = domainViewModel.BrowserType;
				domainDto.TestType = domainViewModel.TestType;
				domainDto.LastExecuted = DateTime.Now.AddHours(-3).ToLocalTime();

				domainExaminationService.AddOrEdit(domainDto);
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(new { result = "true" });
		}

		public ActionResult DomainEdit()
		{
			ViewBag.Url = System.Web.HttpContext.Current.Request.Url.Query.Remove(0, 1);

			return View();
		}

		[HttpPost]
		public string DomainInfo(string id)
		{
			dynamic returnData = new ExpandoObject();
			ProvinceValue provinceValue = new ProvinceValue();
			try
			{
				returnData.DomainInfo = domainExaminationService.GetById(id);
				returnData.AllCustomerId = userService.GetAllCustomerId().Where(s => !s.Equals(returnData.DomainInfo.CustomerId)).OrderBy(p => p).ToList(); ;
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(returnData);
		}

		public string GroupSearch(string customerId)
		{
			var refaceDomain = new List<DomainExaminationDto>();
			if (customerId.ToLower().Equals("toffstech"))
			{
				refaceDomain = domainExaminationService.GetAll();
			}
			else
			{
				refaceDomain = domainExaminationService.Find(s => s.CustomerId.Equals(customerId));
			}

			foreach (var dmn in refaceDomain)
			{
				switch (dmn.BrowserType)
				{
					case "1":
						dmn.BrowserType = "Chrome";
						break;
					case "2":
						dmn.BrowserType = "Firefox";
						break;
					case "1,2":
						dmn.BrowserType = "Chrome/Firefox";
						break;
				}
				switch (dmn.TestType)
				{
					case "1":
						dmn.TestType = "Performance";
						break;
					case "2":
						dmn.TestType = "Monitoring";
						break;
					case "1,2":
						dmn.TestType = "Performance/Monitoring";
						break;
				}
			}

			return JsonConvert.SerializeObject(refaceDomain);
		}

		[HttpPost]
		public ActionResult DeleteDomain(string Id)
		{
			var url = domainExaminationService.Find(s => s.Id.Equals(Id)).FirstOrDefault().Domain;
			var previousResults = performanceDataService.Find(p => p.Url.Equals(url)).ToList();

			if (previousResults.Any())
			{
				foreach (var result in previousResults)
				{
					performanceDataService.Remove(result.Id);
				}
			}
			domainExaminationService.Remove(Id);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public string ChangeStatus(string Id, string value)
		{
			var data = domainExaminationService.Find(s => s.Id.Equals(Id)).First();
			data.Status = int.Parse(value);
			domainExaminationService.AddOrEdit(data);

			return "true";
		}

	}
}