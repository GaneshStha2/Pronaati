
function segmentController() {

    var self = this;
    var url = '/Api/SegmentApi';
    self.Segment = ko.observable(new SegmentModel());
    self.SegmentArray = ko.observableArray([]);

    self.SelectedSegment = ko.observable();
    self.SelectedSegmentDetail = ko.observable();

    self.ModeOfButton = ko.observable('Create');

    self.Languages = ko.observableArray([]); 
    self.LanguageId = ko.observable(0);

    getLanguage();
    function getLanguage() {
        Riddha.ajax.get(url + "/GetLanguages", null)
            .done(function (result) {
                debugger;
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.Data), DropDownCourseTypeModel);
                self.Languages(data);
            });
    };



    self.KendoGridOptions = {
        title: "Segments",
        target: "#segmentKendoGrid",
        url: "/Api/SegmentApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: false,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'LanguageName', title: "Language", filterable: true },
            { field: 'Name', title: "Segment Name", filterable: true },
            { field: 'FromLanguageName', title: "From", filterable: false },
            { field: 'ToLanguageName', title: "To", filterable: false },
        ],
        SelectedItem: function (item) {
            self.SelectedSegment(new SegmentModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedSegment(new SegmentModel());
        $("#segmentKendoGrid").getKendoGrid().dataSource.read();
    };


    //Details Crud

    self.DetailModeOfButton = ko.observable('Add');
    self.AddSegment = function (item) {
        if (self.Segment().Name() == '') {
            Riddha.UI.Toast("Please enter Name..", 0)
            return;
        }
        if (self.Segment().FromLanguageId() == 0 || self.Segment().FromLanguageId() == undefined) {
            Riddha.UI.Toast("Please select From Language ..", 0)
            return;
        }
        if (self.Segment().ToLanguageId() == 0 || self.Segment().ToLanguageId() == undefined) {
            Riddha.UI.Toast("Please select To Language ..", 0)
            return;
        }


        if (self.DetailModeOfButton() == 'Add') {
            self.SegmentArray.push(new SegmentModel(ko.toJS(item)));
            self.ResetSegment();
        }
        else {
            self.SegmentArray.replace(self.SelectedSegmentDetail(), new SegmentModel(ko.toJS(item)));
            self.ResetSegment();
        }
    };

    self.ResetSegment = function () {
        self.Segment(new SegmentModel());
        self.DetailModeOfButton = ko.observable('Add');
    };

    self.SelectSegment = function (model) {
        self.SelectedSegmentDetail(model);
        self.Segment(new SegmentModel(ko.toJS(model)));
        self.DetailModeOfButton('Update');
    };

    self.DeleteSegmentDetail = function (model) {
        self.SegmentArray.remove(model);
    };


    //Main Crud

    self.CreateUpdate = function () {

        if (self.LanguageId() == 0 || self.LanguageId() == undefined) {

            Riddha.UI.Toast("Please select Language..", 0)
            return;
        }

        if (self.SegmentArray().length <= 0) {
            Riddha.UI.Toast("Atleast one Segment accessory is required..", 0)
            return;
        }

        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS({ LanguageId: self.LanguageId(), Segments: self.SegmentArray() }))
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

            Riddha.ajax.put(url, ko.toJS({ LanguageId: self.LanguageId(), Segments: self.SegmentArray() }))
                .done(function (result) {
                    if (result.Status == 4) {
                        self.ModeOfButton("Create");
                        self.RefreshKendoGrid();
                        self.CloseModal();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        }
    };

    self.Reset = function () {
        self.Segment(new SegmentModel());
        self.SegmentArray([]);
        self.ModeOfButton("Create");
        self.LanguageId(undefined);
    }

    self.Delete = function (model) {
        if (self.SelectedSegment() === undefined || self.SelectedSegment().length > 1 || self.SelectedSegment().Id() === 0) {

            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedSegment().Id(), null)
                .done(function (result) {
                    if (result.Status === 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        });
    };

    self.Select = function (model) {
        if (self.SelectedSegment() === undefined || self.SelectedSegment().length > 1 || self.SelectedSegment().Id() === 0) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        Riddha.ajax.get(url + "/GetSegmentsFromLanguageId?languageId=" + self.SelectedSegment().LanguageId(), null)
            .done(function (result) {
                if (result.Status == 4) {
                    self.LanguageId(result.Data.LanguageId);

                    self.SegmentArray(Riddha.ko.global.arrayMap(result.Data.Segments, SegmentModel));
                    self.ModeOfButton('Update');
                    self.ShowModal();
                }

            });
    }

    self.GetLanguageName = function (id) {
        var mapped = ko.utils.arrayFirst(ko.toJS(self.Languages()), function (data) {
            return data.Id == id();
        });
        return mapped = (mapped || { Name: '' }).Name;
    };




    self.ShowModal = function () {
        $("#segmentCreationModal").modal('show');
    };
    self.CloseModal = function () {
        $("#segmentCreationModal").modal('hide');
    };

    $("#segmentCreationModal").on('hidden.bs.modal', function () {
        self.Reset();
    });
}