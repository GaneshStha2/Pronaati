/// <reference path="riddha.script.boostercollocation.model.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />

function BoosterCollocationController() {
    var self = this;
    var url = "/Api/BoosterCollocationApi";
    self.BoosterCollocation = ko.observable(new BoosterCollocationModel());
    self.SelectedBoosterCollocation = ko.observable();
    self.BoosterCollocationList = ko.observableArray([]);
    self.BoosterCollocationOption = ko.observable(new BoosterCollocationOptionsModel());
    self.BoosterCollocationOptionList = ko.observableArray([]);
    self.ModeOfButton = ko.observable('Create');

    self.CreateUpdate = function () {
        if (self.BoosterCollocation().Question() == "") {
            Riddha.UI.Toast("Please enter the Question !", 0);
            return;
        }
        if (self.BoosterCollocation().QuestionText() == "") {
            return Riddha.UI.Toast("Please enter the Question Text !", 0);
        }
        if (self.BoosterCollocation().OptionStatement() == "") {
            return Riddha.UI.Toast("Please enter the Option Statement !", 0);
        }
        var data = { BoosterCollocationMaster: ko.toJS(self.BoosterCollocation()), Options: ko.toJS(self.BoosterCollocationOptionList()) }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, data)
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
            Riddha.ajax.put(url, data)
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
        if (self.SelectedBoosterCollocation() == undefined || self.SelectedBoosterCollocation().length > 1 || self.SelectedBoosterCollocation().Id() == 0) {
            return Riddha.UI.Toast("Please select a row to delete.", 0);
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedBoosterCollocation().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.ModeOfButton("Create");
                        self.Reset();
                        self.RefreshKendoGrid();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    self.KendoGridOptions = {
        title: "Booster Collocation",
        target: "#BoosterCollocation",
        url: "/Api/BoosterCollocationApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Question', title: "Question", filterable: true },
            { field: 'QuestionText', title: "Question Text", filterable: false },
            { field: 'OptionStatement', title: "Option Statement", filterable: false },
            { field: 'Options', title: "Options", filterable: false },
            { field: 'CorrectAnswer', title: "Correct Answer", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedBoosterCollocation(new BoosterCollocationModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedBoosterCollocation(new BoosterCollocationModel());
        $("#BoosterCollocation").getKendoGrid().dataSource.read();
    }

    self.Reset = function () {
        self.BoosterCollocation(new BoosterCollocationModel({ Id: self.BoosterCollocation().Id() }));
        self.BoosterCollocationOptionList([]);
    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.BoosterCollocation(new BoosterCollocationModel());
        }
        $("#BoosterCollocationCreationModal").modal('show');
    };

    $("#BoosterCollocationCreationModal").on('hidden.bs.modal', function () {
        self.ModeOfButton("Create");
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        self.ModeOfButton("Create");
        $("#BoosterCollocationCreationModal").modal('hide');
    }

    self.Select = function (model) {
        debugger;
        if (self.SelectedBoosterCollocation() == undefined || self.SelectedBoosterCollocation().length > 1 || self.SelectedBoosterCollocation().Id() == 0) {
            return Riddha.UI.Toast("Please select a row to edit !", 0);
        }
        Riddha.ajax.get("/Api/BoosterCollocationApi/getOptionDetails?MasterId=" + self.SelectedBoosterCollocation().Id(), null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(result.Data, BoosterCollocationOptionsModel);
                self.BoosterCollocationOptionList(data);
                self.BoosterCollocation(new BoosterCollocationModel(ko.toJS(self.SelectedBoosterCollocation())));
                self.ModeOfButton("Update");
                self.ShowModal();
            });
    }

    self.AddOption = function (model) {
        debugger;
        if (model.IsAnswer() == true) {
            var length = self.BoosterCollocationOptionList().length;
            for (var i = 0; i < length; i++) {
                if (self.BoosterCollocationOptionList()[i].IsAnswer() == true) {
                    return Riddha.UI.Toast("There is already one correct answer !", 0);

                }
            }
        }
        self.BoosterCollocationOptionList.push(new BoosterCollocationOptionsModel(ko.toJS(model)));
        self.BoosterCollocationOption(new BoosterCollocationOptionsModel());
    }

    self.RemoveOption = function (model) {
        self.BoosterCollocationOptionList.remove(model);
    }
}

