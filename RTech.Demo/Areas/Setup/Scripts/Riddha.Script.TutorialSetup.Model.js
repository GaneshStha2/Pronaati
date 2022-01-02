
function TutorialSetupModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.Description = ko.observable(item.Description || '');
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.LastModifiedDateTime = ko.observable(item.LastModifiedDateTime || '');
    self.LastModifiedBy = ko.observable(item.LastModifiedBy || 0);
}


function TutorialDetailModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.FileName = ko.observable(item.FileName || '')
    self.TutorialID = ko.observable(item.TutorialID || '');
    self.FileURL = ko.observable(item.FileURL || '');


}
