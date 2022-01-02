/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Package.Model.js" />


function PackageController() {
    var self = this;
    var url = "/Api/PackageApi";

    self.Package = ko.observable(new PackageMasterModel());
    self.PackageDetail = ko.observable(new PackageDetailsModel());

    self.PackageDetails = ko.observableArray([]);
    self.SelectedPackage = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {

        if (self.Package().Code() == "") {
            Riddha.UI.Toast("Please enter Code", 0);
            return;
        }
        if (self.Package().PackageTitle() == "") {
            Riddha.UI.Toast("Please enter Package Title", 0);
            return;
        }


        if (self.Package().Description() == "") {
            Riddha.UI.Toast("Please enter Description", 0);
            return;
        }

        if (self.PackageDetails().length <= 0) {
            Riddha.UI.Toast("Please select some Files", 0);
            return;
        }



        if (self.ModeOfButton() == 'Create') {
            var data = {
                PackageMasterVm: self.Package(),
                PackageDetailsVm: self.PackageDetails()
            };
            //Riddha.ajax.post(url + "/", ko.toJS(data))
            Riddha.ajax.post(url, ko.toJS(data))
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                        self.CloseModal();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
        else if (self.ModeOfButton() == 'Update') {
            var data = {
                PackageMasterVm: self.Package(),
                PackageDetailsVm: self.PackageDetails()
            };
            Riddha.ajax.put(url, ko.toJS(data))
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.ModeOfButton("Create");
                        self.Reset();
                        self.CloseModal();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
    };

    self.Delete = function (model) {
        if (self.SelectedPackage() == undefined || self.SelectedPackage().Id() == 0) {
            return Riddha.UI.Toast("Please select a row to delete", 0);
        }

        Riddha.UI.Confirm("Delete Confirm", function () {
            debugger;
            Riddha.ajax.delete(url + "/" + self.SelectedPackage().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.ModeOfButton("Create");
                        self.Reset();
                        self.RefreshKendoGrid();
                    }

                    Riddha.UI.Toast(result.Message, result.Status);
                });

        });
    };

    self.KendoGridOptions = {
        title: "Package",
        target: "#Package",
        url: "/Api/PackageApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Package Code", filterable: true },
            { field: 'PackageTitle', title: "Package Title", filterable: true },
            { field: 'CreatedDate', title: "Created Date", filterable: false },
            { field: 'CreatedBy', title: "Created By", filterable: false },
            //{ field: 'Description', title: "Description", filterable: false, },

        ],
        SelectedItem: function (item) {
            self.SelectedPackage(new PackageMasterModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedPackage(new PackageMasterModel());
        $("#Package").getKendoGrid().dataSource.read();

    };

    self.Reset = function () {
        self.Package(new PackageMasterModel({ Id: self.Package().Id(), CreatedDate: self.Package().CreatedDate(), CreatedBy: self.Package().CreatedBy() }));
        self.PackageDetail(new PackageDetailsModel());
        self.PackageDetails([]);
    };

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.Package(new PackageMasterModel());
            self.PackageDetail(new PackageDetailsModel());
        }
        $("#PackageCreationModel").modal('show');
    };

    $("#PackageCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });
    self.CloseModal = function () {
        debugger;
        $("#PackageCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    self.Select = function (model) {

        if (self.SelectedPackage() == undefined || self.SelectedPackage().length > 1 || self.SelectedPackage().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetDetails?MasterId=" + self.SelectedPackage().Id(), null)
            .done(function (result) {

                if (result.Status == 4) {

                    var data = Riddha.ko.global.arrayMap(result.Data, PackageDetailsModel);
                    self.PackageDetails(data);
                }
            });
        self.Package(new PackageMasterModel(ko.toJS(self.SelectedPackage())));
        self.ModeOfButton("Update");
        self.ShowModal();
    };

    self.AddNewFile = function (model) {
        self.PackageDetails.push(new PackageDetailsModel(ko.toJS(model)));
        self.PackageDetail(new PackageDetailsModel());
    }

    self.RemoveSelectedFile = function (model) {
        self.PackageDetails.remove(model);
    }

    var preloaded = false;   
    self.OpenFileManager = function () {              

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {
                preloaded = true;
            }
            else {
                var data = ko.toJS(result);
                var FileType = self.PackageDetail().FileType();
                self.PackageDetail(new PackageDetailsModel());
                self.PackageDetail().FileUrl(data.URL);
                self.PackageDetail().FileName(data.Name);
                self.PackageDetail().FileId(data.Id);
                self.PackageDetail().FileType(FileType);                

                preloaded = true;
            }

            $("#fileManager").modal('hide');           
        });

    }
    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
    }


}