/// <reference path="../../../scripts/knockout-3.4.2.js" />
/// <reference path="../../../scripts/app/globals/riddha.dateconversion.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.backend.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.mastertopicsentencebooster.model.js" />

function MasterTopicSentenceBoosterController() {


    var self = this;
    var url = "/Api/MasterTopicSentenceBoosterApi";
    self.MasterTopicSentenceBooster = ko.observable(new MasterTopicSentenceBoosterModel());
    self.SelectedMasterTopicSentenceBooster = ko.observable();
    self.MasterTopicSentenceBoosterList = ko.observableArray([]);
    self.MasterTopicSentenceBoosterOption = ko.observable(new MasterTopicSentenceBoosterOptionDetailModel());
    self.MasterTopicSentenceBoosterOptionsList = ko.observableArray([]);
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.MasterTopicSentenceBooster().QuestionTitle() == "") {
            Riddha.UI.Toast("Please enter the Word !", 0);
            return;
        }
        if (self.MasterTopicSentenceBooster().Question() == "") {
            Riddha.UI.Toast("Please enter the Word Type !", 0);
            return;
        }

        if (self.MasterTopicSentenceBooster().OptionStatement() == "") {
            Riddha.UI.Toast("Please enter the Word Type !", 0);
            return;
        }   

        //if (self.MasterTopicSentenceBooster().WordMeaning() == "") {
        //    Riddha.UI.Toast("Please select the audio file", 0);
        //    return;
        //}
        debugger;
        var data = { Master: ko.toJS(self.MasterTopicSentenceBooster()), Details: ko.toJS(self.MasterTopicSentenceBoosterOptionsList()) }
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
        if (self.SelectedMasterTopicSentenceBooster() == undefined || self.SelectedMasterTopicSentenceBooster().length > 1 || self.SelectedMasterTopicSentenceBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedMasterTopicSentenceBooster().Id(), null)
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
        target: "#MasterTopicSentenceBooster",
        url: "/Api/MasterTopicSentenceBoosterApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'QuestionTitle', title: "Question Title", filterable: true },
            { field: 'Question', title: "Question", filterable: true },
            { field: 'OptionStatement', title: "Option Statement", filterable: false },
            { field: 'Options', title: "Options", filterable: false },
            { field: 'CorrectAnswer', title: "Correct Answer", filterable: false },


        ],
        SelectedItem: function (item) {
            self.SelectedMasterTopicSentenceBooster(new MasterTopicSentenceBoosterModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedMasterTopicSentenceBooster(new MasterTopicSentenceBoosterModel());
        $("#MasterTopicSentenceBooster").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.MasterTopicSentenceBooster(new MasterTopicSentenceBoosterModel({ Id: self.MasterTopicSentenceBooster().Id() }));
        self.MasterTopicSentenceBoosterOptionsList([]);

    }

    self.ShowModal = function () {
        debugger;
        if (self.ModeOfButton() == "Create") {
            self.MasterTopicSentenceBooster(new MasterTopicSentenceBoosterModel());
        }
        $("#MasterTopicSentenceBoosterCreationModel").modal('show');
    };

    $("#MasterTopicSentenceBoosterCreationModel").on('hidden.bs.modal', function () {

        self.ModeOfButton("Create");

        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();

        self.ModeOfButton("Create");
        $("#MasterTopicSentenceBoosterCreationModel").modal('hide');
    }

    //self.View = function () {
    //    debugger;
    //    if (self.SelectedMasterTopicSentenceBooster() == undefined || self.SelectedMasterTopicSentenceBooster().length > 1 || self.SelectedMasterTopicSentenceBooster().Id() == 0) {

    //        return Riddha.UI.Toast("Please select row to view.", 0);

    //    }
    //    self.MasterTopicSentenceBooster(new MasterTopicSentenceBoosterModel(ko.toJS(self.SelectedMasterTopicSentenceBooster())));
    //    $("#MasterTopicSentenceBoosterViewModel").modal('show');
    //}

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {
        debugger;
        if (self.SelectedMasterTopicSentenceBooster() == undefined || self.SelectedMasterTopicSentenceBooster().length > 1 || self.SelectedMasterTopicSentenceBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }

        Riddha.ajax.get("/Api/MasterTopicSentenceBoosterApi/getOptionDetails?MasterId=" + self.SelectedMasterTopicSentenceBooster().Id(), null)
            .done(function (result) {
                debugger;
                var data = Riddha.ko.global.arrayMap(result.Data, MasterTopicSentenceBoosterOptionDetailModel);
                self.MasterTopicSentenceBoosterOptionsList(data);
                self.MasterTopicSentenceBooster(new MasterTopicSentenceBoosterModel(ko.toJS(self.SelectedMasterTopicSentenceBooster())));
                self.ModeOfButton("Update");
                self.ShowModal();
            });





    }

    self.AddOption = function (model) {
        if (model.IsCorrectAnswer() == true) {
            var length = self.MasterTopicSentenceBoosterOptionsList().length;
            for (var i = 0; i < length; i++) {
                if (self.MasterTopicSentenceBoosterOptionsList()[i].IsCorrectAnswer() == true) {
                    return Riddha.UI.Toast("There is already one correct answer !",0);
                }
            }
        }
        self.MasterTopicSentenceBoosterOptionsList.push(new MasterTopicSentenceBoosterOptionDetailModel(ko.toJS(model)));
        self.MasterTopicSentenceBoosterOption(new MasterTopicSentenceBoosterOptionDetailModel());
    }

    self.RemoveOption = function (model) {


        self.MasterTopicSentenceBoosterOptionsList.remove(model);
    }
}


