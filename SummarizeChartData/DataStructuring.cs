using HijackMonitoringApplication.BusinessLayer.Dtos;
using HijackMonitoringApplication.BusinessLayer.Services.IndependentServices;
using HijackMonitoringApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummarizeChartData
{
	public class DataStructuring
	{
		public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		private readonly ChartDataService _chartDataService = new ChartDataService();
		private readonly DataFiltering _dataFiltering = new DataFiltering();
		private readonly HijackingTestResultService hijackingTestResultService = new HijackingTestResultService();

		public void SummarizeDataNew(List<HijackingTestResultDto> currentData)
		{
			var mainList = currentData.GroupBy(s => s.Verified).ToList();

			var groupingTest = currentData.GroupBy(s => new { s.Domain, s.Isp, s.ExecutionTime }).ToList();

			var savedDataForChart = _chartDataService.Find(p => p.CreateDate.Value > DateTime.UtcNow.AddDays(-1));

			var dnsHijackedData = mainList.Where(s => s.Key.ToLower().Contains("dns")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.DestinationIp, z.Destination, z.ExecutionTime }).Select(k =>
					  new DnsObject
					  {
						  DomainName = k.Key.Domain,
						  DestinationIp = k.Key.DestinationIp,
						  Isp = k.Key.Isp,
						  Destination = k.Key.Destination,
						  ExecutionTime = k.Key.ExecutionTime,
						  Count = k.Count()
					  }).ToList();

			var httpHijackedData = mainList.Where(s => s.Key.ToLower().Contains("http")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.Redirection, z.ExecutionTime }).Select(k =>
					  new HttpObject
					  {
						  DomainName = k.Key.Domain,
						  Isp = k.Key.Isp,
						  Redirection = k.Key.Redirection,
						  ExecutionTime = k.Key.ExecutionTime,
						  Count = k.Count()
					  })
				.ToList();

			var normalData = mainList.Where(s => s.Key.ToLower().Contains("normal")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.DestinationIp, z.Destination, z.ExecutionTime }).Select(k =>
					  new NormalObject()
					  {
						  DomainName = k.Key.Domain,
						  DestinationIp = k.Key.DestinationIp,
						  Isp = k.Key.Isp,
						  Destination = k.Key.Destination,
						  ExecutionTime = k.Key.ExecutionTime,
						  Count = k.Count()
					  }).ToList();

			var redirectionData = mainList.Where(s => s.Key.ToLower().Contains("normal")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.Redirection, z.ExecutionTime }).Select(k =>
					  new RedirectionObject()
					  {
						  DomainName = k.Key.Domain,
						  Isp = k.Key.Isp,
						  Redirection = k.Key.Redirection,
						  ExecutionTime = k.Key.ExecutionTime,
						  Count = k.Count()
					  }).ToList();

			var provinceData = mainList.Where(s => s.Key.ToLower().Contains("dns") || s.Key.ToLower().Contains("http")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.Province, z.ExecutionTime }).Select(k =>
					  new ProvinceObject()
					  {
						  Domain = k.Key.Domain,
						  Isp = k.Key.Isp,
						  Province = k.Key.Province,
						  DnsCounts = k.Count(s => s.Verified.ToLower().Contains("dns")),
						  HttpCount = k.Count(s => s.Verified.ToLower().Contains("http")),
						  ExecutionTime = k.Key.ExecutionTime
					  }).ToList();

			var errorData = mainList.Where(s => s.Key.ToLower().Contains("error")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.ExecutionTime, z.Verified }).Select(k =>
					  new ErrorObject()
					  {
						  Domain = k.Key.Domain,
						  Isp = k.Key.Isp,
						  ErrMsg = k.Key.Verified,
						  ErrorCount = k.Count(),
						  ExecutionTime = k.Key.ExecutionTime
					  }).ToList();

			var resolutionData = mainList.Where(s => !s.Key.ToLower().Contains("error")).SelectMany(p => p.ToList())
				.GroupBy(z => new { z.Domain, z.Isp, z.ExecutionTime }).Select(k =>
					  new ResolutionTimeObject()
					  {
						  Domain = k.Key.Domain,
						  Isp = k.Key.Isp,
						  NsLookupTime = Math.Round(k.Average(s => s.NsLookupTime), 4),
						  ConnectionTime = Math.Round(k.Average(s => s.ConnectionTime), 4),
						  TimeToFirstByte = Math.Round(k.Average(s => s.TimeToFirstByte), 4),
						  DownloadTime = Math.Round(k.Average(s => s.DownloadTime), 4),
						  TotalTime = Math.Round(k.Average(s => s.TotalTime), 4),
						  ExecutionTime = k.Key.ExecutionTime
					  }).ToList();

			var currentRecordTime = groupingTest.Select(s =>
								s.Key.ExecutionTime.AddMinutes(-s.Key.ExecutionTime.Minute).AddSeconds(-s.Key.ExecutionTime.Second).AddMilliseconds(-s.Key.ExecutionTime.Millisecond)).Distinct().ToList();

			foreach (var groupDomain in groupingTest)
			{
				try
				{
					var dnsModelToSave = _dataFiltering.DnsHijackedModel(dnsHijackedData, groupDomain.Key.Domain, groupDomain.Key.Isp);
					var httpModelToSave = _dataFiltering.HttpHijackedModel(httpHijackedData, groupDomain.Key.Domain, groupDomain.Key.Isp);
					var normalModelToSave = _dataFiltering.NormalResolutionModel(normalData, groupDomain.Key.Domain, groupDomain.Key.Isp);
					var redirectModelToSave = _dataFiltering.RedirectionModel(redirectionData, groupDomain.Key.Domain, groupDomain.Key.Isp);
					var provinceModelToSave = _dataFiltering.ProvinceHijackedModel(provinceData, groupDomain.Key.Domain, groupDomain.Key.Isp);
					var errorModelToSave = _dataFiltering.ErrorModel(errorData, groupDomain.Key.Domain, groupDomain.Key.Isp);
					var resolutionTimeModelToSave = _dataFiltering.ResolutionTimeModel(resolutionData, groupDomain.Key.Domain, groupDomain.Key.Isp);




					foreach (var recordTime in currentRecordTime)
					{
						if (savedDataForChart.Any(s => s.DataHourCategory.Equals(recordTime) && s.Domain.Equals(groupDomain.Key.Domain) && s.Isp.Equals(groupDomain.Key.Isp)))
						{
							var existedData = savedDataForChart.FirstOrDefault(s => s.DataHourCategory.Equals(recordTime) && s.Domain.Equals(groupDomain.Key.Domain) && s.Isp.Equals(groupDomain.Key.Isp));

							if (existedData != null)
							{
								if (dnsModelToSave.HijackedToDestinationModel != null)
								{
									if (existedData.DnsHijackedCounts.HijackedToDestinationModel == null)
									{
										existedData.DnsHijackedCounts.HijackedToDestinationModel = dnsModelToSave.HijackedToDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
										existedData.DnsHijackedCounts.DnsHijackedCount += dnsModelToSave.HijackedToDestinationModel.Where(p => p.ExecutionTime.Equals(recordTime)).Sum(s => s.DestinationCount);
									}
									else
									{
										foreach (var hijackedData in dnsModelToSave.HijackedToDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											if (existedData.DnsHijackedCounts.HijackedToDestinationModel.Any(s => s.HijackedToIps.Equals(hijackedData.HijackedToIps)))
											{
												existedData.DnsHijackedCounts.HijackedToDestinationModel.First(s => s.HijackedToIps.Equals(hijackedData.HijackedToIps)).DestinationCount += hijackedData.DestinationCount;
												existedData.DnsHijackedCounts.DnsHijackedCount += hijackedData.DestinationCount;
											}
											else
											{
												var newData = new DnsHijackedDestinationModel()
												{
													HijackedToIps = hijackedData.HijackedToIps,
													Destination = hijackedData.Destination,
													DestinationCount = hijackedData.DestinationCount
												};
												existedData.DnsHijackedCounts.HijackedToDestinationModel.Add(newData);
												existedData.DnsHijackedCounts.DnsHijackedCount += hijackedData.DestinationCount;
											}
										}
									}
								}
								if (httpModelToSave.HttpHijackedDestinationModel != null)
								{
									if (existedData.HttpHijackedCounts.HttpHijackedDestinationModel == null)
									{
										existedData.HttpHijackedCounts.HttpHijackedDestinationModel = httpModelToSave.HttpHijackedDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
										existedData.HttpHijackedCounts.HttpHijackedCounts += httpModelToSave.HttpHijackedDestinationModel.Where(p => p.ExecutionTime.Equals(recordTime)).Sum(s => s.RedirectedCounts);
									}
									else
									{
										foreach (var hijackedData in httpModelToSave.HttpHijackedDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											if (existedData.HttpHijackedCounts.HttpHijackedDestinationModel.Any(s => s.RedirectedDestination.Equals(hijackedData.RedirectedDestination)))
											{
												existedData.HttpHijackedCounts.HttpHijackedDestinationModel.First(s => s.RedirectedDestination.Equals(hijackedData.RedirectedDestination)).RedirectedCounts += hijackedData.RedirectedCounts;
												existedData.HttpHijackedCounts.HttpHijackedCounts += hijackedData.RedirectedCounts;
											}
											else
											{
												var newData = new HttpHijackedDestinationModel()
												{
													RedirectedDestination = hijackedData.RedirectedDestination,
													RedirectedCounts = hijackedData.RedirectedCounts
												};
												existedData.HttpHijackedCounts.HttpHijackedDestinationModel.Add(newData);
												existedData.HttpHijackedCounts.HttpHijackedCounts += hijackedData.RedirectedCounts;
											}
										}
									}
								}
								if (normalModelToSave.NormalResolutionDestinationModel != null)
								{
									if (existedData.DestinationIpInfo.NormalResolutionDestinationModel == null)
									{
										existedData.DestinationIpInfo.NormalResolutionDestinationModel = normalModelToSave.NormalResolutionDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
										existedData.DestinationIpInfo.NormalCount += normalModelToSave.NormalResolutionDestinationModel.Where(p => p.ExecutionTime.Equals(recordTime)).Sum(s => s.IpCounts);
									}
									else
									{
										foreach (var hijackedData in normalModelToSave.NormalResolutionDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											if (existedData.DestinationIpInfo.NormalResolutionDestinationModel.Any(s => s.Ip.Equals(hijackedData.Ip)))
											{
												existedData.DestinationIpInfo.NormalResolutionDestinationModel.First(s => s.Ip.Equals(hijackedData.Ip)).IpCounts += hijackedData.IpCounts;
												existedData.DestinationIpInfo.NormalCount += hijackedData.IpCounts;
											}
											else
											{
												var newData = new NormalResolutionDestinationModel()
												{
													Ip = hijackedData.Ip,
													Destination = hijackedData.Destination,
													IpCounts = hijackedData.IpCounts
												};
												existedData.DestinationIpInfo.NormalResolutionDestinationModel.Add(newData);
												existedData.DestinationIpInfo.NormalCount += hijackedData.IpCounts;
											}
										}
									}

								}
								if (redirectModelToSave.NormalRedirectionDestinationModel != null)
								{
									if (existedData.RedirectionInfo.NormalRedirectionDestinationModel == null)
									{
										existedData.RedirectionInfo.NormalRedirectionDestinationModel = redirectModelToSave.NormalRedirectionDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
										existedData.RedirectionInfo.RedirectionCounts += redirectModelToSave.NormalRedirectionDestinationModel.Where(p => p.ExecutionTime.Equals(recordTime)).Sum(s => s.RedirectedCounts);
									}
									else
									{
										foreach (var hijackedData in redirectModelToSave.NormalRedirectionDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											if (existedData.RedirectionInfo.NormalRedirectionDestinationModel.Any(s => s.RedirectToDestination.Trim().Equals(hijackedData.RedirectToDestination.Trim())))
											{
												existedData.RedirectionInfo.NormalRedirectionDestinationModel.First(s => s.RedirectToDestination.Trim().Equals(hijackedData.RedirectToDestination.Trim())).RedirectedCounts += hijackedData.RedirectedCounts;
												existedData.RedirectionInfo.RedirectionCounts += hijackedData.RedirectedCounts;
											}
											else
											{
												var newData = new NormalRedirectionDestinationModel()
												{
													RedirectToDestination = hijackedData.RedirectToDestination,
													RedirectedCounts = hijackedData.RedirectedCounts
												};
												existedData.RedirectionInfo.NormalRedirectionDestinationModel.Add(newData);
												existedData.RedirectionInfo.RedirectionCounts += hijackedData.RedirectedCounts;
											}
										}
									}
								}
								if (provinceModelToSave.ProvinceHijackedDestinationModel != null)
								{
									if (existedData.Province.ProvinceHijackedDestinationModel == null)
									{
										existedData.Province.ProvinceHijackedDestinationModel = provinceModelToSave.ProvinceHijackedDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
									}
									else
									{
										foreach (var hijackedData in provinceModelToSave.ProvinceHijackedDestinationModel.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											if (existedData.Province.ProvinceHijackedDestinationModel.Any(s => s.Province.Equals(hijackedData.Province)))
											{
												existedData.Province.ProvinceHijackedDestinationModel.First(s => s.Province.Equals(hijackedData.Province)).DnsCount += hijackedData.DnsCount;
												existedData.Province.ProvinceHijackedDestinationModel.First(s => s.Province.Equals(hijackedData.Province)).HttpCount += hijackedData.HttpCount;
											}
											else
											{
												var newData = new ProvinceHijackedDestinationModel()
												{
													Province = hijackedData.Province,
													DnsCount = hijackedData.DnsCount,
													HttpCount = hijackedData.HttpCount
												};
												existedData.Province.ProvinceHijackedDestinationModel.Add(newData);
											}
										}

									}
								}
								if (errorModelToSave.ErrorMessange != null)
								{
									if (existedData.ErrInfo.ErrorMessange == null)
									{
										existedData.ErrInfo.ErrorMessange = errorModelToSave.ErrorMessange.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
										existedData.ErrInfo.ErrorCounts += errorModelToSave.ErrorMessange.Where(p => p.ExecutionTime.Equals(recordTime)).Sum(s => s.ErrCount);
									}
									else
									{
										foreach (var hijackedData in errorModelToSave.ErrorMessange.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											if (existedData.ErrInfo.ErrorMessange.Any(s => s.ErrMsg.Equals(hijackedData.ErrMsg)))
											{
												existedData.ErrInfo.ErrorMessange.First(s => s.ErrMsg.Equals(hijackedData.ErrMsg)).ErrCount += hijackedData.ErrCount;
												existedData.ErrInfo.ErrorCounts += hijackedData.ErrCount;
											}
											else
											{
												var newData = new ErrorDestinationModel()
												{
													ErrMsg = hijackedData.ErrMsg,
													ErrCount = hijackedData.ErrCount
												};
												existedData.ErrInfo.ErrorMessange.Add(newData);
												existedData.ErrInfo.ErrorCounts += hijackedData.ErrCount;
											}
										}
									}

								}
								if (resolutionTimeModelToSave.ResolutionTimeDestination != null)
								{
									if (existedData.ResolutionTimeModel.ResolutionTimeDestination.First().TotalTime == 0)
									{
										existedData.ResolutionTimeModel.ResolutionTimeDestination = resolutionTimeModelToSave.ResolutionTimeDestination.Where(s => s.ExecutionTime.Equals(recordTime)).ToList();
									}
									else
									{
										foreach (var hijackedData in resolutionTimeModelToSave.ResolutionTimeDestination.Where(s => s.ExecutionTime.Equals(recordTime)))
										{
											existedData.ResolutionTimeModel.ResolutionTimeDestination.First().NsLookupTime = (existedData.ResolutionTimeModel.ResolutionTimeDestination.First().NsLookupTime
																																+ hijackedData.NsLookupTime) / 2;

											existedData.ResolutionTimeModel.ResolutionTimeDestination.First().ConnectionTime = (existedData.ResolutionTimeModel.ResolutionTimeDestination.First().ConnectionTime
																																+ hijackedData.ConnectionTime) / 2;

											existedData.ResolutionTimeModel.ResolutionTimeDestination.First().TimeToFirstByte = (existedData.ResolutionTimeModel.ResolutionTimeDestination.First().TimeToFirstByte
																																+ hijackedData.TimeToFirstByte) / 2;

											existedData.ResolutionTimeModel.ResolutionTimeDestination.First().DownloadTime = (existedData.ResolutionTimeModel.ResolutionTimeDestination.First().DownloadTime
																																+ hijackedData.DownloadTime) / 2;

											existedData.ResolutionTimeModel.ResolutionTimeDestination.First().TotalTime = (existedData.ResolutionTimeModel.ResolutionTimeDestination.First().TotalTime
																																+ hijackedData.TotalTime) / 2;

										}
									}

								}

							}
							_chartDataService.AddOrEdit(existedData);
						}
						else
						{
							var chartDataDto = new ChartDataDto()
							{
								Domain = groupDomain.Key.Domain,
								Isp = groupDomain.Key.Isp,
								DnsHijackedCounts = dnsModelToSave,
								HttpHijackedCounts = httpModelToSave,
								DestinationIpInfo = normalModelToSave,
								RedirectionInfo = redirectModelToSave,
								Province = provinceModelToSave,
								ErrInfo = errorModelToSave,
								ResolutionTimeModel = resolutionTimeModelToSave,
								DataHourCategory = recordTime
							};
							_chartDataService.AddOrEdit(chartDataDto);
						}

					}
				}
				catch (Exception ex)
				{
					log.Error(ex.Message);
				}
			}
		}

		public void UpdateSummarizedData(List<HijackingTestResultDto> summarizedData)
		{
			foreach (var data in summarizedData)
			{
				data.Summarized = true;
				hijackingTestResultService.AddOrEdit(data);
			}
		}
	}
}
