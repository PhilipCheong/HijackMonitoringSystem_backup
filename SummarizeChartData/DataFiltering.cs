using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HijackMonitoringApplication.ViewModel;

namespace SummarizeChartData
{
	public class DataFiltering
	{
		public DnsHijackedModel DnsHijackedModel(List<DnsObject> dnsHijackedData, string domainName, string isp)
		{
			var dnsHijackedRecord = dnsHijackedData
				.Where(s => s.DomainName.Equals(domainName) && s.Isp.Equals(isp)).GroupBy(p => new { p.Destination, p.DestinationIp, p.ExecutionTime, p.Count }).Where(z => !z.Key.DestinationIp.Equals("NIL"))
				.Select(s => new DnsHijackedDestinationModel()
				{
					Destination = s.Key.Destination,
					HijackedToIps = s.Key.DestinationIp,
					DestinationCount = s.Key.Count,
					ExecutionTime = s.Key.ExecutionTime.AddMinutes(-s.Key.ExecutionTime.Minute).AddSeconds(-s.Key.ExecutionTime.Second).AddMilliseconds(-s.Key.ExecutionTime.Millisecond)
				}).ToList();

			var dnsModelToSave = new DnsHijackedModel();

			if (!dnsHijackedRecord.Any()) return dnsModelToSave;
			{
				dnsModelToSave.DnsHijackedCount = dnsHijackedRecord.Sum(s => s.DestinationCount);
				dnsModelToSave.HijackedToDestinationModel = dnsHijackedRecord;
			}

			return dnsModelToSave;
		}

		public HttpHijackedModel HttpHijackedModel(List<HttpObject> httpHijackedData, string domainName, string isp)
		{
			var httpHijackedRecord = httpHijackedData
				.Where(s => s.DomainName.Equals(domainName) && s.Isp.Equals(isp)).GroupBy(p => new { p.Redirection, p.ExecutionTime, p.Count }).Where(z => !z.Key.Redirection.Equals("NIL"))
				.Select(s => new HttpHijackedDestinationModel()
				{
					RedirectedDestination = s.Key.Redirection,
					RedirectedCounts = s.Key.Count,
					ExecutionTime = s.Key.ExecutionTime.AddMinutes(-s.Key.ExecutionTime.Minute).AddSeconds(-s.Key.ExecutionTime.Second).AddMilliseconds(-s.Key.ExecutionTime.Millisecond)
				}).ToList();

			var httpModelToSave = new HttpHijackedModel();

			if (!httpHijackedRecord.Any()) return httpModelToSave;
			{
				httpModelToSave.HttpHijackedCounts = httpHijackedRecord.Sum(s => s.RedirectedCounts);
				httpModelToSave.HttpHijackedDestinationModel = httpHijackedRecord;
			}

			return httpModelToSave;
		}

		public NormalResolutionModel NormalResolutionModel(List<NormalObject> normalData, string domainName, string isp)
		{
			var normalRecord = normalData
				.Where(s => s.DomainName.Equals(domainName) && s.Isp.Equals(isp)).GroupBy(p => new { p.Destination, p.DestinationIp, p.ExecutionTime, p.Count }).Where(z => !z.Key.DestinationIp.Equals("NIL"))
				.Select(s => new NormalResolutionDestinationModel()
				{
					Ip = s.Key.DestinationIp,
					Destination = s.Key.Destination,
					IpCounts = s.Key.Count,
					ExecutionTime = s.Key.ExecutionTime.AddMinutes(-s.Key.ExecutionTime.Minute).AddSeconds(-s.Key.ExecutionTime.Second).AddMilliseconds(-s.Key.ExecutionTime.Millisecond)
				}).ToList();

			var normalModelToSave = new NormalResolutionModel();

			if (!normalRecord.Any()) return normalModelToSave;
			{
				normalModelToSave.NormalCount = normalRecord.Sum(s => s.IpCounts);
				normalModelToSave.NormalResolutionDestinationModel = normalRecord;
			}

			return normalModelToSave;
		}

		public NormalRedirectionModel RedirectionModel(List<RedirectionObject> redirectData, string domainName, string isp)
		{
			var redirectRecord = redirectData
				.Where(s => s.DomainName.Equals(domainName) && s.Isp.Equals(isp)).GroupBy(p => new { p.Redirection, p.ExecutionTime, p.Count }).Where(z => !z.Key.Redirection.Equals("NIL"))
				.Select(s => new NormalRedirectionDestinationModel()
				{
					RedirectToDestination = s.Key.Redirection,
					RedirectedCounts = s.Key.Count,
					ExecutionTime = s.Key.ExecutionTime.AddMinutes(-s.Key.ExecutionTime.Minute).AddSeconds(-s.Key.ExecutionTime.Second).AddMilliseconds(-s.Key.ExecutionTime.Millisecond)
				}).ToList();

			var redirectModelToSave = new NormalRedirectionModel();

			if (!redirectRecord.Any()) return redirectModelToSave;
			{
				redirectModelToSave.RedirectionCounts = redirectRecord.Sum(s => s.RedirectedCounts);
				redirectModelToSave.NormalRedirectionDestinationModel = redirectRecord;
			}

			return redirectModelToSave;
		}
		public ProvinceHijackedModel ProvinceHijackedModel(List<ProvinceObject> provinceData, string domain, string isp)
		{


			var provinceModel = provinceData.Where(p => p.Domain.Equals(domain) && p.Isp.Equals(isp)).Where(z => !z.Province.Equals("NIL")).Select(s => new ProvinceHijackedDestinationModel()
			{
				Province = s.Province,
				DnsCount = s.DnsCounts,
				HttpCount = s.HttpCount,
				ExecutionTime = s.ExecutionTime.AddMinutes(-s.ExecutionTime.Minute).AddSeconds(-s.ExecutionTime.Second).AddMilliseconds(-s.ExecutionTime.Millisecond)
			}).ToList();

			var provinceModelToSave = new ProvinceHijackedModel();

			if (!provinceModel.Any()) return provinceModelToSave;
			{
				provinceModelToSave.ProvinceHijackedDestinationModel = provinceModel;
			}

			return provinceModelToSave;
		}
		public ErrorModel ErrorModel(List<ErrorObject> errorData, string domainName, string isp)
		{
			var errorRecord = errorData
				.Where(s => s.Domain.Equals(domainName) && s.Isp.Equals(isp)).GroupBy(p => new { p.ErrMsg, p.ErrorCount, p.ExecutionTime }).Where(z => !z.Key.ErrMsg.Equals("NIL"))
				.Select(s => new ErrorDestinationModel()
				{
					ErrMsg = s.Key.ErrMsg,
					ErrCount = s.Key.ErrorCount,
					ExecutionTime = s.Key.ExecutionTime.AddMinutes(-s.Key.ExecutionTime.Minute).AddSeconds(-s.Key.ExecutionTime.Second).AddMilliseconds(-s.Key.ExecutionTime.Millisecond)
				}).ToList();

			var errorModelToSave = new ErrorModel();

			if (!errorRecord.Any()) return errorModelToSave;
			{
				errorModelToSave.ErrorCounts = errorRecord.Sum(s => s.ErrCount);
				errorModelToSave.ErrorMessange = errorRecord;
			}

			return errorModelToSave;
		}

		public ResolutionTimeModel ResolutionTimeModel(List<ResolutionTimeObject> resolutionData, string domainName, string isp)
		{
			var resolutionRecord = resolutionData
				.Where(s => s.Domain.Equals(domainName) && s.Isp.Equals(isp)).Select(s => new ResolutionTimeDestinationModel()
				{
					NsLookupTime = s.NsLookupTime,
					ConnectionTime = s.ConnectionTime,
					TimeToFirstByte = s.TimeToFirstByte,
					DownloadTime = s.DownloadTime,
					TotalTime = s.TotalTime,
					ExecutionTime = s.ExecutionTime.AddMinutes(-s.ExecutionTime.Minute).AddSeconds(-s.ExecutionTime.Second).AddMilliseconds(-s.ExecutionTime.Millisecond)
				}).ToList();

			var resolutiontimeModelToSave = new ResolutionTimeModel();

			if (!resolutionRecord.Any()) return resolutiontimeModelToSave;
			{
				resolutiontimeModelToSave.ResolutionTimeDestination = resolutionRecord;
			}

			return resolutiontimeModelToSave;
		}

	}
}
