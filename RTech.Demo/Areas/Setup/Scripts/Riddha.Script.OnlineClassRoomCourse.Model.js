function OnlineClassRoomCourseMasterModel(item) {

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
    self.CourseTypeId = ko.observable(item.CourseTypeId || 0);
    self.CourseTypeName = ko.observable(item.CourseTypeName || '');
    self.IsPracticeEnabled = ko.observable(item.IsPracticeEnabled || false);

}

function OnlineClassRoomCourseDetailsModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.FileName = ko.observable(item.FileName || '');
    self.FileUrl = ko.observable(item.FileUrl || '');
    self.FileId = ko.observable(item.FileId || 0);
    self.FileType = ko.observable(item.FileType || undefined);
    self.FileTypeName = ko.observable(item.FileTypeName || '');
    self.OnlineClasRoomCourseId = ko.observable(item.OnlineClasRoomCourseId || '')
}

function DropDownCourseTypeModel(item) {
    var self = this;
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
}
