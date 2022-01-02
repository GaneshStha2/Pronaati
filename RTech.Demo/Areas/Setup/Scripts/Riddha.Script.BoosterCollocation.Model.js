function BoosterCollocationModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Question = ko.observable(item.Question || '');
    self.QuestionText = ko.observable(item.QuestionText || '');
    self.OptionStatement = ko.observable(item.OptionStatement || '');
}

function BoosterCollocationOptionsModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.EBoosterCollocationMasterId = ko.observable(item.EBoosterCollocationMasterId || 0);
    self.Options = ko.observable(item.Options || '');
    self.IsAnswer = ko.observable(item.IsAnswer || false);

}