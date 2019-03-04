using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;

namespace HijackMonitoringApplication.Controllers
{
	[Authorize(Roles = "Toffstech_Admin")]
	public class ServerController : BaseController
	{
		TFCDNserversService tfcdnServers = new TFCDNserversService();

		private class ServerTag
		{
			public string value { get; set; }
			public string text { get; set; }
		}

		// GET: Server
		public ActionResult Index()
		{
			dynamic returnData = new ExpandoObject();

			try
			{
				var fullList = tfcdnServers.GetAll().OrderBy(p => p.ServerCname).ThenBy(s => s.GroupName).OrderBy(s => s.ServerCname).ToList();
				returnData.FullList = fullList;
				returnData.Distinct = fullList.Select(s => s.ServerCname).Distinct().ToList();
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return View(returnData);
		}
		public string SearchByCname(string userInput)
		{
			dynamic returnData = new ExpandoObject();

			try
			{
				returnData = tfcdnServers.GetAll().Where(s => s.ServerCname.ToLower().Contains(userInput.ToLower())).OrderBy(s => s.ServerCname).ToList();
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(returnData);
		}
		public string BulkEditDropdown()
		{
			Regex regexSingle = new Regex(@"ipg\d{1}s");
			Regex regexDouble = new Regex(@"ipg\d{2}s");
			Regex regexTriple = new Regex(@"ipg\d{3}s");
			var returnList = new List<string>();
			try
			{
				var serverList = tfcdnServers.GetAll().Select(s => s.ServerCname).Distinct().ToList();
				var singleNumber = serverList.Where(s => regexSingle.IsMatch(s)).OrderBy(p => p).ToList();
				var doubleNumber = serverList.Where(s => regexDouble.IsMatch(s)).OrderBy(p => p).ToList();
				var tripleNumber = serverList.Where(s => regexTriple.IsMatch(s)).OrderBy(p => p).ToList();
				var otherString = serverList.Where(s => !regexSingle.IsMatch(s) && !regexDouble.IsMatch(s) && !regexTriple.IsMatch(s))
					.OrderBy(p => p).ToList();

				returnList = singleNumber.Concat(doubleNumber).Concat(tripleNumber).Concat(otherString).ToList();
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(returnList);
		}

		[HttpPost]
		public string BulkEditPopulateData(string serverCname)
		{
			return JsonConvert.SerializeObject(tfcdnServers.GetAll().Where(s => s.ServerCname.Equals(serverCname)).ToList());
		}

		[HttpPost]
		public ActionResult BulkEdit(List<ServerViewModel> serverList)
		{
			try
			{
				foreach (var viewModel in serverList)
				{
					var server = tfcdnServers.GetById(viewModel.Id);
					if (server == null) continue;
					server.ServerCname = viewModel.ServerCname.ToLower().Trim();
					server.GroupName = viewModel.GroupName.Trim();
					server.ServerIp = viewModel.ServerIp.Trim();
					server.BandwidthLimitation = viewModel.BandwidthLimitation.Trim();

					tfcdnServers.AddOrEdit(server);
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult AddOrEditForServer(ServerViewModel serverViewModel)
		{
			try
			{
				if (!string.IsNullOrEmpty(serverViewModel.Id))
				{
					var serverDto = tfcdnServers.GetById(serverViewModel.Id);

					serverDto.GroupName = serverViewModel.GroupName.Trim();
					serverDto.ServerCname = serverViewModel.ServerCname.ToLower().Trim();
					serverDto.ServerIp = serverViewModel.ServerIp.Trim();
					serverDto.BandwidthLimitation = serverViewModel.BandwidthLimitation.Trim();

					tfcdnServers.AddOrEdit(serverDto);
				}
				else
				{
					var ips = serverViewModel.ServerIp.Split(',');

					foreach (var ip in ips)
					{
						var serverDto = new TFCDNserverDto
						{
							GroupName = serverViewModel.GroupName.Trim(),
							ServerCname = serverViewModel.ServerCname.ToLower().Trim(),
							ServerIp = ip.Trim(),
							BandwidthLimitation = serverViewModel.BandwidthLimitation.Trim()
						};

						tfcdnServers.AddOrEdit(serverDto);
					}
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return RedirectToAction("Index", "Server");
		}

		[HttpPost]
		public string FulfillDataForEdit(string id)
		{
			return JsonConvert.SerializeObject(tfcdnServers.GetById(id));
		}

		[HttpPost]
		public string RefillAllDomain()
		{
			return JsonConvert.SerializeObject(tfcdnServers.GetAll());
		}

		[HttpPost]
		public ActionResult DeleteServer(string id)
		{
			tfcdnServers.Remove(id);
			return RedirectToAction("Index", "Server");
		}

		[HttpPost]
		public ActionResult BulkDelete(string id)
		{
			try
			{
				if (!id.Contains(","))
				{
					tfcdnServers.Remove(id);
				}
				else
				{
					var multipleDelete = id.Split(',');

					foreach (var delId in multipleDelete)
					{
						tfcdnServers.Remove(delId);
					}
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return RedirectToAction("Index");
		}

		public string GetServerForServerResult()
		{
			string currentId = CurrentAccount.CustomerId;
			var serverList = new List<string>();
			var allowedCname = new List<ServerTag>();
			try
			{
				serverList = tfcdnServers.GetAll().Select(s => s.ServerCname).OrderBy(p => p).Distinct().ToList();
				allowedCname.AddRange(serverList.Select(s => new ServerTag { value = s, text = s }));
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			return JsonConvert.SerializeObject(allowedCname);
		} 
	}
}