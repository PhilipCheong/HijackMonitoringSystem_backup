jQuery.validator.addMethod("CheckUsername", function (value, element) {
	var validationObj = this;
	var valid = true;
	if (!$(element).is(':focus')) {
		AccountManagement.IsUsernameExisted(value,
			function (data) {
				//return data.result;
				valid = data.result !== true;
			});
	}
	return this.optional(element) || valid;

}, "Username is registered. Please try another name.");

jQuery.validator.addMethod("CheckEmail", function (value, element) {
	var validationObj = this;
	var valid = true;
	if (!$(element).is(':focus')) {
		AccountManagement.IsEmailExisted(value,
			function (data) {
				//return data.result;
				valid = data.result !== true;
			});
	}
	return this.optional(element) || valid;

}, "Email address is already registered. Please try another email address.");

jQuery.validator.addMethod("Check-Ips", function (value, element) {
	var validatorObj = this;
	var valid = true;
	var listIpValue = value.split(",");
	$.each(listIpValue, function (i, e) {
		var result = /^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])(\/([0-9]|[1-2][0-9]|3[0-2]))?$/.test(e);
		if (!result) {
			valid = false;
		}
	});
	return this.optional(element) || valid;

}, "Invalid Ip Address found.");

jQuery.validator.addMethod("CheckSpace", function (value, element) {
	var validatorObj = this;
	var valid = false;

	return this.optional(element) || value.indexOf(" ") < 0;

}, "Not allowed space.");
