/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Company.Model.js" />
/// <reference path="Riddha.Script.QuestionPackage.Model.js" />

function questionPackageController() {
    var self = this;
    var url = "/Api/QuestionPackageApi";
    self.QuestionPackage = ko.observable(new QuestionPackageModel());
    self.QuestionPackages = ko.observableArray([]);
    self.SelectedQuestionPackage = ko.observable();
    self.ModeOfButton = ko.observable('Create');
    self.CreateQuestionSetList = ko.observableArray([]);
    self.QuestionSetList = ko.observableArray([]);
    self.QuestionSet = ko.observable(new QuestionSetsDropDownModel());


    GetQuestionSetList();
    function GetQuestionSetList() {
        Riddha.ajax.get(url + "/GetQuestionSetDropDown")
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), QuestionSetsDropDownModel);
                    self.QuestionSetList(data);
                }
            });
    }


    self.CheckUniqueCode = function (model) {
        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.QuestionPackage().PackageCode() + "&Id=" + self.QuestionPackage().Id(), null)
            .done(function (result) {
                if (result == false) {
                    self.QuestionPackage().PackageCode('');
                    Riddha.UI.Toast("Question Package Code has been taken", 0);
                    return;
                }
            });
    }


    self.KendoGridOptions = {
        title: "Question Package",
        target: "#questionPackageKendoGrid",
        url: url + "/GetQuestionPackageKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'PackageName', title: "Question Package Name", width: 225 },
            { field: 'PackageCode', title: "Question Package Code", width: 200 },
            { field: 'PackagePrice', title: "Question Package Price", width: 200, filterable: false },
            { field: 'CreatedBy', title: "Created By", width: 200, filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedQuestionPackage(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#questionPackageKendoGrid").getKendoGrid().dataSource.read();
    };

    self.AddNewQuestionSet = function (model) {
        debugger;
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.CreateQuestionSetList(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            Riddha.UI.Toast("Question Set has already been selected !", 0);
        }
        else {
            var data = ko.utils.arrayFirst(self.QuestionSetList(), function (item) {
                return model.Id() == item.Id();
            });
            var Validate = model.ValidDuration();
            data.ValidDuration(Validate);
            self.CreateQuestionSetList.push(data);            
            self.ResetQuestionSetModel();
        }
    }

    self.ResetQuestionSetModel = function () {
        self.QuestionSet(new QuestionSetDropDownModel());
    }

    



    self.RemoveQuestionSet = function (model) {
        self.CreateQuestionSetList.remove(model);
    }

    self.CreateUpdate = function () {
        if (self.QuestionPackage().PackageName() == "") {
            return Riddha.UI.Toast("Package Name can't be empty !", 0);
        }
        if (self.QuestionPackage().ExpiryDuration() == 0) {
            return Riddha.UI.Toast("Expiry Duration can't be empty !");
        }
        if (self.QuestionPackage().PackageCode() == "") {
            return Riddha.UI.Toast("Package Code can't be empty !", 0);
        }
        if (self.QuestionPackage().PackagePrice() == 0) {
            return Riddha.UI.Toast("Please enter the package price !", 0);
        }
        var data = {
            QuestionPackageVm: ko.toJS(self.QuestionPackage()),
            QuestionPackageDetailList: self.CreateQuestionSetList(),
        };


        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, data)
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
            Riddha.ajax.put(url, data)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
    };

    self.Reset = function () {
        self.QuestionPackage(new QuestionPackageModel({ Id: self.QuestionPackage().Id() }));
        self.CreateQuestionSetList([]);
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedQuestionPackage() == undefined) {
            Riddha.UI.Toast("Please select a row to edit !", 0);
            return;
        }
        Riddha.ajax.get(url + "/GetPackageDetailsByMasterId?Id=" + self.SelectedQuestionPackage().Id, null)
            .done(function (result) {
                self.QuestionPackage(new QuestionPackageModel(ko.toJS(result.Data.QuestionPackageVm)));
                var questionSetList = Riddha.ko.global.arrayMap(result.Data.QuestionPackageDetailList, QuestionSetsDropDownModel);
                self.CreateQuestionSetList(questionSetList);
            });
        self.ShowModal();
        self.ModeOfButton("Update");
    };

    self.Delete = function () {
        if (self.SelectedQuestionPackage() == undefined) {
            Riddha.UI.Toast("Please select a row to edit.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedQuestionPackage().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedQuestionPackage(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    $("#QuestionPackageModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    })

    self.ShowModal = function (model) {
        $("#QuestionPackageModel").modal('show');
    }

    self.CloseModal = function () {
        $("#QuestionPackageModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
   
}