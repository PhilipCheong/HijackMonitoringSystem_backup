function GetDomain(callback) {
	$.ajax({
		url: "/ChartsAndReports/DomainPermission",
		dataType: "json",
		method: "GET",
		success: function (data) {
			var domainDataset = [];
			for (var z = 0; z < data.length; z++) {
				domainDataset.push(data[z]);
			}
			if (callback) {
				callback(domainDataset);
			}
			
		}
	});
}

function GetServerCname(callback) {
	$.ajax({
		url: "/Server/GetServerForServerResult",
		dataType: "json",
		method: "GET",
		success: function (data) {
			var serverDataset = [];
			for (var z = 0; z < data.length; z++) {
				serverDataset.push(data[z]);
			}
			if (callback) {
				callback(serverDataset);
			}

		}
	});
}


var tagComponent = {
    init: function (el, options) {
        /*var cities = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.obj.whitespace("text"),
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: "https://bootstrap-tagsinput.github.io/bootstrap-tagsinput/examples/assets/cities.json"
        });
        cities.initialize();*/
        var defaults = {
            tagClass: "bg-dark text-white",
            trimValue: true,
            confirmKeys: [13, 44],
            delimiterRegex: /[,]+/
        };
        options = $.extend(defaults, options);
        $(el).tagsinput(options);
    },
    setValue: function (el, values) {
        var arrayValue = values.split(",");
        $.each(arrayValue,
            function (i, e) {
                $(el).tagsinput("add", e);
            });
        $(el).tagsinput("refresh");
    },
    setValueDataset: function(el, dataset,values) {
        var arrayValue = values.split(",");
        $.each(arrayValue,
            function(i, e) {
                var item = dataset.filter(a => a.code === e);
                $(el).tagsinput("add", item[0]);
            });
        $(el).tagsinput("refresh");
    }
};
$.fn.tagComponent = function (options) {
    return this.each(function () {
        new tagComponent.init(this, options);
    });
};
$.fn.tagComponentVal = function (values) {
    return this.each(function () {
        new tagComponent.setValue(this, values);
    });
};
$.fn.tagComponentValDataset = function(dataset,values) {
    return this.each(function() {
        new tagComponent.setValueDataset(this, dataset, values);
    });
};
$.fn.tagComponentRemoveAll = function () {
    return this.each(function () {
        $(this).tagsinput("removeAll");
    });
};