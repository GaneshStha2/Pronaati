/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.AudioScoring.Model.js" />


function AudioScoringController() {
    var self = this;
    var url = "/Api/AudioScoringApi";

    self.AudioScoring = ko.observable(new AudioScoringModel());
    self.DialogueDetails = ko.observableArray([]);
    //self.SegmentTwoDetails = ko.observableArray([]);
    self.QuestionSets = ko.observableArray([]);
    self.SelectedAudioScoring = ko.observable();

    self.ModeOfButton = ko.observable('Create');
    self.DialogueId = ko.observable(0);
    //For FeedBacks
    self.FeedBackArray = ko.observableArray([]);
    GetFeedBacks();
    self.SelectedFeedBacks = ko.observableArray([]);


    self.GetSelectedFeedBack = function (id) {
        var lots = "";
        ko.utils.arrayForEach(self.FeedBackArray(), function (data) {
            if (data.Checked() == true) {
                if (lots.length != 0)
                    lots += "," + data.Id();
                else
                    lots = data.Id() + '';
            }
        });
        if (lots.length == 0) {
            ko.utils.arrayForEach(self.FeedBackArray(), function (data) {
                if (lots.length != 0)
                    lots += "," + data.Id();
                else
                    lots = data.Id() + '';
            });
        }
        self.SelectedFeedBacks(lots);
        return lots;
    }

    function GetFeedBacks() {
        Riddha.ajax.get(url + '/GetFeedBacks', null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), checkBoxModel);
                self.FeedBackArray(data);
            });
    };


    self.CreateUpdate = function () {
        var feedBacks = self.GetSelectedFeedBack()
        var data = {
            Master: self.AudioScoring(),
            DialogueDetails: self.DialogueDetails(),
            FeedBacks: feedBacks
        };

        if (self.DialogueId() == 0 || self.DialogueId() == undefined) {
            Riddha.UI.Toast("Please select dialogue to score", 0);
            return;
        }

        if (self.ModeOfButton() == 'Create') {
            if (self.AudioScoring().IsScored() == true) {
                Riddha.UI.Toast("This test is already Scored", 0);
                return;
            }
            Riddha.ajax.post(url, ko.toJS(data))
                .done(function (result) {
                    debugger;
                    if (result.Status == 4) {
                        self.AudioScoring(new AudioScoringModel(result.Data.Master))
                        self.ModeOfButton("Update");
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
        else if (self.ModeOfButton() == 'Update') {

            Riddha.ajax.put(url, ko.toJS(data))
                .done(function (result) {
                    if (result.Status == 4) {
                        self.AudioScoring(new AudioScoringModel(result.Data.Master))
                        self.ModeOfButton("Update");
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });



        }


    }


    self.KendoGridOptions = {
        title: "Mocktest Scoring",
        target: "#AudioScoring",
        url: "/Api/AudioScoringApi/GetAudioScoresKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: false,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'MockTestName', title: "Mocktest", filterable: true },
            { field: 'PackageName', title: "PackageName", filterable: false },
            { field: 'StudentName', title: "Student", filterable: true },
            { field: 'IsScored', title: "Is Scored", filterable: false, },

        ],
        SelectedItem: function (item) {
            self.SelectedAudioScoring(new AudioScoringModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedAudioScoring(new AudioScoringModel());
        $("#AudioScoring").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.AudioScoring(new AudioScoringModel());
        self.DialogueDetails([]);
        self.DialogueId(undefined);
        ko.utils.arrayForEach(self.FeedBackArray(), function (item) {
            item.Checked(false);
        });
        self.ModeOfButton("Create");
    }

    self.ShowModal = function () {
        $("#audioScoringCreationModel").modal('show');
    };

    $("#audioScoringCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.CloseModal = function () {
        $("#audioScoringCreationModel").modal('hide');
        self.RefreshKendoGrid();
        self.Reset();
    }


    self.Score = function (model) {
        debugger;
        if (self.SelectedAudioScoring() == undefined || self.SelectedAudioScoring().MockTestId() == 0) {

            return Riddha.UI.Toast("Please select row to score.", 0);

        }
        if (self.SelectedAudioScoring().IsScored() == true) {

            return Riddha.UI.Toast("This test is already scored", 0);

        }
        Riddha.ajax.get(url + "/GetMockTestmasterDataToScoreByMockTestIdAndStudentId?packageId=" + self.SelectedAudioScoring().PackageId() + "&mockTestId=" + self.SelectedAudioScoring().MockTestId() + "&studentId=" + self.SelectedAudioScoring().StudentId())
            .done(function (result) {
                if (result.Status == 4) {


                    self.AudioScoring(new AudioScoringModel(result.Data.Master))
                    self.GetQuestionSets();
                    var dataOne = Riddha.ko.global.arrayMap(ko.toJS(result.Data.DialogueDetails), AudioScoringDetailModel);
                    self.DialogueDetails(dataOne);
                }
            });

        //self.ModeOfButton("Update");
        self.ShowModal();
    }

    self.Select = function () {
        debugger;
        if (self.SelectedAudioScoring() == undefined || self.SelectedAudioScoring().IsScored() == false) {
            return Riddha.UI.Toast("Please select scored mocktest to edit.", 0);
        }

        Riddha.ajax.get(url + "/GetMockTestmasterDataToScoreByMockTestIdAndStudentId?packageId=" + self.SelectedAudioScoring().PackageId() + "&mockTestId=" + self.SelectedAudioScoring().MockTestId() + "&studentId=" + self.SelectedAudioScoring().StudentId())
            .done(function (result) {
                if (result.Status == 4) {
                    self.AudioScoring(new AudioScoringModel(result.Data.Master))
                    self.GetQuestionSets();
                    var dataOne = Riddha.ko.global.arrayMap(ko.toJS(result.Data.DialogueDetails), AudioScoringDetailModel);
                    self.DialogueDetails(dataOne);
                    //For feedBack
                    debugger;
                    self.TempFeedBackArray = ko.computed(function () { return result.Data.FeedBacks && result.Data.FeedBacks.split(",") || []; });
                    for (var i = 0; i < self.TempFeedBackArray().length; i++) {
                        CheckedFeeedBackOnEdit(self.TempFeedBackArray[i]);
                    }
                }
            });

        self.ModeOfButton("Update");
        self.ShowModal();
    };


    self.View = function () {
        if (self.SelectedAudioScoring() == undefined || self.SelectedAudioScoring().IsScored() == false) {
            return Riddha.UI.Toast("Please select scored mocktest to view.", 0);
        }

        Riddha.ajax.get(url + "/GetMockTestmasterDataToScoreByMockTestIdAndStudentId?packageId=" + self.SelectedAudioScoring().PackageId() + "&mockTestId=" + self.SelectedAudioScoring().MockTestId() + "&studentId=" + self.SelectedAudioScoring().StudentId())
            .done(function (result) {
                if (result.Status == 4) {
                    self.AudioScoring(new AudioScoringModel(result.Data.Master))
                    self.GetQuestionSets();
                    var dataOne = Riddha.ko.global.arrayMap(ko.toJS(result.Data.DialogueDetails), AudioScoringDetailModel);
                    self.DialogueDetails(dataOne);
                    //For feedBack
                    debugger;
                    self.TempFeedBackArray = ko.computed(function () { return result.Data.FeedBacks && result.Data.FeedBacks.split(",") || []; });
                    for (var i = 0; i < self.TempFeedBackArray().length; i++) {
                        CheckedFeeedBackOnEdit(self.TempFeedBackArray[i]);
                    }
                }
            });

        self.ShowViewModal();
    }

    self.ShowViewModal = function () {
        $("#audioScoringViewModel").modal('show');
    }

    self.CloseViewModal = function () {
        $("#audioScoringViewModel").modal('hide');
        self.RefreshKendoGrid();
        self.Reset();
    }
    $("#audioScoringViewModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });


    function CheckedFeeedBackOnEdit(value) {

        var mapped = ko.utils.arrayFirst(self.FeedBackArray(), function (item) {
            return item.Id() == parseInt(value);
        });
        mapped.Checked(true);
    }


    self.GetQuestionSets = function () {
        Riddha.ajax.get(url + "/GetQuestionSetList?mockTestId=" + self.AudioScoring().MockTestId())
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownTypeModel);
                    self.QuestionSets(data);
                }
            });
    }

    self.GetAllDataToScore = function () {
        debugger;
        if (self.DialogueId() == 0 || self.DialogueId() == undefined) {
            return;
        }

        Riddha.ajax.get(url + "/GetMockTestToScoreByMockTestIdAndStudentId?packageId=" + self.SelectedAudioScoring().PackageId() +"&mockTestId=" + self.SelectedAudioScoring().MockTestId() + "&studentId=" + self.SelectedAudioScoring().StudentId() + "&dialogueId=" + self.DialogueId())
            .done(function (result) {
                if (result.Status == 4) {
                    debugger;
                    self.AudioScoring(new AudioScoringModel(result.Data.Master))
                    self.GetQuestionSets();

                    var dataOne = Riddha.ko.global.arrayMap(ko.toJS(result.Data.DialogueDetails), AudioScoringDetailModel);
                    self.DialogueDetails(dataOne);

                    //For feedBack
                    debugger;
                    self.TempFeedBackArray = ko.computed(function () { return result.Data.FeedBacks && result.Data.FeedBacks.split(",") || []; });
                    for (var i = 0; i < self.TempFeedBackArray().length; i++) {
                        CheckedFeeedBackOnEdit(self.TempFeedBackArray()[i]);
                    }
                }
            });
    }


}