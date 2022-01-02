function transactionHistoryController() {

	var self = this;
	var url = "/Api/TransactionApi";

	self.KendoGridOptions = {
		title: "Transaction History",
		target: "#TransactionHistory",
		url: "/Api/TransactionApi/GetKendoGrid",
		height: 490,
		paramData: {},
		multiSelect: false,
		group: true,
		groupParam: { field: "StudentName" },
		columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'StudentName', title: "Student Name", filterable: true },
            { field: 'TestName', title: "Test Name", filterable: true },
            { field: 'Amount', title: "Amount", filterable: false },
            { field: 'TransactionType', title: "Transaction Type", filterable: true, },
            { field: 'PurchasedDate', title: "Purchased Date", filterable: false, },

		],
	};
}