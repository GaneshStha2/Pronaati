function QuestionPackageModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.PackageCode = ko.observable(item.PackageCode || '');
    self.PackageName = ko.observable(item.PackageName || '');
    self.PackagePrice = ko.observable(item.PackagePrice || 0);
    self.CreatedDate = ko.observable(item.CreatedDate || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.LastModifiedDate = ko.observable(item.LastModifiedDate || '');
    self.LastModifiedBy = ko.observable(item.LastModifiedBy || 0);
    self.ExpiryDuration = ko.observable(item.ExpiryDuration || 0);
   
}

function QuestionSetsDropDownModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.QuestionSetId = ko.observable(item.Id || 0);
    self.QuestionSetName = ko.observable(item.QuestionSetName || '');
    self.QuestionSetCode = ko.observable(item.QuestionSetCode || '');
    self.ValidDuration = ko.observable(item.ValidDuration || 0);
}