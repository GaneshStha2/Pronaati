
function NaatiPackageMasterModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Code = ko.observable(item.Code || '');
    self.Name = ko.observable(item.Name || '');
    self.Duration = ko.observable(item.Duration || 0);
    self.Description = ko.observable(item.Description || '');
    self.ImageURL = ko.observable(item.ImageURL || '');
    self.CreatedDate = ko.observable(item.CreatedDate || '');
    self.Price = ko.observable(item.Price || 0);
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.LanguageTypeId = ko.observable(item.LanguageTypeId || 0);
    self.PackageTypeId = ko.observable(item.PackageTypeId || 0);

}

function PackageFileModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.FileName = ko.observable(item.FileName || '');
    self.FileUrl = ko.observable(item.FileUrl || '');
    self.FileId = ko.observable(item.FileId || 0);
    self.FileType = ko.observable(item.FileType || undefined);
    self.FileTypeName = ko.observable(item.FileTypeName || '');

}

function DropDownTypeModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
}



function PackageMockTestModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.QuestionSetId = ko.observable(item.QuestionSetId || 0);
    self.NaatiPackageId = ko.observable(item.NaatiPackageId || 0);
}
