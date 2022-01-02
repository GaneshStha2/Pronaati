/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Company.Model.js" />


function readingTypeOneController() {
    var self = this;
    var url = "/Api/ReadingTypeOneApi";
    self.ReadingTypeOne = ko.observable(new ReadingTypeOneModel());
    self.ReadingTypeOnes = ko.observableArray([]);
    self.SelectedReadingTypeOne = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Reading Type One",
        target: "#readingTypeOneKendoGrid",
        url: url + "/GetReadingTypeOneKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'ReadingText', title: "Reading Text", width: 200, filterable: false },
            { field: 'Question', title: "Question", width: 200, filterable: false },
            { field: 'Response1', title: "Response1", width: 200, filterable: false },
            { field: 'Response2', title: "Response2", width: 200, filterable: false },
            { field: 'Response3', title: "Response3", width: 200, filterable: false },
            { field: 'Response4', title: "Response4", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedReadingTypeOne(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#readingTypeOneKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ReadingTypeOne().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank", 0);
            return;
        }
        if (self.ReadingTypeOne().ReadingText() == "") {
            Riddha.UI.Toast("Reading Text field can't be blank", 0);
            return;
        }
        if (self.ReadingTypeOne().Question() == "") {
            Riddha.UI.Toast("Question field can't be blank", 0);
            return;
        }
        if (self.ReadingTypeOne().Response1() == "" || self.ReadingTypeOne().Response2() == "" || self.ReadingTypeOne().Response3() == "" || self.ReadingTypeOne().Response4() == "") {
            Riddha.UI.Toast("Response field can't be blank", 0);
            return;
        }
        if (self.ReadingTypeOne().Response1IsCorrect() == false && self.ReadingTypeOne().Response2IsCorrect() == false && self.ReadingTypeOne().Response3IsCorrect() == false && self.ReadingTypeOne().Response4IsCorrect() == false && self.ReadingTypeOne().Response5IsCorrect() == false) {
            Riddha.UI.Toast("Please provide atleast one correct answer", 0);
            return;
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ReadingTypeOne()))
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
            Riddha.ajax.put(url, ko.toJS(self.ReadingTypeOne()))
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
        self.ReadingTypeOne(new ReadingTypeOneModel({ Id: self.ReadingTypeOne().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedReadingTypeOne() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }

        Riddha.ajax.get(url + "/Get?Id=" + self.SelectedReadingTypeOne().Id, null)
            .done(function (result) {
                debugger;
                if (result.Status == 4) {
                    self.ReadingTypeOne(new ReadingTypeOneModel(ko.toJS(result.Data)));
                    self.ShowModal();
                    self.ModeOfButton('Update');

                };

            });
    };

    self.Delete = function () {
        if (self.SelectedReadingTypeOne() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedReadingTypeOne().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedReadingTypeOne(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ReadingTypeOneCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ReadingTypeOneCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ReadingTypeOneCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}


function readingTypeTwoController() {
    var self = this;
    var url = "/Api/ReadingTypeTwoApi";
    self.ReadingTypeTwo = ko.observable(new ReadingTypeTwoModel());
    self.ReadingTypeTwos = ko.observableArray([]);
    self.SelectedReadingTypeTwo = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Reading Type Two",
        target: "#readingTypeTwoKendoGrid",
        url: url + "/GetReadingTypeTwoKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'ReadingText', title: "Reading Text", width: 200, filterable: false },
            { field: 'Question', title: "Question", width: 200, filterable: false },
            { field: 'Response1', title: "Response1", width: 200, filterable: false },
            { field: 'Response2', title: "Response2", width: 200, filterable: false },
            { field: 'Response3', title: "Response3", width: 200, filterable: false },
            { field: 'Response4', title: "Response4", width: 200, filterable: false },
            { field: 'Response5', title: "Response5", width: 200, filterable: false },
            { field: 'Response6', title: "Response6", width: 200, filterable: false },
            { field: 'Response7', title: "Response7", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", width:100, filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedReadingTypeTwo(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#readingTypeTwoKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        debugger;
        if (self.ReadingTypeTwo().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank", 0);
            return;
        }
        if (self.ReadingTypeTwo().Question() == "") {
            Riddha.UI.Toast("Question field can't be blank", 0);
            return;
        }
        if (self.ReadingTypeTwo().ReadingText() == "") {
            Riddha.UI.Toast("Reading Text field can't be blank !", 0);
            return;
        }
        //if (self.ReadingTypeTwo().Response1() == "" || self.ReadingTypeTwo().Response2() == "" || self.ReadingTypeTwo().Response3() == "" || self.ReadingTypeTwo().Response4() == "" || self.ReadingTypeTwo().Response5() == "") {
        //    Riddha.UI.Toast("Response field can't be blank", 0);
        //    return;
        //}
        if (self.ReadingTypeTwo().Response1IsCorrect() == false && self.ReadingTypeTwo().Response2IsCorrect() == false && self.ReadingTypeTwo().Response3IsCorrect() == false && self.ReadingTypeTwo().Response4IsCorrect() == false && self.ReadingTypeTwo().Response5IsCorrect() == false && self.ReadingTypeTwo().Response6IsCorrect() == false && self.ReadingTypeTwo().Response7IsCorrect() == false) {
            Riddha.UI.Toast("Please provide atleast one correct answer", 0);
            return;
        }
        if (self.ReadingTypeTwo().Response1() == "" && self.ReadingTypeTwo().Response2() == "" && self.ReadingTypeTwo().Response3() == "" && self.ReadingTypeTwo().Response4() == "" && self.ReadingTypeTwo().Response5() == "" && self.ReadingTypeTwo().Response6() == "" && self.ReadingTypeTwo().Response7 == "") {
            Riddha.UI.Toast("Please provide at least one response !", 0);
            return;
        }
        if ((self.ReadingTypeTwo().Response1() == "" && self.ReadingTypeTwo().Response1IsCorrect() == true) || (self.ReadingTypeTwo().Response2() == "" && self.ReadingTypeTwo().Response2IsCorrect() == true) || (self.ReadingTypeTwo().Response3() == "" && self.ReadingTypeTwo().Response3IsCorrect() == true) || (self.ReadingTypeTwo().Response4() == "" && self.ReadingTypeTwo().Response4IsCorrect() == true) || (self.ReadingTypeTwo().Response5() == "" && self.ReadingTypeTwo().Response5IsCorrect() == true) || (self.ReadingTypeTwo().Response6() == "" && self.ReadingTypeTwo().Response6IsCorrect() == true) || (self.ReadingTypeTwo().Response7() == "" && self.ReadingTypeTwo().Response7IsCorrect() == true)) {
            Riddha.UI.Toast("Response can't be empty for the correct marked response !", 0);
            return;
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ReadingTypeTwo()))
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
            Riddha.ajax.put(url, ko.toJS(self.ReadingTypeTwo()))
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
        self.ReadingTypeTwo(new ReadingTypeTwoModel({ Id: self.ReadingTypeTwo().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedReadingTypeTwo() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        Riddha.ajax.get(url + "/Get?Id=" + self.SelectedReadingTypeTwo().Id, null)
            .done(function (result) {
                debugger;
                if (result.Status == 4) {
                    self.ReadingTypeTwo(new ReadingTypeTwoModel(ko.toJS(result.Data)));
                    self.ShowModal();
                    self.ModeOfButton('Update');

                };

            });


    };

    self.Delete = function () {
        if (self.SelectedReadingTypeTwo() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedReadingTypeTwo().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedReadingTypeTwo(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ReadingTypeTwoCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ReadingTypeTwoCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ReadingTypeTwoCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}

function readingTypeThreeController() {
    var self = this;
    var url = "/Api/ReadingTypeThreeApi";
    self.ReadingTypeThree = ko.observable(new ReadingTypeThreeModel());
    self.ReadingTypeThrees = ko.observableArray([]);
    self.SelectedReadingTypeThree = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Reading Type Three",
        target: "#readingTypeThreeKendoGrid",
        url: url + "/GetReadingTypeThreeKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false},
            { field: 'QuestionSource1', title: "Question Source 1", width: 200, filterable: false },
            { field: 'QuestionSource2', title: "Question Source 2", width: 200, filterable: false },
            { field: 'QuestionSource3', title: "Question Source 3", width: 200, filterable: false },
            { field: 'QuestionSource4', title: "Question Source 4", width: 200, filterable: false },
            { field: 'QuestionSource5', title: "Question Source 5", width: 200, filterable: false },
            { field: 'QuestionSource6', title: "Question Source 6", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedReadingTypeThree(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#readingTypeThreeKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ReadingTypeThree().Title() == "") {
            Riddha.UI.Toast("Title can't be blank", 0);
            return;
        }
        if (self.ReadingTypeThree().QuestionSource1() == "" && self.ReadingTypeThree().QuestionSource2() == "" && self.ReadingTypeThree().QuestionSource3() == "" && self.ReadingTypeThree().QuestionSource4() == "" && self.ReadingTypeThree().QuestionSource5() == "" && self.ReadingTypeThree().QuestionSource6() == "") {
            Riddha.UI.Toast("All QuestionSource fields can't be blank", 0);
            return;
        }
        if (self.ReadingTypeThree().QuestionSource1() == "" || self.ReadingTypeThree().QuestionSource2() == "" || self.ReadingTypeThree().QuestionSource3() == "" || self.ReadingTypeThree().QuestionSource4() == "") {
            Riddha.UI.Toast("First four question sources are mandatory !", 0);
            return;
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ReadingTypeThree()))
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
            Riddha.ajax.put(url, ko.toJS(self.ReadingTypeThree()))
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
        self.ReadingTypeThree(new ReadingTypeThreeModel({ Id: self.ReadingTypeThree().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedReadingTypeThree() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.ReadingTypeThree(new ReadingTypeThreeModel(ko.toJS(self.SelectedReadingTypeThree())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedReadingTypeThree() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedReadingTypeThree().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedReadingTypeThree(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ReadingTypeThreeCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ReadingTypeThreeCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ReadingTypeThreeCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}

function readingTypeFourController() {
    var self = this;
    var url = "/Api/ReadingTypeFourApi";
    self.ReadingTypeFour = ko.observable(new ReadingTypeFourModel());
    self.ReadingTypeFours = ko.observableArray([]);
    self.SelectedReadingTypeFour = ko.observable();
    self.ModeOfButton = ko.observable('Create');
    self.ReadingTypeOptionsFour = ko.observable(new ReadingTypeFourOptionsModel());

    self.KendoGridOptions = {
        title: "Reading Type Four",
        target: "#readingTypeFourKendoGrid",
        url: url + "/GetReadingTypeFourKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'QuestionText', title: "Question Text", width: 200, filterable: false },
            { field: 'Option1', title: "Option1", width: 200, filterable: false },
            { field: 'Option2', title: "Option2", width: 200, filterable: false },
            { field: 'Option3', title: "Option3", width: 200, filterable: false },
            { field: 'Option4', title: "Option4", width: 200, filterable: false },
            { field: 'Option5', title: "Option5", width: 200, filterable: false },
            { field: 'Option6', title: "Option6", width: 200, filterable: false },
            { field: 'Option7', title: "Option7", width: 200, filterable: false },
            { field: 'Option8', title: "Option8", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", width: 100, filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedReadingTypeFour(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#readingTypeFourKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ReadingTypeFour().Title() == "") {
            Riddha.UI.Toast("Title can't be empty !", 0);
            return;
        }
        if (self.ReadingTypeFour().QuestionText() == "") {
            Riddha.UI.Toast("Question Text can't be empty !", 0);
            return;
        }
        if (self.ReadingTypeFour().Option1() == "" && self.ReadingTypeFour().Option2() == "" && self.ReadingTypeFour().Option3() == "" && self.ReadingTypeFour().Option4() == "" && self.ReadingTypeFour().Option5() == "" && self.ReadingTypeFour().Option6() == "" && self.ReadingTypeFour().Option7() == "" && self.ReadingTypeFour().Option8() == "") {
            Riddha.UI.Toast("Options field can't be blank !", 0);
            return;
        }


        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ReadingTypeFour()))
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
            Riddha.ajax.put(url, ko.toJS(self.ReadingTypeFour()))
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
        self.ReadingTypeFour(new ReadingTypeFourModel({ Id: self.ReadingTypeFour().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedReadingTypeFour() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.ReadingTypeFour(new ReadingTypeFourModel(ko.toJS(self.SelectedReadingTypeFour())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedReadingTypeFour() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedReadingTypeFour().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedReadingTypeFour(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    self.CreateUpdateOption = function () {
        debugger;
        var opCount = 0;
        if (self.ReadingTypeFour().Option1() == '') {
            self.ReadingTypeFour().Option1(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 0;
        }
        else if (self.ReadingTypeFour().Option2() == '') {
            self.ReadingTypeFour().Option2(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 1;
        }
        else if (self.ReadingTypeFour().Option3() == '') {
            self.ReadingTypeFour().Option3(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 2;
        }
        else if (self.ReadingTypeFour().Option4() == '') {
            self.ReadingTypeFour().Option4(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 3;
        }
        else if (self.ReadingTypeFour().Option5() == '') {
            self.ReadingTypeFour().Option5(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 4;
        }
        else if (self.ReadingTypeFour().Option6() == '') {
            self.ReadingTypeFour().Option6(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 5;
        }
        else if (self.ReadingTypeFour().Option7() == '') {
            self.ReadingTypeFour().Option7(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 6;
        }
        else if (self.ReadingTypeFour().Option8() == '') {
            self.ReadingTypeFour().Option8(self.ReadingTypeOptionsFour().CorrectAnswer());
            opCount = 7;
        }
        else {
            Riddha.UI.Toast("You have already provided 8 Correct Answers !", 0);
            return;
        }
        //self.ReadingTypeFour().Dropdowns.push(new ReadingTypeFiveDropdownModel(ko.toJS(self.ReadingTypeDropdownFive)));

        var curentText = self.ReadingTypeFour().QuestionText();
        curentText += "{" + opCount + "}";
        self.ReadingTypeFour().QuestionText(curentText);

        $("#ReadingTypeFourCreationOptionsModel").modal('hide');

    }
    //self.TypeFourOptionsModeOfButton = ko.observable('Create');
    self.ResetOption = function () {
        self.ReadingTypeOptionsFour(new ReadingTypeFourOptionsModel());
        //self.ModeOfButton("Create");
    };
    self.AddOptionsFour = function () {
        //self.OptionsModeOfButton('Create');
        self.ReadingTypeOptionsFour(new ReadingTypeFourOptionsModel());
        $("#ReadingTypeFourCreationOptionsModel").modal('show');

    };

    self.CloseOptionModal = function () {
        $("#ReadingTypeFourCreationOptionsModel").modal('hide');
        self.ResetOption();
        //self.ModeOfButton("Create");
    }


    $("#ReadingTypeFourCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ReadingTypeFourCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ReadingTypeFourCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
}

function readingTypeFiveController() {
    var self = this;
    var url = "/Api/ReadingTypeFiveApi";
    self.ReadingTypeFive = ko.observable(new ReadingTypeFiveModel());
    self.ReadingTypeDropdownFive = ko.observable(new ReadingTypeFiveDropdownModel());
    self.ReadingTypeFives = ko.observableArray([]);
    self.SelectedReadingTypeFive = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.KendoGridOptions = {
        title: "Reading Type Five",
        target: "#readingTypeFiveKendoGrid",
        url: url + "/GetReadingTypeFiveKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'QuestionText', title: "Question Text", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },


        ],
        SelectedItem: function (item) {
            self.SelectedReadingTypeFive(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#readingTypeFiveKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ReadingTypeFive().Title() == "") {
            Riddha.UI.Toast("Please enter Title.", 0);
            return;
        }
        if (self.ReadingTypeFive().QuestionText() == "") {
            Riddha.UI.Toast("Please enter Question Text !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ReadingTypeFive()))
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
            Riddha.ajax.put(url, ko.toJS(self.ReadingTypeFive()))
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
    self.CreateUpdateDropdown = function () {
        switch (self.OptionsModeOfButton()) {
            case "Create":
                self.ReadingTypeFive().Dropdowns.push(new ReadingTypeFiveDropdownModel(ko.toJS(self.ReadingTypeDropdownFive)));

                var curentText = self.ReadingTypeFive().QuestionText();
                curentText += "{" + (self.ReadingTypeFive().Dropdowns().length - 1) + "}";
                self.ReadingTypeFive().QuestionText(curentText);
                break;
            case "Update":
                self.ReadingTypeFive().Dropdowns.replace(self.SelectedOptions(), new ReadingTypeFiveDropdownModel(ko.toJS(self.ReadingTypeDropdownFive)));
                break;
            default:
        }
        $("#ReadingTypeFiveCreationDropdownModel").modal('hide');

    }
    self.SelectedOptions = ko.observable({});
    self.OptionsModeOfButton = ko.observable('Create');
    self.SelectOptions = function (model) {
        self.SelectedOptions(model);
        self.ReadingTypeDropdownFive(new ReadingTypeFiveDropdownModel(ko.toJS(model)));
        $("#ReadingTypeFiveCreationDropdownModel").modal('show');
        self.OptionsModeOfButton('Update');
    };

    self.AddOptions = function () {
        //self.ReadingTypeFive().Dropdowns;
        self.OptionsModeOfButton('Create');
        self.ReadingTypeDropdownFive(new ReadingTypeFiveDropdownModel());
        $("#ReadingTypeFiveCreationDropdownModel").modal('show');

    };
    self.Reset = function () {
        self.ReadingTypeFive(new ReadingTypeFiveModel({ Id: self.ReadingTypeFive().Id() }));
        self.ModeOfButton("Create");
    };

    self.ResetDropDown = function () {
        self.ReadingTypeDropdownFive(new ReadingTypeFiveDropdownModel({ Id: self.ReadingTypeDropdownFive().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedReadingTypeFive() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        debugger;
        self.ReadingTypeFive(new ReadingTypeFiveModel(ko.toJS(self.SelectedReadingTypeFive())));

        Riddha.ajax.get("/Api/ReadingTypeFiveApi/getOptionsDropDown?readingTypeFiveId=" + self.SelectedReadingTypeFive().Id)
            .done(function (result) {
                if (result.Status == 4) {
                    debugger;
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), ReadingTypeFiveDropdownModel);
                    //self.ReadingTypeFive().DropDowns(data);
                    ko.utils.arrayForEach(data, function (data) {

                        self.ReadingTypeFive().Dropdowns.push(new ReadingTypeFiveDropdownModel(ko.toJS(data)));

                    });


                }
            })
        //GET DROPDOWNS LIST 
        //MAP ARRAY TO THE LIST
        // var a=Riddha.ko.global.arrayMap(item.Dropdowns, ReadingTypeFiveDropdownModel)
        //self.ReadingTypeFive().DropDowns(a)
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedReadingTypeFive() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedReadingTypeFive().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedReadingTypeFive(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    $("#ReadingTypeFiveCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ReadingTypeFiveCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ReadingTypeFiveCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    self.CloseDropdownModal = function () {
        $("#ReadingTypeFiveCreationDropdownModel").modal('hide');
        self.ResetDropDown();
        //self.ModeOfButton("Create");
    }
}

