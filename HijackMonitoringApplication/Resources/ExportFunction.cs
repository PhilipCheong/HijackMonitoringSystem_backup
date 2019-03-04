using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace HijackMonitoringApplication.Resources
{
	public class ExportFunction
	{
		public string ExcelExportFunction(string serverCname, List<List<List<List<string>>>> data)
		{
			char[] Col = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			string[] sheetName = new string[] { "Province Data", "Isp Data", "Highest Resolution Time", "Over 5", "Over 10", "Total Test" };
			XLWorkbook workBook = new XLWorkbook();
			int rowsCount = 0;
			int colCount = 1;
			int sheetCount = 0;
			var servers = new List<string>();
			var serversCount = 0;
			if (serverCname.Contains(","))
			{
				servers = serverCname.Split(',').ToList();
			}
			else
			{
				servers.Add(serverCname);
			}
			#region Closed XML
			//foreach (var hours in header)
			//{
			//	var headerRows = workSheet.Cell("A" + counts.ToString());
			//	headerRows.Value = hours;
			//	headerRows.WorksheetColumn().AdjustToContents();
			//	counts++;
			//}
			foreach (var x in data)
			{
				rowsCount = 0;
				colCount = 1;
				var workSheet = workBook.Worksheets.Add(sheetName[sheetCount]);
				int tempCount = 0;
				bool multiple = false;
				int yCount = 0;

				foreach (var z in x)
				{
					var writeName = servers[serversCount];
					if (tempCount == 0)
					{
						var nameCol = workSheet.Cell("A" + colCount);
						nameCol.Value = writeName;
						nameCol.WorksheetColumn().AdjustToContents();
						colCount++;
					}
					else
					{
						var nameCol = workSheet.Cell("A" + tempCount);
						nameCol.Value = writeName;
						nameCol.WorksheetColumn().AdjustToContents();
						tempCount++;
					}
					if(serversCount == servers.Count() - 1)
					{
						serversCount = 0;
					}
					else
					{
						serversCount++;
					}
					foreach (var y in z)
					{
						colCount = multiple == true ? tempCount : 2;
						yCount = yCount > y.Count() ? yCount : y.Count();
						if (y.Count() < yCount)
						{
							int difference = yCount - y.Count();
							for (var p = 0; p < difference; p++)
							{
								y.Add("-");
							}
						}
						for (var k = 0; k < yCount; k++)
						{
							var domainName = workSheet.Cell(Col[rowsCount].ToString() + colCount.ToString());
							domainName.Value = y[k] == null ? "0" : y[k];
							domainName.Style.Border.SetTopBorder(XLBorderStyleValues.Medium);
							domainName.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium);
							domainName.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium);
							domainName.Style.Border.SetRightBorder(XLBorderStyleValues.Medium);
							domainName.WorksheetColumn().AdjustToContents();
							colCount += 1;
							if (y[k].Equals(y.Last()))
							{
								if (multiple == false)
								{
									tempCount = colCount;
								}
								else
								{
									tempCount = colCount - yCount;
								}
							}
						}
						rowsCount++;
					}
					rowsCount = 0;
					if (multiple == false)
					{
						tempCount = colCount + 2;
					}
					else
					{
						tempCount = tempCount + yCount + 2;
					}
					multiple = true;
				}
				sheetCount++;
			}


			try
			{
				var fileName = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString() + "-" + DateTime.Now.ToLocalTime().ToString("dd-mm-yy") + "_" + "ServersReport.xlsx";
				var servePath = HttpContext.Current.Server.MapPath("~");
				workBook.SaveAs(servePath + "\\Downloads\\" + fileName);
				return "Downloads\\" + fileName;
			}
			catch (Exception ex)
			{

			}

			return "";
			#endregion

		}
	}
}