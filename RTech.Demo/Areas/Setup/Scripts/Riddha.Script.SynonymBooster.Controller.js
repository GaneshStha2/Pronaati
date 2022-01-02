/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.synonymbooster.model.js" />


function SynonymBoosterController() {


    var self = this;
    var url = "/Api/SynonymBoosterApi";
    self.SynonymBooster = ko.observable(new SynonymBoosterModel());
    self.SelectedSynonymBooster = ko.observable();
    self.SynonymBoosterList = ko.observableArray([]);
    self.SynonymBoosterOption = ko.observable(new SynonymBoosterOptionsModel());
    self.SynonymBoosterOptionsList = ko.observableArray([]);
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.SynonymBooster().Word() == "") {
            Riddha.UI.Toast("Please enter the Word !", 0);
            return;
        }
        if (self.SynonymBooster().WordType() == "") {
            Riddha.UI.Toast("Please enter the Word Type !", 0);
            return;
        }

        if (self.SynonymBooster().Question() == "") {
            Riddha.UI.Toast("Please enter the Word Type !", 0);
            return;
        }

        //if (self.SynonymBooster().WordMeaning() == "") {
        //    Riddha.UI.Toast("Please select the audio file", 0);
        //    return;
        //}
        debugger;
        var data = { SynonymBoosterMaster: ko.toJS(self.SynonymBooster()), Options: ko.toJS(self.SynonymBoosterOptionsList()) }
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
        if (self.SelectedSynonymBooster() == undefined || self.SelectedSynonymBooster().length > 1 || self.SelectedSynonymBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSynonymBooster().Id(), null)
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
        title: "Synonym Booster",
        target: "#SynonymBooster",
        url: "/Api/SynonymBoosterApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Word', title: "Word", filterable: true },
            { field: 'WordType', title: "Word Type", filterable: true },
            { field: 'Question', title: "Question", filterable: true },
            { field: 'Options', title: "Options", filterable: false },
            { field: 'CorrectAnswer', title: "Correct Answer", filterable: false },


        ],
        SelectedItem: function (item) {
            self.SelectedSynonymBooster(new SynonymBoosterModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSynonymBooster(new SynonymBoosterModel());
        $("#SynonymBooster").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.SynonymBooster(new SynonymBoosterModel({ Id: self.SynonymBooster().Id() }));
        self.SynonymBoosterOptionsList([]);

    }

    self.ShowModal = function () {
        debugger;
        if (self.ModeOfButton() == "Create") {
            self.SynonymBooster(new SynonymBoosterModel());
        }
        $("#SynonymBoosterCreationModel").modal('show');
    };

    $("#SynonymBoosterCreationModel").on('hidden.bs.modal', function () {

        self.ModeOfButton("Create");
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();

        self.ModeOfButton("Create");
        $("#SynonymBoosterCreationModel").modal('hide');
    }

    //self.View = function () {
    //    debugger;
    //    if (self.SelectedSynonymBooster() == undefined || self.SelectedSynonymBooster().length > 1 || self.SelectedSynonymBooster().Id() == 0) {

    //        return Riddha.UI.Toast("Please select row to view.", 0);

    //    }
    //    self.SynonymBooster(new SynonymBoosterModel(ko.toJS(self.SelectedSynonymBooster())));
    //    $("#SynonymBoosterViewModel").modal('show');
    //}

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {
        debugger;
        if (self.SelectedSynonymBooster() == undefined || self.SelectedSynonymBooster().length > 1 || self.SelectedSynonymBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }

        Riddha.ajax.get("/Api/SynonymBoosterApi/getOptionDetails?MasterId=" + self.SelectedSynonymBooster().Id(), null)
            .done(function (result) {
                debugger;
                var data = Riddha.ko.global.arrayMap(result.Data, SynonymBoosterOptionsModel);
                self.SynonymBoosterOptionsList(data);
                self.SynonymBooster(new SynonymBoosterModel(ko.toJS(self.SelectedSynonymBooster())));
                self.ModeOfButton("Update");
                self.ShowModal();
            });





    }

    self.AddOption = function (model) {
        debugger;
        if (model.IsAnswer() == true) {
            var length = self.SynonymBoosterOptionsList().length;
            for (var i = 0; i < length; i++) {
                if (self.SynonymBoosterOptionsList()[i].IsAnswer() == true) {
                    return Riddha.UI.Toast("There is already one correct answer !", 0);
                }
            }
        }
        self.SynonymBoosterOptionsList.push(new SynonymBoosterOptionsModel(ko.toJS(model)));
        self.SynonymBoosterOption(new SynonymBoosterOptionsModel());
    }

    self.RemoveOption = function (model) {


        self.SynonymBoosterOptionsList.remove(model);
    }
}