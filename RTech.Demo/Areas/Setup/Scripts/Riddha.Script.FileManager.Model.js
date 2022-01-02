

function FolderModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
    self.ParentId = ko.observable(item.ParentId || 0);
}


function FilesModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
    self.FolderId = ko.observable(item.FolderId || 0);
    self.URL = ko.observable(item.URL || '');
    self.FileExtension = ko.observable(item.FileExtension || '');
    self.FileExtensionIconPath = ko.observable(item.FileExtensionIconPath || '');

}

function GlobalDropdownModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
}