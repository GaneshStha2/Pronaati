/// <reference path="../../Scripts/knockout-3.4.2.js" />
/// <reference path="../../Scripts/App/Globals/Riddha.DateConversion.js" />
/// <reference path="../../Scripts/App/Globals/Riddha.Globals.Backend.js" />
/// <reference path="../../Scripts/App/Globals/Riddha.Globals.ko.js" />
/// <reference path="Riddhasoft.Home.Model.js" />
function homeController() {
    var self = this;
    var url = "/Api/HomeApi"
    var config = new Riddha.config();
    var userId = config.UserId;
    var language = config.CurrentLanguage;
    var docHeight = $(document).height();
    self.TableInfos = ko.observableArray([]);
    self.DashboardInfoCount = ko.observable(new DashboardInfoCountModel());
    getDashboardCount();
    function getDashboardCount() {
        Riddha.ajax.get(url + "/GetDashboardCount")
        .done(function(result) {
            if (result.Status==4) {
                self.DashboardInfoCount(result.Data);
            }
        })
    }
    self.kendoPendingOrderOptions = function (title) {
        return {
            title: title,
            target: "#kendoPendingOrder",
            urlMaster: "/Api/HomeApi/GetPendingOrders",
            urlDetail: "/Api/HomeApi/GetOrderItems",
            width: '70%',
            height: docHeight - 20,
            multiSelect: false,
            maximize: true,
            actions: [
                "Close"
            ],
            pageSize: 50,
            columnsMaster: self.columnSpecForPendingOrder,
            columnsChild: self.columnSpecForItems,
            autoOpen: false
        }
    }
    self.columnSpecForPendingOrder = [
        { field: "OrderNo",title:"Order No",filterable:false },
        { field: "Table", title: "Table", filterable: false },
        { field: "Time", title: "Order Time", filterable: false },
        { field: "Delay", title: "Pending Time", filterable: false },
        { field: "Status", title: "Status", filterable: false }
    ]

    self.columnSpecForItems = [
         { field: "Item",title:"Item" },
         { field: "Quantity", title: "Quantity", template: "#=Quantity+' - '+Unit #" },
         { field: "Price", title: "Rate", template: "#='Rs '+ Price#" },
         { field: "Discount", title: "Discount", template: "#=Discount+' %'#" }
    ]
    self.kendoTotalOrderOptions = function (title) {
        return {
            title: title,
            target: "#kendoTotalOrder",
            urlMaster: "/Api/HomeApi/GetTotalOrders",
            urlDetail: "/Api/HomeApi/GetOrderItems",
            width: '70%',
            height: docHeight - 20,
            multiSelect: false,
            maximize: true,
            actions: [
                "Close"
            ],
            pageSize: 50,
            columnsMaster: self.columnSpecForTotalOrder,
            columnsChild: self.columnSpecForItems,
            autoOpen: false
        }
    }
    self.columnSpecForTotalOrder = [
        { field: "OrderNo", title: "Order No", filterable: false },
        { field: "Table", title: "Table", filterable: false },
        { field: "OrderDate", title: "Order Date", filterable: false },
        { field: "BillAmount", title: "Total Amount", filterable: false, template: "#='Rs '+BillAmount#" ,format:"N2"},
        { field: "ServiceCharge", title: "Service Charge", filterable: false, template: "#=ServiceCharge+' %'#", format: "N2" },
        { field: "Vat", title: "VAT", filterable: false, template: "#=Vat+' %'#", format: "N2" },
        { field: "Discount", title: "Discount", filterable: false, template: "#='Rs '+Discount#", format: "N2" },
        { field: "RecivedAmount", title: "Recived Amount", filterable: false, template: "#='Rs '+RecivedAmount#", format: "N2" }
    ]
    self.TableMode = ko.observable(0);
    self.ShowActiveTableInfo = function () {
        showTableInfo(0);
    }
    self.ShowReservedTableInfo = function () {
        showTableInfo(2);
    }
    function showTableInfo(tableMode) {
        self.TableMode(tableMode);
        Riddha.ajax.get(url + "/GetTableInfo")
        .done(function (result) {
            if (result.Status == 4) {
                var data = Riddha.ko.global.arrayMap(result.Data, TableInfoModel);
                self.TableInfos(data);
                $("#tableInfoModal").modal('show');
            }
        })
    }
}

function menuActionController() {
    var self = this;
    self.Menus = ko.observableArray([]);
    self.ActionCodes = ko.observableArray([]);
    self.SearchField = ko.observable('check');
    getMenus();
    function getMenus() {
        Riddha.ajax.get("/Api/MenuApi/GetMenus")
        .done(function (result) {
            var data = Riddha.ko.global.arrayMap(result.Data, MenuModel);
            self.Menus(data);
        })
    }

}


function licenseInfoController() {
    var self = this;
    self.licenseInfo = ko.observable(new LicenseInfoVm());
    var docHeight = $(document).height();
    function getlicenseInfo() {
        Riddha.ajax.get("/Api/CompanyApi/GetLicenseInfo")
        .done(function (result) {
            self.licenseInfo(new LicenseInfoVm(result.Data));
        })
    };
    self.ShowModal = function () {
        getlicenseInfo();
        $("#licenseInfoModal").modal('show');
    };
}
function changePasswordController() {
    var self = this;
    var url = "/Api/UserApi/ChangePassword";
    var language = Riddha.config().CurrentLanguage;
    self.User = ko.observable(new ChangePasswordModel());
    self.Password = ko.observable('');
    self.IsValid = ko.observable(false);
    self.PasswordInfo = ko.observable('');
    self.PasswordInfoStyle = ko.observable('');

    function GetUser() {
        Riddha.ajax.get(url, null)
        .done(function (result) {
            var data = new ChangePasswordModel(result.Data);
            self.Password(result.Data.Password)
            self.User(data);
        });
    }

    self.PasswordInfo = ko.computed(function () {
        var password = self.User().NewPassword();
        self.PasswordInfoStyle('form-control text-center bg-red-gradient');
        if (/^([0|\+[0-9]{1,5})?([7-9][0-9]{9})$/.test(password) == false) {
            if (password == "") {
                self.PasswordInfoStyle('form-control text-center bg-aqua-gradient');
                return language == "ne" ? "उदाहरण: Hamro@hajiri123 " : "eg:(valid password):Hamro@hajiri123";
            }
            else if (password.length < 6) {
                self.IsValid(false);
                return language == "ne" ? "पास्वर्डको लम्मबाइ ६ भन्दा बढी चाहिन्छ " : "Password should be of more than 6 chars";
            } else if (password.search(/\d/) == -1) {
                self.IsValid(false);
                return language == "ne" ? "पास्वर्डमा नम्बर अनिवार्य छ" : "Password should contain a number";
            } else if (password.search(/[a-zA-Z]/) == -1) {
                self.IsValid(false);
                return language == "ne" ? "पास्वर्डमा अल्फाबेट अनिवार्य छ" : "Password should contain a alphabet";
            } else if (/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(password) == false) {
                self.IsValid(false);
                return language == "ne" ? "पास्वर्डमा बिषेस स्ंकेत अनिवार्य छ" : "Password should contain a special symbol";
            } else if ((/[A-Z]/.test(password) && /[a-z]/.test(password)) == false) {
                self.IsValid(false);
                return language == "ne" ? "पास्वर्डमा " : "Password should contain a upper & lower case";
            }
            else {
                self.IsValid(true);
                self.PasswordInfoStyle('form-control text-center bg-aqua-gradient');
                return language == "ne" ? "पास्वर्ड ठीक छ" : "Valid Password";
            }
        }
        else {
            self.IsValid(true);
            self.PasswordInfoStyle('form-control text-center bg-aqua-gradient');
            return language == "ne" ? "पास्वर्ड ठीक छ" : "Valid Password";
        }

    }, self);

    self.Change = function () {
        if (self.User().CurrentPassword() == "")
        {
            Riddha.util.localize.Required("CurrentPassword");
            return;
        }
        if (self.User().NewPassword() == "") {
            Riddha.util.localize.Required("NewPassword");
            return;
        }
        if (self.User().ConfirmPassword() == "") {
            Riddha.util.localize.Required("ConfirmPassword");
            return;
        }
        if (!self.IsValid()) {
            Riddha.UI.Toast("Password not valid", 0);
            return;
        }
        if (self.IsValidCurrentPassword() && self.ComparePassword()) {
            Riddha.ajax.post(url, self.User())
        .done(function (result) {
            if (result.Status == 4) {
                self.CloseModal();
            }
            Riddha.UI.Toast(result.Message, result.Status);
        });
        }
    }

    self.ShowModal = function () {
        GetUser();
        $("#changePasswordModal").modal('show');
    }

    self.CloseModal = function () {
        $("#changePasswordModal").modal('hide');
    }

    self.Reset = function () {
        self.User().CurrentPassword('');
        self.User().NewPassword('');
        self.User().ConfirmPassword('');
    }

    self.IsValidCurrentPassword = function() {
        if (self.User().CurrentPassword() != self.Password()) {
            Riddha.UI.Toast("Invalid Current Password", 0);
            return false;
        }
        else {
            return true;
        }
    }
    self.ComparePassword = function () {
        if (self.User().ConfirmPassword() != self.User().NewPassword()) {
            Riddha.UI.Toast("Passwords do not match", 0);
            return false;
        }
        else {
            return true;
        }
    }
}
function showLicenseModal() {
    licenseObj.ShowModal();
}
function showChangePasswordModal() {
    changePasswordObj.ShowModal();
}