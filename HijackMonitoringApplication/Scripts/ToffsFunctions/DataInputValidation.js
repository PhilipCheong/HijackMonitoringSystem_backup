var toff_validation = {
	valid: function (element, options) {
		$(element).validate({
			debug: false
		});
		return $(element).valid();
	}

};
$.fn.toffValidation = function (options) {
	var valid = false;
	this.each(function () {
		valid = toff_validation.valid(this, options);
	});
	return valid;
};