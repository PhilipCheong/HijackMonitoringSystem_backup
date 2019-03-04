using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance_Monitoring.Repository
{
	public class ViewModels
	{
		public string RequestId { get; set; }
		public string Url { get; set; }
		public string Type { get; set; }
		public decimal Length { get; set; }
		public string Protocol { get; set; }
		public int Status { get; set; }
		public string ServerIp { get; set; }
		public decimal RequestTime { get; set; }
		public decimal Dns { get; set; }
		public decimal Connect { get; set; }
		public decimal SSL { get; set; }
		public decimal Send { get; set; }
		public decimal TimeToFirstByte { get; set; }
		public decimal Load { get; set; }
		public decimal Download { get; set; }
	}

	public class RequestData
	{
		public decimal TimeStamp { get; set; }
		public string RequestId { get; set; }
		public string Url { get; set; }
	}
	public class DataReceived
	{
		public string RequestId { get; set; }
		public decimal TimeStamp { get; set; }
	}
	public class LoadingFinished
	{
		public string RequestId { get; set; }
		public decimal TimeStamp { get; set; }
	}
	public class LoadingFailed
	{
		public string RequestId { get; set; }
		public decimal TimeStamp { get; set; }
	}

	public class ResponseParams
	{
		public string RequestId { get; set; }
		public decimal TimeStamp { get; set; }
	}
	public class ResponseDetail
	{
		public string Url { get; set; }
		public string Status { get; set; }
		public string MimeType { get; set; }
		public string RemoteIPAddress { get; set; }
		public string Protocol { get; set; }
	}
	public class ResponseRawData
	{
		public decimal RequestTime { get; set; }
		public decimal ProxyStart { get; set; }
		public decimal ProxyEnd { get; set; }
		public decimal DnsStart { get; set; }
		public decimal DnsEnd { get; set; }
		public decimal ConnectStart { get; set; }
		public decimal ConnectEnd { get; set; }
		public decimal SslStart { get; set; }
		public decimal SslEnd { get; set; }
		public decimal WorkerStart { get; set; }
		public decimal WorkerReady { get; set; }
		public decimal SendStart { get; set; }
		public decimal SendEnd { get; set; }
		public decimal PushStart { get; set; }
		public decimal PushEnd { get; set; }
		public decimal ReceiveHeadersEnd { get; set; }
	}
	public class ResponseModel
	{
		public ResponseParams ResponseParams { get; set; }
		public ResponseDetail ResponseDetail { get; set; }
		public ResponseRawData ResponseRawData { get; set; }
	}
	public class ResponseData
	{
		public decimal RequestTime { get; set; }
		public decimal ResponseTime { get; set; }
		public string Url { get; set; }
		public string MimeType { get; set; }
		public string RemoteIPAddress { get; set; }
		public string Protocol { get; set; }
		public string Status { get; set; }
		public decimal Dns { get; set; }
		public decimal Connect { get; set; }
		public decimal SSL { get; set; }
		public decimal Send { get; set; }
		public decimal TTFB { get; set; }
		public decimal Download { get; set; }
	}
}
