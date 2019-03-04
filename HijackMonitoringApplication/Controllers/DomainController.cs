using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.Resources;
using HijackMonitoringApplication.ViewModel;
using Newtonsoft.Json;

namespace HijackMonitoringApplication.Controllers
{
	[Authorize(Roles = "Toffstech_Admin")]
	public class DomainController : BaseController
	{
		private readonly HijackingDomainService hijackingDomainService = new HijackingDomainService();
		private readonly UserService userService = new UserService();
		// GET: Domain
		public ActionResult Index()
		{
			dynamic returnData = new ExpandoObject();
			try
			{
				returnData.DomainName = hijackingDomainService.GetAll();
				returnData.customerId = userService.GetAllCustomerId().Where(p => !p.Equals(CurrentAccount.CustomerId));
				returnData.selfId = CurrentAccount.CustomerId;
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return View(returnData);
		}

		//public ActionResult DomainSetup()
		//{
		//	dynamic returnData = new ExpandoObject();
		//	ProvinceValue provinceValue = new ProvinceValue();
		//	returnData.Province = provinceValue.CallForKeyPair();

		//	return View(returnData);
		//}
		public ActionResult DomainSetup()
		{
			dynamic returnData = new ExpandoObject();
			ProvinceValue provinceValue = new ProvinceValue();
			try
			{
				returnData.Province = provinceValue.CallForKeyPair();
				returnData.CustomerId = userService.GetAllCustomerId();
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return View(returnData);
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
				returnData.Province = provinceValue.CallForKeyPair();
				returnData.DomainInfo = hijackingDomainService.GetById(id);
				returnData.AllCustomerId = userService.GetAllCustomerId().Where(s => !s.Equals(returnData.DomainInfo.CustomerId)).OrderBy(p => p).ToList(); ;
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(returnData);
		}

		[HttpPost]
		public string AddOrEditForDomain(DomainViewModel domainViewModel)
		{
			var hijackingDomainDto = new HijackingDomainDto();

			try
			{
				if (domainViewModel.Id != null)
				{
					hijackingDomainDto = hijackingDomainService.GetById(domainViewModel.Id);
				}
				hijackingDomainDto.Protocol = domainViewModel.Protocol;
				hijackingDomainDto.Domain = domainViewModel.Domain;
				hijackingDomainDto.DestinationIp = domainViewModel.DestinationIp;
				hijackingDomainDto.CustomerId = domainViewModel.CustomerID;
				hijackingDomainDto.Status = (int)StatusEnum.enabled;
				hijackingDomainDto.ToStartTime = Convert.ToDateTime(domainViewModel.ToStartTime).ToLocalTime();
				hijackingDomainDto.ToEndTime = Convert.ToDateTime(domainViewModel.ToEndTime).ToLocalTime();
				hijackingDomainDto.Interval = domainViewModel.Interval;
				hijackingDomainDto.Province = domainViewModel.CheckedProvince;
				hijackingDomainDto.Isp = domainViewModel.CheckedIsp;
				hijackingDomainDto.LastExecuted = DateTime.Now.AddHours(-3).ToLocalTime();
				hijackingDomainDto.Redirection = string.IsNullOrEmpty(domainViewModel.Redirection) ? "" : domainViewModel.Redirection.ToLower().Trim();

				hijackingDomainService.AddOrEdit(hijackingDomainDto);
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(new { result = "true" });
		}

		public string GroupSearch(string customerId)
		{
			return JsonConvert.SerializeObject(hijackingDomainService.Find(p => p.CustomerId.Equals(customerId)));
		}

		public string FulfillEdit(string id)
		{
			return JsonConvert.SerializeObject(hijackingDomainService.GetById(id));
		}
		[HttpPost]
		public ActionResult DeleteDomain(string Id)
		{
			hijackingDomainService.Remove(Id);

			return RedirectToAction("Index", "Domain");
		}

		[HttpPost]
		public string ChangeStatus(string Id, string value)
		{
			var data = hijackingDomainService.Find(s => s.Id.Equals(Id)).First();
			data.Status = int.Parse(value);
			hijackingDomainService.AddOrEdit(data);

			return "true";
		}

	}


	public class ProvinceValue
	{
		private readonly List<KeyValuePair<string, int>> provinceValue = new List<KeyValuePair<string, int>>();

		public ProvinceValue()
		{
			provinceValue.Add(new KeyValuePair<string, int>("Beijing", 202));
			provinceValue.Add(new KeyValuePair<string, int>("Changsha", 461));
			provinceValue.Add(new KeyValuePair<string, int>("Chongqing", 51));
			provinceValue.Add(new KeyValuePair<string, int>("Fuan", 472));
			provinceValue.Add(new KeyValuePair<string, int>("Xiamen", 476));
			provinceValue.Add(new KeyValuePair<string, int>("Lanzhou", 602));
			provinceValue.Add(new KeyValuePair<string, int>("Qingyang", 722));
			provinceValue.Add(new KeyValuePair<string, int>("NeiMongol", 205));
			provinceValue.Add(new KeyValuePair<string, int>("Guiyang", 392));
			provinceValue.Add(new KeyValuePair<string, int>("Ningxia", 211));
			provinceValue.Add(new KeyValuePair<string, int>("Jinan", 212));
			provinceValue.Add(new KeyValuePair<string, int>("HaErBing", 584));
			provinceValue.Add(new KeyValuePair<string, int>("Datong", 661));
			provinceValue.Add(new KeyValuePair<string, int>("Changzhi", 662));
			provinceValue.Add(new KeyValuePair<string, int>("Xian", 601));
			provinceValue.Add(new KeyValuePair<string, int>("Dongguan", 217));
			provinceValue.Add(new KeyValuePair<string, int>("Foshan", 263));
			provinceValue.Add(new KeyValuePair<string, int>("Zhengzhou", 271));
			provinceValue.Add(new KeyValuePair<string, int>("Nanyang", 701));
			provinceValue.Add(new KeyValuePair<string, int>("Shanghai", 280));
			provinceValue.Add(new KeyValuePair<string, int>("Jingan", 245));
			provinceValue.Add(new KeyValuePair<string, int>("Yuxi", 563));
			provinceValue.Add(new KeyValuePair<string, int>("Wuhan", 259));
			provinceValue.Add(new KeyValuePair<string, int>("Yichang", 444));
			provinceValue.Add(new KeyValuePair<string, int>("Hefei", 609));
			provinceValue.Add(new KeyValuePair<string, int>("Huangshan", 611));
			provinceValue.Add(new KeyValuePair<string, int>("Xizang", 274));
			provinceValue.Add(new KeyValuePair<string, int>("Nanchang", 275));
			provinceValue.Add(new KeyValuePair<string, int>("Jiujiang", 402));
			provinceValue.Add(new KeyValuePair<string, int>("Tianjing", 279));
			provinceValue.Add(new KeyValuePair<string, int>("Tangshan", 289));
			provinceValue.Add(new KeyValuePair<string, int>("Baoding", 294));
			provinceValue.Add(new KeyValuePair<string, int>("Xinjiang", 390));
			provinceValue.Add(new KeyValuePair<string, int>("Ulumuqi", 719));
			provinceValue.Add(new KeyValuePair<string, int>("Dalian", 443));
			provinceValue.Add(new KeyValuePair<string, int>("Shenyang", 582));
			provinceValue.Add(new KeyValuePair<string, int>("Hengyang", 463));
			provinceValue.Add(new KeyValuePair<string, int>("Changchun", 587));
			provinceValue.Add(new KeyValuePair<string, int>("Liuzhou", 531));
			provinceValue.Add(new KeyValuePair<string, int>("Nanning", 532));
			provinceValue.Add(new KeyValuePair<string, int>("Guangxi", 533));
			provinceValue.Add(new KeyValuePair<string, int>("Yibing", 538));
			provinceValue.Add(new KeyValuePair<string, int>("Chengdu", 539));
			provinceValue.Add(new KeyValuePair<string, int>("Haikou", 557));
			provinceValue.Add(new KeyValuePair<string, int>("Wenchang", 676));
			provinceValue.Add(new KeyValuePair<string, int>("Hangzhou", 573));
			provinceValue.Add(new KeyValuePair<string, int>("Wenzhou", 580));
			provinceValue.Add(new KeyValuePair<string, int>("Xining", 574));
			provinceValue.Add(new KeyValuePair<string, int>("Nanjing", 590));
			provinceValue.Add(new KeyValuePair<string, int>("Ningbo", 732));
			provinceValue.Add(new KeyValuePair<string, int>("Qingyuan", 642));
			provinceValue.Add(new KeyValuePair<string, int>("Sanya", 679));
			provinceValue.Add(new KeyValuePair<string, int>("Shenzhen", 264));
			provinceValue.Add(new KeyValuePair<string, int>("Suzhou", 595));
			provinceValue.Add(new KeyValuePair<string, int>("Taiyuan", 595));
			provinceValue.Add(new KeyValuePair<string, int>("Zhanjiang", 635));
			provinceValue.Add(new KeyValuePair<string, int>("Zhaoqing", 643));
			provinceValue.Add(new KeyValuePair<string, int>("Zhongshan", 268));
			provinceValue.Add(new KeyValuePair<string, int>("Zhuhai", 267));
		}

		public List<KeyValuePair<string, int>> CallForKeyPair()
		{
			return provinceValue;
		}
	}
}