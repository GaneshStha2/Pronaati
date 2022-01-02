/// <reference path="../../../scripts/knockout-3.4.2.js" />
/// <reference path="../../../scripts/app/globals/riddha.dateconversion.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.backend.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.vocabularyandpronunciationbooster.model.js" />

function VocabularyAndPronunciationBoosterController() {

    var self = this;
    var url = "/Api/VocabularyAndPronunciationBoosterApi";
    self.VocabularyAndPronunciationBooster = ko.observable(new VocabularyAndPronunciationBoosterModel());
    self.SelectedVocabularyAndPronunciationBooster = ko.observable();
    self.VocabularyAndPronunciationBoosterList = ko.observableArray([]);

    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.VocabularyAndPronunciationBooster().Word() == "") {
            Riddha.UI.Toast("Please enter the Word !", 0);
            return;
        }
        if (self.VocabularyAndPronunciationBooster().WordType() == "") {
            Riddha.UI.Toast("Please enter the Word Type !", 0);
            return;
        }
        
        if (self.VocabularyAndPronunciationBooster().FileUrl() == '') {
            Riddha.UI.Toast("Please select the audio file", 0);
            return;
        }
        //if (self.VocabularyAndPronunciationBooster().WordMeaning() == "") {
        //    Riddha.UI.Toast("Please select the audio file", 0);
        //    return;
        //}
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.VocabularyAndPronunciationBooster()))
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
            Riddha.ajax.put(url, ko.toJS(self.VocabularyAndPronunciationBooster()))
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
        if (self.SelectedVocabularyAndPronunciationBooster() == undefined || self.SelectedVocabularyAndPronunciationBooster().length > 1 || self.SelectedVocabularyAndPronunciationBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedVocabularyAndPronunciationBooster().Id(), null)
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
        title: "Vocabulary & Pronunciation Booster",
        target: "#VocabularyAndPronunciationBooster",
        url: "/Api/VocabularyAndPronunciationBoosterApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Word', title: "Word", filterable: true },
            { field: 'WordType', title: "Word Type", filterable: true },
            //{ field: 'WordMeaning', title: "Meaning", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedVocabularyAndPronunciationBooster(new VocabularyAndPronunciationBoosterModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedVocabularyAndPronunciationBooster(new VocabularyAndPronunciationBoosterModel());
        $("#VocabularyAndPronunciationBooster").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.VocabularyAndPronunciationBooster(new VocabularyAndPronunciationBoosterModel({ Id: self.VocabularyAndPronunciationBooster().Id() }));

    }

    self.ShowModal = function () {
        debugger;
        if (self.ModeOfButton() == "Create") {
            self.VocabularyAndPronunciationBooster(new VocabularyAndPronunciationBoosterModel());
        }
        $("#VocabularyAndPronunciationBoosterCreationModel").modal('show');
    };

    $("#VocabularyAndPronunciationBoosterCreationModel").on('hidden.bs.modal', function () {

        self.ModeOfButton("Create"); 
        self.Reset();
    });



    
    self.CloseModal = function () {
        self.Reset();

        self.ModeOfButton("Create");
        $("#VocabularyAndPronunciationBoosterCreationModel").modal('hide');
    }

    self.View = function () {
        debugger;
        if (self.SelectedVocabularyAndPronunciationBooster() == undefined || self.SelectedVocabularyAndPronunciationBooster().length > 1 || self.SelectedVocabularyAndPronunciationBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to view.", 0);

        }
        self.VocabularyAndPronunciationBooster(new VocabularyAndPronunciationBoosterModel(ko.toJS(self.SelectedVocabularyAndPronunciationBooster())));
        $("#VocabularyAndPronunciationBoosterViewModel").modal('show');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {
        debugger;
        if (self.SelectedVocabularyAndPronunciationBooster() == undefined || self.SelectedVocabularyAndPronunciationBooster().length > 1 || self.SelectedVocabularyAndPronunciationBooster().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.VocabularyAndPronunciationBooster(new VocabularyAndPronunciationBoosterModel(ko.toJS(self.SelectedVocabularyAndPronunciationBooster())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    var Preloaded = false;
    self.OpenFileManager = function () {

        debugger;
        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded , function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.VocabularyAndPronunciationBooster().FileUrl(data.URL);

            $("#fileManager").modal("hide");
        });



    }


    $("#fileManager").on('hidden.bs.modal', function () {
        Preloaded = true;

    });

}