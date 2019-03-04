var AccountManagement = {

	showCreateModal: function () {
		var createModal = $("#createUser");
		createModal.find('#ModalLabel').html("New User");
		$("#RegistrationForm").trigger("reset");
		createModal.modal("show");
	},

	SearchUser: function () {
		var userName = $("#inputSearch").val();
		$.ajax({
			url: "/Account/Search",
			type: "Post",
			dataType: "json",
			data: { userName: userName },
			success:
				function (returnData) {
					var table = $("#userInfo");
					table.html("");
					for (var i = 0; i < returnData.length; i++) {
						table.append('<tr><td id="id" style="display: none;">' +
							returnData[i].Id +
							'</td><td>' +
							returnData[i].Username +
							'</td><td>' +
							returnData[i].Type +
							'</td><td>' +
							returnData[i].Email +
							'</td><td>' +
							returnData[i].CustomerId +
							'</td><td>' +
							'<div><button class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit" onclick="AccountManagement.PopulateDataForEdit(\'' +
							returnData[i].Id + '\')">' +
							'<i class="fa fa-edit" aria-hidden="true"></i></button>' +
							'<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="AccountManagement.DeleteUser(\'' +
							returnData[i].Id + '\')">' +
							'<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></tr>'
						);
					}

				}
		});

	},

	FilterByCustomerId: function () {
		var customerId = $("#customerId").val();
		$.ajax({
			url: "/Account/GroupSearch",
			method: "POST",
			dataType: "json",
			data: { customerId: customerId },
			success: function (returnData) {
				var table = $("#userInfo");
				table.html("");
				for (var i = 0; i < returnData.length; i++) {
					table.append('<tr><td id="id" style="display: none;">' +
						returnData[i].Id +
						'</td><td>' +
						returnData[i].Username +
						'</td><td>' +
						returnData[i].Type +
						'</td><td style="display:none">' +
						returnData[i].Password +
						'</td><td>' +
						returnData[i].Email +
						'</td><td>' +
						returnData[i].CustomerId +
						'</td><td>' +
						'<div><button class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit" onclick="AccountManagement.PopulateDataForEdit(\'' +
						returnData[i].Id + '\')">' +
						'<i class="fa fa-edit" aria-hidden="true"></i></button>' +
						'<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="AccountManagement.DeleteUser(\'' +
						returnData[i].Id + '\')">' +
						'<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></tr>'
					);
				}

			}
		});

	},

	PopulateDataForEdit: function (id) {
		var editModal = $("#createUser");
		editModal.find('#ModalLabel').html("Edit User");
		$("#RegistrationForm").trigger("reset");
		$.ajax({
			url: "/Account/FulfillEdit",
			data: { id: id },
			dataType: "json",
			success: function (data) {
				$("#RegistrationForm").find("#Id").val(data.Id);
				$("#RegistrationForm").find("#Username").val(data.Username);
				$("#RegistrationForm").find("#Password").val(data.Password);
				$("#RegistrationForm").find("#c_Password").val(data.Password);
				$("#RegistrationForm").find("#Type").val(data.Type).change();
				$("#RegistrationForm").find("#Email").val(data.Email);
				$("#RegistrationForm").find("#CustomerId").val(data.CustomerId);
			}
		});
		editModal.modal("show");

	},

	DeleteUser: function (delId) {
		var deleteNotice = $("#DeleteModal");
		deleteNotice.modal("show");
		deleteNotice.trigger("reset");
		$("#formDelete").find("#Id").val(delId);
	},

	FilterUser: function () {
		var input, filter, table, tr, td, i;
		input = document.getElementById("inputSearch");
		filter = input.value.toLowerCase();
		table = document.getElementById("userTable");
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

	IsUsernameExisted: function (value, callback) {
		var Id = $("#Id").val();
		$.ajax({
			async: false,
			url: "/Account/IsUsernameExisted",
			dataType: "json",
			method: "POST",
			data: { username: value, id:Id },
			success: function (data) {
				if (callback) {
					callback(data);
				}
			}
		});
	},

	IsEmailExisted: function (value, callback) {
		var Id = $("#Id").val();
		$.ajax({
			async: false,
			url: "/Account/IsEmailExisted",
			dataType: "json",
			method: "POST",
			data: { email: value, id: Id },
			success: function (data) {
				if (callback) {
					callback(data);
				}
			}
		});
	},
};
//var AccountBackendFunction = {
//    GetForEdit: function (id, callback) {
//    }
//}


