

function MasterTopicSentenceBoosterModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.QuestionTitle = ko.observable(item.QuestionTitle || '');
    self.Question = ko.observable(item.Question || '');
    self.OptionStatement = ko.observable(item.OptionStatement || '');

}

function MasterTopicSentenceBoosterOptionDetailModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.MasterTopicSentenceBoosterMasterId = ko.observable(item.MasterTopicSentenceBoosterMasterId || 0);
    self.Options = ko.observable(item.Options || '');
    self.IsCorrectAnswer = ko.observable(item.IsCorrectAnswer || false);
}