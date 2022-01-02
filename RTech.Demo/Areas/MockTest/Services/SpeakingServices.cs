using Riddhasoft.DB;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.QuestionSet;
using Riddhasoft.Setup.Services.Speaking;
using RTech.Demo.Areas.MockTest.ViewModels.SpeakingMockTestVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.Services
{
    public class SpeakingServices
    {
        RiddhaDBContext dbContext = null;
        SQuestionSetDetail _questionSetDetailServices = null;
        SSpeakingTypeOne _speakingTypeOneServices = null;
        SSpeakingTypeTwo _speakingTypeTwoServices = null;
        SSpeakingTypeThree _speakingTypeThreeServices = null;
        SSpeakingTypefour _speakingTypeFourServices = null;
        SSpeakingTypeFive _speakingTypeFiveServices = null;
        SSpeakingTypeSix _speakingTypeSixServices = null;
        SSpeakingTypeSeven _speakingTypeSevenServices = null;
        

        public SpeakingServices()
        {
            dbContext = new RiddhaDBContext();
            _speakingTypeOneServices = new SSpeakingTypeOne();
            _questionSetDetailServices = new SQuestionSetDetail();
            _speakingTypeTwoServices = new SSpeakingTypeTwo();
            _speakingTypeThreeServices = new SSpeakingTypeThree();
            _speakingTypeFourServices = new SSpeakingTypefour();
            _speakingTypeFiveServices = new SSpeakingTypeFive();
            _speakingTypeSixServices = new SSpeakingTypeSix();
            _speakingTypeSevenServices = new SSpeakingTypeSeven();
        }

        public StartSpeakingTypeOneViewModel getSpeakingOneQuestion(int questionId)
        {
            if (questionId != 0 )
            {
                
                var result = (from c in _speakingTypeOneServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeOneViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  Title = c.Title,
                                  Question = c.Question,
                                  BeginWithinTImeSec = c.BeginWithinTImeSec,
                                  SpeakingTimeSec = c.SpeakingTimeSec
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartSpeakingTypeTwoViewModel getSpeakingTypeTwoQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _speakingTypeTwoServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeTwoViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  BeginWithinTime = c.BeginWithInTimeSec,
                                  SpeakingTime = c.SpeakingTimeSec,
                                  Title = c.Title,
                                  TextToRead = c.TextToRead
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }


        public StartSpeakingTypeThreeViewModel getSpeakingTypeThreeQuestion(int questionId)
        {
            if(questionId!= 0)
            {
                var result = (from c in _speakingTypeThreeServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeThreeViewModel()
                              {
                                  BeginWithinTime = c.BeginWithInTimeSec,
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  SpeakingTime = c.SpeakingTimeSec,
                                  StudentName = Common.StudentSession.LoggedStudent.StudentName,
                                  TextToRead = c.TextToRead,
                                  Title = c.Title
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }
        public StartSpeakingTypeFourViewModel getSpeakingTypeFourQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _speakingTypeFourServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeFourViewModel()
                              {
                               Id = c.Id,
                               QuestionId = c.Id,
                               BeginWithinTime = c.BeginWithInTimeSec,
                               SpeakingTime = c.SpeakingTimeSec,
                               StudentName = Common.StudentSession.LoggedStudent.StudentName,
                               AudioUrl = c.AudioUrl,
                               Title = c.Title,                               
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartSpeakingTypeFiveViewModel getSpeakingTypeFiveQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _speakingTypeFiveServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeFiveViewModel()
                              {
                                 Id = c.Id,
                                 QuestionId = c.Id,
                                 Title = c.Title,
                                 BeginWithinTime = c.BeginWithInTimeSec,
                                 NameOfImage = c.NameOfImage,
                                 FileUrl = c.ImageURL,
                                 StudentName = Common.StudentSession.LoggedStudent.StudentName,    
                                 SpeakingTime = c.SpeakingTimeSec
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }


        public StartSpeakingTypeSixViewModel getSpeakingTypeSixQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _speakingTypeSixServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeSixViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  AudioUrl = c.AudioUrl,
                                  ImageUrl = c.ImageUrl == null ? "" : c.ImageUrl,
                                  Title = c.Title,
                                  BeginWithinTime = c.BeginWithInTimeSec,
                                  SpeakingTime = c.SpeakingTimeSec,
                                  StudentName = Common.StudentSession.LoggedStudent.StudentName,
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        public StartSpeakingTypeSevenViewModel getSpeakingTypeSevenQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _speakingTypeSevenServices.List().Data.Where(x => x.Id == questionId)
                              select new StartSpeakingTypeSevenViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  Title = c.Title,
                                  AudioUrl = c.QuestionAudioURl,
                                  BeginWithinTime = c.BeginWithInTimeSec,
                                  SpeakingTime = c.SpeakingTimeSec,
                                  StudentName = Common.StudentSession.LoggedStudent.StudentName,
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }
    }

    
}