/// <reference path="riddha.script.tutorialsetup.model.js" />


function TutorialSetupController() {

    var self = this;
    var url = "/Api/TutorialSetupApi";
    self.Tutorial = ko.observable(new TutorialSetupModel());
    self.TutorialDetail = ko.observable(new TutorialDetailModel());
    self.TutorialDetails = ko.observableArray([]);
    self.SelectedTutorial = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        debugger;

        var data = {
            TutorialMasterVm: ko.toJS(self.Tutorial()),
            TutorialDetails: self.TutorialDetails(),
        };
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, data)
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
            Riddha.ajax.put(url, data)
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

    self.Delete = function () {
        debugger;
        if (self.SelectedTutorial() == undefined) {
            return Riddha.UI.Toast("Please select a row to delete !", 0);
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedTutorial().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedTutorial(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };
    self.KendoGridOptions = {
        title: "Tutorials ",
        target: "#Tutorials",
        url: "/Api/TutorialSetupApi/GetkendoList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,

        columns: [
            { field: 'Title', title: "Title", filterable: false },
            { field: 'Description', title: "Description", filterable: false },
            { field: 'CreatedDateTime', title: "Created DateTime", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedTutorial(new TutorialSetupModel(item));
        },
        SelectedItems: function (items) {

        },
    };



    self.RefreshKendoGrid = function () {
        self.SelectedTutorial(new TutorialSetupModel());
        $("#Tutorials").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.Tutorial(new TutorialSetupModel({ Id: self.Tutorial().Id() }));

        self.TutorialDetails(undefined);
    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.Tutorial(new TutorialSetupModel());
        }
        $("#TutorialCreationModel").modal('show');
    };

    //$("#TutorialCreationModel").on('hidden.bs.modal', function () {
    //    self.Reset();
    //});

    self.CloseModal = function () {
        self.Reset();
        $("#TutorialCreationModel").modal('hide');
    }

    self.ClosePopUp = function () {
        $("#fileManager").modal("hide");
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {
        if (self.SelectedTutorial() == undefined) {
            Riddha.UI.Toast("Please select a row to edit !", 0);
            return;
        }
        debugger;
        Riddha.ajax.get(url + "/GetTutorialDetailsByMasterId?Id=" + self.SelectedTutorial().Id(), null)
            .done(function (result) {
                self.Tutorial(new TutorialSetupModel(ko.toJS(result.Data.TutorialMasterVm)));
                debugger;
                var detailList = Riddha.ko.global.arrayMap(result.Data.TutorialDetails, TutorialDetailModel);
                self.TutorialDetails(detailList);
            });
        self.ShowModal();
        self.ModeOfButton("Update");
    };

    self.RemoveSelectedFile = function (model) {

        self.TutorialDetails.remove(model);
    }


    self.AddNewTutorial = function (model) {

        self.TutorialDetails.push(new TutorialDetailModel(ko.toJS(model)));
        self.TutorialDetail(new TutorialDetailModel());
    }

    var PreLoaded = false;
    self.OpenFileManager = function () {        

        //
        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", PreLoaded, function (result) {
            debugger;
            if (result == undefined || result == "") {

                PreLoaded = true;
            }
            else {
                var data = ko.toJS(result);

                self.TutorialDetail(new TutorialDetailModel());
                self.TutorialDetail().FileURL(data.URL);
                self.TutorialDetail().FileName(data.Name);
                PreLoaded = true;
            }

            $("#fileManager").modal('hide');           

        });

    }
    self.CloseFileManager = function () {
        PreLoaded = true;
        $("#fileManager").modal('hide');
    }



}
