var PerformanceAnalysis = {
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

    FilterByCustomerId: function () {

        var customerId = $("#customerId").val();
        $.ajax({
            url: "/PerformanceAnalysis/GroupSearch",
            method: "POST",
            dataType: "json",
            data: { customerId: customerId },
            success: function (returnData) {
                var table = $("#domainExaminationInfo");
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
                        returnData[i].TestType +
                        '</td><td>' +
                        returnData[i].BrowserType +
                        '</td><td>' +
                        '<div class="form-group"><label class="switch switch-3d switch-primary"><input type="checkbox" class="switch-input" name="Status" value="1" onchange="DomainManagement.ChangeStatus(this)"><span class="switch-label"></span><span class="switch-handle"></span></label></div>' +
                        '</td><td>' +
                        returnData[i].CustomerId +
                        '</td><td>' +
                        '<div><a href="/PerformanceAnalysis/DomainEdit?' +
                        returnData[i].Id +
                        '\" class="btn btn-primary btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Edit">' +
                        '<i class="fa fa-edit" aria-hidden="true"></i></a>' +
                        '<button class="btn btn-danger btn-setting cssCustomButton" data-toggle="tooltip" data-placement="bottom" title="Delete" onclick="PerformanceAnalysis.DeleteDomain(\'' +
                        returnData[i].Id +
                        '\')">' +
                        '<i class="fa fa-trash-o" aria-hidden="true"></i></button></div></td></tr>'
                    );
                    var status = $('input[name="Status"]').last();
                    if (returnData[i].Status === 1) {
                        status.prop("checked", true);
                    }
                }
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
            var typeCheckBoxes = form.find(".browserType");
            var checkedType = "";
            var type = form.find("#checkedBrowser");
            for (var z = 0; typeCheckBoxes[z]; ++z) {
                if (typeCheckBoxes[z].checked) {
                    if (checkedType === "") {
                        checkedType = typeCheckBoxes[z].value;
                    }
                    else {
                        checkedType += "," + typeCheckBoxes[z].value;
                    }
                }
            }
            type.val(checkedType);
            var testCheckBoxes = form.find(".testType");
            var testType = "";
            var test = form.find("#checkedTest");
            for (var i = 0; testCheckBoxes[i]; ++i) {
                if (testCheckBoxes[i].checked) {
                    if (testType === "") {
                        testType = testCheckBoxes[i].value;
                    }
                    else {
                        testType += "," + testCheckBoxes[i].value;
                    }
                }
            }
            test.val(testType);
            var formData = form.serialize();
            $.ajax({
                url: "/PerformanceAnalysis/AddOrEditForDomain",
                data: formData,
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.result) {
                        window.location.href = "/PerformanceAnalysis/Index";
                    }
                }
            });
        }
    },

    SubmitEditDomain: function () {

        var form = $("#FormEditDomain");
        if (form.toffValidation()) {
            var browserCheckBoxes = form.find(".browserCheckBoxes");
            var testCheckBoxes = form.find(".testCheckBoxes");
            var checkedBrowser = "";
            var checkedTest = "";
            var browser = form.find("#checkedBrowser");
            var test = form.find("#checkedTest");
            for (var i = 0; browserCheckBoxes[i]; ++i) {
                if (browserCheckBoxes[i].checked) {
                    if (checkedBrowser === "") {
                        checkedBrowser = browserCheckBoxes[i].value;
                    }
                    else {
                        checkedBrowser += "," + browserCheckBoxes[i].value;
                    }
                }
            }
            browser.val(checkedBrowser);
            for (var z = 0; testCheckBoxes[z]; ++z) {
                if (testCheckBoxes[z].checked) {
                    if (checkedTest === "") {
                        checkedTest = testCheckBoxes[z].value;
                    }
                    else {
                        checkedTest += "," + testCheckBoxes[z].value;
                    }
                }
            }
            test.val(checkedTest);
            var formData = form.serialize();
            $.ajax({
                url: "/PerformanceAnalysis/AddOrEditForDomain",
                data: formData,
                dataType: "json",
                type: "POST",
                success: function (data) {
                    if (data.result) {
                        window.location.href = "/PerformanceAnalysis/Index";
                    }
                }
            });
        }
    },

    EditDomain: function (id) {
        $.ajax({
            url: "/PerformanceAnalysis/DomainInfo",
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

                intervalBoxes.append(
                    '<label class="radio-inline"><input type="radio" name="Interval" checked="checked" id="Interval_' +
                    5 +
                    '" value="' +
                    5 +
                    '" style="margin-bottom:10px">' +
                    5 +
                    ' mins</label>');
                intervalBoxes.append(
                    '<label class="radio-inline"><input type="radio" name="Interval" checked="checked" id="Interval_' +
                    10 +
                    '" value="' +
                    10 +
                    '" style="margin-bottom:10px">' +
                    10 +
                    ' mins</label>');
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

                var browserBoxes = formEdit.find("#browserType");

                browserBoxes.append(
                    '<input class="hidden" type="text" name="BrowserType" id="checkedBrowser" />' +
                    '<label class="checkbox-inline col-md-2"><input class="browserCheckBoxes" type="checkbox" name="browserType[0]" id="Checkboxes0" value="1" style="margin-bottom: 10px">Google Chrome</label>' +
                    '<label class="checkbox-inline col-md-2"><input class="browserCheckBoxes" type="checkbox" name="browserType[1]" id="Checkboxes1" value="2" style="margin-bottom: 10px">Firefox</label>');
                var browserArray = [];
                if (data.DomainInfo.BrowserType.indexOf(",") > -1) {
                    barowserArray = data.DomainInfo.BrowserType.split(",");
                }
                else {
                    barowserArray = data.DomainInfo.BrowserType;
                }
                var browserCheckedBoxes = formEdit.find(".browserCheckBoxes");
                for (var p = 0; p < 2; p++) {
                    if (barowserArray.indexOf(browserCheckedBoxes[p].value) > -1) {
                        $(browserCheckedBoxes[p]).prop("checked", true);
                    }
                }

                var testBoxes = formEdit.find("#testType");

                testBoxes.append(
                    '<input class="hidden" type="text" name="TestType" id="checkedTest" />' +
                    '<label class="checkbox-inline col-md-2"><input class="testCheckBoxes" type="checkbox" name="testType[0]" id="Checkboxes0" value="1" style="margin-bottom: 10px">Performance Test</label>' +
                    '<label class="checkbox-inline col-md-2"><input class="testCheckBoxes" type="checkbox" name="testType[1]" id="Checkboxes1" value="2" style="margin-bottom: 10px">Monitoring Test</label>');

                var testArray = [];
                if (data.DomainInfo.TestType.indexOf(",") > -1) {
                    testArray = data.DomainInfo.TestType.split(",");
                }
                else {
                    testArray = data.DomainInfo.TestType;
                }
                var testCheckedBoxes = formEdit.find(".testCheckBoxes");
                for (var q = 0; q < 2; q++) {
                    if (testArray.indexOf(testCheckedBoxes[q].value) > -1) {
                        $(testCheckedBoxes[q]).prop("checked", true);
                    }
                }
            }
        });
    },

    FilterDomain: function () {
        var input, filter, table, tr, td, i;
        input = document.getElementById("inputSearch");
        filter = input.value.toLowerCase();
        table = document.getElementById("DomainExaminationTable");
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

    ChangeStatus: function (e) {
        var Id = e.closest('tr').cells.item(0).innerHTML;
        if (e.checked) {
            $.ajax({
                url: "/PerformanceAnalysis/ChangeStatus",
                data: { Id: Id, value: "1" },
                type: "POST",
                dataType: "json",
                succes: function () {

                }
            });
        }
        else {
            $.ajax({
                url: "/PerformanceAnalysis/ChangeStatus",
                data: { Id: Id, value: "0" },
                type: "POST",
                dataType: "json",
                succes: function () {

                }
            });
        }
    }


};