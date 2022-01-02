using Riddhasoft.DB;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services.QuestionSet;
using Riddhasoft.Setup.Services.Reading;
using RTech.Demo.Areas.MockTest.ViewModels;
using RTech.Demo.Areas.MockTest.ViewModels.ReadingMockTestVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.Services
{
    public class ReadingServices
    {
        RiddhaDBContext db = null;
        SReadingTypeOne _readingTypeOneServices = null;
        SReadingTypeTwo _readingTypeTwoServices = null;
        SReadingTypeThree _readingTypeThreeServices = null;
        SReadingTypeFour _readingTypeFourServices = null;
        SReadingTypeFive _readingTypeFiveServices = null;
        SReadingTypeFiveOptions _readingTypeFiveOptionsServices = null;
        SQuestionSetDetail _questionSetDetailServices = null;
        MockTestHomeServices _mockTestHomeServices = null;

        public ReadingServices()
        {
            db = new RiddhaDBContext();
            _questionSetDetailServices = new SQuestionSetDetail();
            _readingTypeOneServices = new SReadingTypeOne();
            _readingTypeTwoServices = new SReadingTypeTwo();
            _readingTypeThreeServices = new SReadingTypeThree();
            _readingTypeFourServices = new SReadingTypeFour();
            _readingTypeFiveServices = new SReadingTypeFive();
            _readingTypeFiveOptionsServices = new SReadingTypeFiveOptions();
            _mockTestHomeServices = new MockTestHomeServices();
        }
        public StartReadingTypeOneViewModel getReadingQuestionType1(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _readingTypeOneServices.List().Data.Where(x => x.Id == questionId)
                              select new StartReadingTypeOneViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  TextToRead = c.ReadingText,
                                  Question = c.Question,
                                  Response1 = c.Response1,
                                  Response2 = c.Response2,
                                  Response3 = c.Response3,
                                  Response4 = c.Response4,
                                  StudentName = Common.StudentSession.LoggedStudent.StudentName,
                                  Title = c.Title
                              }).FirstOrDefault();
                return result;
            }
            return null;

        }

        public StartReadingTypeTwoViewModel getReadingQuestionType2(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _readingTypeTwoServices.List().Data.Where(x => x.Id == questionId)
                              select new StartReadingTypeTwoViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  Question = c.Question,
                                  TextToRead = c.ReadingText,
                                  Title = c.Title,
                                  Response1 = c.Response1,
                                  Response2 = c.Response2,
                                  Response3 = c.Response3,
                                  Response4 = c.Response4,
                                  Response5 = c.Response5,
                                  Response6 = c.Response6,
                                  Response7 = c.Response7,
                                  StudentName = Common.StudentSession.LoggedStudent.StudentName
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartReadingTypeThreeViewModel getReadingQuestionType3(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _readingTypeThreeServices.List().Data.Where(x => x.Id == questionId)
                              select new StartReadingTypeThreeViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  QuestionSource1 = c.QuestionSource1,
                                  QuestionSource2 = c.QuestionSource2,
                                  QuestionSource3 = c.QuestionSource3,
                                  QuestionSource4 = c.QuestionSource4,
                                  QuestionSource5 = c.QuestionSource5,
                                  QuestionSource6 = c.QuestionSource6,
                                  Title = c.Title,
                                  StudentName = Common.StudentSession.LoggedStudent.StudentName
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartReadingTypeFourViewModel getReadingQuestionType4(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _readingTypeFourServices.List().Data.Where(x => x.Id == questionId)
                              select new StartReadingTypeFourViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  Option1 = c.Option1 == null ? "" : c.Option1,
                                  Option2 = c.Option2 == null ? "" : c.Option2,
                                  Option3 = c.Option3 == null ? "" : c.Option3,
                                  Option4 = c.Option4 == null ? "" : c.Option4,
                                  Option5 = c.Option5 == null ? "" : c.Option5,
                                  Option6 = c.Option6 == null ? "" : c.Option6,
                                  Option7 = c.Option7 == null ? "" : c.Option7,
                                  Option8 = c.Option8 == null ? "" : c.Option8,
                                  QuestionText = c.QuestionText,
                                  //StudentName = Common.StudentSession.LoggedStudent.StudentName,
                                  Title = c.Title
                              }).FirstOrDefault();
                return result;
            }
            return null;

        }

        public StartReadingTypeFiveViewModel getReadingQuestionType5(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _readingTypeFiveServices.List().Data.Where(x => x.Id == questionId)
                              select new StartReadingTypeFiveViewModel()
                              {
                                  Id = c.Id,
                                  QuestionText = c.QuestionText,
                                  QuestionId = c.Id,
                                  //StudentName = Common.StudentSession.LoggedStudent.StudentName,
                                  Title = c.Title
                              }).FirstOrDefault();

                var options = _readingTypeFiveServices.ReadingTypeFiveDropdownList().Data.Where(x => x.ReadingTypeFiveId == questionId).ToList();

                var OptionsDropDowns = new List<OptionsDropDownVm>();
                for (int i = 0; i < options.Count; i++)
                {
                    var optionDropDownVm = new OptionsDropDownVm();

                    string Options = options[i].Options;

                    string[] optionArray = Options.Split(',');

                    var readingTypeFiveOptions = (from c in optionArray
                                                  select new ReadingTypeFiveOptions()
                                                  {

                                                      OptionName = c,


                                                  }).ToList();

                    optionDropDownVm.Options = readingTypeFiveOptions;
                    optionDropDownVm.IsCorrectAnswer = options[i].CorrectIndex;

                    // Add Option Vm in Option DropDown List 
                    OptionsDropDowns.Add(optionDropDownVm);



                }

                result.OptionsDropDowns = OptionsDropDowns;
                return result;

            }
            return null;

        }

        public bool CheckReadingTypeOneAnswer(ReadingTypeOneAnswerMockTestViewModel readingTypeOneAnswer)
        {
            var result = _readingTypeOneServices.List().Data.Where(x => x.Id == readingTypeOneAnswer.QuestionId).FirstOrDefault();
            if (!string.IsNullOrEmpty(result.IsCorrectAnswer))
            {
                var CorrectAnswer = result.IsCorrectAnswer.Trim().ToLower();
                if (readingTypeOneAnswer.Answer.Trim().ToLower() == CorrectAnswer)
                {
                    return true;

                }

            }
            return false;

        }

        public int calculateNumberOfCorrectAnswersForTypeTwo(string providedCorrectAnswers, int questionId)
        {
            providedCorrectAnswers = providedCorrectAnswers.ToLower().Trim();
            string[] proAnswers = providedCorrectAnswers.Split(',');
            proAnswers = _mockTestHomeServices.RemoveNullEntriesInArray(proAnswers);
            string data = _readingTypeTwoServices.List().Data.Where(x => x.Id == questionId).FirstOrDefault().IsCorrectAnswer.ToLower().Trim();
            string[] dataAnswers = data.Split(',');
            dataAnswers = _mockTestHomeServices.RemoveNullEntriesInArray(dataAnswers);
            int noOfCorrectAnswers = 0;
            int noOfAnswers = proAnswers.Count();
            foreach (var item in dataAnswers)
            {
                for (int i = 0; i < noOfAnswers; i++)
                {
                    if (proAnswers[i].Trim() == item.Trim())
                    {
                        noOfCorrectAnswers++;
                    }
                }
            }
            return noOfCorrectAnswers;

        }
    }

}