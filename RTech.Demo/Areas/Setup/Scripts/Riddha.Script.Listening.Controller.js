/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="Riddha.Script.Company.Model.js" />


function listeningTypeOneController() {
    var self = this;
    var url = "/Api/ListeningTypeOneApi";
    self.ListeningTypeOne = ko.observable(new ListeningTypeOneModel());
    self.ListeningTypeOnes = ko.observableArray([]);
    self.SelectedListeningTypeOne = ko.observable();
    self.ModeOfButton = ko.observable('Create');
    self.AudioPath = ko.observable();

    self.KendoGridOptions = {
        title: "Listening Type One",
        target: "#listeningTypeOneKendoGrid",
        url: url + "/GetListeningTypeOneKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'TimeBeforeAudio', title: "Time Before Audio (Sec)", width: 200, filterable: false },
            { field: 'TotalTime', title: "Total Time", width: 200, filterable: false },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            {
                command:
                    [{ name: "play", template: '<a class="k-grid-play  k-button" ><span class="fa fa-play text-blue"  ></span></a>', click: Play }],
                width: "90px",
                title: "Play"

            },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeOne(item)
        },
        SelectedItems: function (items) {
        },
    };
    var audio = new Audio();
    var lastAudioUrl = "";
    var lastAudioEventButton = {};
    var toolTipText = "Play Audio";
    function Play(e) {      
        setTimeout(function () {
            var grid = $("#listeningTypeOneKendoGrid").getKendoGrid();
            var item = grid.dataItem($(e.target).closest("tr"));

            var node = [];

            if ($(e.target).is('span')) {
                node = e.target.parentNode;
            }
            else {
                node = e.target;
            }

            if (lastAudioUrl == item.AudioUrl) {

                audio.pause();
                lastAudioUrl = "";
                node.innerHTML = '<span class="fa fa-play text-blue"></span>';
                toolTipText = "Play Audio";
                return;

            }
            else {
                if (lastAudioUrl != "") {
                    lastAudioEventButton.innerHTML = '<span class="fa fa-play text-blue"></span>';
                }
                audio.src = item.AudioUrl;
                lastAudioUrl = item.AudioUrl;
                lastAudioEventButton = node;
                node.innerHTML = '<span class="fa fa-stop text-red"></span>';
                toolTipText = "Pause Audio";
            }


            audio.play();
        }, 0);
        //self.AudioPath()
        //$("#playAudioModal").modal('show');
    };
    function paused() {
        alert('paused')
    }
    $("#listeningTypeOneKendoGrid").kendoTooltip({
        filter: ".k-grid-3333",
        position: "bottom",
        content: function (e) {


            return "";
        }
    });

    self.RefreshKendoGrid = function () {
        // self.SelectedListeningTypeOne(new ListeningTypeOneModel());
        $("#listeningTypeOneKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ListeningTypeOne().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeOne().AudioUrl() == "") {
            Riddha.UI.Toast("AudioUrl field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeOne().TimeBeforeAudio() == "") {
            Riddha.UI.Toast("Audio Playing Time field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeOne().TotalTime() == "") {
            Riddha.UI.Toast("Total Time  field can't be blank !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeOne()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeOne()))
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedListeningTypeOne(undefined);

                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);
                });

        }
    };

    self.Reset = function () {

        self.ListeningTypeOne(new ListeningTypeOneModel({ Id: self.ListeningTypeOne().Id() }));

        self.ModeOfButton("Create");
    };

    self.Select = function () {

        if (self.SelectedListeningTypeOne() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.ListeningTypeOne(new ListeningTypeOneModel(ko.toJS(self.SelectedListeningTypeOne())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedListeningTypeOne() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeOne().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedListeningTypeOne(undefined);


                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ListeningTypeOneCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeOneCreationModel").modal('show');
    };


    self.CloseModal = function () {
        $("#ListeningTypeOneCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    var Preloaded = false;
    self.OpenFileManager = function () {
        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", Preloaded, function (result) {

            Preloaded = true;
            var data = ko.toJS(result);
            self.ListeningTypeOne().AudioUrl(data.URL);

            $("#fileManager").modal("hide");
        });
    }


    $("#fileManager").on('hidden.bs.modal', function () {
        Preloaded = true;
    });
    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
    }
}

function listeningTypeTwoController() {
    var self = this;
    var url = "/Api/ListeningTypeTwoApi";
    self.ListeningTypeTwo = ko.observable(new ListeningTypeTwoModel());
    self.ListeningTypeTwos = ko.observableArray([]);
    self.SelectedListeningTypeTwo = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Listening Type Two",
        target: "#listeningTypeTwoKendoGrid",
        url: url + "/GetListeningTypeTwoKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false },
            { field: 'Title', title: "Title", width: 225, filterable: false  },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            { field: 'Question', title: "Question", width: 200, filterable: false },
            { field: 'Response1', title: "Response1", width: 200, filterable: false },
            { field: 'Response2', title: "Response2", width: 200, filterable: false },
            { field: 'Response3', title: "Response3", width: 200, filterable: false },
            { field: 'Response4', title: "Response4", width: 200, filterable: false },
            { field: 'Response5', title: "Response5", width: 200, filterable: false },
            { field: 'Response6', title: "Response6", width: 200, filterable: false },
            { field: 'Response7', title: "Response7", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", width: 100, filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeTwo(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeTwoKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        debugger;
        if ((self.ListeningTypeTwo().Title() == "") || (self.ListeningTypeTwo().Question() == "")) {
            Riddha.UI.Toast("Title and Question fields can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeTwo().AudioUrl() == "") {
            Riddha.UI.Toast("Please upload an audio file !", 0);
            return;
        }
        if (self.ListeningTypeTwo().BeginWithin() == 0) {
            Riddha.UI.Toast("Please enter time before audio !", 0);
            return;
        }
        if ((self.ListeningTypeTwo().Response1() == "") && (self.ListeningTypeTwo().Response2() == "") && (self.ListeningTypeTwo().Response3() == "") && (self.ListeningTypeTwo().Response4() == "") && (self.ListeningTypeTwo().Response5() == "") && (self.ListeningTypeTwo().Response6() == "") && (self.ListeningTypeTwo().Response7() == "")) {
            Riddha.UI.Toast("Response fields can't be blank !", 0);
            return;
        }
        if ((self.ListeningTypeTwo().Response1IsCorrect() == false) && (self.ListeningTypeTwo().Response2IsCorrect() == false) && (self.ListeningTypeTwo().Response3IsCorrect() == false) && (self.ListeningTypeTwo().Response4IsCorrect() == false) && (self.ListeningTypeTwo().Response5IsCorrect() == false) && (self.ListeningTypeTwo().Response6IsCorrect() == false) && (self.ListeningTypeTwo().Response7IsCorrect() == false)) {
            Riddha.UI.Toast("Please Choose atleast one correct response !", 0);
            return;
        }
       
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeTwo()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeTwo()))
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
        self.ListeningTypeTwo(new ListeningTypeTwoModel({ Id: self.ListeningTypeTwo().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedListeningTypeTwo() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        Riddha.ajax.get(url + "/GET?Id=" + self.SelectedListeningTypeTwo().Id, null)
            .done(function (result) {
                debugger;
                if (result.Status == 4) {
                    self.ListeningTypeTwo(new ListeningTypeTwoModel(ko.toJS(result.Data)));
                    self.ShowModal();
                    self.ModeOfButton('Update');

                };

            });

    };

    self.Delete = function () {
        if (self.SelectedListeningTypeTwo() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeTwo().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedListeningTypeTwo(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ListeningTypeTwoCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeTwoCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeTwoCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    var preloaded = false;
    self.OpenFileManager = function () {


        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {

                preloaded = true;

            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeTwo().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');

        });

    }
    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
    }

}

function listeningTypeThreeController() {
    var self = this;
    var url = "/Api/ListeningTypeThreeApi";
    var markInt = 0;
    self.ListeningTypeThree = ko.observable(new ListeningTypeThreeModel());
    self.ListeningTypeThrees = ko.observableArray([]);
    self.SelectedListeningTypeThree = ko.observable();
    self.ModeOfButton = ko.observable('Create');
    self.ListeningTypeAnswersThree = ko.observable(new ListeningTypeThreeAnswersModel());

    self.KendoGridOptions = {
        title: "Listening Type Three",
        target: "#listeningTypeThreeKendoGrid",
        url: url + "/GetListeningTypeThreeKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false  },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            { field: 'QuestionText', title: "Question Text", width: 200, filterable: false },
            { field: 'Answers', title: "Answers", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeThree(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeThreeKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if ((self.ListeningTypeThree().Title() == "") || (self.ListeningTypeThree().QuestionText() == "")) {
            Riddha.UI.Toast("Title and Question Text fields can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeThree().AudioUrl() == "") {
            Riddha.UI.Toast("AudioUrl field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeThree().Answers() == "") {
            Riddha.UI.Toast("Please provide atleast one correct answer !", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {

            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeThree()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeThree()))
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
        self.ListeningTypeThree(new ListeningTypeThreeModel({ Id: self.ListeningTypeThree().Id() }));
        self.ModeOfButton("Create");
        markInt = 0;
    };

    self.Select = function () {
        if (self.SelectedListeningTypeThree() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.ListeningTypeThree(new ListeningTypeThreeModel(ko.toJS(self.SelectedListeningTypeThree())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedListeningTypeThree() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeThree().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedListeningTypeThree(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    self.AddMark = function () {
        debugger;
        self.ListeningTypeAnswersThree(new ListeningTypeThreeAnswersModel());
        $("#ListeningTypeThreeCreationAnswersModel").modal('show');

    }

    self.AddAnswerOption = function () {
        var answerText = self.ListeningTypeThree().Answers();
        answerText += self.ListeningTypeAnswersThree().Answer() + ", ";
        self.ListeningTypeThree().Answers(answerText);



        var currentText = self.ListeningTypeThree().QuestionText();
        currentText += "{" + markInt + "}";
        self.ListeningTypeThree().QuestionText(currentText);
        markInt = markInt + 1;
        $("#ListeningTypeThreeCreationAnswersModel").modal('hide');
    }

    self.CloseOptionModal = function () {
        $("#ListeningTypeThreeCreationAnswersModel").modal('hide');

    }
    $("#ListeningTypeThreeCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeThreeCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeThreeCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    var preloaded = false;
    self.OpenFileManager = function () {


        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {

                preloaded = true;

            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeThree().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');

        });

    }
    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');
    }
}

function listeningTypeFourController() {
    var self = this;
    var url = "/Api/ListeningTypeFourApi";
    self.ListeningTypeFour = ko.observable(new ListeningTypeFourModel());
    self.ListeningTypeFours = ko.observableArray([]);
    self.SelectedListeningTypeFour = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Listening Type Four",
        target: "#listeningTypeFourKendoGrid",
        url: url + "/GetListeningTypeFourKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            { field: 'Paragraph1', title: "Paragraph1", width: 200, filterable: false },
            { field: 'Paragraph2', title: "Paragraph2", width: 200, filterable: false },
            { field: 'Paragraph3', title: "Paragraph3", width: 200, filterable: false },
            { field: 'Paragraph4', title: "Paragraph4", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeFour(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeFourKendoGrid").getKendoGrid().dataSource.read();
    };



    self.CreateUpdate = function () {
        if ((self.ListeningTypeFour().Title() == "") || (self.ListeningTypeFour().Paragraph1() == "") || (self.ListeningTypeFour().Paragraph2() == "") || (self.ListeningTypeFour().Paragraph3() == "") || (self.ListeningTypeFour().Paragraph4() == "") || (self.ListeningTypeFour().AudioUrl() == "") ) {
            Riddha.UI.Toast("All fields are mandatory !", 0);
            return;
        }
        if ((self.ListeningTypeFour().Paragraph1IsCorrect() == false) && (self.ListeningTypeFour().Paragraph2IsCorrect() == false) && (self.ListeningTypeFour().Paragraph3IsCorrect() == false) && (self.ListeningTypeFour().Paragraph4IsCorrect() == false)) {
            Riddha.UI.Toast("Please select atleast one correct answer!", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeFour()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeFour()))
                .done(function (result) {
                    if (result.Status == 4) {

                        self.Reset();
                        self.RefreshKendoGrid();
                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);
                });

        }
    };

    self.Reset = function () {
        self.ListeningTypeFour(new ListeningTypeFourModel({ Id: self.ListeningTypeFour().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedListeningTypeFour() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        Riddha.ajax.get(url + "/Get?Id=" + self.SelectedListeningTypeFour().Id, null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.ListeningTypeFour(new ListeningTypeFourModel(ko.toJS(result.Data)));
                    self.ShowModal();
                    self.ModeOfButton('Update');

                };

            });
    };

    self.Delete = function () {
        if (self.SelectedListeningTypeFour() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeFour().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        //self.Reset();
                        self.SelectedListeningTypeFour(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    $("#ListeningTypeFourCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeFourCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeFourCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    $($("[type=checkbox]")).on('change', function (data) {

        $($("[type=checkbox]")).not(this).prop('checked', false);

    });

    var preloaded = false;
    self.OpenFileManager = function () {

        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {
                preloaded = true;
            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeFour().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');
        });

    }


    self.CloseFileManager = function () {
        preloaded = true;
        $("#fileManager").modal('hide');

    }

}

function listeningTypeFiveController() {
    var self = this;
    var url = "/Api/ListeningTypeFiveApi";
    self.ListeningTypeFive = ko.observable(new ListeningTypeFiveModel());
    self.ListeningTypeFives = ko.observableArray([]);
    self.SelectedListeningTypeFive = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Listening Type Five",
        target: "#listeningTypeFiveKendoGrid",
        url: url + "/GetListeningTypeFiveKendoGrid",
        height: 5000,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false  },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            { field: 'Question', title: "Question", width: 200, filterable: false },
            { field: 'Option1', title: "Option1", width: 200, filterable: false },
            { field: 'Option2', title: "Option2", width: 200, filterable: false },
            { field: 'Option3', title: "Option3", width: 200, filterable: false },
            { field: 'Option4', title: "Option4", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeFive(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeFiveKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ListeningTypeFive().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeFive().AudioUrl() == "") {
            Riddha.UI.Toast("Audio field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeFive().Question() == "") {
            Riddha.UI.Toast("Question field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeFive().Option1() == "" || self.ListeningTypeFive().Option2() == "" || self.ListeningTypeFive().Option3() == "" ||self.ListeningTypeFive().Option4() == ""  ) {
            Riddha.UI.Toast("Options field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeFive().Option1IsCorrect() == false && self.ListeningTypeFive().Option2IsCorrect() == false && self.ListeningTypeFive().Option3IsCorrect() == false && self.ListeningTypeFive().Option4IsCorrect() == false) {
            return Riddha.UI.Toast("Please choose at least One correct answer !", 0);
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeFive()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeFive()))
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
        self.ListeningTypeFive(new ListeningTypeFiveModel({ Id: self.ListeningTypeFive().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {

        if (self.SelectedListeningTypeFive() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        Riddha.ajax.get(url + "/Get?Id=" + self.SelectedListeningTypeFive().Id, null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.ListeningTypeFive(new ListeningTypeFiveModel(ko.toJS(result.Data)));
                    self.ShowModal();
                    self.ModeOfButton('Update');

                };

            });

    };

    self.Delete = function () {
        if (self.SelectedListeningTypeFive() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeFive().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedListeningTypeFive(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ListeningTypeFiveCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeFiveCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeFiveCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
    var preloaded = false;
    self.OpenFileManager = function () {


        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {

                preloaded = true;

            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeFive().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');
            if (self.ListeningTypeFive().Id() > 0) {
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
    }
}

function listeningTypeSixController() {
    var self = this;
    var url = "/Api/ListeningTypeSixApi";
    self.ListeningTypeSix = ko.observable(new ListeningTypeSixModel());
    self.ListeningTypeSixs = ko.observableArray([]);
    self.SelectedListeningTypeSix = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Listening Type Six",
        target: "#listeningTypeSixKendoGrid",
        url: url + "/GetListeningTypeSixKendoGrid",
        height: 5000,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            { field: 'Option1', title: "Option1", width: 200, filterable: false },
            { field: 'Option2', title: "Option2", width: 200, filterable: false },
            { field: 'Option3', title: "Option3", width: 200, filterable: false },
            { field: 'Option4', title: "Option4", width: 200, filterable: false },
            { field: 'Option5', title: "Option5", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },

        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeSix(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeSixKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ListeningTypeSix().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeSix().AudioUrl() == "") {
            Riddha.UI.Toast("AudioUrl field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeSix().Option1() == "" || self.ListeningTypeSix().Option2() == "" || self.ListeningTypeSix().Option3() == "" || self.ListeningTypeSix().Option4() == "") {
            Riddha.UI.Toast("Options field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeSix().Option1IsCorrect() == false && self.ListeningTypeSix().Option2IsCorrect() == false && self.ListeningTypeSix().Option3IsCorrect() == false && self.ListeningTypeSix().Option4IsCorrect() == false) {
            return Riddha.UI.Toast("Please choose at least One correct answer !", 0);
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeSix()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeSix()))
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
        self.ListeningTypeSix(new ListeningTypeSixModel({ Id: self.ListeningTypeSix().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedListeningTypeSix() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }

        Riddha.ajax.get(url + "/Get?Id=" + self.SelectedListeningTypeSix().Id, null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.ListeningTypeSix(new ListeningTypeSixModel(ko.toJS(result.Data)));
                    self.ShowModal();
                    self.ModeOfButton('Update');

                };

            });

    };

    self.Delete = function () {
        if (self.SelectedListeningTypeSix() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeSix().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.SelectedListeningTypeSix(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ListeningTypeSixCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeSixCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeSixCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    var preloaded = false;
    self.OpenFileManager = function () {


        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {

                preloaded = true;

            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeSix().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');
            if (self.ListeningTypeSix().Id() > 0) {
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
    }

}

function listeningTypeSevenController() {
    var self = this;
    var url = "/Api/ListeningTypeSevenApi";
    self.ListeningTypeSeven = ko.observable(new ListeningTypeSevenModel());
    self.ListeningTypeSevens = ko.observableArray([]);
    self.SelectedListeningTypeSeven = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Listening Type Seven",
        target: "#listeningTypeSevenKendoGrid",
        url: url + "/GetListeningTypeSevenKendoGrid",
        height: 5000,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'AudioUrl', title: "AudioUrl", width: 200, filterable: false },
            { field: 'TranscriptionText', title: "TranscriptionText", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeSeven(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeSevenKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ListeningTypeSeven().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeSeven().AudioUrl() == "") {
            Riddha.UI.Toast("AudioUrl field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeSeven().TranscriptionText() == "") {
            Riddha.UI.Toast("Transcription Text field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeSeven().Answers() == "") {
            Riddha.UI.Toast("Please provide atleast one correct answer!", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeSeven()))
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
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeSeven()))
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
        self.ListeningTypeSeven(new ListeningTypeSevenModel({ Id: self.ListeningTypeSeven().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedListeningTypeSeven() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.ListeningTypeSeven(new ListeningTypeSevenModel(ko.toJS(self.SelectedListeningTypeSeven())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedListeningTypeSeven() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeSeven().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        //self.Reset();
                        self.SelectedListeningTypeSeven(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ListeningTypeSevenCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeSevenCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeSevenCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };
    var preloaded = false;
    self.OpenFileManager = function () {


        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {

                preloaded = true;

            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeSeven().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');
            if (self.ListeningTypeSeven().Id() > 0) {
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
    }
}

function listeningTypeEightController() {
    var self = this;
    var url = "/Api/ListeningTypeEightApi";
    self.ListeningTypeEight = ko.observable(new ListeningTypeEightModel());
    self.ListeningTypeEights = ko.observableArray([]);
    self.SelectedListeningTypeEight = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "Listening Type Eight",
        target: "#listeningTypeEightKendoGrid",
        url: url + "/GetListeningTypeEightKendoGrid",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.N", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'Title', title: "Title", width: 225, filterable: false },
            { field: 'AudioUrl', title: "Audio Url", width: 200, filterable: false },
            { field: 'Sentence', title: "Answer Sentence", width: 200, filterable: false },
            { field: 'UsedInQuestionSets', title: "Used In", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedListeningTypeEight(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#listeningTypeEightKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.ListeningTypeEight().Title() == "") {
            Riddha.UI.Toast("Title field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeEight().Sentence() == "") {
            Riddha.UI.Toast("Sentence field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeEight().AudioUrl() == "") {
            Riddha.UI.Toast("AudioUrl field can't be blank !", 0);
            return;
        }
        if (self.ListeningTypeEight().BeginWithin() == "") {
            Riddha.UI.Toast("Please provide audio beginning time!", 0);
            return;
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.ListeningTypeEight()))
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();

                        self.CloseModal();
                    };
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
        else if (self.ModeOfButton() == 'Update') {
            Riddha.ajax.put(url, ko.toJS(self.ListeningTypeEight()))
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
        self.ListeningTypeEight(new ListeningTypeEightModel({ Id: self.ListeningTypeEight().Id() }));
        self.ModeOfButton("Create");
    };

    self.Select = function () {
        if (self.SelectedListeningTypeEight() == undefined) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.ListeningTypeEight(new ListeningTypeEightModel(ko.toJS(self.SelectedListeningTypeEight())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Delete = function () {
        if (self.SelectedListeningTypeEight() == undefined) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedListeningTypeEight().Id, null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        //self.Reset();
                        self.SelectedListeningTypeEight(undefined);
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })

    };

    $("#ListeningTypeEightCreationModel").on('hidden.bs.modal', function () {
        self.Reset();
        self.ModeOfButton("Create");
    });

    self.ShowModal = function (mode) {
        $("#ListeningTypeEightCreationModel").modal('show');
    };

    self.CloseModal = function () {
        $("#ListeningTypeEightCreationModel").modal('hide');
        self.Reset();
        self.ModeOfButton("Create");
    };

    var preloaded = false;
    self.OpenFileManager = function () {


        Riddha.UI.modal.show("fileManager", "/Setup/FileManager/Index", preloaded, function (result) {

            if (result == undefined || result == "") {

                preloaded = true;

            }
            else {
                preloaded = true;
                var data = ko.toJS(result);
                self.ListeningTypeEight().AudioUrl(data.URL);
            }

            $("#fileManager").modal('hide');


            if (self.ListeningTypeEight().Id() > 0) {
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
    }

}