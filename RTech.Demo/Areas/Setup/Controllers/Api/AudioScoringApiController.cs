using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.Setup.ViewModels;
using RTech.Demo.Common;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class AudioScoringApiController : ApiController
    {
        SNaatiScore _scoreServices = null;

        public AudioScoringApiController()
        {
            _scoreServices = new SNaatiScore();
        }

        [HttpPost]
        public KendoGridResult<List<AudioScoringViewModel>> GetAudioScoresKendoGrid(KendoPageListArguments arg)
        {

            var list = GetMockTestsForNatiScoreGrid();
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            List<AudioScoringViewModel> paginatedQuery;

            switch (searchField)
            {
                case "StudentName":
                    if (searchOp == "startswith")
                    {

                        paginatedQuery = list.Where(x => x.StudentName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.StudentName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;
                case "MockTestName":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.MockTestName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.MockTestName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;

                default:
                    paginatedQuery = list.OrderByDescending(x => x.Id).ThenBy(x => x.MockTestId).Skip(arg.Skip).Take(arg.Take).ToList();
                    break;
            }



            //var result = (from c in paginatedQuery
            //              select new AudioScoringViewModel()
            //              {
            //                  Id = c.Id,
            //                  MockTestId = c.MockTestId,
            //                  MockTestName = c.MockTestName,
            //                  StudentId = c.StudentId,
            //                  StudentName = c.StudentName,
            //                  IsScored = c.IsScored
            //              }).ToList();

            return new KendoGridResult<List<AudioScoringViewModel>>()
            {
                Data = paginatedQuery,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };
        }

        [HttpPost]
        public ServiceResult<AudioScoringSetupVm> Post(AudioScoringSetupVm model)
        {
            SNaatiMockTestTaken _naatiMockTestTakenServices = new SNaatiMockTestTaken();

            ENaatiScore questionPackage = new ENaatiScore()
            {
                MockTestId = model.Master.MockTestId,
                ScoredBy = RiddhaSession.UserId,
                ScoredDate = DateTime.Now,
                StudentId = model.Master.StudentId,
                FeedBacks = model.FeedBacks,
                PackageId = model.Master.PackageId
            };

            var result = _scoreServices.Add(questionPackage);
            if (result.Status == ResultStatus.Ok)
            {
                if (model.DialogueDetails != null)
                {

                    foreach (var item in model.DialogueDetails)
                    {
                        item.NaatiScoreId = result.Data.Id;
                    }

                    List<ENaatiScoreDetail> details = (from c in model.DialogueDetails
                                                       select new ENaatiScoreDetail()
                                                       {
                                                           NaatiScoreId = c.NaatiScoreId,
                                                           QuestionSetId = c.QuestionSetId,
                                                           QuestionId = c.QuestionId,
                                                           QuestionScore = c.QuestionScore,
                                                       }).ToList();
                    _scoreServices.AddNaatiScoreDetails(details);

                    //Making scored
                    int questionSetId = details.Select(x => x.QuestionSetId).FirstOrDefault();
                    var naatimocktesttaken = _naatiMockTestTakenServices.List().Data.Where(x => x.MockTestId == model.Master.MockTestId && x.StudentId == model.Master.StudentId && x.PackageId == model.Master.PackageId).FirstOrDefault(); 
                    if (naatimocktesttaken == null)
                    {
                        naatimocktesttaken = _naatiMockTestTakenServices.List().Data.Where(x => x.MockTestId == model.Master.MockTestId && x.StudentId == model.Master.StudentId).FirstOrDefault();
                    }
                   
                    if (naatimocktesttaken != null)
                    {
                        naatimocktesttaken.IsScored = true;
                        _naatiMockTestTakenServices.Update(naatimocktesttaken);

                        SendReportAvailableEmail(model.Master.StudentId);

                        //model.Master.IsScored = true;
                    }
                }
            }

            return new ServiceResult<AudioScoringSetupVm>()
            {
                Data = model,
                Message = result.Message,
                Status = result.Status
            };
        }

        private void SendReportAvailableEmail(int studentId)
        {
            CommonServices _commonServices = new CommonServices();
            string studentEmail = _commonServices.getStudentEmailFromId(studentId);

            if (!string.IsNullOrEmpty(studentEmail))
            {
                MailCommon mail = new MailCommon();
                var baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                string mailLink = baseUrl + "/EmailTemplete/TestCompletionEmailTemplate/ScoreAvailable";
                string mailBody = Common.CommonServices.GetTemplate(mailLink);

                string senderEmail = WebConfigurationManager.AppSettings["GmailUserName"].ToString();
                string senderPassword = WebConfigurationManager.AppSettings["GmailPassword"].ToString();

                mail.SendMail(studentEmail, "PRO NAATI Mock Test Score Report Available Notification", mailBody, senderEmail, senderPassword);

            }

        }

        [HttpPut]
        public ServiceResult<AudioScoringSetupVm> Put(AudioScoringSetupVm model)
        {
            ENaatiScore naatiScore = _scoreServices.List().Data.Where(x => x.Id == model.Master.Id).FirstOrDefault();
            naatiScore.ScoredBy = RiddhaSession.UserId;
            naatiScore.ScoredDate = DateTime.Now;
            naatiScore.FeedBacks = model.FeedBacks;
            var result = _scoreServices.Update(naatiScore);

            if (result.Status == ResultStatus.Ok)
            {
                int questionSetId = model.DialogueDetails.Select(x => x.QuestionSetId).FirstOrDefault();
                var existingDetails = _scoreServices.ListNaatiScoreDetails().Data.Where(x => x.NaatiScoreId == model.Master.Id && x.QuestionSetId == questionSetId).ToList();
                if (existingDetails != null)
                {
                    _scoreServices.RemoveNaatiScoreDetails(existingDetails);
                }

                List<ENaatiScoreDetail> details = (from c in model.DialogueDetails
                                                   select new ENaatiScoreDetail()
                                                   {
                                                       NaatiScoreId = model.Master.Id,
                                                       QuestionSetId = c.QuestionSetId,
                                                       QuestionId = c.QuestionId,
                                                       QuestionScore = c.QuestionScore,
                                                   }).ToList();
                _scoreServices.AddNaatiScoreDetails(details);
            }
            return new ServiceResult<AudioScoringSetupVm>()
            {
                Data = model,
                Message = result.Message,
                Status = result.Status
            };
        }


        [HttpGet]
        public ServiceResult<AudioScoringSetupVm> GetMockTestToScoreByMockTestIdAndStudentId(int packageId, int mockTestId, int studentId, int dialogueId)
        {
            var data = GetMockTestForScoring(packageId, mockTestId, studentId, dialogueId);

            return new ServiceResult<AudioScoringSetupVm>()
            {
                Data = data,
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        [HttpGet]
        public ServiceResult<AudioScoringSetupVm> GetMockTestmasterDataToScoreByMockTestIdAndStudentId(int packageId, int mockTestId, int studentId)
        {
            AudioScoringSetupVm vm = new AudioScoringSetupVm();

            var masterData = GetMockTestsForNatiScoreGrid();
            vm.Master = masterData.Where(x => x.MockTestId == mockTestId && x.StudentId == studentId && x.PackageId == packageId).FirstOrDefault();
            return new ServiceResult<AudioScoringSetupVm>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        //used to fetch Data for Grid
        private List<AudioScoringViewModel> GetMockTestsForNatiScoreGrid()
        {
            SNaatiMockTestTaken _takenTestServices = new SNaatiMockTestTaken();
            SNaatiMockTest _mockTestServices = new SNaatiMockTest();
            SStudentInformation _studentServices = new SStudentInformation();

            var data = (from c in _takenTestServices.List().Data.ToList()
                        join d in _mockTestServices.List().Data.ToList() on c.MockTestId equals d.Id
                        join e in _studentServices.List().Data.ToList() on c.StudentId equals e.Id
                        select new AudioScoringViewModel()
                        {
                            MockTestId = c.MockTestId,
                            MockTestName = d.Title,
                            StudentId = c.StudentId,
                            StudentName = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                            IsScored = c.IsScored,
                            PackageName = GetPackageName(c.PackageId),
                            PackageId = c.PackageId == null ? 0 : (int)c.PackageId
                        }).ToList();

            return data;
        }

        private string GetPackageName(int? packageId)
        {
            SNaatiPackage _naatiPackage = new SNaatiPackage();
            string packageName = "";
            if (packageId != null)
            {
                packageName = _naatiPackage.List().Data.Where(x => x.Id == packageId).Select(x => x.Title).FirstOrDefault();
            }
            return packageName;
        }


        //userd to fetch mockTest detail of a certain mockTest 
        private AudioScoringSetupVm GetMockTestForScoring(int packageId, int mockestId, int studentId, int dialogieId)
        {
            SNaatiMockTestTaken _takenTestServices = new SNaatiMockTestTaken();
            SNaatiMockTest _mockTestServices = new SNaatiMockTest();
            SStudentInformation _studentServices = new SStudentInformation();
            SNaatiScore _naatiScoreServices = new SNaatiScore();

            AudioScoringSetupVm vm = new AudioScoringSetupVm();

            var masterData = GetMockTestsForNatiScoreGrid();

            vm.Master = masterData.Where(x => x.MockTestId == mockestId && x.StudentId == studentId && x.PackageId == packageId).FirstOrDefault();
            //For Scores 

            int naatiScoreId = _naatiScoreServices.List().Data.Where(x => x.MockTestId == mockestId && x.StudentId == studentId && x.PackageId == packageId).Select(x => x.Id).FirstOrDefault();
            var naatiScoreDetail = _naatiScoreServices.ListNaatiScoreDetails().Data.Where(x => x.NaatiScoreId == naatiScoreId && x.QuestionSetId == dialogieId).ToList();
            vm.Master.Id = naatiScoreId;
            //Fetchiong all data with questions and answers
            var answerData = GetDetailData(packageId, mockestId, studentId, dialogieId);


            //Selecting Data for Dialogue

            vm.DialogueDetails = answerData.ToList();

            //for feedBack
            vm.FeedBacks = _naatiScoreServices.List().Data.Where(x => x.Id == vm.Master.Id).Select(x => x.FeedBacks).FirstOrDefault();

            return vm;
        }

        public List<AudioScoreDetailsViewModel> GetDetailData(int packageId, int mockTestId, int studentId, int questionSetId)
        {
            SNaatiMockTest _naatiMockTestServices = new SNaatiMockTest();
            SNaatiMockTestAnswer _naatiMockTestAnswer = new SNaatiMockTestAnswer();
            SQuestionSetMaster _questionMasterServices = new SQuestionSetMaster();
            SQuestionSetDetail _questionSetDetailServices = new SQuestionSetDetail();
            SMockTestQuestion _mockTestQuestionServices = new SMockTestQuestion();
            SSegment _segmentServices = new SSegment();

            var data = (from c in _naatiMockTestAnswer.List().Data.ToList()
                        join d in _questionMasterServices.List().Data.ToList() on c.QuestionSetId equals d.Id
                        join f in _mockTestQuestionServices.ListMockTestQuestionDetail().Data.ToList() on c.QuestionDetailId equals f.Id
                        where c.MockTestId == mockTestId && c.StudentId == studentId && c.PackageId == packageId
                        select new AudioScoreDetailsViewModel()
                        {
                            CorrectAnswerUrl = f.AnswerAudioUrl,
                            GivenAnswerUrl = c.AnswerAudioUrl,
                            QuestionAudioUrl = f.QuestionAudioUrl,
                            QuestionId = c.QuestionDetailId,
                            QuestionSetId = c.QuestionSetId,
                            QuestionSetName = d.QuestionSetName,
                            QuestionScore = GetQuestionScore(c.PackageId, c.StudentId, c.MockTestId, c.QuestionDetailId)
                        }).Where(x => x.QuestionSetId == questionSetId).GroupBy(x => x.QuestionId).Select(x => x.FirstOrDefault()).ToList();

            return data;
        }

        private decimal GetQuestionScore(int packageId, int studentId, int mockTestId, int questionDetailId)
        {
            SNaatiScore _naatiScoreServices = new SNaatiScore();
            int naatiScoreId = _naatiScoreServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.PackageId == packageId).Select(x => x.Id).FirstOrDefault();
            decimal score = _naatiScoreServices.ListNaatiScoreDetails().Data.Where(x => x.NaatiScoreId == naatiScoreId && x.QuestionId == questionDetailId).Select(x => x.QuestionScore).FirstOrDefault();
            return score;
        }

        [HttpGet]
        public ServiceResult<List<GlobalDropdownVm>> GetQuestionSetList(int mockTestId)
        {
            SNaatiMockTest _mockTestServices = new SNaatiMockTest();
            SQuestionSetMaster _questionSetServices = new SQuestionSetMaster();
            SQuestionSetDetail _questionSetDetailServices = new SQuestionSetDetail();

            var result = (from c in _mockTestServices.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == mockTestId).ToList()
                          join d in _questionSetServices.List().Data.ToList()
                          on c.QuestionSetId equals d.Id
                          select new GlobalDropdownVm()
                          {
                              Id = d.Id,
                              Name = d.QuestionSetName
                          }).Distinct().ToList();
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        [HttpGet]
        public ServiceResult<List<GlobalDropdownVm>> GetFeedBacks()
        {
            SFeedbBack _feedBackServices = new SFeedbBack();
            var feedBacks = (from c in _feedBackServices.List().Data
                             select new GlobalDropdownVm()
                             {
                                 Id = c.Id,
                                 Name = c.Feedback
                             }).ToList();

            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = feedBacks,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
    }
}
