/// <reference path="../../../scripts/bootstrap-dialog.js" />
/// <reference path="../../../scripts/knockout-2.3.0.js" />
/// <reference path="../../../scripts/app/globals/riddha.globals.ko.js" />
/// <reference path="riddha.script.viewstudentsmocktestreport.model.js" />



function viewStudentsMockTestReportController() {
    var self = this;
    var url = "/Api/ViewStudentsMockTestReportApi";
    self.ViewMockTestReport = ko.observable(new MockTestReportModel());
    self.SelectedViewMockTestReport = ko.observable();

    

    self.KendoGridOptions = {
        title: "Mock Test Reports ",
        target: "#MockTestReportKendoGrid",
        url: url + "/GetMockTestReportsList",
        height: 500,
        paramData: {},
        multiselect: false,
        group: true,
        columns: [
            { field: '#', title: "S.No", width: 80, template: "#= ++record #", filterable: false, },
            { field: 'StudentName', title: "Student Name", width: 225},
            { field: 'QuestionSetName', title: "Question Set Name", width: 200},
            { field: 'StudentEmailAddress', title: "Student Email Address", width: 200},
            { field: 'TestTakenDate', title: "Test Taken Date", width: 200},
            { field: 'SpeakingMarks', title: "Speaking Marks", width: 200, filterable: false },
            { field: 'WritingMarks', title: "Writing Marks", width: 200, filterable: false },
            { field: 'ReadingMarks', title: "Reading Marks", width: 200, filterable: false },
            { field: 'ListeningMarks', title: "Listening Marks", width: 200, filterable: false },
            { field: 'TotalMarks', title: "Total Marks", width: 200, filterable: false },
            { field: 'IsReportReady', title: "Is Report Ready ?", width: 200, filterable: false},
        ],
        SelectedItem: function (item) {
            self.SelectedViewMockTestReport(item)
        },
        SelectedItems: function (items) {
        },
    };

    self.RefreshKendoGrid = function () {
        $("#mockTestReportKendoGrid").getKendoGrid().dataSource.read();
    };


    self.ViewReport = function () {
        debugger;
        if (self.SelectedViewMockTestReport() == undefined) {
            Riddha.UI.Toast("Please select a row to view the report.", 0);
            return;
        }
        if (self.SelectedViewMockTestReport().IsReportReady == false) {
            Riddha.UI.Toast("Report is not ready to view yet.", 0);
            return;
        }
        var mockTestReportId = self.SelectedViewMockTestReport().Id;
        var studentId = self.SelectedViewMockTestReport().StudentId;
        //window.location.href = "/Student/Dashboard/ViewMockTestReport?mockTestReportId=" + mockTestReportId + "&studentId=" + studentId;

        window.open( '/Student/Dashboard/ViewMockTestReport?mockTestReportId=' + mockTestReportId + "&studentId=" + studentId, '_blank');
    };



}