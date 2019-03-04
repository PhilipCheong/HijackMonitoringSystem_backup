var PerformancesResults = {

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
        //ServerExamination.ServerCname();
        //$("#ChartTable").attachDragger();
    },

    PlotResults: function () {
        var domain = $('#inputSearch').val();
        var start = $('#ToStartDate').val();
        var end = $('#ToEndDate').val();
        $.ajax({
            url: "/PerformancesResults/PlotCharts",
            dataType: "json",
            data: { domainName: domain, startDate: start, endDate: end },
            method: "POST",
            success: function (data) {
                if (data !== undefined) {
                    for (var i = 0; i < data.length; i++) {
                        PerformancesResults.ResponseLineChart(data[i]);
                    }
                }
            }
        });
    },

    ResponseLineChart: function (data) {

        var lineChart1 = echarts.init(document.getElementById('ResponseLineChart1'));
        var lineChart2 = echarts.init(document.getElementById('ResponseLineChart2'));
        var lineChart3 = echarts.init(document.getElementById('ResponseLineChart3'));
        var lineChart4 = echarts.init(document.getElementById('ResponseLineChart4'));

        var option1 = {
            title: {
                text: 'Response Time',
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
                showContent: false,
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
                source: data.ResponseData[0]
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
                }
            ]

        };
        var option2 = {
            title: {
                text: 'Response Time',
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
                showContent: false,
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
                source: data.ResponseData[1]
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
                }
            ]

        };
        var option3 = {
            title: {
                text: 'Response Time',
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
                showContent: false,
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
                source: data.ResponseData[2]
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
                }
            ]

        };
        var option4 = {
            title: {
                text: 'Response Time',
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
                showContent: false,
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
                source: data.ResponseData[3]
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
                }
            ]

        };


        // use configuration item and data specified to show chart
        lineChart1.setOption(option1);
        lineChart2.setOption(option2);
        lineChart3.setOption(option3);
        lineChart4.setOption(option4);


        function ResDetail(param) {
            if (typeof param.seriesIndex !== 'undefined') {

                var province = param.seriesName;
                var dateTime = param.name;
                var domain = $("#inputSearch").val();

                $.ajax({
                    url: "/PerformancesResults/DetailModelData",
                    dataType: "json",
                    data: { province: province, domainName: domain, dateTime: dateTime },
                    method: "POST",
                    success: function (data) {
                        if (data !== undefined) {
                            var detailModal = $("#ResolutionDetail");
                            var contentContainer = detailModal.find('#detailContainer');
                            contentContainer.html('');
                            for (var i = 0; i < data.length; i++) {
                                contentContainer.append('<tr><td style="text-overflow: ellipsis;white-space: nowrap;overflow: hidden;"><a href="/PerformancesResults/GetWaterFall?id=' +
                                    data[i].Id +
                                    '">' + data[i].Url +
                                    '</a></td><td><span>'
                                    + data[i].Province +
                                    '</span></td><td><span>' +
                                    data[i].PerformancesData[0].RemoteIPAddress +
                                    '</span></td><td>' + data[i].Response + '</span></td></tr >');
                            }
                        }
                        detailModal.modal('show');
                    }
                });
            }
        }

        lineChart1.on('click', ResDetail);
        lineChart2.on('click', ResDetail);
        lineChart3.on('click', ResDetail);
        lineChart4.on('click', ResDetail);
    },

    PlotWaterfallChart: function (id) {
        $.ajax({
            url: "/PerformancesResults/PlotWaterFallChart",
            dataType: "json",
            data: { id: id },
            method: "POST",
            success: function (data) {
                if (data !== undefined) {
                    //PerformancesResults.WaterFall(data);
                    PerformancesResults.ChromeWaterFallTitle(data.Title, data.Image);

                    PerformancesResults.ChromeWaterFall(data.Chart);

                }
            }
        });

    },

    WaterFall: function (data) {
        var table = $("#chartContainer");
        for (var i = 0; i < data.ObjectForChart.Url.length; i++) {
            table.append('<tr>\n<td class="WaterFall"><a href="' +
                data.ObjectForChart.Url[i] + '">' +
                data.ObjectForChart.Url[i] +
                '</a></td>\n<td>' +
                data.ObjectForChart.MimeType[i] +
                '</td>\n<td>' +
                data.ObjectForChart.Status[i] +
                '</td>\n<td>' +
                data.ObjectForChart.RemoteIPAddress[i] +
                '</td>\n<td>\n<div class="progress thin">\n<div class="progress-bar startedTime" data-toggle="tooltip" data-placement="top" title="Dns : ' +
                data.ObjectForChart.Dns[i] +
                'ms&*;Connect : ' + data.ObjectForChart.Connect[i] + 'ms&*;Send : ' +
                data.ObjectForChart.Send[i] + 'ms&*;TTFB : ' + data.ObjectForChart.TTFB[i] +
                'ms&*;Download : ' + data.ObjectForChart.Download[i] + 'ms" role="progressbar" aria-valuenow="' +
                data.ObjectForChart.RequestTime[i] +
                '" aria-valuemin="0" aria-valuemax="' +
                data.ObjectForChart.Response / 5 +
                '" style="width:' +
                data.ObjectForChart.RequestTime[i] / 5 +
                'px">\n</div>\n<div class="progress-bar progress-bar-danger" data-toggle="tooltip" data-placement="top" title="Dns : ' +
                data.ObjectForChart.Dns[i] +
                'ms&*;Connect : ' + data.ObjectForChart.Connect[i] + 'ms&*;Send : ' +
                data.ObjectForChart.Send[i] + 'ms&*;TTFB : ' + data.ObjectForChart.TTFB[i] +
                'ms&*;Download : ' + data.ObjectForChart.Download[i] + 'ms" role="progressbar" aria-valuenow="' +
                data.ObjectForChart.Dns[i] +
                '" aria-valuemin="0" aria-valuemax="' +
                data.ObjectForChart.Response / 5 +
                '" style="width:' +
                data.ObjectForChart.Dns[i] / 5 +
                'px">\n</div>\n<div class="progress-bar progress-bar-danger" data-toggle="tooltip" data-placement="top" title="Dns : ' +
                data.ObjectForChart.Dns[i] +
                'ms&*;Connect : ' + data.ObjectForChart.Connect[i] + 'ms&*;Send : ' +
                data.ObjectForChart.Send[i] + 'ms&*;TTFB : ' + data.ObjectForChart.TTFB[i] +
                'ms&*;Download : ' + data.ObjectForChart.Download[i] + 'ms" role="progressbar" aria-valuenow="' +
                data.ObjectForChart.Connect[i] +
                '" aria-valuemin="0" aria-valuemax="' +
                data.ObjectForChart.Response / 5 +
                '" style="width:' +
                data.ObjectForChart.Connect[i] / 5 +
                'px">\n</div>\n<div class="progress-bar progress-bar-danger" data-toggle="tooltip" data-placement="top" title="Dns : ' +
                data.ObjectForChart.Dns[i] +
                'ms&*;Connect : ' + data.ObjectForChart.Connect[i] + 'ms&*;Send : ' +
                data.ObjectForChart.Send[i] + 'ms&*;TTFB : ' + data.ObjectForChart.TTFB[i] +
                'ms&*;Download : ' + data.ObjectForChart.Download[i] + 'ms" role="progressbar" aria-valuenow="' +
                data.ObjectForChart.Send[i] +
                '" aria-valuemin="0" aria-valuemax="' +
                data.ObjectForChart.Response / 5 +
                '" style="width:' +
                data.ObjectForChart.Send[i] / 5 +
                'px">\n</div>\n<div class="progress-bar progress-bar-success" data-toggle="tooltip" data-placement="top" title="Dns : ' +
                data.ObjectForChart.Dns[i] +
                'ms&*;Connect : ' + data.ObjectForChart.Connect[i] + 'ms&*;Send : ' +
                data.ObjectForChart.Send[i] + 'ms&*;TTFB : ' + data.ObjectForChart.TTFB[i] +
                'ms&*;Download : ' + data.ObjectForChart.Download[i] + 'ms" role="progressbar" aria-valuenow="' +
                data.ObjectForChart.TTFB[i] +
                '" aria-valuemin="0" aria-valuemax="' +
                data.ObjectForChart.Response / 5 +
                '" style="width:' +
                data.ObjectForChart.TTFB[i] / 5 +
                'px">\n</div>\n' +
                '<div class="progress-bar progress-bar" data-toggle="tooltip" data-placement="top" title="Dns : ' +
                data.ObjectForChart.Dns[i] +
                'ms&*;Connect : ' + data.ObjectForChart.Connect[i] + 'ms&*;Send : ' +
                data.ObjectForChart.Send[i] + 'ms&*;TTFB : ' + data.ObjectForChart.TTFB[i] +
                'ms&*;Download : ' + data.ObjectForChart.Download[i] + 'ms" role="progressbar" aria-valuenow="' +
                data.ObjectForChart.Download[i] +
                '" aria-valuemin="0" aria-valuemax="' +
                data.ObjectForChart.Response +
                '" style="width:' +
                data.ObjectForChart.Download[i] / 5 +
                'px">\n</div></div>'
            );
        }
        PerformancesResults.RefaceTooltip();
    },

    RefaceTooltip: function () {
        var table = $("#chartContainer");

        var progresBar = table.find('.progress-bar');

        for (var i = 0; i < progresBar.length; i++) {
            var pro = progresBar[i];
            var proTitle = pro.getAttribute('title');
            var splittext = proTitle.split('&*;');
            var refaceText = "";
            for (var z = 0; z < splittext.length; z++) {
                if (splittext[z].indexOf(': 0ms') < 1) {
                    if (refaceText === "") {
                        refaceText = splittext[z] + "\n";
                    } else {
                        refaceText += splittext[z] + "\n";
                    }
                }
            }
            pro.setAttribute('title', refaceText);
        }
    },

    ChromeWaterFall: function (data) {


        google.charts.load('current', { 'packages': ['timeline'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var container = document.getElementById('timeline');
            var chart = new google.visualization.Timeline(container);

            var dataTable = new google.visualization.DataTable(data.ObjectForChart);

            var options = {
                timeline: { showBarLabels: false },
                tooltip: {
                    //trigger: 'none'
                }

            };

            google.visualization.events.addListener(chart, 'ready', function () {
                var rows;
                var barIndex;
                var labelIndex = 0;
                var labels = container.getElementsByTagName('text');
                var startLabel;

                for (var z = 0; z < labels.length; z++) {
                    if (labels[z].textContent.indexOf('://') > 1) {
                        startLabel = z;
                        break;
                    }
                }

                for (var i = startLabel; i < labels.length; i++) {

                    var labelText = labels[i].textContent;
                    var dataTableText = dataTable.getValue((i - startLabel) * 5, 2);

                    if (dataTable.getValue((i - startLabel) * 5, 2) === "error") {
                        labels[i].setAttribute('fill', '#c62828');
                    }

                }

                //Array.prototype.forEach.call(labels, function (label) {
                //	// process bar labels
                //	if (label.getAttribute('text-anchor') === 'end') {
                //		// find data rows for label
                //		rows = dataTable.getFilteredRows([{
                //			column: 0,
                //			test: function (value) {
                //				var labelFound;
                //				var labelText;

                //				// chart will cutoff label if not enough width
                //				if (label.textContent.indexOf('…') > -1) {
                //					labelText = label.textContent.replace('…', '');
                //					labelFound = (value.indexOf(labelText) > -1);
                //				} else {
                //					labelText = label.textContent;
                //					labelFound = (value === labelText);
                //				}
                //				return labelFound;
                //			}
                //		}]);
                //		if (rows.length > 0) {
                //			// process label rows
                //			rows.forEach(function (rowIndex) {
                //				// check if row has error
                //				if (dataTable.getValue(rowIndex, 2) === 'error') {
                //					// change color of label
                //					label.setAttribute('fill', '#c62828');

                //					// change color of bar
                //					barIndex = 0;
                //					var bars = container.getElementsByTagName('rect');
                //					Array.prototype.forEach.call(bars, function (bar) {
                //						if ((bar.getAttribute('x') === '0') && (bar.getAttribute('stroke-width') === '0')) {
                //							if (barIndex === labelIndex) {
                //								bar.setAttribute('fill', '#ffcdd2');
                //							}
                //							barIndex++;
                //						}
                //					});
                //				}
                //			});
                //		}
                //		labelIndex++;
                //	}
                //});
            });

            google.visualization.events.addListener(chart, 'select', function () {
                var selection = chart.getSelection();
                var dataModal = $("#DataInDetail");
                if (selection.length > 0) {

                    selection[0].tooltip = false;

                    var dataRow = Math.trunc(selection[0].row / 5);

                    var dataInfo = data.ChartData.PerformancesData[dataRow];

                    dataModal.find("#Url").html('').append('<b>' + dataInfo.Url + '</b>');
                    dataModal.find("#RemoteIp").html('').append('Remote Ip Address:<b class="detailBold">' + dataInfo.RemoteIPAddress + '</b>');
                    dataModal.find("#MimeType").html('').append('Mime Type:<br/><b class="detailBold">' + dataInfo.MimeType + '</b>');
                    dataModal.find("#StatusCode").html('').append('Http Code:<br/><b class="detailBold">' + dataInfo.Status + '</b>');
                    dataModal.find("#DNS").html('').append('DNS:<br/><b class="detailBold">' + dataInfo.Dns + 'ms</b>');
                    dataModal.find("#Connect").html('').append('Connect:<br/><b class="detailBold">' + dataInfo.Connect + 'ms</b>');
                    dataModal.find("#Send").html('').append('Send:<br/><b class="detailBold">' + dataInfo.Send + 'ms</b>');
                    dataModal.find("#TTFB").html('').append('Time To First Byte:<br/><b class="detailBold">' + dataInfo.TTFB + 'ms</b>');
                    dataModal.find("#Download").html('').append('Download:<br/><b class="detailBold">' + dataInfo.Download + 'ms</b>');
                }

                dataModal.modal('show');
            });
            chart.draw(dataTable, options);
        }


    },

    ChromeWaterFallTitle: function (data, image) {

        var container = $("#WaterFallContainer");

        container.append('<div class="row"><div><img src="' + image + '" style="height:500px; margin-left:50%; margin-right:50%; margin-bottom:10%;" /></div></div>');

        container.append('<div class="card card-accent-info"><div class="card-body"><div class="row"><div class="col-4"><p>Domain</p><hr><p>' +
            data.Url +
            '</p></div><div class="col-4"><p>Response</p><hr><p>' +
            data.Response + 'ms</p></div> <div class="col-4"><p>Failed</p><hr><p style="color:red;">#' +
            data.FailedCount + '</p ></div ></div ></div ></div > ');

        container.append('<div id="WaterFallChart" class="form-group required col-md-12"><div id="timeline"></div></div>');
        $('#timeline').css('height', $(window).height() - 300 + 'px');
    }
};