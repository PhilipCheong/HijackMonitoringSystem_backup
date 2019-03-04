var serverManagement = {

	SearchEmptyFulfillDomain: function () {
		var searchBox = $("#inputSearch").val();
		if (searchBox === "") {
			$.ajax({
				url: "/Server/RefillAllDomain",
				method: "POST",
				dataType: "json",
				success: function (returnData) {
					var table = $("#ServerInfo");
					table.html("");
					for (var i = 0; i < returnData.length; i++) {
						table.append('<tr><td id="id" style="display: none;">' +
							returnData[i].Id +
							'</td><td>' +
							returnData[i].GroupName +
							'</td><td>' +
							returnData[i].ServerCname +
							'</td><td>' +
							returnData[i].ServerIp +
							'</td><td>' +
							returnData[i].BandwidthLimitation +
							'</td><td>' +
							'<div><button class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit" onclick="serverManagement.PopulateDataForEdit(\'' +
							returnData[i].Id +
							'\')">' +
							'<i class="fa fa-edit" aria-hidden="true"></i></button>' +
							'<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="serverManagement.DeleteServer(\'' +
							returnData[i].Id +
							'\')">' +
							'<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></tr>'
						);
					}

				}
			});
		}
	},

	SearchByCname: function () {
		var cName = $("#inputSearch").val();
		$.ajax({
			url: "/Server/SearchByCname",
			method: "POST",
			dataType: "json",
			data: { userInput: cName },
			success: function (returnData) {
				var table = $("#ServerInfo");
				table.html("");
				for (var i = 0; i < returnData.length; i++) {
					table.append('<tr><td id="id" style="display: none;">' +
						returnData[i].Id +
						'</td><td>' +
						returnData[i].GroupName +
						'</td><td>' +
						returnData[i].ServerCname +
						'</td><td>' +
						returnData[i].ServerIp +
						'</td><td>' +
						returnData[i].BandwidthLimitation +
						'</td><td>' +
						'<div><button class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit" onclick="serverManagement.PopulateDataForEdit(\'' +
						returnData[i].Id + '\')">' +
						'<i class="fa fa-edit" aria-hidden="true"></i></button>' +
						'<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="serverManagement.DeleteServer(\'' +
						returnData[i].Id + '\')">' +
						'<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></tr>'
					);
				}
				var pager = new TableFunction.tablePagination('serverTable', 20);
				pager.init();
				pager.showPageNav('pager');
				pager.showPage(1);
			}
		});
	},

	addNewServer: function () {
		var createModal = $("#NewServer");
		$("#RegistrationForm").trigger("reset");
		$("#RegistrationForm").find(".fa-question-circle").removeClass('hidden');
		createModal.modal("show");
	},

	BulkEditDropDown: function () {
		var modal = $("#bulkEdit");
		$("#formAppendRow").empty();
		$("#BulkEditForm").trigger('reset');
		$.ajax({
			url: "/Server/BulkEditDropdown",
			dataType: "json",
			success: function (data) {
				var dropDown = $("#cNameDropDown");

				for (var i = 0; i < data.length; i++) {
					dropDown.append(
						'<option value="' + data[i] + '" selected="selected">' + data[i] + '</option>');
				}
			}
		});
		$("#bulkDeleteButton").addClass('hidden');
		$("#SubmitBulkDelete").addClass('hidden');
		modal.modal('show');
	},

	BulkEdit: function () {
		var cName = $("#cNameDropDown").val();
		var modalDiv = $("#formAppendRow");
		modalDiv.empty();
		$.ajax({
			url: "/Server/BulkEditPopulateData",
			method: "POST",
			dataType: "json",
			data: { serverCname: cName },
			success: function (data) {
				for (var i = 0; i < data.length; i++) {
					modalDiv.append('<div class="row"><div class="form-group form-control-sm" style="display:none">' +
						'<label for="Id[' + i + ']" class="col-form-label-lg"> Id:</label>' +
						'<input type="text" name="serverList[' + i + '].Id" class="form-control form-control-sm" value="' + data[i].Id + '" id="Id[' + i + ']"></div>' +
						'<div class="form-group form-control-sm col-md-4"><label for="GroupName[' + i + ']" class="col-form-label-lg">GroupName:</label>' +
						'<input type="text" name="serverList[' + i + '].GroupName" class="form-control form-control-sm" id="GroupName[' + i + ']" value="' + data[i].GroupName + '" required></div>' +
						'<div class="form-group form-control-sm col-md-3"><label for="ServerCname[' + i + ']" class="col-form-label-lg">CName:</label>' +
						'<input type="text" name="serverList[' + i + '].ServerCname" class="form-control form-control-sm" id="ServerCname[' + i + ']" value="' + data[i].ServerCname + '"  required /></div>' +
						'<div class="form-group form-control-sm col-md-3"><label for="ServerIp[' + i + ']" class="col-form-label-lg">Server Ip:</label>' +
						'<input type="checkbox" class="form-check-input hidden checkDelete" value="' + data[i].Id + '" style="margin:9px;">' +
						'<input type="text" name="serverList[' + i + '].ServerIp" class="form-control form-control-sm" id="ServerIp[' + i + ']" value="' + data[i].ServerIp + '" required /></div>' +
						'<div class="form-group form-control-sm col-md-2"><label for="BandwidthLimitation[' + i + ']" class="col-form-label-lg">Bandwidth:</label>' +
						'<input type="text" name="serverList[' + i + '].BandwidthLimitation" class="form-control form-control-sm" id="BandwidthLimitation[' + i + ']" value="' + data[i].BandwidthLimitation + '" required/>' +
						'</div ></div > ');
				}
			}
		});
		$("#bulkDeleteButton").removeClass('hidden');
	},

	submitBulkEdit: function () {
		var bulkForm = $("#BulkEditForm");
		var data = bulkForm.serialize();
		$.ajax({
			url: "/Server/Bulkedit",
			dataType: "json",
			method: "POST",
			data: data
		});
	},

	PopulateDataForEdit: function (id) {
		var editModal = $("#NewServer");
		$("#RegistrationForm").trigger("reset");
		$("#RegistrationForm").find(".fa-question-circle").addClass('hidden');
		$.ajax({
			url: "/Server/FulfillDataForEdit",
			data: { id: id },
			dataType: "json",
			method: "POST",
			success: function (data) {
				$("#RegistrationForm").find("#Id").val(data.Id);
				$("#RegistrationForm").find("#GroupName").val(data.GroupName);
				$("#RegistrationForm").find("#ServerCname").val(data.ServerCname);
				$("#RegistrationForm").find("#ServerIp").val(data.ServerIp);
				$("#RegistrationForm").find("#BandwidthLimitation").val(data.BandwidthLimitation);
			}
		});
		editModal.modal("show");
	},

	DeleteServer: function (id) {
		var deleteNotice = $("#DeleteModal");
		deleteNotice.trigger("reset");
		deleteNotice.modal("show");
		$("#formDelete").find("#Id").val(id);
		$.ajax({
			url: "/Server/BulkDelete",
			dataType: "json",
			method: "POST",
			data: { id: submitDel },
		});
	},

	FilterCName: function () {
		var input, filter, table, tr, td, i;
		input = document.getElementById("inputSearch");
		filter = input.value.toLowerCase();
		table = document.getElementById("serverTable");
		tr = table.getElementsByTagName("tr");
		for (i = 0; i < tr.length; i++) {
			td = tr[i].getElementsByTagName("td")[2];
			if (td) {
				if (td.innerHTML.toLowerCase().indexOf(filter) > -1) {
					tr[i].style.display = "";
				} else {
					tr[i].style.display = "none";
				}
			}
		}
	},

	BulkDeleteOption: function () {
		$(".checkDelete").removeClass('hidden');
		$("#SubmitBulkDelete").removeClass('hidden');
	},

	submitBulkDelete: function () {
		var submitDel;

		var bulkDelete = $("#formAppendRow").find("input:checked");

		bulkDelete.each(function (i, e) {
			if (submitDel === undefined) {
				submitDel = e.value;
			} else {
				submitDel += "," + e.value;
			}
		});

		if (submitDel !== undefined) {
			$.ajax({
				url: "/Server/BulkDelete",
				dataType: "json",
				method: "POST",
				data: { id: submitDel },
			});
		}
	}
};