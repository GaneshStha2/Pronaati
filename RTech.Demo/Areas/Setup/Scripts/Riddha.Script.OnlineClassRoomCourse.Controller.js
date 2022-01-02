/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.OnlineClassRoomCourse.Model.js" />

function OnlineClassRoomCourseController() {
    var self = this;
    var url = "/Api/OnlineClassRoomCourseApi";

    self.OnlineClassRoomCourse = ko.observable(new OnlineClassRoomCourseMasterModel());
    self.OnlineClassRoomCourseDetails = ko.observableArray([]);
    self.OnlineClassRoomCourseDetail = ko.observable(new OnlineClassRoomCourseDetailsModel());
    self.SelectedClaassRoomCourse = ko.observable();
    self.FileTypes = ko.observableArray([
        { Id: 0, Name: 'Video' },
        { Id: 1, Name: 'Audio' },
        { Id: 2, Name: 'PDF' },
    ]);
    self.ModeOfButton = ko.observable('Create');

    ///For Course Type Dropdown
    self.CourseTypes = ko.observableArray([]);
    getCourseTypes();

    function getCourseTypes() {
        Riddha.ajax.get(url + '/GetCourseTypeList', null)
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
                self.CourseTypes(data);
            });
    };
    ///End DropDown

    //For Practice Menus
    self.Vocab = ko.observable(new PracticeMenusDropDownModel());
    self.SynonymBooster = ko.observable(new PracticeMenusDropDownModel());
    self.BoosterCollocation = ko.observable(new PracticeMenusDropDownModel());
    self.MasterTopicSentence = ko.observable(new PracticeMenusDropDownModel());
    self.PracticeQuestions = ko.observable(new PracticeMenusDropDownModel());

    self.VocabList = ko.observableArray([]);
    self.SynonymBoosterList = ko.observableArray([]);
    self.BoosterCollocationList = ko.observableArray([]);
    self.MasterTopicSentenceList = ko.observableArray([]);
    self.PracticeQuestionsList = ko.observableArray([]);

    self.CreateVocabList = ko.observableArray([]);
    self.CreateSynonymBoosterList = ko.observableArray([]);
    self.CreateBoosterCollocationList = ko.observableArray([]);
    self.CreateMasterTopicSentenceList = ko.observableArray([]);
    self.CreatePracticeQuestionsList = ko.observableArray([]);

    GetVocabList();
    function GetVocabList() {
        Riddha.ajax.get(url + "/GetVocabDropdownList")
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), PracticeMenusDropDownModel);
                    self.VocabList(data);
                }
            });
    }

    GetSynonymBoosterList();
    function GetSynonymBoosterList() {
        Riddha.ajax.get(url + "/GetSynonymBoosterDropdownList")
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), PracticeMenusDropDownModel);
                    self.SynonymBoosterList(data);
                }
            });
    }

    GetBoosterCollocationList();
    function GetBoosterCollocationList() {
        Riddha.ajax.get(url + "/GetBoosterCollocationDropdownList")
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), PracticeMenusDropDownModel);
                    self.BoosterCollocationList(data);
                }
            });
    }

    GetMasterTopicSentenceList();
    function GetMasterTopicSentenceList() {
        Riddha.ajax.get(url + "/GetMasterTopicSentenceDropdownList")
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), PracticeMenusDropDownModel);
                    self.MasterTopicSentenceList(data);
                }
            });
    }

    self.AddNewVocab = function (model)
    {
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.CreateVocabList(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            var data = ko.utils.arrayFirst(self.VocabList(), function (item) {
                return model.Id() == item.Id();
            });
            self.CreateVocabList.push(data);
        }
    }

    self.AddNewSynonymBooster = function (model) {
        debugger;
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.CreateSynonymBoosterList(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            var data = ko.utils.arrayFirst(self.SynonymBoosterList(), function (item) {
                return model.Id() == item.Id();
            });
            self.CreateSynonymBoosterList.push(data);
        }
    }

    self.AddNewBoosterCollocation = function (model) {
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.CreateBoosterCollocationList(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            var data = ko.utils.arrayFirst(self.BoosterCollocationList(), function (item) {
                return model.Id() == item.Id();
            });
            self.CreateBoosterCollocationList.push(data);
        }
    }

    self.AddNewMasterTopicSentence = function (model) {
        if (model.Id() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.CreateMasterTopicSentenceList(), function (item) {
            return model.Id() == item.Id()
        });
        if (mapped) {
            Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            var data = ko.utils.arrayFirst(self.MasterTopicSentenceList(), function (item) {
                return model.Id() == item.Id();
            });
            self.CreateMasterTopicSentenceList.push(data);
        }
    }

    self.RemoveVocab = function (model) {
        self.CreateVocabList.remove(model);
    }
    self.RemoveSynonymBooster = function (model) {
        self.CreateSynonymBoosterList.remove(model);
    }
    self.RemoveBoosterCollocation = function (model) {
        self.CreateBoosterCollocationList.remove(model);
    }
    self.RemoveMasterTopicSentence = function (model) {
        self.CreateMasterTopicSentenceList.remove(model);
    }


    self.CheckUniqueCode = function (model) {

        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.OnlineClassRoomCourse().Code() + "&Id=" + self.OnlineClassRoomCourse().Id(), null)
            .done(function (result) {
                if (result == false) {
                    debugger
                    self.OnlineClassRoomCourse().Code('');
                    Riddha.UI.Toast("Course Code has been taken", 0);
                    return;
                }
            });
    }

    self.CreateUpdate = function () {
        
        self.CheckUniqueCode();
        if (self.OnlineClassRoomCourse().Code() == "") {
            Riddha.UI.Toast("Please enter Code", 0);
            return;
        }
        if (self.OnlineClassRoomCourse().Name() == "") {
            Riddha.UI.Toast("Please enter Name", 0);
            return;
        }
        if (self.OnlineClassRoomCourse().Duration() == 0) {
            Riddha.UI.Toast("Please enter Duration", 0);
            return;
        }
        if (self.OnlineClassRoomCourse().CourseTypeId() == 0) {
            Riddha.UI.Toast("Please Select CourseType", 0)
            return;
        }
        if (self.OnlineClassRoomCourse().Description() == "") {
            Riddha.UI.Toast("Please enter Description", 0);
            return;
        }

        if (self.OnlineClassRoomCourse().ImageURL() == "") {
            Riddha.UI.Toast("Please upload Image", 0);
            return;
        }
        if (self.OnlineClassRoomCourse().Price() == 0) {
            Riddha.UI.Toast("Please enter Price", 0);
            return;
        }


        if (self.ModeOfButton() == 'Create') {


            var data = {
                OnlineClassRoomCourseMasterVm: self.OnlineClassRoomCourse(),
                OnlineClassRoomCourseDetailsVm: self.OnlineClassRoomCourseDetails(),
                VocabDetails: self.CreateVocabList(),
                SynonymBoosterDetails: self.CreateSynonymBoosterList(),
                BoosterCollocationDetails: self.CreateBoosterCollocationList(),
                MasterTopicSentenceDetails: self.CreateMasterTopicSentenceList(),
                PracticeQuestionsDetails: self.CreatePracticeQuestionsList()
            };

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

            var data = {
                OnlineClassRoomCourseMasterVm: self.OnlineClassRoomCourse(),
                OnlineClassRoomCourseDetailsVm: self.OnlineClassRoomCourseDetails(),
                VocabDetails: self.CreateVocabList(),
                SynonymBoosterDetails: self.CreateSynonymBoosterList(),
                BoosterCollocationDetails: self.CreateBoosterCollocationList(),
                MasterTopicSentenceDetails: self.CreateMasterTopicSentenceList(),
                PracticeQuestionsDetails: self.CreatePracticeQuestionsList()
            };

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
        if (self.SelectedClaassRoomCourse() == undefined || self.SelectedClaassRoomCourse().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedClaassRoomCourse().Id(), null)
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
        title: "Online ClassRoom Course",
        target: "#OnlineClassRoomCourse",
        url: "/Api/OnlineClassRoomCourseApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Course Code", filterable: true },
            { field: 'Name', title: "Name", filterable: true },
            { field: 'Duration', title: "Duration", filterable: false, },
            //{ field: 'Description', title: "Description", filterable: false, },
            //{ field: 'ImageURL', title: "Image URl ", filterable: false },
            { field: 'Price', title: "Price", filterable: true },
            { field: 'CreatedDate', title: "Created Date", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedClaassRoomCourse(new OnlineClassRoomCourseMasterModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedClaassRoomCourse(new OnlineClassRoomCourseMasterModel());
        $("#OnlineClassRoomCourse").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.OnlineClassRoomCourse(new OnlineClassRoomCourseMasterModel({ Id: self.OnlineClassRoomCourse().Id(), CreatedDate: self.OnlineClassRoomCourse().CreatedDate(), CreatedBy: self.OnlineClassRoomCourse().CreatedBy() }));
        self.OnlineClassRoomCourseDetail(new OnlineClassRoomCourseDetailsModel());
        self.OnlineClassRoomCourseDetails([]);
        self.CreateVocabList([]);
        self.CreateSynonymBoosterList([]);
        self.CreateBoosterCollocationList([]);
        self.CreateMasterTopicSentenceList([]);
        self.CreatePracticeQuestionsList([]);
    }


    self.EnablePractice = function (data) {



        if (self.OnlineClassRoomCourse().IsPracticeEnabled() == false) {
            debugger;
            $("#Practice").hide();
        } else {

            $("#Practice").show();
        }
    }

    self.EnablePractice(false);

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.OnlineClassRoomCourse(new OnlineClassRoomCourseMasterModel());
            self.OnlineClassRoomCourseDetail(new OnlineClassRoomCourseDetailsModel());
            self.OnlineClassRoomCourseDetails([]);
            self.CreateVocabList([]);
            self.CreateSynonymBoosterList([]);
            self.CreateBoosterCollocationList([]);
            self.CreateMasterTopicSentenceList([]);
            self.CreatePracticeQuestionsList([]);
        }
        $("#OnlineClassRoomCourseCreationModel").modal('show');
    };

    $("#OnlineClassRoomCourseCreationModel").on('hidden.bs.modal', function () {

        self.ModeOfButton("Create");
    });
    self.CloseModal = function () {
        $("#OnlineClassRoomCourseCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    }


    self.Select = function (model) {

        if (self.SelectedClaassRoomCourse() == undefined || self.SelectedClaassRoomCourse().length > 1 || self.SelectedClaassRoomCourse().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetDetails?MasterId=" + self.SelectedClaassRoomCourse().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(result.Data.OnlineClassRoomCourseDetailsVm, OnlineClassRoomCourseDetailsModel);
                    var vocabData = Riddha.ko.global.arrayMap(result.Data.VocabDetails, PracticeMenusDropDownModel);
                    var synonymBoosterData = Riddha.ko.global.arrayMap(result.Data.SynonymBoosterDetails, PracticeMenusDropDownModel);
                    var boosterCollocationData = Riddha.ko.global.arrayMap(result.Data.BoosterCollocationDetails, PracticeMenusDropDownModel);
                    var masterTopicSentenceData = Riddha.ko.global.arrayMap(result.Data.MasterTopicSentenceDetails, PracticeMenusDropDownModel);

                    self.OnlineClassRoomCourseDetails(data);
                    self.CreateVocabList(vocabData);
                    self.CreateSynonymBoosterList(synonymBoosterData);
                    self.CreateBoosterCollocationList(boosterCollocationData);
                    self.CreateMasterTopicSentenceList(masterTopicSentenceData);
                }
            });
        self.OnlineClassRoomCourse(new OnlineClassRoomCourseMasterModel(ko.toJS(self.SelectedClaassRoomCourse())))
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    self.View = function (model) {
        if (self.SelectedClaassRoomCourse() == undefined || self.SelectedClaassRoomCourse().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to View.", 0);
        }
        //Riddha.ajax.get(url + "/" + "GetDetails?MasterId=" + self.SelectedClaassRoomCourse().Id(), null)
        //    .done(function (result) {
        //        if (result.Status == 4) {
        //            var data = Riddha.ko.global.arrayMap(result.Data, OnlineClassRoomCourseDetailsModel);
        //            self.OnlineClassRoomCourseDetails(data);
        //        }
        //    });
        //self.OnlineClassRoomCourse(new OnlineClassRoomCourseMasterModel(ko.toJS(self.SelectedClaassRoomCourse())));


        Riddha.ajax.get(url + "/" + "GetDetails?MasterId=" + self.SelectedClaassRoomCourse().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(result.Data.OnlineClassRoomCourseDetailsVm, OnlineClassRoomCourseDetailsModel);
                    var vocabData = Riddha.ko.global.arrayMap(result.Data.VocabDetails, PracticeMenusDropDownModel);
                    var synonymBoosterData = Riddha.ko.global.arrayMap(result.Data.SynonymBoosterDetails, PracticeMenusDropDownModel);
                    var boosterCollocationData = Riddha.ko.global.arrayMap(result.Data.BoosterCollocationDetails, PracticeMenusDropDownModel);
                    var masterTopicSentenceData = Riddha.ko.global.arrayMap(result.Data.MasterTopicSentenceDetails, PracticeMenusDropDownModel);

                    self.OnlineClassRoomCourseDetails(data);
                    self.CreateVocabList(vocabData);
                    self.CreateSynonymBoosterList(synonymBoosterData);
                    self.CreateBoosterCollocationList(boosterCollocationData);
                    self.CreateMasterTopicSentenceList(masterTopicSentenceData);
                }
            });
        self.OnlineClassRoomCourse(new OnlineClassRoomCourseMasterModel(ko.toJS(self.SelectedClaassRoomCourse())))

        $("#OnlineClaassRoomCourseViewModel").modal('show');

    }


    self.AddNewFile = function (model) {
        if (model.FileId() == null || model.FileId() == 0) {
            Riddha.UI.Toast("Can't post empty file", 0);
            return;
        }
        if (model.FileType() == undefined) {

            Riddha.UI.Toast("Please select the file type", 0);
            return;
        }
        var mapped = ko.utils.arrayFirst(self.OnlineClassRoomCourseDetails(), function (file) {
            return model.FileId() == file.FileId()
        });
        if (mapped) {
            Riddha.UI.Toast("File Already Exist", 0);
            return;
        }

        var FileTypemapped = ko.utils.arrayFirst(self.FileTypes(), function (data) {
            return model.FileType() == data.Id;
        });
        if (FileTypemapped) {

            model.FileTypeName(FileTypemapped.Name);
        }

        self.OnlineClassRoomCourseDetails.push(new OnlineClassRoomCourseDetailsModel(ko.toJS(model)));
        self.OnlineClassRoomCourseDetail(new OnlineClassRoomCourseDetailsModel());
    }

    self.RemoveSelectedFile = function (model) {
        self.OnlineClassRoomCourseDetails.remove(model)
    }

    var preloaded = false;
    var BrowseFrommodel = "";
    self.OpenFileManager = function (model) {


        BrowseFrommodel = model.constructor.name;
        $("#OnlineClassRoomCourseCreationModel").modal('hide');

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {
                preloaded = true;
            }
            else {
                var data = ko.toJS(result);
                if (BrowseFrommodel == "OnlineClassRoomCourseMasterModel") {
                    self.OnlineClassRoomCourse().ImageURL(data.URL);
                } else {
                    var FileType = self.OnlineClassRoomCourseDetail().FileType();
                    self.OnlineClassRoomCourseDetail(new OnlineClassRoomCourseDetailsModel());
                    self.OnlineClassRoomCourseDetail().FileUrl(data.URL);
                    self.OnlineClassRoomCourseDetail().FileName(data.Name);
                    self.OnlineClassRoomCourseDetail().FileId(data.Id);
                    self.OnlineClassRoomCourseDetail().FileType(FileType);
                }

                preloaded = true;
            }

            $("#fileManager").modal('hide');


            $("#OnlineClassRoomCourseCreationModel").modal('show');
            if (self.OnlineClassRoomCourse().Id() > 0) {
                self.ModeOfButton("Update");
            }
            else {
                self.ModeOfButton("Create");
            }
        });

    }

    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
        $("#OnlineClassRoomCourseCreationModel").modal('show');
    }

    //$("#fileManager").on('hidden.bs.modal', function () {
    //    $("#OnlineClassRoomCourseCreationModel").modal('show');
    //});



}