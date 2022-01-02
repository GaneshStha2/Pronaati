
function NaatiMockTestModel(item) {
	var self = this;
	item = item || {};
	self.Id = ko.observable(item.Id || 0);
	self.Code = ko.observable(item.Code || '');
	self.Title = ko.observable(item.Title || '');
	self.Duration = ko.observable(item.Duration || 0);
    self.Description = ko.observable(item.Description || '');
	self.ImageURL = ko.observable(item.ImageURL || '');
	self.CreatedDate = ko.observable(item.CreatedDate || '');
	self.Price = ko.observable(item.Price || 0);
	self.CreatedBy = ko.observable(item.CreatedBy || 0);
	self.LanguageTypeId = ko.observable(item.LanguageTypeId || 0);
	self.PackageTypeId = ko.observable(item.PackageTypeId || 0);
}





function NaatiMockTestDetailModel(item) {
	var self = this;
	item = item || {};
	self.Id = ko.observable(item.Id || 0);
	self.QuestionSetId = ko.observable(item.QuestionSetId || 0);
    self.NaatiMockTestMasterId = ko.observable(item.NaatiMockTestMasterId || 0);
    self.Name = ko.observable(item.Name ||'');
}
