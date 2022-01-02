function UsersModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
    self.Address = ko.observable(item.Address || '');
    self.BirthCountry = ko.observable(item.BirthCountry || '');
    self.Email = ko.observable(item.Email || '');
    self.Gender = ko.observable(item.Gender || '');
    self.GenderName = ko.observable(item.GenderName || '');
    self.Institute = ko.observable(item.Institute || '');
    self.InstituteName = ko.observable(item.InstituteName || '');
    self.Occupation = ko.observable(item.Occupation || '');
    self.OccupationName = ko.observable(item.OccupationName || '');
    self.PhoneNumber = ko.observable(item.PhoneNumber || '');
}


function UserCredentialModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
    self.Email = ko.observable(item.Email || '');
    self.Password = ko.observable(item.Password || '');
}
