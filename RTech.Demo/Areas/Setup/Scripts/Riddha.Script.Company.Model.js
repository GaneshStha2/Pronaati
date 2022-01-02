function CompanyModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Code = ko.observable(item.Code || '');
    self.Name = ko.observable(item.Name || '').extend({ required: 'Customer Name is Required' });
    self.NameNp = ko.observable(item.NameNp || '');
    self.Address = ko.observable(item.Address || '').extend({ required: 'Customer Address is Required' });
    self.AddressNp = ko.observable(item.AddressNp || '');
    self.ContactNo = ko.observable(item.ContactNo || '').extend({ required: 'Customer ContactNo is Required' });
    self.ContactPerson = ko.observable(item.ContactPerson || '').extend({ required: 'Contact Person is Required' });
    self.ContactPersonNp = ko.observable(item.ContactPersonNp || '');
    self.Email = ko.observable(item.Email || '');
    self.WebUrl = ko.observable(item.WebUrl || '');
    self.PAN = ko.observable(item.PAN || '');
    self.LogoUrl = ko.observable(item.LogoUrl || '');
    self.Status = ko.observable(item.Status || false);
    self.EnableMobile = ko.observable(item.EnableMobile || false);
    self.SoftwarePackageType = ko.observable(item.SoftwarePackageType || 0);
    self.Price = ko.observable(item.Price || 0);
}

function CompanyGridVm(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Code = ko.observable(item.Code || '');
    self.Name = ko.observable(item.Name || '');
    self.NameNp = ko.observable(item.NameNp || '');
    self.Address = ko.observable(item.Address || '');
    self.AddressNp = ko.observable(item.AddressNp || '');
    self.ContactNo = ko.observable(item.ContactNo || '');
    self.ContactPerson = ko.observable(item.ContactPerson || '');
    self.ContactPersonNp = ko.observable(item.ContactPersonNp || '');
    self.Email = ko.observable(item.Email || '');
    self.WebUrl = ko.observable(item.WebUrl || '');
    self.PAN = ko.observable(item.PAN || '');
    self.LogoUrl = ko.observable(item.LogoUrl || '');
    self.Status = ko.observable(item.Status || false);
}

function CompanyLoginModel(item) {
    var self = this;
    item = item || {};
    self.CompanyId = ko.observable(item.CompanyId || 0);
    self.UserName = ko.observable(item.UserName || '');
    self.Password = ko.observable(item.Password || '');
}

function CompanyLicenseModel(item) {
    var self = this;
    item = item || {};
    self.CompanyId = ko.observable(item.CompanyId || 0);
    self.IssueDate = ko.observable(item.IssueDate || '');
    self.ExpiryDate = ko.observable(item.ExpiryDate || '');
    self.LicensePeriodInDays = ko.observable(item.LicensePeriodInDays || 0);
    self.RenewDays = ko.observable(item.RenewDays || 0);
}

//function ListeningTypeEightModel(item) {
//    var self = this;
//    item = item || {};
//    self.Id = ko.observable(item.Id || 0);
//    self.Title = ko.observable(item.Title || '');
//    self.AudioUrl = ko.observable(item.AudioUrl || '');
//}