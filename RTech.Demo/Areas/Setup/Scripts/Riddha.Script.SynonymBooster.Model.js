

function SynonymBoosterModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Word = ko.observable(item.Word || '');
    self.WordType = ko.observable(item.WordType || '');
    self.Question = ko.observable(item.Question || '');
}

function SynonymBoosterOptionsModel(item) {


    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Options = ko.observable(item.Options || '');
    self.IsAnswer = ko.observable(item.IsAnswer || false);
    self.SynonymBoosterMasterId = ko.observable(item.SynonymBoosterMasterId || 0);
}