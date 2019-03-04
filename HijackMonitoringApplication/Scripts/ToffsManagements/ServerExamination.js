var ServerExamination = {
	init: function () {
		$("#ToStartDate").datetimepicker({ theme: 'dark', dateFormat: "yy-mm-dd HH:ii:ss" });
		$("#ToEndDate").datetimepicker({ theme: 'dark', dateFormat: "yy-mm-dd HH:ii:ss" });
		$("#ToStartDate").datetimepicker({
			onShow: function () {
				this.setOptions({
					allowTimes: function getArr() {
						var allowTimes = [
							'00:00', '01:00', '02:00', '03:00', '04:00',
							'05:00', '06:00', '07:00', '08:00', '09:00',
							'10:00', '11:00', '12:00', '13:00', '14:00',
							'15:00', '16:00', '17:00', '18:00', '19:00',
							'20:00', '21:00', '22:00', '23:00'
						];
						return allowTimes;
					}()
				});
			},
			timepicker: true

		});
		$("#ToEndDate").datetimepicker({
			onShow: function () {
				var selectedDate = $('#ToStartDate').datetimepicker('getValue');
				var plusDate = new Date();
				plusDate.setDate(selectedDate.getDate() + 2);
				this.setOptions({
					minDate: $("#ToStartDate").val(),
					startDate: $("#ToStartDate").val(),
					maxDate: plusDate,
					allowTimes: function getArr() {
						var allowTimes = [
							'00:00', '01:00', '02:00', '03:00', '04:00',
							'05:00', '06:00', '07:00', '08:00', '09:00',
							'10:00', '11:00', '12:00', '13:00', '14:00',
							'15:00', '16:00', '17:00', '18:00', '19:00',
							'20:00', '21:00', '22:00', '23:00'
						];
						return allowTimes;
					}()

				});
			},
			timepicker: true
		});
		ServerExamination.ServerCname();
		$("#ChartTable").attachDragger();
	},

	PlotResults: function () {
		var cname = $('#inputSearch').val();
		var start = $('#ToStartDate').val();
		var end = $('#ToEndDate').val();
		$.ajax({
			url: "/ServerExamination/GetResult",
			dataType: "json",
			data: { serverName: cname, start: start, end: end },
			method: "POST",
			success: function (data) {
				if (data !== undefined) {
					ServerExamination.ChartDiv(data.length, cname);
					$("#Export").css("display", "initial");
					for (var i = 0; i < data.length; i++) {
						ServerExamination.PlotProvinceData(data[i], i);
						ServerExamination.PlotIspData(data[i], i);
						ServerExamination.PlotHighestData(data[i], i);
						ServerExamination.PlotSlowestCount(data[i], i);
						ServerExamination.PlotSummaryTable(data[i], i);
					}
				}
			}
		});

	},

	ChartDiv: function (dataLength, server) {
		var mainSectionHeader = $("#chartContainerHeader");
		var mainSection = $("#chartContainer");
		mainSection.html('');
		mainSectionHeader.html('');
		if (server !== undefined || server !== null || server !== "") {
			var multiple = dataLength > 1;
			var width;
			if (multiple) {
				var multipleServer = server.split(",");
				width = "1160px";
				mainSectionHeader.append("<tr>");
				for (var z = 0; z < multipleServer.length; z++) {
					mainSectionHeader.append('<th><div><p style="color:green; font-size:25px;">' + multipleServer[z] + '</p></div></th>');
				}
				mainSection.append("</tr>");
			} else {
				width = "100%";
				mainSectionHeader.append("<tr>");
				mainSectionHeader.append('<th><div><p style="color:green; font-size:25px;">' + server + '</p></div></th>');
				mainSectionHeader.append("</tr>");
			}
			if (width === "100%") {
				$("#ChartTable").removeClass("table-responsive");
			}
			mainSection.append('<tr>');
			for (var i = 0; i < dataLength; i++) {
				mainSection.append('<td><div class="row" style="width:' + width + '"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div><div id="provinceInfo" class="panel panel-info"><p>Province Result (second)</p></div><div  class="panel-body-map"><div id="chartProvince[' +
					i + ']" style="height: 480px;"></div></div></div></div></td>');
			}
			mainSection.append('</tr><tr>');
			for (var o = 0; o < dataLength; o++) {
				mainSection.append('<td><div class="row" style="width:' + width + '"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div><div id="ispInfo" class="panel panel-info"><p>Isp Average Result (second)</p></div><div  class="panel-body-map"><div id="chartIsp[' +
					o +
					']" style="height: 480px;"></div></div ></div ></div ></td >');
			}
			mainSection.append('</tr><tr>');
			for (var p = 0; p < dataLength; p++) {
				mainSection.append('<td><div class="row" style="width:' + width + '"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div > <div id="highestInfo" class="panel panel-info"><p>Highest Resolution Time (second)</p></div> <div class="panel-body-map"><div id="HighestResponse[' +
					p +
					']" style="height: 480px;"></div></div></div ></div ></td >');
			}
			mainSection.append('</tr><tr>');
			for (var k = 0; k < dataLength; k++) {
				mainSection.append('<td><div class="row" style="width:' + width + '"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div > <div id="slowestInfo" class="panel panel-info"><p>Resolution Status</p></div> <div class="panel-body-map"><div id="SlowestCount[' +
					k +
					']" style="height: 480px;"></div></div></div ></div></td >');
			}
			mainSection.append('</tr><tr>');
			for (var t = 0; t < dataLength; t++) {
				mainSection.append('<td><div class="row" style="width:' + width + '"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div><div id="summaryTable" class="panel panel-info"><p>Summary Table</p></div> <div class="panel-body-map"><div id="SummaryTable[' +
					t +
					']" class="table-responsive" style="height: 480px;"></div></div></div ></div></td >');
			}
			mainSection.append('</tr>');
		}
		//mainSection.append('<div class="row"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div><div id="provinceInfo" class="panel panel-info"><p>Province Result (second)</p></div><div class="panel-body-map">' +
		//	'<div id="ProvinceResult" style="height: 480px;"></div></div></div></div><div class="row"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div><div id="ispInfo" class="panel panel-info">' +
		//	'<p>Isp Average Result (second)</p></div > <div class="panel-body-map"><div id="IspResult" style="height: 480px;"></div></div></div ></div > <div class="row"><div class="col-lg-12 col-md-12"><div class="panel panel-default">' +
		//	'</div > <div id="highestInfo" class="panel panel-info"><p>Highest Resolution Time (second)</p></div> <div class="panel-body-map"><div id="HighestResponse" style="height: 480px;"></div></div></div ></div >' +
		//	'<div class="row"><div class="col-lg-12 col-md-12"><div class="panel panel-default"></div><div id="slowestInfo" class="panel panel-info"><p>Resolution Status</p></div><div class="panel-body-map"><div id="SlowestCount" style="height: 480px;"></div></div></div></div>');
	},

	PlotProvinceData: function (data, i) {
		var lineChart = echarts.init(document.getElementById('chartProvince[' + i + ']'));

		var option = {
			title: {
				text: 'Province Result (Hourly Average)',
				show: false,
				textStyle: {
					color: "red",
					fontStyle: "oblique",
					fontWeight: "Bold"
				}
			},
			tooltip: {
				trigger: 'axis',
				axisPointer: {
					type: 'cross',
					lineStyle: {
						type: 'dashed',
						width: 1
					}

				}
			},
			toolbox: {
			},
			legend: {
			},
			xAxis: { type: 'category' },
			yAxis: {},
			dataset: {
				source: data.ProvinceData
			},
			dataZoom: {
				type: 'inside',
				show: true,
				realtime: true
			},
			series: [
				{
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}
			]

		};

		// use configuration item and data specified to show chart
		lineChart.setOption(option);
	},

	PlotIspData: function (data, i) {

		var lineChart = echarts.init(document.getElementById('chartIsp[' + i + ']'));

		var option = {
			title: {
				text: 'Isp Resolution (Hourly Average)',
				show: false,
				textStyle: {
					color: "red",
					fontStyle: "oblique",
					fontWeight: "Bold"
				}
			},
			tooltip: {
				trigger: 'axis',
				axisPointer: {
					type: 'cross',
					lineStyle: {
						type: 'dashed',
						width: 1
					}

				}
			},
			toolbox: {
			},
			legend: {
			},
			xAxis: { type: 'category' },
			yAxis: {},
			dataset: {
				source: data.IspData
			},
			dataZoom: {
				type: 'inside',
				show: true,
				realtime: true
			},
			series: [
				{
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}
			]

		};

		// use configuration item and data specified to show chart
		lineChart.setOption(option);
	},

	PlotHighestData: function (data, i) {

		var lineChart = echarts.init(document.getElementById('HighestResponse[' + i + ']'));

		var option = {
			title: {
				text: 'Hourly Slowest Response',
				show: false,
				textStyle: {
					color: "red",
					fontStyle: "oblique",
					fontWeight: "Bold",
					align: "center",
					verticalAlign: "middle"
				}
			},
			tooltip: {
				trigger: 'axis',
				axisPointer: {
					type: 'cross',
					lineStyle: {
						type: 'dashed',
						width: 1
					}

				}
			},
			toolbox: {
			},
			legend: {
			},
			xAxis: { type: 'category' },
			yAxis: {},
			dataset: {
				source: data.HighestData
			},
			dataZoom: {
				type: 'inside',
				show: true,
				realtime: true
			},
			series: [
				{
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}, {
					type: 'line',
					smooth: true,
					lineStyle: {
						normal: {
							width: 3,
							shadowColor: 'rgba(0,0,0,0.4)',
							shadowBlur: 10,
							shadowOffsetY: 10,
							hoverAnimation: true
						}
					},
					seriesLayoutBy: 'row'
				}
			]

		};

		// use configuration item and data specified to show chart
		lineChart.setOption(option);
	},

	PlotSlowestCount: function (data, i) {

		var barChart = echarts.init(document.getElementById('SlowestCount[' + i + ']'));

		option = {
			title: {
				text: 'Total Test Status',
				show: false,
				textStyle: {
					color: "red",
					fontStyle: "oblique",
					fontWeight: "Bold",
					x: "center"
				}
			},
			tooltip: {
				trigger: 'axis',
				axisPointer: {            // 坐标轴指示器，坐标轴触发有效
					type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
				}
			},
			legend: {
			},
			toolbox: {
			},
			dataset: [
				{ source: data.Over3Data },
				{ source: data.Over5Data },
				{ source: data.Over10Data },
				{ source: data.TotalTest }
			],
			calculable: true,
			xAxis: {
				type: 'value'
			},
			yAxis: {
				type: 'category'
			},
			series: [
				{
					datasetIndex: 0,
					type: 'bar',
					stack: "SlowestCount",
					itemStyle: {
						normal:
							{
								label:
									{ show: true, position: 'insideLeft' }
							}
					}
				},
				{
					datasetIndex: 1,
					type: 'bar',
					stack: 'SlowestCount',
					itemStyle: {
						normal:
							{
								label:
									{ show: true, position: 'insideRight' }
							}
					}
				},
				{
					datasetIndex: 2,
					type: 'bar',
					stack: 'SlowestCount',
					itemStyle: {
						normal:
							{
								label:
									{ show: true, position: 'insideRight' }
							}
					}
				},
				{
					datasetIndex: 3,
					type: 'bar',
					stack: 'SlowestCount',
					itemStyle: {
						normal:
							{
								label:
									{ show: true, position: 'insideRight' }
							}
					}
				}
			]
		};
		barChart.setOption(option);

	},

	PlotSummaryTable: function (data, i) {
		var html = '<table id="SumTable[' + i + ']" class="table table-bordered table-striped"><thead><tr><th>Server</th>';
		for (k = 0; k < data.SummaryTable.DistinctTime.length; k++) {
			html += '<th>' + data.SummaryTable.DistinctTime[k] + '</th>';
		}
		html += '</tr></thead/><tbody id="summaryTableBody[' + i + ']"></tbody></table>';

		document.getElementById("SummaryTable[" + i + "]").insertAdjacentHTML("beforeend", html);

		var bodyHtml = '';
		for (var z = 0; z < data.SummaryTable.SummaryValue.length; z++) {
			bodyHtml += '<tr>';
			for (var q = 0; q < data.SummaryTable.SummaryValue[z].length; q++) {
				var average = data.SummaryTable.SummaryValue[z][q].split('|');
				var color = "darkseagreen";
				if (parseFloat(average[0]) >= 1 && parseFloat(average[0]) < 2) {
					color = "moccasin";
				} else if (parseFloat(average[0]) >= 2) {
					color = "indianred";
				}
				bodyHtml += '<td style="background-color:' + color + ';">' + data.SummaryTable.SummaryValue[z][q] + '</td>';
			}
			bodyHtml += '</tr>';
		}
		document.getElementById("summaryTableBody[" + i + "]").insertAdjacentHTML("beforeend", bodyHtml);
	},

	ServerCname: function () {
		GetServerCname(function (serverDataset) {
			var servers = new Bloodhound({

				datumTokenizer: Bloodhound.tokenizers.obj.whitespace("text"),
				queryTokenizer: Bloodhound.tokenizers.whitespace,
				// `states` is an array of state names defined in "The Basics"
				local: serverDataset
			});
			servers.initialize();
			$("#inputSearch").tagComponent({
				typeaheadjs: {
					name: "servers",
					displayKey: "text",
					limit: 1000,
					source: servers.ttAdapter()
				},
				itemValue: "value",
				itemText: "text"
			});

		});

	},

	ExportExcel: function () {
		var cname = $('#inputSearch').val();
		var start = $('#ToStartDate').val();
		var end = $('#ToEndDate').val();

		$.ajax({
			url: "/ServerExamination/ExportToExcel",
			dataType: "json",
			data: { serverCName: cname, startDate: start, endDate: end },
			method: "POST",
			success: function (data) {
				window.location = "/ServerExamination/DownloadReport?downloadfile=" + data.filedownload;
			}
		});
	}
};
$.fn.attachDragger = function () {
	var attachment = false, lastPosition, position, difference;
	$($(this).selector).on("mousedown mouseup mousemove", function (e) {
		if (e.type === "mousedown") attachment = true, lastPosition = [e.clientX, e.clientY];
		if (e.type === "mouseup") attachment = false;
		if (e.type === "mousemove" && attachment === true) {
			position = [e.clientX, e.clientY];
			difference = [(position[0] - lastPosition[0]), (position[1] - lastPosition[1])];
			$(this).scrollLeft($(this).scrollLeft() - difference[0]);
			$(this).scrollTop($(this).scrollTop() - difference[1]);
			lastPosition = [e.clientX, e.clientY];
		}
	});
	$(window).on("mouseup", function () {
		attachment = false;
	});
};