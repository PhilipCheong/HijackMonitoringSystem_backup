$(function (data) {
    $("#GroupId").onChange({
        url: "/Home/CustomerDomains",
        data: data,
        dataType: "json"
    });
});
