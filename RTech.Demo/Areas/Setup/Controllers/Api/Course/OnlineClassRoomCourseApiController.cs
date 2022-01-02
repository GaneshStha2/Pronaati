using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Practice;
using RTech.Demo.Areas.Setup.ViewModels.Course;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.Course
{
    public class OnlineClassRoomCourseApiController : ApiController
    {
        SOnlineClassRoomCourse _onlineClassRoomCourseServices = null;
        SOnlineClassRoomCourseDetails _onlineClassRoomCourseDetailsServices = null;
        SLanguageType _courseTypeServices = null;
        SVocabularyAndPronunciationBooster _vocabServices = null;
        SSynonymBooster _synonymBoosterServices = null;
        SBoosterCollocation _boosterCollocationServices = null;
        SMasterTopicSentenceBooster _masterTopicSentenceServices = null;
        SOnlineClassRoomCoursePracticeDetails _onlineClassroomCoursePracticeDetailsServices = null;

        public OnlineClassRoomCourseApiController()
        {
            _onlineClassRoomCourseServices = new SOnlineClassRoomCourse();
            _onlineClassRoomCourseDetailsServices = new SOnlineClassRoomCourseDetails();
            _courseTypeServices = new SLanguageType();
            _vocabServices = new SVocabularyAndPronunciationBooster();
            _synonymBoosterServices = new SSynonymBooster();
            _boosterCollocationServices = new SBoosterCollocation();
            _masterTopicSentenceServices = new SMasterTopicSentenceBooster();
            _onlineClassroomCoursePracticeDetailsServices = new SOnlineClassRoomCoursePracticeDetails();
        }

        [HttpPost]
        public KendoGridResult<List<OnlineClassRoomCourseKendoVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<EOnlineClassRoomCourse> packageQuery;
            packageQuery = _onlineClassRoomCourseServices.List().Data;
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EOnlineClassRoomCourse> paginatedQuery;

            switch (searchField)
            {
                case "Code":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Code.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Code == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Name":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Name.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Name == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Price":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Price.ToString().StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Price.ToString() == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:

                    paginatedQuery = packageQuery.OrderByDescending(x => x.Id).ThenBy(x => x.Name).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var vm = (from c in paginatedQuery.ToList()
                      select new OnlineClassRoomCourseKendoVm()
                      {
                          Id = c.Id,
                          Code = c.Code,
                          CreatedBy = c.CreatedBy,
                          CreatedDate = c.CreatedDate.ToString("dd/MM/yyyy"),
                          Description = c.Description,
                          Duration = c.Duration.ToString() + " days"  ,
                          ImageURL = c.ImageURL,
                          Name = c.Name,
                          CourseTypeId = c.LanguageTypeId,
                          CourseTypeName = c.LanguageType.Name,
                          Price = c.Price
                      }).ToList();
            return new KendoGridResult<List<OnlineClassRoomCourseKendoVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };

        }

        [HttpPost]
        public ServiceResult<OnlineClassRoomCourseSetupVm> Post(OnlineClassRoomCourseSetupVm vm)
        {
            EOnlineClassRoomCourse OnlineClassRoomCourse = new EOnlineClassRoomCourse()
            {
                Id = vm.OnlineClassRoomCourseMasterVm.Id,
                Code = vm.OnlineClassRoomCourseMasterVm.Code,
                CreatedBy = RiddhaSession.UserId,
                CreatedDate = vm.OnlineClassRoomCourseMasterVm.CreatedDate,
                Description = vm.OnlineClassRoomCourseMasterVm.Description,
                Duration = vm.OnlineClassRoomCourseMasterVm.Duration,
                ImageURL = vm.OnlineClassRoomCourseMasterVm.ImageURL,
                Name = vm.OnlineClassRoomCourseMasterVm.Name,
                Price = vm.OnlineClassRoomCourseMasterVm.Price,
                LanguageTypeId = vm.OnlineClassRoomCourseMasterVm.CourseTypeId,
                IsPracticeEnabled = vm.OnlineClassRoomCourseMasterVm.IsPracticeEnabled

        };

            var result = _onlineClassRoomCourseServices.Add(OnlineClassRoomCourse);
            if (result.Status == ResultStatus.Ok && vm.OnlineClassRoomCourseDetailsVm != null)
            {
                if (vm.OnlineClassRoomCourseDetailsVm != null)
                {
                    foreach (var item in vm.OnlineClassRoomCourseDetailsVm)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }

                    var classRoomCoursedetails = (from c in vm.OnlineClassRoomCourseDetailsVm
                                                  select new EOnlineClassRoomCourseDetails()
                                                  {
                                                      Id = c.Id,
                                                      OnlineClassRoomCourseId = c.OnlineClassRoomCourseId,
                                                      FileName = c.FileName,
                                                      FileUrl = c.FileUrl,
                                                      FileId = c.FileId,
                                                      FileType = c.FileType,
                                                  }).ToList();
                    _onlineClassRoomCourseDetailsServices.AddOnlineClassRoomCourseDetails(classRoomCoursedetails);
                }
                if (vm.VocabDetails != null)
                {
                    foreach (var item in vm.VocabDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;

                    }
                    var practiceVocabDetails = (from c in vm.VocabDetails
                                                select new EOnlineClassRoomCoursePracticeDetails()
                                                {
                                                    EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                    PracticeType = PracticeType.VocabularyAndPronunciationBooster,
                                                    PracticeID = c.Id
                                                }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(practiceVocabDetails);
                }

                if (vm.SynonymBoosterDetails != null)
                {
                    foreach (var item in vm.SynonymBoosterDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var synonymBoosterDetails = (from c in vm.SynonymBoosterDetails
                                                 select new EOnlineClassRoomCoursePracticeDetails()
                                                 {
                                                     EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                     PracticeType = PracticeType.SynonymBooster,
                                                     PracticeID = c.Id,

                                                 }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(synonymBoosterDetails);
                }

                if (vm.BoosterCollocationDetails != null)
                {
                    foreach (var item in vm.BoosterCollocationDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var boosterCollocationDetails = (from c in vm.BoosterCollocationDetails
                                                     select new EOnlineClassRoomCoursePracticeDetails()
                                                     {
                                                         EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                         PracticeType = PracticeType.BoosterCollocation,
                                                         PracticeID = c.Id
                                                     }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(boosterCollocationDetails);
                }

                if (vm.MasterTopicSentenceDetails != null)
                {
                    foreach (var item in vm.MasterTopicSentenceDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var masterTopicSentenceDetails = (from c in vm.MasterTopicSentenceDetails
                                                      select new EOnlineClassRoomCoursePracticeDetails()
                                                      {
                                                          EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                          PracticeType = PracticeType.MasterTopicSentenceBooster,
                                                          PracticeID = c.Id
                                                      }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(masterTopicSentenceDetails);
                }

                //foreach (var item in vm.PracticeQuestionsDetails)
                //{
                //    item.OnlineClassRoomCourseId = result.Data.Id;
                //}
                //var practiceQuestionDetails = (from c in vm.PracticeQuestionsDetails
                //                               select new EOnlineClassRoomCoursePracticeDetails()
                //                               {
                //                                   EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                //                                   //PracticeType = PracticeType.,
                //                                   PracticeID = c.Id
                //                               }).ToList();
                //_onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(practiceQuestionDetails);

            }
            return new ServiceResult<OnlineClassRoomCourseSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut]
        public ServiceResult<OnlineClassRoomCourseSetupVm> Put(OnlineClassRoomCourseSetupVm vm)
        {


            var MasterData = _onlineClassRoomCourseServices.List().Data.Where(x => x.Id == vm.OnlineClassRoomCourseMasterVm.Id).FirstOrDefault();
            MasterData.Id = vm.OnlineClassRoomCourseMasterVm.Id;
            MasterData.Code = vm.OnlineClassRoomCourseMasterVm.Code;
            MasterData.Description = vm.OnlineClassRoomCourseMasterVm.Description;
            MasterData.Duration = vm.OnlineClassRoomCourseMasterVm.Duration;
            MasterData.Name = vm.OnlineClassRoomCourseMasterVm.Name;
            MasterData.Price = vm.OnlineClassRoomCourseMasterVm.Price;
            MasterData.ImageURL = vm.OnlineClassRoomCourseMasterVm.ImageURL;
            MasterData.LanguageTypeId = vm.OnlineClassRoomCourseMasterVm.CourseTypeId;
            MasterData.IsPracticeEnabled = vm.OnlineClassRoomCourseMasterVm.IsPracticeEnabled;

            var result = _onlineClassRoomCourseServices.Update(MasterData);
            if (result.Status == ResultStatus.Ok)
            {
                var onlineClassRoomCourseDetails = _onlineClassRoomCourseDetailsServices.List().Data.Where(x => x.OnlineClassRoomCourseId == vm.OnlineClassRoomCourseMasterVm.Id).ToList();
                _onlineClassRoomCourseDetailsServices.RemoveOnlineClassRoomCourseDetails(onlineClassRoomCourseDetails);

                if (vm.OnlineClassRoomCourseDetailsVm != null)
                {
                    foreach (var item in vm.OnlineClassRoomCourseDetailsVm)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var DetailsVm = (from c in vm.OnlineClassRoomCourseDetailsVm
                                     select new EOnlineClassRoomCourseDetails()
                                     {
                                         Id = c.Id,
                                         OnlineClassRoomCourseId = c.OnlineClassRoomCourseId,
                                         FileName = c.FileName,
                                         FileUrl = c.FileUrl,
                                         FileId = c.FileId,
                                         FileType = c.FileType
                                     }).ToList();

                    _onlineClassRoomCourseDetailsServices.AddOnlineClassRoomCourseDetails(DetailsVm);
                }
                var onlineClassRoomCoursePracticeDetails = _onlineClassroomCoursePracticeDetailsServices.List().Data.Where(x => x.EOnlineClassRoomCourseID == vm.OnlineClassRoomCourseMasterVm.Id).ToList();
                _onlineClassroomCoursePracticeDetailsServices.RemoveOnlineClassroomCoursePracticeDetails(onlineClassRoomCoursePracticeDetails);

                if (vm.VocabDetails != null)
                {
                    foreach (var item in vm.VocabDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var vocabDetails = (from c in vm.VocabDetails
                                        select new EOnlineClassRoomCoursePracticeDetails()
                                        {
                                            EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                            PracticeType = PracticeType.VocabularyAndPronunciationBooster,
                                            PracticeID = c.Id
                                        }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(vocabDetails);
                }

                if (vm.SynonymBoosterDetails != null)
                {
                    foreach (var item in vm.SynonymBoosterDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var synonymBoosterDetails = (from c in vm.SynonymBoosterDetails
                                                 select new EOnlineClassRoomCoursePracticeDetails()
                                                 {
                                                     EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                     PracticeType = PracticeType.SynonymBooster,
                                                     PracticeID = c.Id
                                                 }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(synonymBoosterDetails);
                }

                if (vm.BoosterCollocationDetails != null)
                {
                    foreach (var item in vm.BoosterCollocationDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var boosterCollocationDetails = (from c in vm.BoosterCollocationDetails
                                                     select new EOnlineClassRoomCoursePracticeDetails()
                                                     {
                                                         EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                         PracticeID = c.Id,
                                                         PracticeType = PracticeType.BoosterCollocation
                                                     }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(boosterCollocationDetails);
                }

                if (vm.MasterTopicSentenceDetails != null)
                {
                    foreach (var item in vm.MasterTopicSentenceDetails)
                    {
                        item.OnlineClassRoomCourseId = result.Data.Id;
                    }
                    var masterTopicSentenceDetails = (from c in vm.MasterTopicSentenceDetails
                                                      select new EOnlineClassRoomCoursePracticeDetails()
                                                      {
                                                          EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                                                          PracticeType = PracticeType.MasterTopicSentenceBooster,
                                                          PracticeID = c.Id
                                                      }).ToList();
                    _onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(masterTopicSentenceDetails);
                }
                //foreach (var item in vm.PracticeQuestionsDetails)
                //{
                //    item.OnlineClassRoomCourseId = result.Data.Id;
                //}
                //var practiceQuestionDetails = (from c in vm.PracticeQuestionsDetails
                //                               select new EOnlineClassRoomCoursePracticeDetails()
                //                               {
                //                                   EOnlineClassRoomCourseID = c.OnlineClassRoomCourseId,
                //                                   //PracticeType = PracticeType.,
                //                                   PracticeID = c.Id
                //                               }).ToList();
                //_onlineClassroomCoursePracticeDetailsServices.AddOnlineClassroomPracticeDetails(practiceQuestionDetails);


            }

            return new ServiceResult<OnlineClassRoomCourseSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet]
        public ServiceResult<OnlineClassRoomCourseSetupVm> GetDetails(int MasterId)
        {
            var vm = new OnlineClassRoomCourseSetupVm();
            var detailsVm = (from c in _onlineClassRoomCourseDetailsServices.List().Data.Where(x => x.OnlineClassRoomCourseId == MasterId).ToList()
                             select new OnlineClassRoomCourseDetailsVm()
                             {
                                 FileId = c.FileId,
                                 FileName = c.FileName,
                                 FileUrl = c.FileUrl,
                                 Id = c.Id,
                                 OnlineClassRoomCourseId = c.OnlineClassRoomCourseId,
                                 FileType = c.FileType,
                                 FileTypeName = Enum.GetName(typeof(FileType), c.FileType)
                             }).ToList();
            vm.OnlineClassRoomCourseDetailsVm = detailsVm;

            var vocabVm = (from c in _onlineClassroomCoursePracticeDetailsServices.List().Data.Where(x => x.EOnlineClassRoomCourseID == MasterId && x.PracticeType == PracticeType.VocabularyAndPronunciationBooster).ToList()
                           select new VocabDetailsVm()
                           {
                               Id = c.PracticeID,
                               Question = getQuestion(c.PracticeID, c.PracticeType),
                               OnlineClassRoomCourseId = MasterId
                           }).ToList();
            vm.VocabDetails = vocabVm;

            var synonymBoosterVm = (from c in _onlineClassroomCoursePracticeDetailsServices.List().Data.Where(x => x.EOnlineClassRoomCourseID == MasterId && x.PracticeType == PracticeType.SynonymBooster).ToList()
                                    select new SynonymBoosterDetailsVm()
                                    {
                                        Id = c.PracticeID,
                                        Question = getQuestion(c.PracticeID, c.PracticeType),
                                        OnlineClassRoomCourseId = MasterId
                                    }).ToList();
            vm.SynonymBoosterDetails = synonymBoosterVm;

            var boosterCollocationVm = (from c in _onlineClassroomCoursePracticeDetailsServices.List().Data.Where(x => x.EOnlineClassRoomCourseID == MasterId && x.PracticeType == PracticeType.BoosterCollocation).ToList()
                                        select new BoosterCollocationDetailsVm()
                                        {
                                            Id = c.PracticeID,
                                            Question = getQuestion(c.PracticeID, c.PracticeType),
                                            OnlineClassRoomCourseId = MasterId
                                        }).ToList();
            vm.BoosterCollocationDetails = boosterCollocationVm;

            var masterTopicSentenceVm = (from c in _onlineClassroomCoursePracticeDetailsServices.List().Data.Where(x => x.EOnlineClassRoomCourseID == MasterId && x.PracticeType == PracticeType.MasterTopicSentenceBooster).ToList()
                                         select new MasterTopicSentenceDetailsVm()
                                         {
                                             Id = c.PracticeID,
                                             Question = getQuestion(c.PracticeID, c.PracticeType),
                                             OnlineClassRoomCourseId = MasterId
                                         }).ToList();
            vm.MasterTopicSentenceDetails = masterTopicSentenceVm;
            return new ServiceResult<OnlineClassRoomCourseSetupVm>()
            {
                Data = vm,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        public string getQuestion(int questionId, PracticeType practiceType)
        {
            if (practiceType == PracticeType.VocabularyAndPronunciationBooster)
            {
                var question = _vocabServices.List().Data.Where(x => x.Id == questionId).FirstOrDefault();
                if (question != null)
                {
                    return question.Word;
                }
                return "";
            }
            else if (practiceType == PracticeType.SynonymBooster)
            {
                var question = _synonymBoosterServices.List().Data.Where(x => x.Id == questionId).FirstOrDefault();
                if (question != null)
                {
                    return question.Question;
                }
                return "";
            }
            else if (practiceType == PracticeType.BoosterCollocation)
            {
                var question = _boosterCollocationServices.List().Data.Where(x => x.Id == questionId).FirstOrDefault();
                if (question != null)
                {
                    return question.QuestionText;
                }
                return "";
            }
            else if (practiceType == PracticeType.MasterTopicSentenceBooster)
            {
                var question = _masterTopicSentenceServices.List().Data.Where(x => x.Id == questionId).FirstOrDefault();
                if (question != null)
                {
                    return question.Question;

                }
                return "";
            }
            //else if (practiceType == practiceType)
            return "";
        }

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id = 0)
        {
            var data = new EOnlineClassRoomCourse();

            if (Id > 0)
            {
                data = _onlineClassRoomCourseServices.List().Data.Where(x => x.Id != Id && x.Code == Code).FirstOrDefault();
            }
            else
            {

                data = _onlineClassRoomCourseServices.List().Data.Where(x => x.Code == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }

        [HttpDelete]
        public ServiceResult<int> Delete(int Id)
        {
            var MasterData = _onlineClassRoomCourseServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _onlineClassRoomCourseServices.Remove(MasterData);
            var details = _onlineClassRoomCourseDetailsServices.List().Data.Where(x => x.OnlineClassRoomCourseId == Id).ToList();
            _onlineClassRoomCourseDetailsServices.RemoveOnlineClassRoomCourseDetails(details);

            var practiceDetails = _onlineClassroomCoursePracticeDetailsServices.List().Data.Where(x => x.EOnlineClassRoomCourseID == Id).ToList();
            _onlineClassroomCoursePracticeDetailsServices.RemoveOnlineClassroomCoursePracticeDetails(practiceDetails);

            return new ServiceResult<int>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }

        [HttpGet]
        public ServiceResult<List<LanguageTypeSetupVm>> GetCourseTypeList()
        {
            var data = _courseTypeServices.List().Data;
            var result = (from c in data
                          select new LanguageTypeSetupVm()
                          {
                              Id = c.Id,
                              Name = c.Name
                          }).ToList();
            return new ServiceResult<List<LanguageTypeSetupVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok

            };
        }

        [HttpGet]
        public ServiceResult<List<PracticeMenusDropdownVm>> GetVocabDropdownList()
        {
            var result = (from c in _vocabServices.List().Data
                          select new PracticeMenusDropdownVm()
                          {
                              Id = c.Id,
                              Question = c.Word
                          }).ToList();
            return new ServiceResult<List<PracticeMenusDropdownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        [HttpGet]
        public ServiceResult<List<PracticeMenusDropdownVm>> GetSynonymBoosterDropdownList()
        {
            var result = (from c in _synonymBoosterServices.List().Data
                          select new PracticeMenusDropdownVm()
                          {
                              Id = c.Id,
                              Question = c.Question
                          }).ToList();

            return new ServiceResult<List<PracticeMenusDropdownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        [HttpGet]
        public ServiceResult<List<PracticeMenusDropdownVm>> GetBoosterCollocationDropdownList()
        {
            var result = (from c in _boosterCollocationServices.List().Data
                          select new PracticeMenusDropdownVm()
                          {
                              Id = c.Id,
                              Question = c.QuestionText
                          }).ToList();
            return new ServiceResult<List<PracticeMenusDropdownVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };
        }

        [HttpGet]
        public ServiceResult<List<PracticeMenusDropdownVm>> GetMasterTopicSentenceDropdownList()
        {
            var result = (from c in _masterTopicSentenceServices.List().Data
                          select new PracticeMenusDropdownVm()
                          {
                              Id = c.Id,
                              Question = c.Question
                          }).ToList();
            return new ServiceResult<List<PracticeMenusDropdownVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };
        }

    }
}
