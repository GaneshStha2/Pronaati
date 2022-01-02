/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Company.Model.js" />
/// <reference path="riddha.script.questionset.model.js" />

function questionSetController() {
    var self = this;
    var url = "/Api/QuestionSetApi";
    self.QuestionSet = ko.observable(new QuestionSetModel());
    self.QuestionSets = ko.observableArray([]);
    self.SelectedQuestionSet = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.QuestionsList = ko.observableArray([]);
    self.AddedQuestionsList = ko.observableArray([]);

    self.Question = ko.observable(new QuestionSetDropDownModel());

    



    //Add to List Function   

    self.AddNewQuestion = function (model) {
        debugger;
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.AddedQuestionsList(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            Riddha.UI.Toast("Question has been Already Selected", 0);

        } else {
            var data = ko.utils.arrayFirst(self.QuestionsList(), function (item) {
                return model.Id() == item.Id();
            });

            self.AddedQuestionsList.push(data);
            self.Question(new QuestionSetDropDownModel());
        }
    }


   
    self.KendoGridOptions = {
        title: "Question Set",
        target: "#questionSetKendoGrid",
        url: url + "/GetQuestionSetKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'QuestionSetCode', title: "Dialogue Code", width: 200 },
            { field: 'QuestionSetName', title: "Dialogue Name", width: 225 },
        ],
        SelectedItem: function (item) {
            self.SelectedQuestionSet(new QuestionSetModel(item));
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#questionSetKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        debugger;
        if (self.QuestionSet().QuestionSetName() == "") {
            Riddha.UI.Toast("Please enter Dialogue Name", 0);
            return;
        }
        if (self.QuestionSet().QuestionSetCode() == "") {
            return Riddha.UI.Toast("Please enter Dialogue Code", 0);
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, self.QuestionSet())
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                        self.SelectedQuestionSet(undefined);
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
                        self.SelectedQuestionSet(undefined);
                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }


    };

    self.Reset = function () {
        self.QuestionSet(new QuestionSetModel());
        self.ModeOfButton("Create");
        self.AddedQuestionsList([]);

    };

    self.Select = function () {
        if (self.SelectedQuestionSet() == undefined || self.SelectedQuestionSet().Id() == 0) {
            Riddha.UI.Toast("Please select a row to edit.", 0);
            return;
        }
        Riddha.ajax.get(url + "/GetQuestionSet?Id=" + self.SelectedQuestionSet().Id(), null)
            .done(function (result) {
               
                self.QuestionSet(new QuestionSetModel(ko.toJS(result.Data)));
            });
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedQuestionSet() == undefined || self.SelectedQuestionSet().Id() == 0) {
            Riddha.UI.Toast("Please select a row to delete .", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedQuestionSet().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedQuestionSet(new QuestionSetModel());
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });

        })

    };

    $("#QuestionSetCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#QuestionSetCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#QuestionSetCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    self.CheckUniqueCode = function (model) {
        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.QuestionSet().QuestionSetCode() + "&Id=" + self.QuestionSet().Id(), null)
            .done(function (result) {
                if (result == false) {
                    debugger
                    self.QuestionSet().QuestionSetCode('');
                    Riddha.UI.Toast("Question Set Code has been taken", 0);
                    return;
                }
            });
    }


}