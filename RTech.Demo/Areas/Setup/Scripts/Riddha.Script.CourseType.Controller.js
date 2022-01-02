/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.coursetype.model.js" />


function courseTypeController() {
    var self = this;
    var url = '/Api/CourseTypeApi';
    self.CourseType = ko.observable(new CourseTypeModel());
    self.CourseTypes = ko.observableArray([]);
    self.SelectedCourseType = ko.observable();
    self.ModeOfButton = ko.observable('Create');

    self.KendoGridOptions = {
        title: "CourseType",
        target: "#courseTypeKendoGrid",
        url: "/Api/CourseTypeApi/GetKendoGrid",
        height: 490,
        paramData: {},
        multiSelect: true,
        group: true,
        columns: [
            { field: '#', title: lang == "ne" ? "S.No" : "S.No", width: 60, template: "#= ++record #", filterable: false },
            { field: 'Code', title: "Language Code", filterable: true },
            { field: 'Name', title: "Language", filterable: true },
            
        ],
        SelectedItem: function (item) {
            self.SelectedCourseType(new CourseTypeModel(item));
        },
        SelectedItems: function (items) {

        },
    };

    self.RefreshKendoGrid = function () {
        self.SelectedCourseType(new CourseTypeModel());
        $("#courseTypeKendoGrid").getKendoGrid().dataSource.read();
    };

    self.CreateUpdate = function () {
        if (self.CourseType().Name() == '') {
            return Riddha.UI.Toast("Please enter Name", 0)
        }
        if (self.CourseType().Code() == '') {
            return Riddha.UI.Toast("Please enter Code", 0)
        }
        if (self.ModeOfButton() == 'Create') {
            Riddha.ajax.post(url, ko.toJS(self.CourseType()))
                .done(function (result) {
                    self.RefreshKendoGrid();
                    Riddha.UI.Toast(result.Message, result.Status);
                    self.Reset();
                    self.CloseModal();
                });
        }
        else if (self.ModeOfButton() == 'Update') {
            Riddha.ajax.put(url, ko.toJS(self.CourseType()))
                .done(function (result) {
                    self.RefreshKendoGrid();
                    Riddha.UI.Toast(result.Message, result.Status);
                    self.Reset();
                    self.CloseModal();
                });
        }
    };

    self.Select = function (model) {
        if (self.SelectedCourseType() == undefined || self.SelectedCourseType().length > 1 || self.SelectedCourseType().Id() == 0) {
            Riddha.UI.Toast("Please select row to edit.", 0);
            return;
        }
        self.CourseType(new CourseTypeModel(ko.toJS(self.SelectedCourseType())));
        self.ShowModal();
        self.ModeOfButton('Update');
    };

    self.Reset = function () {
        self.CourseType(new CourseTypeModel({ Id: self.CourseType().Id() }));
        self.ModeOfButton("Create");
    };
    self.Delete = function (model) {
        if (self.SelectedCourseType() == undefined || self.SelectedCourseType().length > 1 || self.SelectedCourseType().Id() == 0) {
            Riddha.UI.Toast("Please select row to delete.", 0);
            return;
        }
        Riddha.UI.Confirm("DeleteConfirm", function () {
            Riddha.ajax.delete(url + "/" + self.SelectedCourseType().Id(), null)
                .done(function (result) {
                    if (result.Status == 4) {
                        self.RefreshKendoGrid();
                        self.Reset();
                    }
                    Riddha.UI.Toast(result.Message, result.Status);
                });
        })
    };

    $("#CourseTypeModel").on('hidden.bs.modal', function () {
        self.Reset();
    });
    self.CloseModal = function () {
        $("#CourseTypeModel").modal('hide');
        self.Reset();
    };

    self.ShowModal = function () {
        $("#CourseTypeModel").modal('show');
    };

    self.CheckUniqueCode = function (model) {
        Riddha.ajax.get(url + "/" + "IsUniqueCode?Code=" + self.CourseType().Code() + "&Id=" + self.CourseType().Id(), null)
            .done(function (result) {
                if (result == false) {
                    debugger
                    self.CourseType().Code('');
                    Riddha.UI.Toast("Course Type Code has been taken", 0);
                    return;
                }
            });
    }

}