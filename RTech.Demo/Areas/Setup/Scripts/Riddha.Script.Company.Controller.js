/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Company.Model.js" />

//Company
function companyController() {
    var self = this;
    var config = new Riddha.config();
    var curDate = config.CurDate;
    var lang = config.CurrentLanguage;
    var url = "/Api/CompanyApi";
    var record = 0;
    self.Company = ko.observable(new CompanyModel());
    self.Companies = ko.observableArray([]);
    self.SelectedCompany = ko.observable(new CompanyGridVm());
    self.UserName = ko.observable('');
    self.Password = ko.observable('');
    self.CompanyLogin = ko.observable(new CompanyLoginModel());
    self.CompanyLicense = ko.observable(new CompanyLicenseModel());
    self.LicenseStatus = ko.observable('');
    self.ModeOfButton = ko.observable('Create');
    //Region Password Validation
    self.IsValid = ko.observable(false);
    self.PasswordInfo = ko.observable('');
    self.PasswordInfoStyle = ko.observable('');
    self.PasswordInfo = ko.computed(function () {
        var language = Riddha.config().CurrentLanguage;
        var password = self.CompanyLogin().Password();
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
    //EndRegion


    self.GetExpiryDate = function () {
        if (self.CompanyLicense().LicensePeriodInDays() == 0) {
            return;
        }
        var result = new Date(self.CompanyLicense().IssueDate());
        result.setDate(result.getDate() + parseInt(self.CompanyLicense().LicensePeriodInDays()));
        self.CompanyLicense().ExpiryDate(Riddha.util.formatDate(result));
    };
    self.RenewLicense = function () {
        var renewPeriod = parseInt(self.CompanyLicense().RenewDays());
        var licensePeriod = parseInt(self.CompanyLicense().LicensePeriodInDays());
        if (renewPeriod == 0) {
            return;
        }
        self.CompanyLicense().LicensePeriodInDays(renewPeriod + licensePeriod);
        var result = new Date(self.CompanyLicense().IssueDate());
        result.setDate(result.getDate() + (renewPeriod + licensePeriod));
        self.CompanyLicense().ExpiryDate(Riddha.util.formatDate(result));
    }
    self.LicenseStatusList = ko.observableArray([
        { Id: '0', Name: 'New' },
        { Id: '1', Name: 'ReNew' }
    ]);

    self.SoftwarePackageType = ko.observableArray([
        { Id: '0', Name: 'Silver' },
        { Id: '1', Name: 'Gold' },
        { Id: '2', Name: 'Platinum' },
    ]);

    GetCompanies();
    function GetCompanies() {
        Riddha.ajax.get(url)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), CompanyGridVm);
                self.Companies(data);
            });
    };

    self.CheckDuplicateSNo = function (item, event) {
        Riddha.ajax.get(url + "/CheckDuplicateSNo/?Code=" + item.Code())
            .done(function (result) {
                if (result == true) {
                    Riddha.UI.Toast("Code already exist!!!", Riddha.CRM.Global.ResultStatus.processError);
                    return self.Company().Code("");
                }
            })
    }
    self.CreateUpdate = function () {
        if (self.Company().Code() == "") {
            return Riddha.UI.Toast("Please enter code!!!", Riddha.CRM.Global.ResultStatus.processError);
        }

        if (self.Company().Name.hasError()) {
            return Riddha.UI.Toast(self.Company().Name.validationMessage(), Riddha.CRM.Global.ResultStatus.processError);
        }
        if (self.Company().Address.hasError()) {
            return Riddha.UI.Toast(self.Company().Address.validationMessage(), Riddha.CRM.Global.ResultStatus.processError);
        }
        if (self.Company().ContactNo.hasError()) {
            return Riddha.UI.Toast(self.Company().ContactNo.validationMessage(), Riddha.CRM.Global.ResultStatus.processError);
        }
        if (self.Company().ContactPerson.hasError()) {
            return Riddha.UI.Toast(self.Company().ContactPerson.validationMessage(), Riddha.CRM.Global.ResultStatus.processError);
        }
        if (self.Company().Email() == "") {
            return Riddha.UI.Toast("Please Email Address!!!", Riddha.CRM.Global.ResultStatus.processError);
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, self.Company())
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);

                });
        }
        else if (self.ModeOfButton() == 'Update') {
            Riddha.ajax.put(url, self.Company())
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.ModeOfButton("Create");
                        self.Reset();
                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
    };

    self.Reset = function () {
        self.Company(new CompanyModel({ Id: self.Company().Id() }));
        self.SelectedCompany(new CompanyGridVm());
    };

    self.Select = function () {
        var id = self.SelectedCompany().Id();
        if (id == 0) {
            Riddha.UI.Toast("Please select row to edit", 0);
            return;
        }
        Riddha.ajax.get(url + "/?id=" + id)
            .done(function (result) {
                self.Company(new CompanyModel(result.Data));
                self.ModeOfButton('Update');
                self.ShowModal();
            });

    };

    self.Delete = function (company, e) {
        var id = self.SelectedCompany().Id();
        if (id == 0) {
            Riddha.UI.Toast("Please select Company to Delete", 0);
            return;
        }
        Riddha.UI.Confirm("Confirm to Delete this Model?", function () {
            Riddha.ajax.delete(url + "/" + id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    self.ApproveSuspend = function (item) {
        var id = self.SelectedCompany().Id();
        if (id == 0) {
            Riddha.UI.Toast("Please select company to issue License", 0);
            return;
        }
        Riddha.UI.Confirm("Confirm to Approve/Suspend Company?", function () {
            Riddha.ajax.get(url + "/SuspendOrApproveCompany" + "/?id=" + id)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                })
        });
    };


    self.ShowModal = function () {
        $("#companyCreationModel").modal('show');
    };

    $("#companyCreationModel").on('hidden.bs.modal', function () {
        self.ModeOfButton("Create");
        self.Reset();
    });

    self.CloseModal = function () {
        $("#companyCreationModel").modal('hide');
        self.ModeOfButton("Create");
    };

    self.CheckDuplicateEmail = function (item, event) {
        var resx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!resx.test(item.Email())) {
            Riddha.UI.Toast("Invalid Emial Address!!!", Riddha.CRM.Global.ResultStatus.processError);
            return self.Company().Email("");
        }
        Riddha.ajax.get(url + "/CheckDuplicateEmail/?Email=" + item.Email())
            .done(function (result) {
                if (result == true) {
                    Riddha.UI.Toast("This Email already exist!!!", Riddha.CRM.Global.ResultStatus.processError);
                    return self.Company().Email("");
                }
            })
    }
    self.KendoGridOptions = {
        title: "Company",
        target: "#companyKendoGrid",
        url: url + "/GetCompanyKendoGrid",
        height: 500,
        paramData: {},
        multiSelect: false,
        selectable: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: lang == "ne" ? "" : "Code" },
            { field: 'Name', title: lang == "ne" ? "विभाग" : "Name" },
            { field: 'Address', title: lang == "ne" ? "फाँट " : "Address" },
            { field: 'ContactNo', title: lang == "ne" ? "फाँट " : "Contact No" },
            { field: 'ContactPerson', title: lang == "ne" ? "फाँट " : "Contact Person" },
            { field: 'Email', title: lang == "ne" ? "फाँट " : "Email" },
            { field: 'WebUrl', title: lang == "ne" ? "फाँट " : "Web Url" },
            { field: 'PAN', title: lang == "ne" ? "फाँट " : "Pan" },
            { field: 'Status', title: lang == "ne" ? "फाँट " : "Status", filterable: false }
        ],
        SelectedItem: function (item) {
            self.SelectedCompany(new CompanyGridVm(item));
        },
        SelectedItems: function (items) {
        }
    };
    self.RefreshKendoGrid = function () {
        $("#companyKendoGrid").getKendoGrid().dataSource.read();
        self.SelectedCompany(new CompanyGridVm());
    }
    self.ShowCompanyLoginModal = function () {
        var id = self.SelectedCompany().Id();
        if (id == 0) {
            Riddha.UI.Toast("Please select row to Register Login", 0);
            return;
        }
        Riddha.ajax.get(url + "/GetCompanyLogin?companyId=" + id)
            .done(function (result) {
                if (result.Status == 4) {
                    self.CompanyLogin(new CompanyLoginModel(result.Data));
                    if (result.Data.CompanyId == 0) {
                        self.CompanyLogin().CompanyId(id);
                    }
                    $("#companyLoginModal").modal('show');
                }
            })
    }
    self.RegisterCompanyLogin = function () {
        if (self.CompanyLogin().UserName().trim() == "") {
            Riddha.UI.Toast("User Name is required", 0);
            return;
        }
        if (self.CompanyLogin().Password() == "") {
            Riddha.UI.Toast("Password is required", 0);
            return;
        }
        if (self.IsValid() == false) {
            Riddha.UI.Toast("Invalid Password", 2);
            return;
        }
        Riddha.ajax.post(url + "/RegisterCompanyLogin", self.CompanyLogin())
            .done(function (result) {
                if (result.Status == 4) {
                    $("#companyLoginModal").modal('hide');
                    self.CompanyLogin(new CompanyLoginModel());
                    self.RefreshKendoGrid();
                }
                Riddha.UI.Toast(result.Message, result.Status);
            })
    }
    self.ShowCompanyLicenseModal = function () {
        var id = self.SelectedCompany().Id();
        if (id == 0) {
            Riddha.UI.Toast("Please select company to issue License", 0);
            return;
        }
        Riddha.ajax.get(url + "/GetCompanyLicense?companyId=" + id)
            .done(function (result) {
                if (result.Status == 4) {
                    self.CompanyLicense(new CompanyLicenseModel(result.Data));
                    if (result.Data.IssueDate == null) {
                        self.LicenseStatus("Provide");
                        self.CompanyLicense().IssueDate(curDate);
                        self.CompanyLicense().CompanyId(id);
                    }
                    else {
                        self.LicenseStatus("Renew");
                    }
                    $("#companyLicenseModal").modal('show');
                }
                else {
                    Riddha.UI.Toast(result.Message, result.Status);
                }
            })
    }
    self.SaveCompanyLicense = function () {
        if (self.CompanyLicense().LicensePeriodInDays() == 0) {
            Riddha.UI.Toast("License period is required", 0);
            return;
        }
        if (self.CompanyLicense().IssueDate() == "") {
            Riddha.UI.Toast("License issue date is required", 0);
            return;
        }
        Riddha.ajax.post(url + "/IssueCompanyLicense", self.CompanyLicense())
            .done(function (result) {
                if (result.Status == 4) {
                    $("#companyLicenseModal").modal('hide');
                    self.CompanyLicense(new CompanyLicenseModel());
                    self.RefreshKendoGrid();
                }
                Riddha.UI.Toast(result.Message, result.Status);
            })
    }
    self.MigrateDefaultData = function () {
        var id = self.SelectedCompany().Id();
        if (id == 0) {
            Riddha.UI.Toast("Please select company to issue License", 0);
            return;
        }
        Riddha.UI.Confirm("Confirm to migrate data?", function () {
            Riddha.ajax.get(url + "/MigrateDefaultData?companyId=" + id)
                .done(function (result) {
                    Riddha.UI.Toast(result.Message, result.Status);
                })
        });
    }
}

function companyProfileController() {
    var self = this;
    var url = "/Api/CompanyApi";
    self.Company = ko.observable(new CompanyModel());
    getCompanyProfile();
    function getCompanyProfile() {
        Riddha.ajax.get(url + "/GetCompanyProfile")
            .done(function (result) {
                self.Company(new CompanyModel(result.Data));
            })
    }
    self.CheckDuplicateSNo = function (item, event) {
        Riddha.ajax.get(url + "/CheckDuplicateSNo/?Code=" + item.Code())
            .done(function (result) {
                if (result == true) {
                    //user toast using process   
                    Riddha.UI.Toast("Code already exist!!!", Riddha.CRM.Global.ResultStatus.processError);
                    return self.Company().Code("");
                }
            })
    }
    self.UpdateCompanyProfile = function () {
        if (self.Company().Code().trim() == "") {
            return Riddha.UI.Toast("Company code is required", 0);
        }
        if (self.Company().Name.hasError()) {
            return Riddha.UI.Toast("Company Name is required", 0);
        }
        if (self.Company().Address.hasError()) {
            return Riddha.UI.Toast("Address is required", 0);
        }
        if (self.Company().ContactNo.hasError()) {
            return Riddha.UI.Toast("Contact number is required", 0);
        }
        if (self.Company().ContactPerson.hasError()) {
            return Riddha.UI.Toast("Contact person is required", 0);
        }
        if (self.Company().Email().trim() == "") {
            return Riddha.UI.Toast("Email is required", 0);
        }
        Riddha.ajax.put(url + "/UpdateCompanyProfile", ko.toJS(self.Company()))
            .done(function (result) {
                Riddha.UI.Toast(result.Message, result.Status);
            })
    }
    self.Reset = function (model) {
        self.Company(new CompanyModel({ Id: model.Id() }));
    }
}




