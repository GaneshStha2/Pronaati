using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.MockTest.ViewModels.Listening;
using RTech.Demo.Areas.MockTest.ViewModels.ListeningMockTestVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.Services
{
    public class ListeningServices
    {
        SQuestionSetDetail _questionDetailsServices = null;
        SListeningTypeOne _listeningTypeOneServices = null;
        SListeningTypeTwo _listeningTypeTwoServices = null;
        SListeningTypeThree _listeningTypeThreeServices = null;
        SListeningTypeFour _listeningTypeFourServices = null;
        SListeningTypeFive _listeningTypeFiveServices = null;
        SListeningTypeSix _listeningTypeSixServices = null;
        SListeningTypeSeven _listeningTypeSevenServices = null;
        SListeningTypeEight _listeningTypeEightServices = null;
        public ListeningServices()
        {
            _listeningTypeOneServices = new SListeningTypeOne();
            _listeningTypeTwoServices = new SListeningTypeTwo();
            _listeningTypeThreeServices = new SListeningTypeThree();
            _listeningTypeFourServices = new SListeningTypeFour();
            _listeningTypeFiveServices = new SListeningTypeFive();
            _listeningTypeSixServices = new SListeningTypeSix();
            _listeningTypeSevenServices = new SListeningTypeSeven();
            _listeningTypeEightServices = new SListeningTypeEight();
            _questionDetailsServices = new SQuestionSetDetail();
        }

        public StartListeningTypeOneViewModel getListeningTypeOneQuestions(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeOneServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeOneViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  Title = c.Title,
                                  AudioUrl = c.AudioUrl,
                                  BeginWithinTime = c.TimeBeforeAudio,
                                  TaskCompletionTime = c.TotalTime,
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartListeningTypeTwoViewModel getListeningTypeTwoQuestion(int questionId)
        {
            if (questionId != 0)
            {


                var result = (from c in _listeningTypeTwoServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeTwoViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  AudioUrl = c.AudioUrl,
                                  Title = c.Title,
                                  QuestionText = c.Question,
                                  Response1 = c.Response1 == null ? "" : c.Response1,
                                  Response2 = c.Response2 == null ? "" : c.Response2,
                                  Response3 = c.Response3 == null ? "" : c.Response3,
                                  Response4 = c.Response4 == null ? "" : c.Response4,
                                  Response5 = c.Response5 == null ? "" : c.Response5,
                                  Response6 = c.Response6 == null ? "" : c.Response6,
                                  Response7 = c.Response7 == null ? "" : c.Response7
                              }).FirstOrDefault();

                return result;
            }
            return null;
        }

        public StartListeningTypeThreeViewModel getListeningTypeThreeQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeThreeServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeThreeViewModel()
                              {
                                  Id = c.Id,
                                  AudioUrl = c.AudioUrl,
                                  Question = c.QuestionText,
                                  Title = c.Title
                              }).FirstOrDefault();

                return result;
            }
            return null;
        }

        public StartListeningTypeFourViewModel getListeningTypeFourQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeFourServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeFourViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  AudioUrl = c.AudioUrl,
                                  Title = c.Title,
                                  Paragraph1 = c.Paragraph1,
                                  Paragraph2 = c.Paragraph2,
                                  Paragraph3 = c.Paragraph3,
                                  Paragraph4 = c.Paragraph4
                              }).FirstOrDefault();

                return result;
            }
            return null;
        }

        public StartListeningTypeFiveViewModel getListeningTypeFiveQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeFiveServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeFiveViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  AudioUrl = c.AudioUrl,
                                  Option1 = c.Option1,
                                  Option2 = c.Option2,
                                  Option3 = c.Option3,
                                  Option4 = c.Option4,
                                  Title = c.Title,
                                  BeginWithin = c.TimeBeforeAudio,
                                  Question = c.Question
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartListeningTypeSixViewModel getListeningTypeSixQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeSixServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeSixViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  BeginWithin = c.TimeBeforeAudio,
                                  AudioUrl = c.AudioUrl,
                                  Title = c.Title,
                                  Option1 = c.Option1,
                                  Option2 = c.Option2,
                                  Option3 = c.Option3,
                                  Option4 = c.Option4,
                                  Option5 = c.Option5
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartListeningTypeSevenViewModel getListeningTypeSevenQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeSevenServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeSevenViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  AudioUrl = c.AudioUrl,
                                  Title = c.Title,
                                  TranscriptionText = c.TranscriptionText,
                                  BeginWithin = c.TimeBeforeAudio
                              }).FirstOrDefault();
                string[] texts = result.TranscriptionText.Split(null);
                result.Texts = new List<string>(texts);
                return result;
            }
            return null;
        }

        public StartListeningTypeEightViewModel getListeningTypeEightQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _listeningTypeEightServices.List().Data.Where(x => x.Id == questionId)
                              select new StartListeningTypeEightViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  BeginWithin = c.TimeBeforeAudio,
                                  AudioUrl = c.AudioUrl,
                                  Sentence = c.Sentence,
                                  Title = c.Title
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }
    }
}