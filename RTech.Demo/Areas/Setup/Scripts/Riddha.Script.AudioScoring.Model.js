function AudioScoringModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.MockTestId = ko.observable(item.MockTestId || 0);
    self.StudentId = ko.observable(item.StudentId || 0);
    //self.DialogueId = ko.observable(item.DialogueId || 0);
    self.StudentName = ko.observable(item.StudentName || '');
    self.MockTestName = ko.observable(item.MockTestName || 0);
    self.IsScored = ko.observable(item.IsScored || false);
    self.DialogueScore = ko.observable(item.DialogueScore || 0)
    self.PackageId = ko.observable(item.PackageId || 0)
    //self.TotalScore = ko.computed(function () {
    //    return parseInt(self.Segment1Score()) + parseInt(self.Segment2Score());
    //});
}

function AudioScoringDetailModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.NaatiScoreId = ko.observable(item.NaatiScoreId || 0);

    //self.SegmentId = ko.observable(item.SegmentId || 0);
    //self.SegmentName = ko.observable(item.SegmentName || '');

    self.QuestionSetId = ko.observable(item.QuestionSetId || 0);
    self.QuestionSetName = ko.observable(item.QuestionSetName || false);
    self.QuestionId = ko.observable(item.QuestionId || 0)
    self.QuestionAudioUrl = ko.observable(item.QuestionAudioUrl || '')
    self.CorrectAnswerUrl = ko.observable(item.CorrectAnswerUrl || '')
    self.GivenAnswerUrl = ko.observable(item.GivenAnswerUrl || '')
    self.QuestionScore = ko.observable(item.QuestionScore || 0)

}