

var DashboardFunction = {

	DropDownId: function (callback) {
		$.ajax({
			url: "/Home/AllCustomerId",
			data: data,
			dataType: "json",
			success: function (data) {
				if (callback) {
					callback(data);
				}
			}
		});
	},
	DomainList: function (id, callback) {
		$.ajax({
			url: "/Home/CustomerDomains",
			data: { id: id },
			dataType: "json",
			success: function (data) {
				if (callback) {
					callback(data);
				}
			}
		});
	}
}
var TableFunction = {

	tablePagination: function (tableName, itemsPerPage) {

		this.tableName = tableName;
		this.itemsPerPage = itemsPerPage;
		this.currentPage = 1;
		this.pages = 0;
		this.inited = false;

		this.showRecords = function (from, to) {
			var rows = document.getElementById(tableName).rows;

			// i starts from 1 to skip table header row
			for (var i = 1; i < rows.length; i++) {
				if (i < from || i > to) {
					rows[i].style.display = 'none';
				} else {
					rows[i].style.display = '';
				}
			}
		};

		this.showPage = function (pageNumber) {
			if (!this.inited) {
				// Not initialized
				return;
			}

			var oldPageAnchor = document.getElementById('pg' + this.currentPage);
			oldPageAnchor.className = 'pg-normal';

			this.currentPage = pageNumber;
			var newPageAnchor = document.getElementById('pg' + this.currentPage);
			newPageAnchor.className = 'pg-selected';

			var from = (pageNumber - 1) * itemsPerPage + 1;
			var to = from + itemsPerPage - 1;
			this.showRecords(from, to);

			var pgNext = document.querySelector('.pg-next'),
				pgPrev = document.querySelector('.pg-prev');

			document.body.scrollTop = 0;
			document.documentElement.scrollTop = 0;


			if (this.currentPage == this.pages) {
				pgNext.style.display = 'none';
			} else {
				pgNext.style.display = '';
			}

			if (this.currentPage === 1) {
				pgPrev.style.display = 'none';
			} else {
				pgPrev.style.display = '';
			}
		};

		this.prev = function () {
			if (this.currentPage > 1) {
				this.showPage(this.currentPage - 1);
			}
		};

		this.next = function () {
			if (this.currentPage < this.pages) {
				this.showPage(this.currentPage + 1);
			}
		};

		this.init = function () {
			var rows = document.getElementById(tableName).rows;
			var records = (rows.length - 1);

			this.pages = Math.ceil(records / itemsPerPage);
			this.inited = true;
		};

		this.showPageNav = function (pagerName) {
			if (!this.inited) {
				// Not initialized
				return;
			}

			var element = document.getElementById('PaginationNav'),
				pagerHtml = '<span onclick="' + pagerName + '.prev();" class="pg-normal pg-prev">&#171;</span>';

			for (var page = 1; page <= this.pages; page++) {
				pagerHtml += '<span id="pg' + page + '" class="pg-normal pg-next" onclick="' + pagerName + '.showPage(' + page + ');">' + page + '</span>';
			}

			pagerHtml += '<span onclick="' + pagerName + '.next();" class="pg-normal">&#187;</span>';

			element.innerHTML = pagerHtml;
		};
	}
}

