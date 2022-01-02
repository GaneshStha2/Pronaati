
function MocktTestQuestionMasterModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Code = ko.observable(item.Code || '');
    self.LanguageTypeId = ko.observable(item.LanguageTypeId || 0);
    self.DialogueId = ko.observable(item.DialogueId || 0);
    self.PreviousDialogueId = ko.observable(item.PreviousDialogueId || 0);
    self.Title = ko.observable(item.Title || '');
    self.Description = ko.observable(item.Description || '');
    self.SegmentId = ko.observable(item.SegmentId || 0);
}
   
function MockTestQuestionDetailModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.QuestionAudioUrl = ko.observable(item.QuestionAudioUrl || '');
    self.Description = ko.observable(item.Description || '');
    self.AnswerAudioUrl = ko.observable(item.AnswerAudioUrl || '');
    self.SegmentId = ko.observable(item.SegmentId || 0);
    self.Order = ko.observable(item.Order || 0);
    self.SegmentName = ko.observable(item.SegmentName || "");
}