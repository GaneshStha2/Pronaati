

function VocabularyAndPronunciationBoosterModel(item) {

    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Word = ko.observable(item.Word || '');
    self.WordType = ko.observable(item.WordType || '');
    self.FileUrl = ko.observable(item.FileUrl || '');
    self.WordMeaning = ko.observable(item.WordMeaning || '');
}   