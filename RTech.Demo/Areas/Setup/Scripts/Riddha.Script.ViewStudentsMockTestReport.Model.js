function MockTestReportModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.StudentId = ko.observable(item.StudentId || 0);
    self.IsReportReady = ko.observable(item.IsReportReady || false);
}