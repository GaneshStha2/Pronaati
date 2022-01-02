/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Company.Model.js" />


function writingTypeOneController() {
    var self = this;
    var url = "/Api/WritingTypeOneApi";
    self.WritingTypeOne = ko.observable(new WritingTypeOneModel());
    self.WritingTypeOnes = ko.observableArray([]);
    self.SelectedWritingTypeOne = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Writing Type One",
        target: "#writingTypeOneKendoGrid",
        url: url + "/GetWritingTypeOneKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false},
            { field: 'Question', title: "Question", width: 200, filterable: false },
            { field: 'TotalTime', title: "Total Time (Sec)", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedWritingTypeOne(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#writingTypeOneKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.WritingTypeOne().Title() == "") {
            Riddha.UI.Toast("Title can't be blank", 0);
            return;
        }
        if (self.WritingTypeOne().Question() == "") {
            Riddha.UI.Toast("Question can't be blank", 0);
            return;
        }
        if (self.WritingTypeOne().TotalTime() == "") {
            Riddha.UI.Toast("Total Time can't be blank", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.WritingTypeOne()))
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
            Riddha.ajax.put(url, ko.toJS(self.WritingTypeOne()))
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
        self.WritingTypeOne(new WritingTypeOneModel({ Id: self.WritingTypeOne().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedWritingTypeOne() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.WritingTypeOne(new WritingTypeOneModel(ko.toJS(self.SelectedWritingTypeOne())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedWritingTypeOne() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedWritingTypeOne().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedWritingTypeOne(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#WritingTypeOneCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#WritingTypeOneCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#WritingTypeOneCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}

function writingTypeTwoController() {
    var self = this;
    var url = "/Api/WritingTypeTwoApi";
    self.WritingTypeTwo = ko.observable(new WritingTypeTwoModel());
    self.WritingTypeTwos = ko.observableArray([]);
    self.SelectedWritingTypeTwo = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Writing Type Two",
        target: "#writingTypeTwoKendoGrid",
        url: url + "/GetWritingTypeTwoKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false},
            { field: 'Question', title: "Question", width: 200, filterable: false },
            { field: 'TotalTime', title: "Total Time (Sec)", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedWritingTypeTwo(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#writingTypeTwoKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.WritingTypeTwo().Title() == "") {
            Riddha.UI.Toast("Title can't be blank", 0);
            return;
        }
        if (self.WritingTypeTwo().Question() == "") {
            Riddha.UI.Toast("Question field can't be blank", 0);
            return;
        }
        if (self.WritingTypeTwo().TotalTime() == "") {
            Riddha.UI.Toast("Total Time can't be blank", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.WritingTypeTwo()))
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
            Riddha.ajax.put(url, ko.toJS(self.WritingTypeTwo()))
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
        self.WritingTypeTwo(new WritingTypeTwoModel({ Id: self.WritingTypeTwo().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedWritingTypeTwo() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.WritingTypeTwo(new WritingTypeTwoModel(ko.toJS(self.SelectedWritingTypeTwo())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedWritingTypeTwo() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedWritingTypeTwo().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedWritingTypeTwo(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#WritingTypeTwoCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#WritingTypeTwoCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#WritingTypeTwoCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}


function mockTestWritingEvalutaionController() {
    var self = this;
    var url = "/Api/MockTestWritingEvaluationApi";
    self.MockTestWritingEvalutaion = ko.observable(new MockTestWritingEvaluationModel());
    self.MockTestWritingEvalutaions = ko.observableArray([]);
    self.SelectedMockTestWritingEvalutaion = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Writing Evaluation ",
        target: "#WritingEvaluationKendoGrid",
        url: url + "/GetMockTestWritingEvaluationKendoGrid",
        height: 800,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'PackageName', title: "Package Name", width: 225, filterable: true },
            { field: 'QuestionSetName', title: "Question Set Name", width: 200, filterable: true },
            { field: 'StudentName', title: "Student Name", width: 200, filterable: true },
            { field: 'TestDate', title: "Test Date", width: 200, filterable: true },
            { field: 'QuestionText', title: "Question Text", width: 200, filterable: false },
            { field: 'AnswerText', title: "Answer Text", width: 200, filterable: false },
            { field: 'ObtainedMarks', title: "Marks Obtained", width: 200, filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedMockTestWritingEvalutaion(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#WritingEvaluationKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.MockTestWritingEvalutaion().ObtainedMarks() == "") {
            Riddha.UI.Toast("MArks Obtained can't be blank", 0);
            return;
        }
       
        
        if (self.ModeOfButton() == 'Update') {
            Riddha.ajax.put(url, ko.toJS(self.MockTestWritingEvalutaion()))
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
        self.MockTestWritingEvalutaion(new MockTestWritingEvaluationModel({ Id: self.MockTestWritingEvalutaion().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedMockTestWritingEvalutaion() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.MockTestWritingEvalutaion(new MockTestWritingEvaluationModel(ko.toJS(self.SelectedMockTestWritingEvalutaion())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedMockTestWritingEvalutaion() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedMockTestWritingEvalutaion().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedWritingTypeTwo(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#WritingEvaluationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#WritingEvaluationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#WritingEvaluationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}