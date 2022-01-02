/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.OnlineTraining.Model.js" />



function OnlineTrainingController() {

    var self = this;
    var url = "/Api/OnlineTrainingApi";

    self.OnlineTraining = ko.observable(new OnlineTrainingMasterModel());
    self.OnlineTrainingDetail = ko.observable(new OnlineTrainingeDetailsModel());

    self.OnlineTrainingDetails = ko.observableArray([]);
    self.ExistingFiles = ko.observableArray([]);
    self.SelectedOnlineTraining = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.FileTypes = ko.observableArray([
        { Id: 0, Name: 'Video' },
        { Id: 1, Name: 'Audio' },
        { Id: 2, Name: 'PDF' }
    ])

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


    self.CreateUpdate = function () {
        debugger;
        if (self.OnlineTraining().Code() == "") {
            Riddha.UI.Toast("Please enter Code", 0);
            return;
        }
        if (self.OnlineTraining().PackageTitle() == "") {
            Riddha.UI.Toast("Please enter Package Title", 0);
            return;
        }
        if (self.OnlineTraining().Duration() == 0) {
            Riddha.UI.Toast("Please enter Duration", 0);
            return;
        }
        if (self.OnlineTraining().CourseTypeId() == 0 || self.OnlineTraining().CourseTypeId() == undefined) {
            Riddha.UI.Toast("Please Select CourseType", 0)
            return;
        }
        if (self.OnlineTraining().Description() == "") {

            Riddha.UI.Toast("Please enter Description", 0);
            return;
        }
        if (self.OnlineTraining().Price() == 0) {

            Riddha.UI.Toast("Please enter Price", 0);
            return;
        }

        if (self.OnlineTraining().ImageURL() == "") {
            Riddha.UI.Toast("Please upload Image", 0);
            return;
        }



        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.OnlineTraining().Code() + "&Id=" + self.OnlineTraining().Id(), null)
            .done(function (result) {
                if (result == false) {
                    Riddha.UI.Toast("The Code has already been taken", 0);
                    return;
                }
                else {
                    var data = {
                        OnlineTrainingMasterVm: self.OnlineTraining(),
                        OnlineTrainingDetailsVm: self.OnlineTrainingDetails()
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
            });


    };


    self.Delete = function (model) {
        if (self.SelectedOnlineTraining() == undefined || self.SelectedOnlineTraining().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to delete.", 0);
        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedOnlineTraining().Id(), null)
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


    self.View = function (model) {
        if (self.SelectedOnlineTraining() == undefined || self.SelectedOnlineTraining().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to View.", 0);
        }

        Riddha.ajax.get(url + "/" + "GetDetails?MasterId=" + self.SelectedOnlineTraining().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(result.Data, OnlineTrainingeDetailsModel);
                    self.OnlineTrainingDetails(data);
                }
            });
        self.OnlineTraining(new OnlineTrainingMasterModel(ko.toJS(self.SelectedOnlineTraining())));

        $("#OnlineTrainingViewModel").modal('show');

    }


    self.KendoGridOptions = {
        title: "Online Training Package",
        target: "#OnlineTraining",
        url: "/Api/OnlineTrainingApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: false,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Course Code", filterable: true },
            { field: 'PackageTitle', title: "Online Training Title", filterable: true },
            { field: 'Duration', title: "Duration", filterable: false, },
            //{ field: 'Description', title: "Description", filterable: false, },
            { field: 'ImageURL', title: "Image URl ", filterable: false },
            { field: 'CreatedDate', title: "Created Date", filterable: false },
            { field: 'Price', title: "Price", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedOnlineTraining(new OnlineTrainingMasterModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedOnlineTraining(new OnlineTrainingMasterModel());
        $("#OnlineTraining").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.OnlineTraining(new OnlineTrainingMasterModel({ Id: self.OnlineTraining().Id(), CreatedDate: self.OnlineTraining().CreatedDate(), CreatedBy: self.OnlineTraining().CreatedBy() }));
        self.OnlineTrainingDetail(new OnlineTrainingeDetailsModel());
        self.OnlineTrainingDetails([]);
    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.OnlineTraining(new OnlineTrainingMasterModel());
            self.OnlineTrainingDetail(new OnlineTrainingeDetailsModel())
            self.OnlineTrainingDetails([]);
        }
        $("#OnlineTrainingCreationModel").modal('show');
    };

    $("#OnlineTrainingCreationModel").on('hidden.bs.modal', function () {
        self.ModeOfButton("Create");
    });
    self.CloseModal = function () {
        $("#OnlineTrainingCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    self.Select = function (model) {

        if (self.SelectedOnlineTraining() == undefined || self.SelectedOnlineTraining().length > 1 || self.SelectedOnlineTraining().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetDetails?MasterId=" + self.SelectedOnlineTraining().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(result.Data, OnlineTrainingeDetailsModel);
                    self.OnlineTrainingDetails(data);
                }
            });
        self.OnlineTraining(new OnlineTrainingMasterModel(ko.toJS(self.SelectedOnlineTraining())));
        self.ModeOfButton("Update");
        self.ShowModal();
    };

    self.AddNewFile = function (model) {

        if (model.FileId() == null || model.FileId() == 0) {
            Riddha.UI.Toast("Can't post empty file", 0);
            return;
        }

        if (model.FileType() == undefined || model.FileType() == null) {

            Riddha.UI.Toast("Please select the file type");
            return;
        }
        var mapped = ko.utils.arrayFirst(self.OnlineTrainingDetails(), function (file) {
            return model.FileId() == file.FileId()
        });
        if (mapped) {
            Riddha.UI.Toast("Please select new file", 0);
            return;
        }

        var fileTypemapped = ko.utils.arrayFirst(self.FileTypes(), function (data) {

            return model.FileType() == data.Id;
        });
        if (fileTypemapped) {

            model.FileTypeName(fileTypemapped.Name);
        }

        self.OnlineTrainingDetails.push(new OnlineTrainingeDetailsModel(ko.toJS(model)));
        self.OnlineTrainingDetail(new OnlineTrainingeDetailsModel());





        //self.OnlineTrainingDetails.push(new OnlineTrainingeDetailsModel(ko.toJS(model)));
        //self.OnlineTrainingDetail(new OnlineTrainingeDetailsModel());
    }

    self.RemoveSelectedFile = function (model) {
        self.OnlineTrainingDetails.remove(model)
    }

    var preloaded = false;
    self.OpenFileManager = function (model) {

        $("#OnlineTrainingCreationModel").modal('hide');
        BrowseFrommodel = model.constructor.name;

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {


            if (result == undefined || result == "") {

                preloaded = true;
            }
            else {
                var data = ko.toJS(result);
                debugger;
                if (BrowseFrommodel == "OnlineTrainingMasterModel") {
                    preloaded = true;
                    self.OnlineTraining().ImageURL(data.URL);
                } else {
                    preloaded = true;
                    var FileType = self.OnlineTrainingDetail().FileType();
                    self.OnlineTrainingDetail(new OnlineTrainingeDetailsModel());
                    self.OnlineTrainingDetail().FileUrl(data.URL);
                    self.OnlineTrainingDetail().FileName(data.Name);
                    self.OnlineTrainingDetail().FileId(data.Id);
                    self.OnlineTrainingDetail().FileType(FileType);
                }
               

            }

            $("#fileManager").modal('hide');
            $("#OnlineTrainingCreationModel").modal('show');

            if (self.OnlineTraining().Id() > 0) {

                self.ModeOfButton("Update");
            } else {

                self.ModeOfButton("Create");
            }

        });



    }

    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
        $("#OnlineTrainingCreationModel").modal('show');
    }

    //$("#fileManager").on('hidden.bs.modal', function () {
    //    $("#OnlineTrainingCreationModel").modal('show');
    //});


}