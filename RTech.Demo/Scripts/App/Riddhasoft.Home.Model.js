﻿/// <reference path="../../Scripts/App/Globals/Riddha.Globals.ko.js" />
/// <reference path="../../Scripts/knockout-3.4.2.js" />
/// <reference path="Riddhasoft.Home.Controller.js" />

function AttendanceInfoModel(item) {
    var self = this;
    item = item || {};
    self.PresentCount = ko.observable(item.PresentCount || 0);
    self.AbsentCount = ko.observable(item.AbsentCount || 0);
    self.LateInCount = ko.observable(item.LateInCount || 0);
    self.OnLeaveCount = ko.observable(item.OnLeaveCount || 0);
    self.EmployeeCount = ko.observable(item.EmployeeCount || 0);
}

function CurrentUserAttendanceModel(item) {
    var self = this;
    item = item || {};
    self.PlannedTime = ko.observable(item.PlannedTime || '');
    self.Attendance = ko.observable(item.Attendance || '');
    self.PresentCount = ko.observable(item.PresentCount || 0);
    self.AbsentCount = ko.observable(item.AbsentCount || 0);
    self.HolidayCount = ko.observable(item.HolidayCount || 0);
}

function NewsModel(item) {
    var self = this;
    item = item || {};
    self.Type = ko.observable(item.Type || '');
    self.Title = ko.observable(item.Title || '');
    self.Desc = ko.observable(item.Desc || '');
    self.Date = ko.observable(item.Date || 0);
}
function ResellerDeviceInfoModel(item) {
    var self = this;
    item = item || {};
    self.ResellerDeviceCount = ko.observable(item.ResellerDeviceCount || 0);
    self.CustomerDeviceCount = ko.observable(item.CustomerDeviceCount || 0);
    self.DamageDeviceCount = ko.observable(item.DamageDeviceCount || 0);
}
function ResellerCompanyModel(item) {
    var self = this;
    item = item || {};
    self.Name = ko.observable(item.Name || '');
    self.Address = ko.observable(item.Address || '');
    self.Quantity = ko.observable(item.Quantity || 0);
}
function OwnerDeviceInfoModel(item) {
    var self = this;
    item = item || {};
    self.NewDeviceCount = ko.observable(item.NewDeviceCount || 0);
    self.ResellerDeviceCount = ko.observable(item.ResellerDeviceCount || 0);
    self.CustomerDeviceCount = ko.observable(item.CustomerDeviceCount || 0);
    self.DamageDeviceCount = ko.observable(item.DamageDeviceCount || 0);
}
function OwnerResellerModel(item) {
    var self = this;
    item = item || {};
    self.Name = ko.observable(item.Name || '');
    self.Address = ko.observable(item.Address || '');
    self.Quantity = ko.observable(item.Quantity || 0);
}
function MenuModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Code = ko.observable(item.Code || '');
    self.Name = ko.observable(item.Name || "");
    self.NameNp = ko.observable(item.NameNp || "");
    self.ParentCode = ko.observable(item.ParentCode || '');
    self.IsGroup = ko.observable(item.IsGroup || false);
    self.MenuUrl = ko.observable(item.MenuUrl || '');
    self.MenuIconCss = ko.observable(item.MenuIconCss || '');
    self.Children = ko.observableArray(item.Menus);
}

function LicenseInfoVm(item) {
    var self = this;
    item = item || {};
    self.LicensePeriod = ko.observable(item.LicensePeriod || 0);
    self.IssueDate = ko.observable(item.IssueDate || '').extend({ date: '' });
    self.ExpiryDate = ko.observable(item.ExpiryDate || '').extend({ date: '' });
    self.SoftwarePackageType = ko.observable(item.SoftwarePackageType || '');
    self.Price = ko.observable(item.Price || 0);
    self.CompanyLogo = ko.observable(item.CompanyLogo || '');
    self.CompanyName = ko.observable(item.CompanyName || '');
}

function ChangePasswordModel(item) {
    var self = this;
    item = item || {};
    self.UserName = ko.observable(item.UserName || '');
    self.CurrentPassword = ko.observable('');
    self.NewPassword = ko.observable('');
    self.ConfirmPassword = ko.observable('');
}

function TableInfoModel(item) {
    var self = this;
    item = item || {};
    self.TableNo = ko.observable(item.TableNo || '');
    self.Capacity = ko.observable(item.Capacity || 0);
    self.Description = ko.observable(item.Description || '');
    self.Status = ko.observable(item.Status || 0);
}

function DashboardInfoCountModel(item) {
    var self = this;
    item = item || {};
    self.PendingOrders = ko.observable(item.PendingOrders || 0);
    self.TotalOrders = ko.observable(item.TotalOrders || 0);
    self.FreeTables = ko.observable(item.FreeTables || 0);
    self.ReservedTables = ko.observable(item.ReservedTables || 0);

}