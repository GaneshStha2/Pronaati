
function SegmentModel(item) {
    var self = this;
    item = item || {};

    self.Id = ko.observable(item.Id || 0);
    self.Name = ko.observable(item.Name || '');
    self.LanguageId = ko.observable(item.LanguageId || 0);
    self.LanguageName = ko.observable(item.LanguageName || '');

    self.FromLanguageId = ko.observable(item.FromLanguageId || 0);
    self.ToLanguageId = ko.observable(item.ToLanguageId || 0);
    self.ToLanguageName = ko.observable(item.FromLanguageName || '');
    self.FromLanguageName = ko.observable(item.FromLanguageName || '');
}
