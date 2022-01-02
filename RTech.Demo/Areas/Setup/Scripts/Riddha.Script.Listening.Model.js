function ListeningTypeOneModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.TimeBeforeAudio = ko.observable(item.TimeBeforeAudio || 0);
    self.TotalTime = ko.observable(item.TotalTime || '');
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);

}

function ListeningTypeTwoModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.Question = ko.observable(item.Question || '');
    self.Response1 = ko.observable(item.Response1 || '');
    self.Response2 = ko.observable(item.Response2 || '');
    self.Response3 = ko.observable(item.Response3 || '');
    self.Response4 = ko.observable(item.Response4 || '');
    self.Response5 = ko.observable(item.Response5 || '');
    self.Response6 = ko.observable(item.Response6 || '');
    self.Response7 = ko.observable(item.Response7 || '');
    self.Response1IsCorrect = ko.observable(item.Response1IsCorrect || false);
    self.Response2IsCorrect = ko.observable(item.Response2IsCorrect || false);
    self.Response3IsCorrect = ko.observable(item.Response3IsCorrect || false);
    self.Response4IsCorrect = ko.observable(item.Response4IsCorrect || false);
    self.Response5IsCorrect = ko.observable(item.Response5IsCorrect || false);
    self.Response6IsCorrect = ko.observable(item.Response6IsCorrect || false);
    self.Response7IsCorrect = ko.observable(item.Response7IsCorrect || false);
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
    self.IsCorrectAnswer = ko.observable(item.IsCorrectAnswer || false); 
}

function ListeningTypeThreeModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.QuestionText = ko.observable(item.QuestionText || '');
    self.Answers = ko.observable(item.Answers || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
}

function ListeningTypeThreeAnswersModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Answer = ko.observable(item.Answer || '');
}

function ListeningTypeFourModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.Paragraph1 = ko.observable(item.Paragraph1 || '');
    self.Paragraph2 = ko.observable(item.Paragraph2 || '');
    self.Paragraph3 = ko.observable(item.Paragraph3 || '');
    self.Paragraph4 = ko.observable(item.Paragraph4 || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
    self.IsCorrectAnswer = ko.observable(item.IsCorrectAnswer || false);

    self.Paragraph1IsCorrect = ko.observable(item.Paragraph1IsCorrect || false);
    self.Paragraph2IsCorrect = ko.observable(item.Paragraph2IsCorrect || false);
    self.Paragraph3IsCorrect = ko.observable(item.Paragraph3IsCorrect || false);
    self.Paragraph4IsCorrect = ko.observable(item.Paragraph4IsCorrect || false);
    
}

function ListeningTypeFiveModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.Question = ko.observable(item.Question || '');
    self.Option1 = ko.observable(item.Option1 || '');
    self.Option2 = ko.observable(item.Option2 || '');
    self.Option3 = ko.observable(item.Option3 || '');
    self.Option4 = ko.observable(item.Option4 || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
    self.IsCorrectAnswer = ko.observable(item.IsCorrectAnswer || false);

    self.Option1IsCorrect = ko.observable(item.Option1IsCorrect || false);
    self.Option2IsCorrect = ko.observable(item.Option2IsCorrect || false);
    self.Option3IsCorrect = ko.observable(item.Option3IsCorrect || false);
    self.Option4IsCorrect = ko.observable(item.Option4IsCorrect || false);

}

function ListeningTypeSixModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.Option1 = ko.observable(item.Option1 || '');
    self.Option2 = ko.observable(item.Option2 || '');
    self.Option3 = ko.observable(item.Option3 || '');
    self.Option4 = ko.observable(item.Option4 || '');
    self.Option5 = ko.observable(item.Option5 || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
    self.IsCorrectAnswer = ko.observable(item.IsCorrectAnswer || false);

    self.Option1IsCorrect = ko.observable(item.Option1IsCorrect || false);
    self.Option2IsCorrect = ko.observable(item.Option2IsCorrect || false);
    self.Option3IsCorrect = ko.observable(item.Option3IsCorrect || false);
    self.Option4IsCorrect = ko.observable(item.Option4IsCorrect || false);
    self.Option5IsCorrect = ko.observable(item.Option5IsCorrect|| false);

}

function ListeningTypeSevenModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.TranscriptionText = ko.observable(item.TranscriptionText || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
    self.Answers = ko.observable(item.Answers || '');
}

function ListeningTypeEightModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.Sentence = ko.observable(item.Sentence || '');
    self.AudioUrl = ko.observable(item.AudioUrl || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    self.BeginWithin = ko.observable(item.BeginWithin || 0);
}