/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.NaatiPackage.Model.js" />


function NaatiMockTestController() {
    var self = this;
    var url = "/Api/NaatiMockTestApi";

    self.NaatiMockTest = ko.observable(new NaatiMockTestModel());
    self.MockTestDetail = ko.observable(new NaatiMockTestDetailModel());
    self.MockTestDetails = ko.observableArray([]);
    self.QuestionSets = ko.observableArray([]);

    self.SelectedNaatiMockTest = ko.observable();

    self.ModeOfButton = ko.observable('Create');

    ///For Language Type Dropdown
    self.LanguageTypes = ko.observableArray([]);

    getLanguageTypes();

    function getLanguageTypes() {
        Riddha.ajax.get(url + '/GetLanguageTypeList', null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownTypeModel);
                self.LanguageTypes(data);
            });
    };
    ///End DropDown


    self.GetQuestionSets = function () {
        Riddha.ajax.get(url + "/GetQuestionSetList?languageId=" + self.NaatiMockTest().LanguageTypeId() + "&segmentId=" + self.MockTestDetail().SegmentId())
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownTypeModel);
                    self.QuestionSets(data);
                }
            });
    };

    //for segments

    self.Dialogues = ko.observableArray([]);

    //self.GetSegmentsFromLanguageId = function () {
    //    Riddha.ajax.get(url + '/GetSegmentsFromLanguageId?languageId=' + self.NaatiMockTest().LanguageTypeId(), null)
    //        .done(function (result) {
    //            var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
    //            self.Segments(data);
    //        });
    //}

    self.GetDialogueFromLanguageId = function () {
        Riddha.ajax.get(url + '/GetQuestionSetList?languageId=' + self.NaatiMockTest().LanguageTypeId(), null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
                self.Dialogues(data);
            });
    };



    self.AddNewQuestion = function (model) {

        if (model.QuestionSetId() == undefined) {
            return Riddha.UI.Toast("Please select Dialogue !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.MockTestDetails(), function (item) {
            return model.QuestionSetId() == item.QuestionSetId()
        });
        if (mapped) {
            return Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            self.MockTestDetails.push(new NaatiMockTestDetailModel(ko.toJS(model)));
            self.MockTestDetail(new NaatiMockTestDetailModel());
        }
    };


    self.RemoveQuestion = function (model) {
        self.MockTestDetails.remove(model);
    };

    self.CheckUniqueCode = function (model) {

        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.NaatiMockTest().Code() + "&Id=" + self.NaatiMockTest().Id(), null)
            .done(function (result) {
                if (result == false) {
                 
                    self.NaatiMockTest().Code('');
                    Riddha.UI.Toast("Mocktest Code has been taken", 0);
                    return;
                }
            });
    }

    self.CreateUpdate = function () {

        self.CheckUniqueCode();
        if (self.NaatiMockTest().Code() == "") {
            Riddha.UI.Toast("Please enter Code", 0);
            return;
        }
        if (self.NaatiMockTest().Title() == "") {
            Riddha.UI.Toast("Please enter Name", 0);
            return;
        }
        if (self.NaatiMockTest().Duration() == 0) {
            Riddha.UI.Toast("Please enter Duration", 0);
            return;
        }
        if (self.NaatiMockTest().LanguageTypeId() == 0 || self.NaatiMockTest().LanguageTypeId() == undefined) {
            Riddha.UI.Toast("Please Select CourseType", 0)
            return;
        }

        if (self.NaatiMockTest().Price() == 0) {
            Riddha.UI.Toast("Please enter Price", 0);
            return;
        }

        var data = {
            NaatiMockTestVm: self.NaatiMockTest(),
            NaatiMockTestDetailVm: ko.toJS(self.MockTestDetails()),
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

        if (self.SelectedNaatiMockTest() == undefined || self.SelectedNaatiMockTest().Id() == 0) {
            return Riddha.UI.Toast("Please select a row to delete.", 0);
        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedNaatiMockTest().Id(), null)
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

    preloaded = false;
    self.OpenImageFileManager = function () {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            preloaded = true;
            var data = ko.toJS(result);
            self.NaatiMockTest().ImageURL(data.URL);
            $("#fileManager").modal('hide');
        });
    }


    self.KendoGridOptions = {
        title: "Naati MockTests",
        target: "#NaatiMockTests",
        url: "/Api/NaatiMockTestApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: false,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Course Code", filterable: true },
            { field: 'Name', title: "Name", filterable: true },
            { field: 'Duration', title: "Duration", filterable: false, },
            { field: 'Price', title: "Price", filterable: true },

        ],
        SelectedItem: function (item) {
            self.SelectedNaatiMockTest(new NaatiMockTestModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedNaatiMockTest(new NaatiMockTestModel());
        $("#NaatiMockTests").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.NaatiMockTest(new NaatiMockTestModel());
        self.MockTestDetail(new NaatiMockTestDetailModel());
        self.MockTestDetails([]);
        self.ModeOfButton("Create");
    }

    self.ShowModal = function () {
        $("#naatiMockTestCreationModel").modal('show');
    };

    self.ShowViewModal = function () {
        $("#naatiMockTestViewModel").modal('show');
    };

    $("#naatiMockTestCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    $("#naatiMockTestViewModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.CloseModal = function () {
        $("#naatiMockTestCreationModel").modal('hide');
        self.Reset();
    };

    self.CloseViewModal = function () {
        $("#naatiMockTestViewModel").modal('hide');
        self.Reset();
    }


    self.Select = function (model) {

        if (self.SelectedNaatiMockTest() == undefined || self.SelectedNaatiMockTest().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetNaatiMockTest?masterId=" + self.SelectedNaatiMockTest().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.NaatiMockTest(new NaatiMockTestModel(ko.toJS(result.Data.NaatiMockTestVm)));
                    //self.GetSegmentsFromLanguageId();
                    self.GetDialogueFromLanguageId();
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data.NaatiMockTestDetailVm), NaatiMockTestDetailModel);
                    self.MockTestDetails(data);
                }
            });
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    self.View = function (model) {

        if (self.SelectedNaatiMockTest() == undefined || self.SelectedNaatiMockTest().Id() == 0) {

            return Riddha.UI.Toast("Please select row to View.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetNaatiMockTest?masterId=" + self.SelectedNaatiMockTest().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.NaatiMockTest(new NaatiMockTestModel(ko.toJS(result.Data.NaatiMockTestVm)));
                    //self.GetSegmentsFromLanguageId();
                    self.GetDialogueFromLanguageId();
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data.NaatiMockTestDetailVm), NaatiMockTestDetailModel);
                    self.MockTestDetails(data);
                }
            });
        self.ShowViewModal();
    }

    self.GetQuestionTitle = function (id) {
       
        var mapped = ko.utils.arrayFirst(ko.toJS(self.Dialogues()), function (data) {
            return data.Id == id;
        });
        return mapped = (mapped || { Name: '' }).Name;
    };

    //self.GetSegmentTitle = function (id) {
       
    //    var mapped = ko.utils.arrayFirst(ko.toJS(self.Segments()), function (data) {
    //        return data.Id == id;
    //    });
    //    return mapped = (mapped || { Name: '' }).Name;
    //};

    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
    }
}