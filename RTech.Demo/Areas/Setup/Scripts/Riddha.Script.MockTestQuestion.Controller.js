/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.mocktestquestion.model.js" />


function MockTestQusetionController() {
    var self = this;
    var url = "/Api/MockTestQuestionApi";

    self.MockTestQustionMaster = ko.observable(new MocktTestQuestionMasterModel());
    self.MockTestQustionDetails = ko.observableArray([]);
    self.MockTestQustionDetail = ko.observable(new MockTestQuestionDetailModel());
    self.SelectedMockTestQuestion = ko.observable();

    self.FileTypes = ko.observableArray([
        { Id: 0, Name: 'Video' },
        { Id: 1, Name: 'Audio' },
        { Id: 2, Name: 'PDF' },
    ]);
    self.ModeOfButton = ko.observable('Create');

    ///For Language Type Dropdown
    self.LanguageTypes = ko.observableArray([]);
    getLanguageTypes();

    function getLanguageTypes() {
        Riddha.ajax.get(url + '/GetLanguageTypeList', null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
                self.LanguageTypes(data);
            });
    };


    //For Dialogues

    self.Dialogues = ko.observableArray([]);
    getDialogues();
    function getDialogues() {
        Riddha.ajax.get(url + '/GetDialogues', null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
                self.Dialogues(data);
            });
    };


    //for segments

    self.Segments = ko.observableArray([]);

    self.GetSegmentsFromLanguageId = function () {

        Riddha.ajax.get(url + '/GetSegmentsFromLanguageId?languageId=' + self.MockTestQustionMaster().LanguageTypeId(), null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
                self.Segments(data);
            });

        if (self.SelectedMockTestQuestion() != undefined) {
            self.MockTestQustionMaster().SegmentId(self.SelectedMockTestQuestion().SegmentId());
        }
    }

    ///End DropDown

    self.CheckUniqueCode = function (model) {

        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.MockTestQustionMaster().Code() + "&Id=" + self.MockTestQustionMaster().Id(), null)
            .done(function (result) {
                if (result == false) {
                    self.MockTestQustionMaster().Code('');
                    Riddha.UI.Toast("This Mocktest question Code has been taken", 0);
                    return;
                }
            });
    }

    self.CreateUpdate = function () {

        self.CheckUniqueCode();
        if (self.MockTestQustionMaster().Code() == "") {
            Riddha.UI.Toast("Please enter Code", 0);
            return;
        }
        if (self.MockTestQustionMaster().Title() == "") {
            Riddha.UI.Toast("Please enter Title", 0);
            return;
        }

        if (self.MockTestQustionMaster().LanguageTypeId() == undefined || self.MockTestQustionMaster().LanguageTypeId() == 0) {
            Riddha.UI.Toast("Please Select Language Type", 0)
            return;
        }

        if (self.MockTestQustionMaster().Description() == "") {
            Riddha.UI.Toast("Please enter Description", 0);
            return;
        }

        if (self.MockTestQustionDetails().length <= 0) {
            return Riddha.UI.Toast("Please enter atleast one quesion", 0);
        }

        var data = {
            Master: self.MockTestQustionMaster(),
            Details: self.MockTestQustionDetails(),
        };

        if (self.ModeOfButton() == 'Create') {

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


    }


    self.Delete = function (model) {
        if (self.SelectedMockTestQuestion() == undefined || self.SelectedMockTestQuestion().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedMockTestQuestion().Id(), null)
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
        title: "Mocktest Qusetion",
        target: "#MocktestQusetion",
        url: "/Api/MockTestQuestionApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: false,
        group: true,
        groupParam: { field: "Language" },
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Question Code", filterable: true },
            { field: 'Title', title: "Question Title", filterable: true },
            { field: 'Language', title: "Language", filterable: true },
            { field: 'CreatedDate', title: "Created Date", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedMockTestQuestion(new MocktTestQuestionMasterModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedMockTestQuestion(new MocktTestQuestionMasterModel());
        $("#MocktestQusetion").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.MockTestQustionMaster(new MocktTestQuestionMasterModel());
        self.MockTestQustionDetail(new MockTestQuestionDetailModel());
        self.MockTestQustionDetails([]);
        self.ResetDetail();
        self.ModeOfButton('Create');
    }

    self.ResetDetail = function () {
        self.FileModeOfButton('Add');
        self.MockTestQustionDetail(new MockTestQuestionDetailModel);
    }

    self.ShowModal = function () {
        $("#mockTestQustionSetupModel").modal('show');
    };

    self.ShowViewModal = function () {
        $("#mockTestQustionViewModel").modal('show');
    };

    self.CloseViewModal = function () {
        $("#mockTestQustionViewModel").modal('hide');
        self.Reset();
        self.FileModeOfButton("Add");
    }
    $("#mockTestQustionViewModel").on('hidden.bs.modal', function () {

        self.Reset();
    });

    $("#mockTestQustionSetupModel").on('hidden.bs.modal', function () {

        self.ModeOfButton("Create");
        self.Reset();
    });

    self.CloseModal = function () {
        $("#mockTestQustionSetupModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
        self.FileModeOfButton("Add");
    }


    self.Select = function (model) {

        if (self.SelectedMockTestQuestion() == undefined || self.SelectedMockTestQuestion().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetMockTestQuestion?masterId=" + self.SelectedMockTestQuestion().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(result.Data.Details, MockTestQuestionDetailModel);
                    self.MockTestQustionDetails(data);
                    self.MockTestQustionMaster(new MocktTestQuestionMasterModel(result.Data.Master));
                    self.GetSegmentsFromLanguageId();

                }
            });
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    self.View = function (model) {

        if (self.SelectedMockTestQuestion() == undefined || self.SelectedMockTestQuestion().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetMockTestQuestion?masterId=" + self.SelectedMockTestQuestion().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(result.Data.Details, MockTestQuestionDetailModel);
                    self.MockTestQustionDetails(data);
                    self.MockTestQustionMaster(new MocktTestQuestionMasterModel(result.Data.Master));
                }
            });
        self.ShowViewModal();
    };

    self.FileModeOfButton = ko.observable('Add');
    self.SelectedFile = ko.observable(new MockTestQuestionDetailModel());
    self.EditSelectedFile = function (model) {
        self.SelectedFile(model);
        self.MockTestQustionDetail(new MockTestQuestionDetailModel(ko.toJS(model)));
        self.FileModeOfButton('Update');
    }

    self.AddNewFile = function (model) {

        if (model.Description() == "") {
            return Riddha.UI.Toast("Please enter description", 0);
        }
        if (model.QuestionAudioUrl() == "") {
            return Riddha.UI.Toast("Please enter Question Audio", 0);
        }
        if (model.AnswerAudioUrl() == "") {
            return Riddha.UI.Toast("Please enter Answer Audio", 0);
        }
        if (model.SegmentId() == undefined || model.SegmentId() == 0) {
            Riddha.UI.Toast("Please Select Segment", 0);
            return;
        }
 
        if (model.Order() == undefined || model.Order() == 0) {
            Riddha.UI.Toast("Please Select Segment", 0);
            return;
        }
        var mapped = ko.utils.arrayFirst(self.Segments(), function (item) {

            return item.Id() == model.SegmentId();

        });
        if (self.FileModeOfButton() == 'Add') {
            if (model.SegmentId() != undefined) {
                model.SegmentName(mapped.Name());
            }
            self.MockTestQustionDetails.push(new MockTestQuestionDetailModel(ko.toJS(model)));
        }
        else {
            if (model.SegmentId() != undefined) {
                model.SegmentName(mapped.Name());
            }
            self.MockTestQustionDetails.replace(self.SelectedFile(), new MockTestQuestionDetailModel(ko.toJS(model)));
            self.FileModeOfButton('Add');
        }
        self.MockTestQustionDetail(new MockTestQuestionDetailModel());
    }

    self.RemoveSelectedFile = function (model) {
        self.MockTestQustionDetails.remove(model)
    }



    var Preloaded = false;
    self.OpenFileManager = function () {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.MockTestQustionDetail().QuestionAudioUrl(data.URL);
            $("#fileManager").modal("hide");
        });
    }



    self.OpenFileManagerForAnswer = function () {
        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.MockTestQustionDetail().AnswerAudioUrl(data.URL);
            $("#fileManager").modal("hide");
        });
    }

    self.CloseFileManager = function () {
        Preloaded = true;
        $("#fileManager").modal('hide');
    }
}