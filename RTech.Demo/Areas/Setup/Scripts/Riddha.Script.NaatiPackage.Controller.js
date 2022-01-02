/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.NaatiPackage.Model.js" />


function NaatiPackageController() {
    var self = this;
    var url = "/Api/NaatiPackageApi";

    self.NaatiPackage = ko.observable(new NaatiPackageMasterModel());
    self.PackageFile = ko.observable(new PackageFileModel());
    self.QuestionSet = ko.observable(new PackageMockTestModel());

    self.PackageFiles = ko.observableArray([]);
    self.PackageMockTestSets = ko.observableArray([]);

    self.PracticeMocktestSets = ko.observableArray([]);

    self.NaatiMockTests = ko.observableArray([]);

    self.SelectedNaatiPackage = ko.observable();

    self.FileTypes = ko.observableArray([
        { Id: 0, Name: 'Video' },
        { Id: 1, Name: 'Audio' },
        { Id: 2, Name: 'PDF' },
    ]);

    self.PackageTypes = ko.observableArray([
        { Id: 0, Name: 'Express' },
        { Id: 1, Name: 'Advanced' },
        { Id: 2, Name: 'Master' },
    ]);

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



    self.GetNaatiMockTests = function () {
        Riddha.ajax.get(url + "/GetNaatiMockTestList?languageId=" + self.NaatiPackage().LanguageTypeId())
            .done(function (result) {
                if (result.Status == 4) {
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownTypeModel);
                    self.NaatiMockTests(data);
                }
            });
    }


    self.AddNewQuestion = function (model) {
        debugger;
        if (model.QuestionSetId() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.PackageMockTestSets(), function (item) {
            return model.QuestionSetId() == item.QuestionSetId()
        });
        if (mapped) {
            return Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            self.PackageMockTestSets.push(new PackageMockTestModel(ko.toJS(model)));
            self.QuestionSet(new PackageMockTestModel());
        }
    };
    self.AddNewPracticeQuestion = function (model) {
        if (model.QuestionSetId() == undefined) {
            return Riddha.UI.Toast("Please select a non-empty value !", 0);
        }
        var mapped = ko.utils.arrayFirst(self.PracticeMocktestSets(), function (item) {
            return model.QuestionSetId() == item.QuestionSetId()
        });
        if (mapped) {
            return Riddha.UI.Toast("Question has already been selected !", 0);
        }
        else {
            self.PracticeMocktestSets.push(new PackageMockTestModel(ko.toJS(model)));
            self.QuestionSet(new PackageMockTestModel());
        }
    };


    self.RemoveQuestion = function (model) {
        self.PackageMockTestSets.remove(model);
    };

    self.RemovePracticeQuestion = function (model) {
        self.PracticeMocktestSets.remove(model);
    };

    self.RemoveFile = function (model) {
        self.PackageFiles.remove(model);
    }

    self.CheckUniqueCode = function (model) {

        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.NaatiPackage().Code() + "&Id=" + self.NaatiPackage().Id(), null)
            .done(function (result) {
                if (result == false) {
                    debugger
                    self.NaatiPackage().Code('');
                    Riddha.UI.Toast("Course Code has been taken", 0);
                    return;
                }
            });
    }

    self.CreateUpdate = function () {

        self.CheckUniqueCode();
        if (self.NaatiPackage().Code() == "") {
            Riddha.UI.Toast("Please enter Code", 0);
            return;
        }
        if (self.NaatiPackage().Name() == "") {
            Riddha.UI.Toast("Please enter Name", 0);
            return;
        }
        if (self.NaatiPackage().Duration() == 0) {
            Riddha.UI.Toast("Please enter Duration", 0);
            return;
        }
        if (self.NaatiPackage().LanguageTypeId() == 0 || self.NaatiPackage().LanguageTypeId() == undefined) {
            Riddha.UI.Toast("Please Select Language Type", 0)
            return;
        }
       
        //if (self.NaatiPackage().ImageURL() == "") {
        //    Riddha.UI.Toast("Please upload Image", 0);
        //    return;
        //}

        if (self.NaatiPackage().Price() == 0) {
            Riddha.UI.Toast("Please enter Price", 0);
            return;
        }


        var data = {
            NaatiPackageVm: self.NaatiPackage(),
            PackageFilesVm: self.PackageFiles(),
            PackageMockTestsVm: self.PackageMockTestSets(),
            PracticeMockTestsVm: self.PracticeMocktestSets()
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
        debugger;
        if (self.SelectedNaatiPackage() == undefined || self.SelectedNaatiPackage().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedNaatiPackage().Id(), null)
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
        title: "Naati Package",
        target: "#NaatiPackage",
        url: "/Api/NaatiPackageApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: false,
        group: true,
        groupParam: { field: "LanguageType" },
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Course Code", filterable: true },
            { field: 'Name', title: "Name", filterable: true },
            { field: 'LanguageType', title: "Language", filterable: true },
            { field: 'Duration', title: "Duration", filterable: false, },
            { field: 'Price', title: "Price", filterable: true },
          
        ],
        SelectedItem: function (item) {
            self.SelectedNaatiPackage(new NaatiPackageMasterModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedNaatiPackage(new NaatiPackageMasterModel());
        $("#NaatiPackage").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.NaatiPackage(new NaatiPackageMasterModel());
        self.PackageFile(new PackageFileModel());
        self.QuestionSet(new PackageMockTestModel());
        self.PackageFiles([]);
        self.PackageMockTestSets([]);
        self.PracticeMocktestSets([]);
        self.ModeOfButton("Create");
    }

    self.ShowModal = function () {
        $("#naatiPackageCreationModel").modal('show');
    };

    self.ShowViewModal = function () {
        $("#naatiPackageViewModel").modal('show');
    };

    $("#naatiPackageCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    $("#naatiPackageViewModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.CloseModal = function () {
        $("#naatiPackageCreationModel").modal('hide');
        self.Reset();
    }

    self.CloseViewModal = function () {
        $("#naatiPackageViewModel").modal('hide');
        self.Reset();
    }

    self.Select = function (model) {

        if (self.SelectedNaatiPackage() == undefined || self.SelectedNaatiPackage().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetNaatiPackage?masterId=" + self.SelectedNaatiPackage().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {

                    self.NaatiPackage(new NaatiPackageMasterModel(result.Data.NaatiPackageVm));

                    self.GetNaatiMockTests();

                    self.PackageFiles(Riddha.ko.global.arrayMap(result.Data.PackageFilesVm, PackageFileModel));
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data.PackageMockTestsVm), PackageMockTestModel);
                    var practiceTests = Riddha.ko.global.arrayMap(ko.toJS(result.Data.PracticeMockTestsVm), PackageMockTestModel);
                    self.PracticeMocktestSets(practiceTests);
                    self.PackageMockTestSets(data);
                }
            });
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    self.View = function (model) {

        if (self.SelectedNaatiPackage() == undefined || self.SelectedNaatiPackage().Id() == 0) {

            return Riddha.UI.Toast("Please select row to View.", 0);

        }
        Riddha.ajax.get(url + "/" + "GetNaatiPackage?masterId=" + self.SelectedNaatiPackage().Id(), null)
            .done(function (result) {
                if (result.Status == 4) {

                    self.NaatiPackage(new NaatiPackageMasterModel(result.Data.NaatiPackageVm));

                    self.GetNaatiMockTests();

                    self.PackageFiles(Riddha.ko.global.arrayMap(result.Data.PackageFilesVm, PackageFileModel));
                    var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data.PackageMockTestsVm), PackageMockTestModel);
                    var practiceTests = Riddha.ko.global.arrayMap(ko.toJS(result.Data.PracticeMockTestsVm), PackageMockTestModel);
                    self.PracticeMocktestSets(practiceTests);
                    self.PackageMockTestSets(data);
                }
            });
        self.ShowViewModal();
    }

    self.AddNewFile = function (model) {
        debugger;
        if (model.FileId() == null || model.FileId() == 0) {
            Riddha.UI.Toast("Can't post empty file", 0);
            return;
        }
        if (model.FileType() == undefined) {

            Riddha.UI.Toast("Please select the file type", 0);
            return;
        }
        var mapped = ko.utils.arrayFirst(self.PackageFiles(), function (file) {
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

        self.PackageFiles.push(model);
        self.PackageFile(new PackageFileModel());
    }

    self.RemoveSelectedFile = function (model) {
        self.PackageFiles.remove(model)
    }

    preloaded = false;
    self.OpenFileManager = function (model) {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {
                preloaded = true;
            }
            else {
                var data = ko.toJS(result);
                var FileType = self.PackageFile().FileType();

                self.PackageFile(new PackageFileModel());
                self.PackageFile().FileUrl(data.URL);
                self.PackageFile().FileName(data.Name);
                self.PackageFile().FileId(data.Id);
                self.PackageFile().FileType(FileType);

                preloaded = true;
            }

            $("#fileManager").modal('hide');

        });

    }

    self.GetQuestionTitle = function (id) {
        debugger;
        var mapped = ko.utils.arrayFirst(ko.toJS(self.NaatiMockTests()), function (data) {
            return data.Id == id;
        });
        return mapped = (mapped || { Name: '' }).Name;
    };

    self.OpenImageFileManager = function () {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            preloaded = true;
            var data = ko.toJS(result);
            self.NaatiPackage().ImageURL(data.URL);
            $("#fileManager").modal('hide');
        });
    }

    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
    }

}