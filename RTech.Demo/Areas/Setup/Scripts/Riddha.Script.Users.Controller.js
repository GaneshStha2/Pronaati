/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.users.model.js" />


function usersController() {
    var self = this;
    var url = '/Api/StudentApi';
    self.User = ko.observable(new UsersModel());
    self.Users = ko.observableArray([]);
    self.SelectedUser = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "User",
        target: "#userKendoGrid",
        url: "/Api/StudentApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Name', title: "Name", filterable: true },
            { field: 'Address', title: "Address", filterable: false },
            { field: 'BirthCountry', title: "BirthCountry", filterable: true },
            { field: 'Email', title: "Email", filterable: false },
            { field: 'GenderName', title: "Gender", filterable: false },
            { field: 'InstituteName', title: "Institute", filterable: false },
            { field: 'OccupationName', title: "Occupation", filterable: false },
            { field: 'PhoneNumber', title: "PhoneNumber", filterable: false },
            //{ field: 'OccupationName', title: "Occupation", filterable: false },
            { field: 'IsDeleted', title: "Is Deleted", filterable: true },
            { field: 'IsActive', title: "Is Active", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedUser(new UsersModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedUser(new UsersModel());
        $("#userKendoGrid").getKendoGrid().dataSource.read();
    };

    self.Delete = function (model) {
        if (self.SelectedUser() == undefined || self.SelectedUser().length > 1 || self.SelectedUser().Id() == 0) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedUser().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    //For User Credential View

    self.UserCredential = ko.observable(new UserCredentialModel());

    self.GetUserLoginCredential = function () {
        if (self.SelectedUser() == undefined || self.SelectedUser().Id() <= 0) {
            return Riddha.UI.Toast("Please select user to view Login Credentials", 0);
        }

        Riddha.ajax.get(url + "/GetUserLoginCredential?userId=" + self.SelectedUser().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.UserCredential(new UserCredentialModel(ko.toJS(result.Data)));
                    self.ShowModal();
                }
                else {
                    return Riddha.UI.Toast(result.Message, result.Status);
                }

            });
    };


    self.ShowModal = function (model) {
        $("#userCredentialsViewModals").modal('show');
    };

    self.CloseModal = function () {
        $("#userCredentialsViewModals").modal('hide');
        self.Reset();
    };

    self.Reset = function () {
        self.UserCredential(new UserCredentialModel());
    }


    //For Assigning mocktest and package


    self.MockTest = ko.observable(new NaatiMockTestModel());
    self.Package = ko.observable(new NaatiPackageMasterModel());

    self.MocktestSets = ko.observableArray([]);

    self.PackageMockTestSets = ko.observableArray([]);

    self.AutoCompleteSearchForStudent = {

        dataTextField: "Name",
        url: "/Api/StudentApi/GetStudentAutoComplete",
        select: function (item) {
            self.User().Id(item.Id);
        },
        clear: function () {
            self.User(new UserModel());
        },
        placeholder: "Search Student"
    };

    self.AutoCompleteSearchForMockTest = {

        dataTextField: "Name",

        url: "/Api/StudentApi/GetMockTestAutoComplete",
        select: function (item) {
            Riddha.ajax.get(url + "/GetNaatiMockTestInfo?id=" + item.Id)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.MockTest(new NaatiMockTestModel(ko.toJS(result.Data)));
                    } else {
                        Riddha.UI.Toast(result.Message, result.Status);
                    }
                })

        },
        clear: function () {
            self.MocktestSets(new NaatiMockTestModel());
        },
        placeholder: "Search Mock Test"
    };

    self.AutoCompleteSearchForPackage = {

        dataTextField: "Name",

        url: "/Api/StudentApi/GetPakcageAutoComplete",
        select: function (item) {
            Riddha.ajax.get(url + "/GetNaatiPackageInfo?id=" + item.Id)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.Package(new NaatiPackageMasterModel(ko.toJS(result.Data)));
                    } else {
                        Riddha.UI.Toast(result.Message, result.Status);
                    }
                })
        },
        clear: function () {
            self.Package(new NaatiPackageMasterModel());
        },
        placeholder: "Search Package"
    };


    self.AddPackageQuestion = function (model) {
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.PackageMockTestSets(), function (item) {
            return model.Id() == item.Id()
        });

        if (mapped) {
            return Riddha.UI.Toast("Package has already been selected !", 0);
        }

        else {
            self.PackageMockTestSets.push(new NaatiPackageMasterModel(ko.toJS(model)));
            self.Package(new NaatiPackageMasterModel());
        }
    };

    self.AddMockTestQuestion = function (model) {

        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please  !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.MocktestSets(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            return Riddha.UI.Toast("Mock Test has already been selected !", 0);
        }
        else {
            self.MocktestSets.push(new NaatiMockTestModel(ko.toJS(model)));

            //self.MocktestSets().push(model);

            self.MockTest(new NaatiMockTestModel());
        }
    };

    self.RemoveMockTestQuestion = function (model) {
        self.MocktestSets.remove(model);
    };

    self.RemovePackageQuestion = function (model) {
        self.PackageMockTestSets.remove(model);
    };


    self.ShowAssignMockTestAndPackageCreationModal = function (model) {
        $("#AssignMockTestAndPackageCreationModal").modal('show');
    };

    self.CloseAssignMockTestAndPackageCreationModal = function () {
        $("#AssignMockTestAndPackageCreationModal").modal('hide');
        self.ResetModal();
    };

    $("#AssignMockTestAndPackageCreationModal").on('hidden.bs.modal', function () {
        self.ResetModal();
      
    });

    self.ResetModal = function () {
        self.MockTest(new NaatiMockTestModel());
        self.Package(new NaatiPackageMasterModel());
        self.MocktestSets([]);
        self.PackageMockTestSets([]);
        self.User(new UsersModel());
        self.ModeOfButton("Create");
    }


    self.CreateUpdate = function () {
        if (self.User().Id() == 0 || self.User().Id() == undefined) {
            Riddha.UI.Toast("Please choose student", 0);
            return;
        }
        if (self.MocktestSets().length <= 0 && self.PackageMockTestSets().length <= 0) {
            Riddha.UI.Toast("Please add Mock Test or Package to assign", 0);
            return;
        }

        var data = {
            StudentId: self.User().Id(),
            Packages: self.PackageMockTestSets(),
            MockTests: self.MocktestSets(),
        };

        Riddha.ajax.post(url, ko.toJS(data))
            .done(function (result) {
                if (result.Status == 4) {
                    self.ResetModal();
                    self.RefreshKendoGrid();
                    self.CloseAssignMockTestAndPackageCreationModal();
                    return Riddha.UI.Toast(result.Message, result.Status);
                }
                else {
                    return Riddha.UI.Toast(result.Message, result.Status);
                }

            });

    }

}