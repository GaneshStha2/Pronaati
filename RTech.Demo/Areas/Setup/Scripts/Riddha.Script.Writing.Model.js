function WritingTypeOneModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.Question = ko.observable(item.Question || '');
    self.TotalTime = ko.observable(item.TotalTime || 0);
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
}

function WritingTypeTwoModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.Question = ko.observable(item.Question || '');
    self.TotalTime = ko.observable(item.TotalTime || 0);
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
}


function MockTestWritingEvaluationModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.ObtainedMarks = ko.observable(item.ObtainedMarks || 0);
    self.AnswerText = ko.observable(item.AnswerText || '');
    self.QuestionText = ko.observable(item.QuestionText || '');
    self.StudentName = ko.observable(item.StudentName || '');
    self.PackageName = ko.observable(item.PackageName || '');
    self.QuestionSetName = ko.observable(item.QuestionSetName || '');
    self.MockTestReportId = ko.observable(item.MockTestReportId || 0);
    self.QuestionPackageMasterId = ko.observable(item.QuestionPackageMasterId || 0);
    self.QuestionSetMasterId = ko.observable(item.QuestionSetMasterId || 0);
    self.QuestionId = ko.observable(item.QuestionId || 0);
    self.QuestionType = ko.observable(item.QuestionType || 0);
    self.StudentInformationId = ko.observable(item.StudentInformationId || 0);
}
