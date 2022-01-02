using Riddhasoft.DB;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.QuestionSet;
using Riddhasoft.Setup.Services.Writing;
using RTech.Demo.Areas.MockTest.ViewModels.WritingMockTestVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.Services
{
    public class WritingServices
    {
        RiddhaDBContext dbContext = null;
        SQuestionSetDetail _questionSetDetailServices = null;
        SWritingTypeOne _writingTypeOneServices = null;
        SWritingTypeTwo _writingTypeTwoServices = null;
        public WritingServices()
        {
            dbContext = new RiddhaDBContext();
            _questionSetDetailServices = new SQuestionSetDetail();
            _writingTypeOneServices = new SWritingTypeOne();
            _writingTypeTwoServices = new SWritingTypeTwo();
        }

        public MockTestWritingTypeOneViewModel getWritingTypeOneQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _writingTypeOneServices.List().Data.Where(x => x.Id == questionId)
                              select new MockTestWritingTypeOneViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  TotalTime = c.TotalTime,
                                  Title = c.Title,
                                  Question = c.Question
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }

        

        
        public MockTestWritingTypeOneViewModel getWritingTypeTwoQuestion(int questionId)
        {
            if (questionId != 0)
            {
                var result = (from c in _writingTypeTwoServices.List().Data.Where(x => x.Id == questionId)
                              select new MockTestWritingTypeOneViewModel()
                              {
                                  Id = c.Id,
                                  QuestionId = c.Id,
                                  TotalTime = c.TotalTime,
                                  Title = c.Title,
                                  Question = c.Question
                              }).FirstOrDefault();
                return result;
            }
            return null;
        }
    }
}