function QuestionSetModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.QuestionSetName = ko.observable(item.QuestionSetName || '');
    self.QuestionSetCode = ko.observable(item.QuestionSetCode || '');
    self.IsUnscored = ko.observable(item.IsUnscored || false);
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDate = ko.observable(item.CreatedDate || '');
    self.LastModifiedDate = ko.observable(item.LastModifiedDate || '');
    self.LastModifiedBy = ko.observable(item.LastModifiedBy || 0);
    self.ValidDuration = ko.observable(item.ValidDuration || 0);
}

function QuestionSetDropDownModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.QuestionTitle = ko.observable(item.QuestionTitle || '');
    self.QuestionId = ko.observable(item.QuestionId || 0);
}

