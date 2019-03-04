var DomainManagement = {

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
	},

	SearchDomain: function () {
		var domainName = $("#inputSearch").val();
		$.ajax({
			url: "/Domain/Search",
			type: "Post",
			dataType: "json",
			data: { domainName: domainName },
			success:
				function (returnData) {
					var table = $("#domainInfo");
					table.html("");
					for (var i = 0; i < returnData.length; i++) {
						table.append('<tr><td id="id" style="display: none;">' +
							returnData[i].Id +
							'</td><td>' +
							returnData[i].Domain +
							'</td><td>' +
							returnData[i].DestinationIp +
							'</td><td>' +
							returnData[i].ToStartTime +
							'</td><td>' +
							returnData[i].ToEndTime +
							'</td><td>' +
							returnData[i].Interval +
							'</td><td>' +
							returnData[i].Status +
							'</td><td>' +
							returnData[i].CustomerId +
							'</td><td>' +
							'<div><button class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit" onclick="AccountManagement.PopulateDataForEdit(\'' +
							returnData[i].Id +
							'\')">' +
							'<i class="fa fa-edit" aria-hidden="true"></i></button>' +
							'<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="AccountManagement.DeleteUser(\'' +
							returnData[i].Id +
							'\')">' +
							'<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></tr>'
						);
					}
					var pager = new TableFunction.tablePagination('domainTable', 20);
					pager.init();
					pager.showPageNav('pager');
					pager.showPage(1);

				}

		});

	},

	FilterByCustomerId: function () {

		var customerId = $("#customerId").val();
		$.ajax({
			url: "/Domain/GroupSearch",
			method: "POST",
			dataType: "json",
			data: { customerId: customerId },
			success: function (returnData) {
				var table = $("#domainInfo");
				table.html("");
				for (var i = 0; i < returnData.length; i++) {
					table.append('<tr><td id="id" style="display: none;">' +
						returnData[i].Id +
						'</td><td>' +
						returnData[i].Protocol + returnData[i].Domain +
						'</td><td>' +
						moment.utc(returnData[i].ToStartTime).format('YYYY/MM/DD HH:mm:ss') +
						'</td><td>' +
						moment.utc(returnData[i].ToEndTime).format('YYYY/MM/DD HH:mm:ss') +
						'</td><td>' +
						returnData[i].Interval +
						'</td><td>' +
						'<div class="form-group"><label class="switch switch-3d switch-primary"><input type="checkbox" class="switch-input" name="Status" value="1" onchange="DomainManagement.ChangeStatus(this)"><span class="switch-label"></span><span class="switch-handle"></span></label></div>' +
						'</td><td>' +
						returnData[i].CustomerId +
						'</td><td>' +
						'<div><a href="/Domain/DomainEdit?' +
						returnData[i].Id +
						'\" class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit">' +
						'<i class="fa fa-edit" aria-hidden="true"></i></a>' +
						'<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="DomainManagement.DeleteDomain(\'' +
						returnData[i].Id +
						'\')">' +
						'<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></td></tr>'
					);
					var status = $('input[name="Status"]').last();
					if (returnData[i].Status === 1) {
						status.prop("checked", true);
					}
				}
				var pager = new TableFunction.tablePagination('domainTable', 20);
				pager.init();
				pager.showPageNav('pager');
				pager.showPage(1);
			}
		});
	},

	DeleteDomain: function (id) {
		var deleteNotice = $("#DeleteModal");
		deleteNotice.modal("show");
		deleteNotice.trigger("reset");
		$("#formDelete").find("#Id").val(id);
	},

	TimeRangeStart: function () {

		$("#ToStartDate").datetimepicker().show();
	},

	TimeRangeEnd: function () {

		$("#ToEndDate").datetimepicker().show();
	},

	SubmitDomain: function () {
		var form = $("#FormNewDomain");
		if (form.toffValidation()) {
			var provinceCheckBoxes = form.find(".CheckboxArray");
			var ispCheckBoxes = form.find(".ispCheckBoxes");
			var checkedIsp = "";
			var checkedProvince = "";
			var isps = form.find("#checkedIsp");
			var provinces = form.find("#checkedProvince");
			for (var i = 0; provinceCheckBoxes[i]; ++i) {
				if (provinceCheckBoxes[i].checked) {
					if (checkedProvince === "") {
						checkedProvince = provinceCheckBoxes[i].value;
					}
					else {
						checkedProvince += "," + provinceCheckBoxes[i].value;
					}
				}
			}
			provinces.val(checkedProvince);
			for (var z = 0; ispCheckBoxes[z]; ++z) {
				if (ispCheckBoxes[z].checked) {
					if (checkedIsp === "") {
						checkedIsp = ispCheckBoxes[z].value;
					}
					else {
						checkedIsp += "," + ispCheckBoxes[z].value;
					}
				}
			}
			isps.val(checkedIsp);
			var formData = form.serialize();
			$.ajax({
				url: "/Domain/AddOrEditForDomain",
				data: formData,
				dataType: "json",
				type: "POST",
				success: function (data) {
					if (data.result) {
						window.location.href = "/Domain/Index";
					}
				}
			});
		}
	},

	SubmitEditDomain: function () {

		var form = $("#FormEditDomain");
		if (form.toffValidation()) {
			var provinceCheckBoxes = form.find(".CheckboxArray");
			var ispCheckBoxes = form.find(".ispCheckBoxes");
			var checkedIsp = "";
			var checkedProvince = "";
			var isps = form.find("#checkedIsp");
			var provinces = form.find("#checkedProvince");
			for (var i = 0; provinceCheckBoxes[i]; ++i) {
				if (provinceCheckBoxes[i].checked) {
					if (checkedProvince === "") {
						checkedProvince = provinceCheckBoxes[i].value;
					}
					else {
						checkedProvince += "," + provinceCheckBoxes[i].value;
					}
				}
			}
			provinces.val(checkedProvince);
			for (var z = 0; ispCheckBoxes[z]; ++z) {
				if (ispCheckBoxes[z].checked) {
					if (checkedIsp === "") {
						checkedIsp = ispCheckBoxes[z].value;
					}
					else {
						checkedIsp += "," + ispCheckBoxes[z].value;
					}
				}
			}
			isps.val(checkedIsp);
			var formData = form.serialize();
			$.ajax({
				url: "/Domain/AddOrEditForDomain",
				data: formData,
				dataType: "json",
				type: "POST",
				success: function (data) {
					if (data.result) {
						window.location.href = "/Domain/Index";
					}
				}
			});
		}
	},

	EditDomain: function (id) {
		$.ajax({
			url: "/Domain/DomainInfo",
			dataType: "json",
			data: { id: id },
			type: "POST",
			success: function (data) {
				var formEdit = $("#FormEditDomain");
				formEdit.append('<input id="Id" type="hidden" name="Id" value="' + data.DomainInfo.Id + '"/>');

				var customerId = formEdit.find("#customerId");

				for (var i = 0; i < data.AllCustomerId.length; i++) {
					customerId.append('<option value="' +
						data.AllCustomerId[i] +
						'" selected="selected">' +
						data.AllCustomerId[i] +
						'</option>');
				}
				customerId.append('<option value="' +
					data.DomainInfo.CustomerId +
					'" selected="selected">' +
					data.DomainInfo.CustomerId +
					'</option>');


				var protocol = formEdit.find("#protocolRadio");
				protocol.append(
					'<label class="radio-inline"><input type="radio" checked="checked" name="Protocol" id="Http" value="http://" style="margin-bottom: 10px">Http</label> ' +
					'<label class="radio-inline"><input type="radio" name="Protocol" id="Https" value="https://" style="margin-bottom: 10px">Https</label>');
				if (data.DomainInfo.Protocol === "https://") {
					formEdit.find("#Https").attr("checked", true);
				}

				var domain = formEdit.find("#domainName");
				domain.append(
					'<input class="form-control" id="domain" name="Domain" placeholder="Domain Name" style="margin-bottom: 10px" value="' + data.DomainInfo.Domain + '"type="text" required/>');

				var IpTextBox = formEdit.find("#IpTextBox");
				IpTextBox.append(
					'<input class="form-control" id="ipAddress" name="DestinationIp" placeholder="Resolution Ip" style="margin-bottom: 10px" value="' + data.DomainInfo.DestinationIp + '" type="text" required/>');

				var redirection = formEdit.find("#redirect");
				redirection.append(
					'<input class="form-control" id="redirection" name="redirection" placeholder="Redirect Url" style="margin-bottom: 10px" value="' + data.DomainInfo.Redirection + '" type="text"/>');

				var dateRangeBoxes = formEdit.find("#dateRangeBoxes");
				dateRangeBoxes.append(
					'<div class="col-md-1"><i class="fa fa-calendar col-md-1" id="faCalender"></i></div><div class="col-md-4"><input type="text" class="form-control" id="ToStartDate" readonly="readonly" name="ToStartTime" placeholder="Start" style="margin-bottom: 10px" value="' +
					moment.utc(data.DomainInfo.ToStartTime).format("YYYY/MM/DD HH:mm") +
					'" onfocus="DomainManagement.TimeRangeStart()" required/></div>' +
					'<div class="col-md-1"><i class="fa fa-calendar col-md-1" id="faCalender"></i ></div>' +
					'<div class="col-md-4"><input type="text" class="form-control" id="ToEndDate" readonly="readonly" name="ToEndTime" placeholder="End" style="margin-bottom: 10px" value="' +
					moment.utc(data.DomainInfo.ToEndTime).format("YYYY/MM/DD HH:mm") +
					'" onfocus="DomainManagement.TimeRangeEnd()"  required/></div>');
				DomainManagement.init();

				var intervalBoxes = formEdit.find("#intervalBoxes");
				for (var k = 15; k <= 60; k += 15) {
					if (data.DomainInfo.Interval === k) {
						intervalBoxes.append(
							'<label class="radio-inline"><input type="radio" name="Interval" checked="checked" id="Interval_' +
							k +
							'" value="' +
							k +
							'" style="margin-bottom:10px">' +
							k +
							' mins</label>');
					} else {
						intervalBoxes.append(
							'<label class="radio-inline"><input type="radio" name="Interval" id="Interval_' +
							k +
							'" value="' +
							k +
							'" style="margin-bottom:10px">' +
							k +
							' mins</label>');

					}
				}

				var ispBoxes = formEdit.find("#ispboxes");

				ispBoxes.append(
					'<input class="hidden" type="text" name="CheckedIsp" id="checkedIsp" />' +
					'<label class="checkbox-inline col-md-2"><input class="ispCheckBoxes" type="checkbox" name="Isp[0]" id="Checkboxes0" value="1" style="margin-bottom: 10px">China Telecom</label>' +
					'<label class="checkbox-inline col-md-2"><input class="ispCheckBoxes" type="checkbox" name="Isp[1]" id="Checkboxes1" value="2" style="margin-bottom: 10px">China Unicom</label>' +
					'<label class="checkbox-inline col-md-2"><input class="ispCheckBoxes" type="checkbox" name="Isp[2]" id="Checkboxes2" value="7" style="margin-bottom: 10px">China Mobile</label>');
				var ispArray = data.DomainInfo.Isp.split(",");

				var ispCheckedBoxes = formEdit.find(".ispCheckBoxes");
				for (var p = 0; p < 3; p++) {
					if (ispArray.indexOf(ispCheckedBoxes[p].value) > -1) {
						$(ispCheckedBoxes[p]).prop("checked", true);
					}
				}

				var provinceBoxes = formEdit.find("#provinceBoxes");
				provinceBoxes.append(
					'<input class="hidden" type="text" name="checkedProvince" id="checkedProvince" />' +
					'<label class="checkbox-inline col-md-2"><input type="checkbox" name="All" id="AllProvince" value="" style="margin-bottom: 10px;">All</label>');

				var provinceArray = data.DomainInfo.Province.split(",");
				for (var z = 0; z < data.Province.length; z++) {
					provinceBoxes.append(
						'<label class="checkbox-inline col-md-2">' +
						'<input class="CheckboxArray" type="checkbox" name="Province[' +
						z +
						']" id="CheckboxesProvince[' +
						z +
						']" value="' +
						data.Province[z].Value +
						'" style="margin-bottom: 10px;">' +
						data.Province[z].Key +
						'</label >');
				}
				var provinceCheckBoxes = $(".CheckboxArray");
				for (var a = 0; a < provinceCheckBoxes.length; a++) {
					if (provinceArray.indexOf(provinceCheckBoxes[a].value) > -1) {
						$(provinceCheckBoxes[a]).prop("checked", true);
					}
				}
			}
		});
	},

	FilterDomain: function () {
		var input, filter, table, tr, td, i;
		input = document.getElementById("inputSearch");
		filter = input.value.toLowerCase();
		table = document.getElementById("domainTable");
		tr = table.getElementsByTagName("tr");
		for (i = 0; i < tr.length; i++) {
			td = tr[i].getElementsByTagName("td")[1];
			if (td) {
				if (td.innerHTML.toLowerCase().indexOf(filter) > -1) {
					tr[i].style.display = "";
				} else {
					tr[i].style.display = "none";
				}
			}
		}
	},

	CheckALL: function () {
		var CheckALL = document.getElementById("AllProvince");
		var CheckboxArray = $(".CheckboxArray");
		if (CheckALL.checked) {
			for (var i = 0; i < CheckboxArray.length; i++) {
				$(CheckboxArray[i]).prop('checked', true);
			}
		}
		else {
			for (var z = 0; z < CheckboxArray.length; z++) {
				$(CheckboxArray[z]).prop('checked', false);
			}
		}
	},

	ChangeStatus: function (e) {
		var Id = e.closest('tr').cells.item(0).innerHTML;
		if (e.checked) {
			$.ajax({
				url: "/Domain/ChangeStatus",
				data: { Id: Id, value: "1" },
				type: "POST",
				dataType: "json",
				succes: function () {

				}
			});
		}
		else {
			$.ajax({
				url: "/Domain/ChangeStatus",
				data: { Id: Id, value: "0" },
				type: "POST",
				dataType: "json",
				succes: function () {

				}
			});
		}
	}
};
