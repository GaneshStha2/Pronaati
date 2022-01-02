/// <reference path="../../../scripts/knockout-3.4.2.js" />
/// <reference path="../../../scripts/app/globals/riddha.dateconversion.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.backend.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="riddha.script.speaking.model.js" />


function SpeakinTypeOneController() {

    var self = this;
    var url = "/Api/SpeakingTypeOneApi";
    self.SpeakingTypeOne = ko.observable(new SpeakingTypeOneModel());
    self.SpeakingTypeOneList = ko.observableArray([]);
    self.SelectedSpeakingTypeOne = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if ((self.SpeakingTypeOne().Title() == "") || (self.SpeakingTypeOne().Question() == "")) {
            Riddha.UI.Toast("Title and Question fields can't be empty !", 0);
            return;
        }
        if ((self.SpeakingTypeOne().BeginWithinTImeSec() == "") || (self.SpeakingTypeOne().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeOne()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeOne()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeOne() == undefined || self.SelectedSpeakingTypeOne().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeOne().Id(), null)
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
        title: "Speaking Type One ",
        target: "#SpeakingTypeOne",
        url: "/Api/SpeakingTypeOneApi/GetSpeakingTypeOne",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'Question', title: "Question", filterable: false },
            { field: 'BeginWithinTImeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeOne(new SpeakingTypeOneModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeOne(new SpeakingTypeOneModel());
        $("#SpeakingTypeOne").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.SpeakingTypeOne(new SpeakingTypeOneModel({ Id: self.SpeakingTypeOne().Id() }));

    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeOne(new SpeakingTypeOneModel());
        }
        $("#SpeakingTypeOneCreationModel").modal('show');
    };

    $("#SpeakingTypeOneCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        $("#SpeakingTypeOneCreationModel").modal('hide');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {

        if (self.SelectedSpeakingTypeOne() == undefined || self.SelectedSpeakingTypeOne().length > 1 || self.SelectedSpeakingTypeOne().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeOne(new SpeakingTypeOneModel(ko.toJS(self.SelectedSpeakingTypeOne())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

}


function SpeakingTypeTwoController() {
    var self = this;
    var url = "/Api/SpeakingTypeTwoApi";
    self.SpeakingTypeTwo = ko.observable(new SpeakingTypeTwoModel());
    self.SpeakingTypeTwoList = ko.observableArray([]);
    self.SelectedSpeakingTypeTwo = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if ((self.SpeakingTypeTwo().Title() == "") || (self.SpeakingTypeTwo().TextToRead() == "")) {
            Riddha.UI.Toast("Title and Reading Text fields can't be empty !", 0);
            return;
        }
        if ((self.SpeakingTypeTwo().BeginWithInTimeSec() == "") || (self.SpeakingTypeTwo().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeTwo()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeTwo()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeTwo() == undefined || self.SelectedSpeakingTypeTwo().length > 1 || self.SelectedSpeakingTypeTwo().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeTwo().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.ModeOfButton("Create");
                        self.Reset();
                        self.RefreshKendoGrid();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    self.KendoGridOptions = {
        title: "Speaking Type Two ",
        target: "#SpeakingTypeTwo",
        url: "/Api/SpeakingTypeTwoApi/GetSpeakingTypeTwoList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'TextToRead', title: "Text To Read", filterable: false },
            { field: 'BeginWithInTimeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeTwo(new SpeakingTypeTwoModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeTwo(new SpeakingTypeTwoModel());
        $("#SpeakingTypeTwo").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {
        self.SpeakingTypeTwo(new SpeakingTypeTwoModel({ Id: self.SpeakingTypeTwo().Id() }));

    }

    self.ShowModal = function () {

        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeTwo(new SpeakingTypeTwoModel());
        }
        $("#SpeakingTypeTwoCreationModel").modal('show');
    };

    $("#SpeakingTypeTwoCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        $("#SpeakingTypeTwoCreationModel").modal('hide');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {
        debugger;
        if (self.SelectedSpeakingTypeTwo() == undefined || self.SelectedSpeakingTypeTwo().length > 1 || self.SelectedSpeakingTypeTwo().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeTwo(new SpeakingTypeTwoModel(ko.toJS(self.SelectedSpeakingTypeTwo())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }


}



function SpeakingTypeThreeController() {
    var self = this;
    var url = "/Api/SpeakingTypeThreeApi";
    self.SpeakingTypeThree = ko.observable(new SpeakingTypeThreeModel());
    self.SpeakingTypeThreeList = ko.observableArray([]);
    self.SelectedSpeakingTypeThree = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if ((self.SpeakingTypeThree().Title() == "") || (self.SpeakingTypeThree().TextToRead() == "")) {
            Riddha.UI.Toast("Title and Reading Text fields can't be empty !", 0);
            return;
        }
        if ((self.SpeakingTypeThree().BeginWithInTimeSec() == "") || (self.SpeakingTypeThree().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeThree()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeThree()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeThree() == undefined || self.SelectedSpeakingTypeThree().length > 1 || self.SelectedSpeakingTypeThree().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeThree().Id(), null)
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
        title: "Speaking Type Three",
        target: "#SpeakingTypeThree",
        url: "/Api/SpeakingTypeThreeApi/GetSpeakingTypeThreeList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'TextToRead', title: "Text To Read", filterable: false },
            { field: 'BeginWithInTimeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeThree(new SpeakingTypeThreeModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeThree(new SpeakingTypeThreeModel());
        $("#SpeakingTypeThree").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.SpeakingTypeThree(new SpeakingTypeThreeModel({ Id: self.SpeakingTypeThree().Id() }));

    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeThree(new SpeakingTypeThreeModel());
        }
        $("#SpeakingTypeThreeCreationModel").modal('show');
    };

    $("#SpeakingTypeThreeCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        $("#SpeakingTypeThreeCreationModel").modal('hide');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {

        if (self.SelectedSpeakingTypeThree() == undefined || self.SelectedSpeakingTypeThree().length > 1 || self.SelectedSpeakingTypeThree().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeThree(new SpeakingTypeThreeModel(ko.toJS(self.SelectedSpeakingTypeThree())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

}




function SpeakingTypeFourController() {

    var self = this;
    var url = "/Api/SpeakingTypeFourApi";

    self.SpeakingTypeFour = ko.observable(new SpeakingTypeFourModel());
    self.SpeakingTypeFourList = ko.observableArray([]);
    self.SelectedSpeakingTypeFour = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.SpeakingTypeFour().Title() == "") {
            Riddha.UI.Toast("Title field can't be empty !", 0);
            return;
        }
        if (self.SpeakingTypeFour().AudioUrl() == "") {
            Riddha.UI.Toast("AudioUrl field can't be empty !", 0);
            return;
        }
        if ((self.SpeakingTypeFour().BeginWithInTimeSec() == "") || (self.SpeakingTypeFour().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeFour()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeFour()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeFour() == undefined || self.SelectedSpeakingTypeFour().length > 1 || self.SelectedSpeakingTypeFour().Id() == 0) {

            return Riddha.UI.Toast("Please select a row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeFour().Id(), null)
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
        title: "Speaking Type Four",
        target: "#SpeakingTypeFour",
        url: "/Api/SpeakingTypeFourApi/GetSpeakingTypeFourList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'AudioUrl', title: "Audio", filterable: false },
            { field: 'BeginWithInTimeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeFour(new SpeakingTypeFourModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeFour(new SpeakingTypeFourModel());
        $("#SpeakingTypeFour").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.SpeakingTypeFour(new SpeakingTypeFourModel({ Id: self.SpeakingTypeFour().Id() }));

    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeFour(new SpeakingTypeFourModel());
        }
        $("#SpeakingTypeFourCreationModel").modal('show');
    };

    $("#SpeakingTypeFourCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });
    self.CloseModal = function () {
        self.Reset();
        self.ModeOfButton("Create");
        $("#SpeakingTypeFourCreationModel").modal('hide');
    }


    self.Select = function (model) {

        if (self.SelectedSpeakingTypeFour() == undefined || self.SelectedSpeakingTypeFour().length > 1 || self.SelectedSpeakingTypeFour().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeFour(new SpeakingTypeFourModel(ko.toJS(self.SelectedSpeakingTypeFour())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    var Preloaded = false;
    self.OpenFileManager = function () {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.SpeakingTypeFour().AudioUrl(data.URL);

            $("#fileManager").modal("hide");
        });



    }
    self.CloseFileManager = function () {
        Preloaded = true;
        $("#fileManager").modal('hide');
    }

}




function SpeakingTypeFiveController() {

    var self = this;
    var url = "/Api/SpeakingTypeFiveApi";

    self.SpeakingTypeFive = ko.observable(new SpeakingTypeFiveModel());
    self.SpeakingTypeFiveList = ko.observableArray([]);
    self.SelectedSpeakingTypeFive = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.SpeakingTypeFive().Title() == "") {
            Riddha.UI.Toast("Title field can't be empty !", 0);
            return;
        }
        if (self.SpeakingTypeFive().ImageURL() == "") {
            Riddha.UI.Toast("Image field can't be empty !", 0);
            return;
        }
        if ((self.SpeakingTypeFive().BeginWithInTimeSec() == "") || (self.SpeakingTypeFive().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }
        

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeFive()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeFive()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeFive() == undefined || self.SelectedSpeakingTypeFive().length > 1 || self.SelectedSpeakingTypeFive().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeFive().Id(), null)
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
        title: "Speaking Type Five",
        target: "#SpeakingTypeFive",
        url: "/Api/SpeakingTypeFiveApi/GetSpeakingTypeFiveList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'ImageURL', title: "Image", filterable: false },
            { field: 'BeginWithInTimeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeFive(new SpeakingTypeFiveModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeFive(new SpeakingTypeFiveModel());
        $("#SpeakingTypeFive").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.SpeakingTypeFive(new SpeakingTypeFiveModel({ Id: self.SpeakingTypeFive().Id() }));

    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeFive(new SpeakingTypeFiveModel());
        }
        $("#SpeakingTypeFiveCreationModel").modal('show');

    };

    $("#SpeakingTypeFiveCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        self.ModeOfButton('Create');
        $("#SpeakingTypeFiveCreationModel").modal('hide');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {

        if (self.SelectedSpeakingTypeFive() == undefined || self.SelectedSpeakingTypeFive().length > 1 || self.SelectedSpeakingTypeFive().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeFive(new SpeakingTypeFiveModel(ko.toJS(self.SelectedSpeakingTypeFive())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    var Preloaded = false;
    self.OpenFileManager = function () {

        debugger;
        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.SpeakingTypeFive().ImageURL(data.URL);

            $("#fileManager").modal("hide");
        });



    }
    self.CloseFileManager = function () {
        Preloaded = true;
        $("#fileManager").modal('hide');
    }

}


function SpeakingTypeSixController() {

    var self = this;
    var url = "/Api/SpeakingTypeSixApi";

    self.SpeakingTypeSix = ko.observable(new SpeakingTypeSixModel());
    self.SpeakingTypeSixList = ko.observableArray([]);
    self.SelectedSpeakingTypeSix = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.SpeakingTypeSix().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        //if (self.SpeakingTypeSix().ImageUrl() == "") {
        //    Riddha.UI.Toast("Image field can't be blank !", 0);
        //    return;
        //}
        if (self.SpeakingTypeSix().AudioUrl() == "") {
            Riddha.UI.Toast("Audio field can't be blank !", 0);
            return;
        }
        if ((self.SpeakingTypeSix().BeginWithInTimeSec() == "") || (self.SpeakingTypeSix().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeSix()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeSix()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeSix() == undefined || self.SelectedSpeakingTypeSix().length > 1 || self.SelectedSpeakingTypeSix().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeSix().Id(), null)
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
        title: "Speaking Type Six",
        target: "#SpeakingTypeSix",
        url: "/Api/SpeakingTypeSixApi/GetSpeakingTypeSixList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'ImageUrl', title: "Image", filterable: false },
            { field: 'AudioUrl', title: "Audio", filterable: false },
            { field: 'BeginWithInTimeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeSix(new SpeakingTypeSixModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeSix(new SpeakingTypeSixModel());
        $("#SpeakingTypeSix").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.SpeakingTypeSix(new SpeakingTypeSixModel({ Id: self.SpeakingTypeSix().Id() }));

    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeSix(new SpeakingTypeSixModel());
        }
        $("#SpeakingTypeSixCreationModel").modal('show');

    };

    $("#SpeakingTypeSixCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        self.ModeOfButton('Create');
        $("#SpeakingTypeSixCreationModel").modal('hide');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {

        if (self.SelectedSpeakingTypeSix() == undefined || self.SelectedSpeakingTypeSix().length > 1 || self.SelectedSpeakingTypeSix().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeSix(new SpeakingTypeSixModel(ko.toJS(self.SelectedSpeakingTypeSix())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    // Two cases for browsing File Manager Audio and Image
    var BrowseFor = "";
    self.OpenFileManagerForImage = function () {
        BrowseFor = "Image";
        self.OpenFileManager(BrowseFor);
    }
    self.OpenFileManagerForAudio = function () {

        BrowseFor = "Audio";

        self.OpenFileManager(BrowseFor);
    }


    var Preloaded = false;

    self.OpenFileManager = function (BrowseFor) {
        debugger;

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            if (result == undefined || result == "") {
                Preloaded = true;
            }
            else {
                var data = ko.toJS(result);
                if (BrowseFor == "Image") {
                    self.SpeakingTypeSix().ImageUrl(data.URL);

                } else {
                    self.SpeakingTypeSix().AudioUrl(data.URL);

                }

                Preloaded = true;
            }

            $("#fileManager").modal('hide');
        });

        self.CloseFileManager = function () {
            Preloaded = true;
            $("#fileManager").modal('hide');
        }

    }

}


function SpeakingTypeSevenController() {

    var self = this;
    var url = "/Api/SpeakingTypeSevenApi";

    self.SpeakingTypeSeven = ko.observable(new SpeakingTypeSevenModel());
    self.SpeakingTypeSevenList = ko.observableArray([]);
    self.SelectedSpeakingTypeSeven = ko.observable();
    self.ModeOfButton = ko.observable('Create');


    self.CreateUpdate = function () {
        if (self.SpeakingTypeSeven().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        if (self.SpeakingTypeSeven().QuestionAudioURl() == "") {
            Riddha.UI.Toast("Question Audio can't be blank !", 0);
            return;
        }
        if ((self.SpeakingTypeSeven().BeginWithInTimeSec() == "") || (self.SpeakingTypeSeven().SpeakingTimeSec() == "")) {
            Riddha.UI.Toast("Speaking time and audio beginning time can't be empty !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.SpeakingTypeSeven()))
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
            Riddha.ajax.put(url, ko.toJS(self.SpeakingTypeSeven()))
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
    };

    self.Delete = function (model) {
        if (self.SelectedSpeakingTypeSeven() == undefined || self.SelectedSpeakingTypeSeven().length > 1 || self.SelectedSpeakingTypeSeven().Id() == 0) {

            return Riddha.UI.Toast("Please select row to delete.", 0);

        }

        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSpeakingTypeSeven().Id(), null)
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
        title: "Speaking Type Seven",
        target: "#SpeakingTypeSeven",
        url: "/Api/SpeakingTypeSevenApi/GetSpeakingTypeSevenList",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", filterable: false },
            { field: 'QuestionAudioURl', title: "Audio", filterable: false },
            { field: 'BeginWithInTimeSec', title: "Begin With In Time", filterable: false, },
            { field: 'SpeakingTimeSec', title: "Total Time to Speak ", filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedSpeakingTypeSeven(new SpeakingTypeSevenModel(item));
        },
        SelectedItems: function (items) {

        },
    };


    self.RefreshKendoGrid = function () {
        self.SelectedSpeakingTypeSeven(new SpeakingTypeSevenModel());
        $("#SpeakingTypeSeven").getKendoGrid().dataSource.read();

    }

    self.Reset = function () {

        self.SpeakingTypeSeven(new SpeakingTypeSevenModel({ Id: self.SpeakingTypeSeven().Id() }));

    }

    self.ShowModal = function () {
        if (self.ModeOfButton() == "Create") {
            self.SpeakingTypeSeven(new SpeakingTypeSevenModel());
        }
        $("#SpeakingTypeSevenCreationModel").modal('show');

    };

    $("#SpeakingTypeSevenCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        self.Reset();
        self.ModeOfButton('Create');
        $("#SpeakingTypeSevenCreationModel").modal('hide');
    }

    //self.createUpdateText = function () {
    //    self.ModeOfButton("");
    //};
    self.Select = function (model) {

        if (self.SelectedSpeakingTypeSeven() == undefined || self.SelectedSpeakingTypeSeven().length > 1 || self.SelectedSpeakingTypeSeven().Id() == 0) {

            return Riddha.UI.Toast("Please select row to edit.", 0);

        }
        self.SpeakingTypeSeven(new SpeakingTypeSevenModel(ko.toJS(self.SelectedSpeakingTypeSeven())));
        self.ModeOfButton("Update");
        self.ShowModal();
    }

    var Preloaded = false;
    self.OpenFileManager = function () {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.SpeakingTypeSeven().QuestionAudioURl(data.URL);

            $("#fileManager").modal("hide");
        });



    }
    self.CloseFileManager = function () {
        Preloaded = true;
        $("#fileManager").modal('hide');
    }
}