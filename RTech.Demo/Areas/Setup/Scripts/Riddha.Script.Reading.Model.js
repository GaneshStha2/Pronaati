function ReadingTypeOneModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.Title = ko.observable(item.Title || '');
    self.ReadingText = ko.observable(item.ReadingText || '');
    self.Question = ko.observable(item.Question || '');
    self.Response1 = ko.observable(item.Response1 || '');
    self.Response2 = ko.observable(item.Response2 || '');
    self.Response3 = ko.observable(item.Response3 || '');
    self.Response4 = ko.observable(item.Response4 || '');
    self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');

    self.Response1IsCorrect = ko.observable(item.Response1IsCorrect || false);
    self.Response2IsCorrect = ko.observable(item.Response2IsCorrect || false);
    self.Response3IsCorrect = ko.observable(item.Response3IsCorrect || false);
    self.Response4IsCorrect = ko.observable(item.Response4IsCorrect || false);
    self.Response5IsCorrect = ko.observable(item.Response5IsCorrect || false);
}

function ReadingTypeTwoModel(item) {
	var self = this;
	item = item || {};
	self.Id = ko.observable(item.Id || 0);
	self.Title = ko.observable(item.Title || '');
	self.ReadingText = ko.observable(item.ReadingText || '');
	self.Question = ko.observable(item.Question || '');
	self.Response1 = ko.observable(item.Response1 || '');
	self.Response2 = ko.observable(item.Response2 || '');
	self.Response3 = ko.observable(item.Response3 || '');
	self.Response4 = ko.observable(item.Response4 || '');
    self.Response5 = ko.observable(item.Response5 || '');
    self.Response6 = ko.observable(item.Response6 || '');
    self.Response7 = ko.observable(item.Response7 || '');
	self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');

    self.Response1IsCorrect = ko.observable(item.Response1IsCorrect || false);
    self.Response2IsCorrect = ko.observable(item.Response2IsCorrect || false);
    self.Response3IsCorrect = ko.observable(item.Response3IsCorrect || false);
    self.Response4IsCorrect = ko.observable(item.Response4IsCorrect || false);
    self.Response5IsCorrect = ko.observable(item.Response5IsCorrect || false);
    self.Response6IsCorrect = ko.observable(item.Response6IsCorrect || false);
    self.Response7IsCorrect = ko.observable(item.Response7IsCorrect || false);
}

function ReadingTypeThreeModel(item) {
	var self = this;
	item = item || {};
	self.Id = ko.observable(item.Id || 0);
	self.Title = ko.observable(item.Title || '');
	self.QuestionSource1 = ko.observable(item.QuestionSource1 || '');
	self.QuestionSource2 = ko.observable(item.QuestionSource2 || '');
	self.QuestionSource3 = ko.observable(item.QuestionSource3 || '');
	self.QuestionSource4 = ko.observable(item.QuestionSource4 || '');
    self.QuestionSource5 = ko.observable(item.QuestionSource5 || '');
    self.QuestionSource6 = ko.observable(item.QuestionSource5 || '');
	self.CreatedBy = ko.observable(item.CreatedBy || 0);
	self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
}

function ReadingTypeFourModel(item) {
	var self = this;
	item = item || {};
	self.Id = ko.observable(item.Id || 0);
	self.Title = ko.observable(item.Title || '');
	self.QuestionText = ko.observable(item.QuestionText || '');
	self.Option1 = ko.observable(item.Option1 || '');
	self.Option2 = ko.observable(item.Option2 || '');
	self.Option3 = ko.observable(item.Option3 || '');
	self.Option4 = ko.observable(item.Option4 || '');
	self.Option5 = ko.observable(item.Option5 || '');
	self.Option6 = ko.observable(item.Option6 || '');
    self.Option7 = ko.observable(item.Option7 || '');
    self.Option8 = ko.observable(item.Option8 || '');
	self.CreatedBy = ko.observable(item.CreatedBy || 0);
	self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
}

function ReadingTypeFiveModel(item) {
	var self = this;
	item = item || {};
	self.Id = ko.observable(item.Id || 0);
	self.Title = ko.observable(item.Title || '');
	self.QuestionText = ko.observable(item.QuestionText || '');
	self.CreatedBy = ko.observable(item.CreatedBy || 0);
    self.CreatedDateTime = ko.observable(item.CreatedDateTime || '');
    //Riddha.ko.global.arrayMap(item.Dropdowns, ReadingTypeFiveDropdownModel)
    self.Dropdowns = ko.observableArray([]);
}

function ReadingTypeFiveDropdownModel(item) {
	var self = this;
	item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.ReadingTypeFiveId = ko.observable(item.ReadingTypeFiveId || 0); 
    self.Options = ko.observable(item.Options || ''); 
    self.CorrectIndex = ko.observable(item.CorrectIndex || 0);   
}

function ReadingTypeFourOptionsModel(item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.Id || 0);
    self.CorrectAnswer = ko.observable(item.CorrectAnswer || '');
}


