function PackageMasterModel(item) {
    var self = this;
    item = item || {};

    self.Id = ko.observable(item.Id || 0);
    self.Code = ko.observable(item.Code || '');
    self.PackageTitle = ko.observable(item.PackageTitle || '');
    self.Description = ko.observable(item.Description || '');
    self.CreatedDate = ko.observable(item.CreatedDate || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);

}

function PackageDetailsModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.FileName = ko.observable(item.FileName || '');
    self.FileId = ko.observable(item.FileId || 0);
    self.FileType = ko.observable(item.FileType || '');
    self.FileUrl = ko.observable(item.FileUrl || '');
    self.EPackageId = ko.observable(item.EPackageId || '');
}