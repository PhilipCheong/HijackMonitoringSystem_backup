var PlotChart = {
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
				this.setOptions({
					minDate: $("#ToStartDate").val(),
					startDate: $("#ToStartDate").val(),
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
		PlotChart.AllowedDomain();
	},

	HijackCountLineChart: function () {
		var searchForm = document.getElementById('GenerateChart');
		var domain = $('#inputSearch').val();
		var start = $('#ToStartDate').val();
		var end = $('#ToEndDate').val();
		$.ajax({
			url: "/ChartsAndReports/HijackedCountsLine",
			dataType: "json",
			data: { domainName: domain, start: start, end: end },
			method: "POST",
			success: function (data) {
				if (data.HijackedLine !== undefined) {
					PlotChart.PlotHijackCountLineChart(data);
					PlotChart.HijackedIspLinePie(data);
					PlotChart.HijackedDetailNestedPie(data);
					PlotChart.HijackedProvinceBar(data);
					PlotChart.HijackedResolutionTime(data);
				}
			}
		});

	},

	PlotHijackCountLineChart: function (data) {

		var lineChart = echarts.init(document.getElementById('HijackLineChart'));

		var option = {
			title: {
				text: 'Hijacked Conditions',
				show: false,
				textStyle: {
					color: "red",
					fontStyle: "oblique",
					fontWeight: "Bold",
					align: "center",
					verticalAlign: "middle"
				}
			},
			color: ['#37A2DA', '#32C5E9', '#67E0E3', '#9FE6B8', '#FFDB5C', '#ff9f7f', '#fb7293', '#E062AE', '#E690D1', '#e7bcf3', '#9d96f5', '#8378EA', '#96BFFF'],
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
			legend: {
				//data: ['Dns Hijacked', 'Http Hijacked']
			},
			xAxis: { type: 'category' },
			yAxis: {},
			dataset: {
				source: data.HijackedLine
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

	HijackedIspLinePie: function (data) {

		var HijackedISP = echarts.init(document.getElementById('HijackedISP'));


		option = {
			legend: {},
			tooltip: {
				trigger: 'axis',
				showContent: false
			},
			toolbox: {
				y: 'bottom',
				feature: {
					mark: { show: true },
					dataView: { show: true, readOnly: false },
					magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
					restore: { show: true },
					saveAsImage: { show: true },
					trigger: 'axis',
					axisPointer: {
						type: 'cross',
						lineStyle: {
							type: 'dashed',
							width: 1
						}

					}

				}
			},
			dataset: {
				source: data.HijackedIspLinePie
			},
			xAxis: { type: 'category' },
			yAxis: { gridIndex: 0 },
			grid: { top: '55%' },
			series: [
				{ type: 'line', smooth: true, seriesLayoutBy: 'row' },
				{ type: 'line', smooth: true, seriesLayoutBy: 'row' },
				{ type: 'line', smooth: true, seriesLayoutBy: 'row' },
				{
					type: 'pie',
					id: 'pie',
					radius: '30%',
					center: ['50%', '25%'],
					label: {
						formatter: '{b}: ({d}%)'
					},
					encode: {
						itemName: 'Date',
						//value: ''
						//tooltip: '2012'
					}
				}
			]
		};

		HijackedISP.on('updateAxisPointer', function (event) {
			var xAxisInfo = event.axesInfo[0];
			if (xAxisInfo) {
				var dimension = xAxisInfo.value + 1;
				HijackedISP.setOption({
					series: {
						id: 'pie',
						label: {
							formatter: '{b}: {@[' + dimension + ']} ({d}%)'
						},
						encode: {
							value: dimension,
							tooltip: dimension
						}
					}
				});
			}
		});
		HijackedISP.setOption(option);
	},

	HijackedDetailNestedPie: function (data) {

		var hijackTypePie = echarts.init(document.getElementById('HijackTypePie'));


		option = {
			tooltip: {
				trigger: 'item',
				formatter: "{a} <br/>{b}: {c} ({d}%)"
			},
			legend: {
				orient: 'vertical',
				x: 'left'
			},
			dataset: [
				{ source: data.HijackedSmallPie },
				{ source: data.HijackedBigPie }
			],
			toolbox: {
				show: true,
				feature: {
					mark: { show: true },
					dataView: { show: true, readOnly: false },
					magicType: {
						show: true,
						type: ['pie', 'funnel']
					},
					restore: { show: true },
					saveAsImage: { show: true }
				}
			},
			calculable: false,
			series: [
				{
					datasetIndex: 0,
					name: 'Type Of Hijacked',
					type: 'pie',
					selectedMode: 'single',
					radius: [0, '30%'],
					center: ['50%', '50%'],
					label: {
						normal: {
							position: 'inner'
						}
					},
					labelLine: {
						normal: {
							show: false
						}
					}
				},
				{
					datasetIndex: 1,
					name: 'Hijacked Destination',
					type: 'pie',
					radius: ['40%', '55%'],
					center: ['50%', '50%'],
					label: {
						normal: {
							formatter: '{a|{a}}{abg|}\n{hr|}\n  {b|{b}：}{c}  {per|{d}%}  ',
							backgroundColor: '#eee',
							borderColor: '#aaa',
							borderWidth: 1,
							borderRadius: 4,
							rich: {
								a: {
									color: '#999',
									lineHeight: 22,
									align: 'center'
								},
								hr: {
									borderColor: '#aaa',
									width: '100%',
									borderWidth: 0.5,
									height: 0
								},
								b: {
									fontSize: 16,
									lineHeight: 33
								},
								per: {
									color: '#eee',
									backgroundColor: '#334455',
									padding: [2, 4],
									borderRadius: 2
								}
							}
						}
					}
				}
			]
		};
		hijackTypePie.setOption(option);
	},

	HijackedProvinceBar: function (data) {

		var barChart = echarts.init(document.getElementById('HijackedProvince'));

		option = {
			tooltip: {
				trigger: 'axis',
				axisPointer: {            // 坐标轴指示器，坐标轴触发有效
					type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
				}
			},
			legend: {
			},
			toolbox: {
				show: true,
				feature: {
					mark: { show: true },
					dataView: { show: true, readOnly: false },
					magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
					restore: { show: true },
					saveAsImage: { show: true }
				}
			},
			dataset: [
				{ source: data.HijackedProvinceDns },
				{ source: data.HijackedProvinceHttp }
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
					stack: "Hijacked",
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
					stack: 'Hijacked',
					itemStyle: {
						normal:
							{
								label:
									{ show: true, position: 'insideRight' }
							}
					},
				}
			]
		};
		barChart.setOption(option);

	},

	HijackedResolutionTime: function (data) {
		var lineChart = echarts.init(document.getElementById('ResolutionTime'));

		var option = {
			title: {
				show: false,
				textStyle: {
					color: "red",
					fontStyle: "oblique",
					fontWeight: "Bold",
					align: "center",
					verticalAlign: "middle"
				}
			},
			color: ['#37A2DA', '#32C5E9', '#67E0E3', '#9FE6B8', '#FFDB5C', '#ff9f7f', '#fb7293', '#E062AE', '#E690D1', '#e7bcf3', '#9d96f5', '#8378EA', '#96BFFF'],
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
			legend: {
			},
			xAxis: { type: 'category' },
			yAxis: {},
			dataset: {
				source: data.HijackedResolutionTime
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
				}
			]

		};
		lineChart.setOption(option);
	},

	AllowedDomain: function () {
		GetDomain(function (domainDataset) {
			var domain = new Bloodhound({

				datumTokenizer: Bloodhound.tokenizers.obj.whitespace("text"),
				queryTokenizer: Bloodhound.tokenizers.whitespace,
				// `states` is an array of state names defined in "The Basics"
				local: domainDataset
			});
			domain.initialize();
			$("#inputSearch").tagComponent({
				typeaheadjs: {
					name: "domain",
					displayKey: "text",
					limit: 1000,
					source: domain.ttAdapter()
				},
				itemValue: "value",
				itemText: "text"
			});

		});
		
	},
};