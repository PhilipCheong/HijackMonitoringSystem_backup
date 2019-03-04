using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using _17ceBackendFunction.BusinessLayer;
using System.Threading.Tasks;
using _17ceBackendFunction.BusinessLayer.Services.IndependentServices;
using _17ceBackendFunction.BusinessLayer.Dtos;
using _17ceBackendFunction.Repository;
using _17ceBackendFunction.Resources;
using Nager.PublicSuffix;
using _17ceBackendFunction;

namespace _17ceBackendFunction.Repository
{
	public class DataOrganizing
	{
		HijackingDomainService hijackingDomainService = new HijackingDomainService();
		HijackingTestResultService hijackingTestResultService = new HijackingTestResultService();
		TFCDNserversService _TFCDNserversService = new TFCDNserversService();

		public List<HijackingDomainDto> DomainList()
		{
			var domainForTest = new List<HijackingDomainDto>();
			try
			{
				var filterDomain = hijackingDomainService.GetAll().Where(s => s.ToEndTime > DateTime.Now.ToLocalTime() && DateTime.Now.ToLocalTime() > s.ToStartTime && s.Status == 1);

				domainForTest = filterDomain.Where(s => DateTime.UtcNow > s.LastExecuted.AddMinutes(s.Interval)).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			foreach (var domain in domainForTest)
			{
				domain.LastExecuted = DateTime.Now.ToLocalTime();
				hijackingDomainService.AddOrEdit(domain);
			}
			return domainForTest;

		}

		public void OrganizeData(List<string> rawData, Dictionary<int, string> domainId, DateTime executionTime)
		{
			var returnList = new List<ResultsReturned>();

			foreach (var data in rawData)
			{
				var returnData = new ResultsReturned();

				var unparsedData = JObject.Parse(data);
				try
				{
					if (!unparsedData.ContainsKey("type")) continue;
					if (!unparsedData["type"].ToString().Equals("NewData") ||
						!string.IsNullOrEmpty(unparsedData["error"].ToString())) continue;

					var verifyId = domainId.FirstOrDefault(p => p.Key.ToString().Equals(unparsedData["txnid"].ToString())).Value;

					var domainName = verifyId;

					var results = unparsedData["data"];
					//if (!results["HttpCode"].ToString().Equals("200") && !results["HttpCode"].ToString().Equals("301") &&
					//	!results["HttpCode"].ToString().Equals("302") && !results["HttpCode"].ToString().Equals("307") && !results["ErrMsg"].ToString().Equals(""))
					if (results["HttpCode"] == null)
					{
						returnData.DomainName = domainName;
						returnData.Isp = string.IsNullOrEmpty(results["NodeInfo"]["isp"].ToString()) ? "NIL" : "China " + Enum.GetName(typeof(IspEnum), Convert.ToInt32(results["NodeInfo"]["isp"]));
						returnData.City = Enum.GetName(typeof(CitiesEnum), Convert.ToInt32(results["NodeInfo"]["city_id"]));
						returnData.Province = Enum.GetName(typeof(ProvincesEnum), Convert.ToInt32(results["NodeInfo"]["pro_id"]));
						returnData.ErrMsg = results["ErrMsg"].ToString();
						returnData.ExecutionTime = executionTime;
						returnData.Verified = "Error";
						returnData.IpFrom = "NIL";
						returnData.Redirection = "NIL";
						returnData.ResolutionIp = "NIL";
						returnData.ExecutionTime = executionTime;
					}
					else
					{
						//var ipInfo = new WebClient().DownloadString("http://ipinfo.io/" + results["SrcIP"]);

						returnData.DomainName = domainName;
						returnData.Isp = string.IsNullOrEmpty(results["NodeInfo"]["isp"].ToString()) ? "NIL" : "China " + Enum.GetName(typeof(IspEnum), Convert.ToInt32(results["NodeInfo"]["isp"]));
						returnData.City = Enum.GetName(typeof(CitiesEnum), Convert.ToInt32(results["NodeInfo"]["city_id"]));
						returnData.Province = Enum.GetName(typeof(ProvincesEnum), Convert.ToInt32(results["NodeInfo"]["pro_id"]));
						returnData.ResolutionIp = string.IsNullOrEmpty(results["SrcIP"].ToString()) ? "NIL" : results["SrcIP"].ToString();
						returnData.IpFrom = results["srcip"]["srcip_from"].ToString();
						returnData.ErrMsg = "NIL";
						returnData.NsLookupTime = (decimal)results["NsLookup"];
						returnData.ConnectionTime = (decimal)results["ConnectTime"];
						returnData.TimeToFirstByte = (decimal)results["TTFBTime"];
						returnData.DownloadTime = (decimal)results["DownTime"];
						returnData.TotalTime = (decimal)results["TotalTime"];
						returnData.ExecutionTime = executionTime;

						var testing = results["HttpHead"].ToString();
						var testresult = Convert.FromBase64String(testing);
						var resultSplitArray = Regex.Split(Encoding.UTF8.GetString(testresult), "\r\n").ToArray();

						foreach (var result in resultSplitArray)
						{
							if (!result.ToLower().Contains("location: http")) continue;

							var redirected = Regex.Split(result, "Location: ")[1];
							returnData.Redirection = string.IsNullOrEmpty(returnData.Redirection)
													? redirected
													: returnData.Redirection + "," + redirected;
						}

						returnData.Redirection = string.IsNullOrEmpty(returnData.Redirection) ? "NIL" : returnData.Redirection;
					}
					returnList.Add(returnData);
				}
				catch (Exception ex)
				{
					BackendFunction.log.Error(ex);
				}
			}
			VerifyAndSaveData(returnList, executionTime);
		}

		public void VerifyAndSaveData(List<ResultsReturned> returnResults, DateTime executionTime)
		{
			var dmnRegIp = hijackingDomainService.GetAll();
			var tfcdnServer = _TFCDNserversService.GetAll();

			foreach (var result in returnResults)
			{
				try
				{
					if (!result.ErrMsg.Equals("NIL"))
					{
						result.Verified = $"Connection Error - {result.ErrMsg}";
					}
					else
					{
						var multipleRecords = dmnRegIp.Any(s => (s.Protocol + s.Domain).Equals(result.DomainName) && s.DestinationIp.Contains(","));
						var verifying = false;

						if (multipleRecords)
						{
							var records = dmnRegIp.FirstOrDefault(s => (s.Protocol + s.Domain).Equals(result.DomainName)).DestinationIp.Split(',');
							foreach (var record in records)
							{
								if (verifying == true) break;
								if (record.Equals("TFCDNservers"))
								{
									verifying = tfcdnServer.Any(s => s.ServerIp.Equals(result.ResolutionIp));
								}
								else
								{
									verifying = record.Equals(result.ResolutionIp);
								}
							}
						}
						else
						{
							verifying = dmnRegIp.Any(s => s.Domain.Equals(result.DomainName) && s.DestinationIp.Equals(result.ResolutionIp));
						}

						if (verifying)
						{
							result.Verified = "Normal";
						}
						else
						{
							result.Verified = "Caution - Dns Hijacked";
						}

						if (result.Verified.Equals("Normal") && !string.IsNullOrEmpty(result.Redirection) && !result.Redirection.Equals("NIL"))
						{
							var domainParser = new DomainParser(new WebTldRuleProvider());
							var regRedirection = new DomainParser(new WebTldRuleProvider());

							var redirectionList = new List<string>();

							if (!result.Redirection.Contains(","))
							{
								redirectionList.Add(result.Redirection);
							}
							else
							{
								redirectionList = result.Redirection.Split(',').ToList();
							}

							var domainName = domainParser.Get(redirectionList.LastOrDefault());

							verifying = dmnRegIp.Any(p => p.Domain.Equals(result.DomainName) && !string.IsNullOrEmpty(p.Redirection))
								? dmnRegIp.Any(z => z.Domain.Equals(result.DomainName) && regRedirection.Get(z.Redirection).Domain.Equals(domainName.Domain))
								: true;

							if (!verifying)
							{
								result.Verified = "Caution - Http Hijacked";
							}
						}
					}
				}
				catch (Exception ex)
				{
					BackendFunction.log.Error(ex);
				}
			}
			foreach (var record in returnResults)
			{
				var newRecord = new HijackingTestResultDto()
				{
					Domain = record.DomainName,
					Province = record.Province,
					Location = record.City,
					Isp = record.Isp,
					DestinationIp = record.ResolutionIp,
					Destination = record.IpFrom,
					Redirection = record.Redirection,
					Verified = record.Verified,
					Summarized = false,
					ExecutionDate = executionTime,
					ErrMsg = record.ErrMsg,
					NsLookupTime = Math.Round(record.NsLookupTime, 4),
					ConnectionTime = Math.Round(record.ConnectionTime, 4),
					TimeToFirstByte = Math.Round(record.TimeToFirstByte, 4),
					DownloadTime = Math.Round(record.DownloadTime, 4),
					TotalTime = Math.Round(record.TotalTime, 4)
				};
				hijackingTestResultService.AddOrEdit(newRecord);
			}
		}
	}
}

