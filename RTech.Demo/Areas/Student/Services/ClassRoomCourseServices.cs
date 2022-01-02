using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Practice;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.Services
{
    public class ClassRoomCourseServices
    {
        SOnlineClassRoomCourse _OnlineClassRoomCourseServies = null;
        SOnlineClassRoomCourseDetails _onlineClassRoomCourseDetailsServices = null;
        SVocabularyAndPronunciationBooster _vocabularyAndPronunciationBoosterServices = null;
        SOnlineClassRoomCoursePracticeDetails _onlineClassRoomCoursePracticeDetails = null;
        SSynonymBooster _synonymBoosterServices = null;
        SMasterTopicSentenceBooster _masterTopicSentenceServices = null;
        SPackagePaymentDetails _packagePaymentDetailsServices = null;
        SBoosterCollocation _boosterCollocationServices = null;


        public ClassRoomCourseServices()
        {
            _OnlineClassRoomCourseServies = new SOnlineClassRoomCourse();
            _onlineClassRoomCourseDetailsServices = new SOnlineClassRoomCourseDetails();
            _vocabularyAndPronunciationBoosterServices = new SVocabularyAndPronunciationBooster();
            _onlineClassRoomCoursePracticeDetails = new SOnlineClassRoomCoursePracticeDetails();
            _synonymBoosterServices = new SSynonymBooster();
            _masterTopicSentenceServices = new SMasterTopicSentenceBooster();
            _packagePaymentDetailsServices = new SPackagePaymentDetails();
            _boosterCollocationServices = new SBoosterCollocation();
        }

        public List<ClassRoomCourseMasterViewModel> GetCourseList()
        {


            var result = (from c in  _OnlineClassRoomCourseServies.List().Data.ToList()
                          join d in _packagePaymentDetailsServices.List().Data.Where(x => x.PaymentFor == PaymentFor.OnlineCourse && x.UserId == Common.StudentSession.LoggedStudent.StudentId && x.ExpiryDate >= DateTime.Now) on c.Code.ToLower() equals d.CourseCode.ToLower()
                          select new ClassRoomCourseMasterViewModel()
                          {
                              CourseId = c.Id,
                              CourseCode = c.Code,
                              Amount = c.Price,
                              CourseName = c.Name,
                              ImageUrl = c.ImageURL,
                              Description = c.Description,
                              CreatedDate = c.CreatedDate.ToString("dd-MM-yyyy")
                          }).ToList();

            return result;

        }

        public List<ClassRoomCourseMasterViewModel> GetOnlineCourseList()
        {


            var result = (from c in _OnlineClassRoomCourseServies.List().Data.ToList()
                          select new ClassRoomCourseMasterViewModel()
                          {
                              CourseId = c.Id,
                              CourseCode = c.Code,
                              Amount = c.Price,
                              CourseName = c.Name,
                              ImageUrl = c.ImageURL,
                              Description = c.Description,
                              CreatedDate = c.CreatedDate.ToString("dd-MM-yyyy")
                          }).ToList();

            return result;

        }

        

        public ClassRoomCourseMasterViewModel GetClassRoomCourseDetails(int id)
        {
            var vm = (from c in _OnlineClassRoomCourseServies.List().Data.Where(x => x.Id == id).ToList()
                      select new ClassRoomCourseMasterViewModel()
                      {
                          CourseId = c.Id,
                          Amount = c.Price,
                          CourseName = c.Name,
                          CreatedDate = c.CreatedDate.ToString("dd-MM-yyyy"),
                          Description = c.Description,
                          IsPracticeEnabled = c.IsPracticeEnabled,
                          ImageUrl = c.ImageURL
                      }).FirstOrDefault();

            vm.ClassRoomCourseDetailsList = (from c in _onlineClassRoomCourseDetailsServices.List().Data.Where(x => x.OnlineClassRoomCourseId == id).ToList()
                                             select new ClassRoomCourseDetailsViewModel()
                                             {

                                                 FileName = c.FileName,
                                                 FileUrl = c.FileUrl,
                                                 CourseDetailsId = c.Id,
                                                 FileExtension = Common.CommonServices.GetExtension(c.FileUrl),
                                                 FileType = c.FileType
                                             }).ToList();

            return vm;
        }

        public VocabularyAndPronounciationViewModel GetVocabDetails(int courseId)
        {
            var result = (from c in _vocabularyAndPronunciationBoosterServices.List().Data.ToList()
                          join d in _onlineClassRoomCoursePracticeDetails.List().Data.Where(x => x.EOnlineClassRoomCourseID == courseId && x.PracticeType == PracticeType.VocabularyAndPronunciationBooster) on c.Id equals d.PracticeID
                          select new VocabularyAndPronounciationViewModel()
                          {
                              Id = c.Id,
                              Word = c.Word,
                              WordType = c.WordType,
                              FileUrl = c.FileUrl,
                              WordMeaning = c.WordMeaning
                          }).FirstOrDefault();
            return result;
        }

        public SynonymBoosterViewModel GetSynonymBoosterDetails(int courseId)
        {
            if (courseId == 0)
            {
                return null;
            }
            var result = (from c in _synonymBoosterServices.List().Data.ToList()
                          join d in _onlineClassRoomCoursePracticeDetails.List().Data.Where(x => x.EOnlineClassRoomCourseID == courseId && x.PracticeType == PracticeType.SynonymBooster) on c.Id equals d.PracticeID
                          select new SynonymBoosterViewModel()
                          {
                              Id = c.Id,
                              Word = c.Word,
                              WordType = c.WordType,
                              Question = c.Question
                          }).FirstOrDefault();
            if (result != null)
            {
                result.OptionsList = (from c in _synonymBoosterServices.OptionsList().Data.Where(x => x.SynonymBoosterMasterId == result.Id).ToList()
                                      select new SynonymBoosterOptionsViewmodel()
                                      {
                                          Id = c.Id,
                                          SynonymBoosterMasterId = c.SynonymBoosterMasterId,
                                          Option = c.Options,
                                          IsAnswer = c.IsAnswer
                                      }).ToList();
                if (result.OptionsList != null)
                {
                    result.CorrectAnswer = result.OptionsList.Where(x => x.IsAnswer == true).FirstOrDefault().Option;
                }
            }
            return result;
        }

        public MasterTopicSentenceViewModel GetMasterTopicSentenceDetails(int courseId)
        {
            if (courseId == 0)
            {
                return null;
            }
            var result = (from c in _masterTopicSentenceServices.List().Data.ToList()
                          join d in _onlineClassRoomCoursePracticeDetails.List().Data.Where(x => x.EOnlineClassRoomCourseID == courseId && x.PracticeType == PracticeType.MasterTopicSentenceBooster) on c.Id equals d.PracticeID
                          select new MasterTopicSentenceViewModel()
                          {
                              Id = c.Id,
                              QuestionTitle = c.QuestionTitle,
                              Question = c.Question,
                              OptionStatement = c.OptionStatement
                          }).FirstOrDefault();
            if (result != null)
            {
                result.OptionsList = (from c in _masterTopicSentenceServices.DetailsList().Data.Where(x => x.MasterTopicSentenceBoosterMasterId == result.Id).ToList()
                                      select new MasterTopicSentenceOptionsViewModel()
                                      {
                                          Id = c.Id,
                                          MasterTopicSentenceBoosterMasterId = c.MasterTopicSentenceBoosterMasterId,
                                          Options = c.Options,
                                          IsCorrectAnswer = c.IsCorrectAnswer
                                      }).ToList();
                if (result.OptionsList != null)
                {
                    result.CorrectAnswer = result.OptionsList.Where(x => x.IsCorrectAnswer == true).FirstOrDefault().Options;
                }
            }
            return result;
        }

        public BoosterCollocationViewModel GetBoosterCOllocationdetails(int courseId)
        {
            if (courseId == 0)
            {
                return null;
            }

            var result = (from c in _boosterCollocationServices.List().Data.ToList()
                          join d in _onlineClassRoomCoursePracticeDetails.List().Data.Where(x => x.EOnlineClassRoomCourseID == courseId && x.PracticeType == PracticeType.BoosterCollocation) on c.Id equals d.PracticeID
                          select new BoosterCollocationViewModel()
                          {
                              Id = c.Id,
                              Question = c.Question,
                              QuestionText = c.QuestionText,
                              OptionStatement = c.OptionStatement
                          }).FirstOrDefault();
            if (result != null)
            {
                result.OptionsList = (from c in _boosterCollocationServices.OptionsList().Data.Where(x => x.EBoosterCollocationMasterId == result.Id).ToList()
                                      select new BoosterCollocationOptionsViewModel()
                                      {
                                          Id = c.Id,
                                          EBoosterCollocationMasterId = c.EBoosterCollocationMasterId,
                                          Options = c.Options,
                                          IsAnswer = c.IsAnswer
                                      }).ToList();
                result.CorrectAnswer = result.OptionsList.Where(x => x.IsAnswer == true).FirstOrDefault().Options;
            }

            return result;
        }


    }
}