using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Entity.Package;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.MockTest.ViewModels;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace RTech.Demo.Areas.MockTest.Controllers
{
    public class RealTestController : Controller
    {

        SNaatiMockTest _naatiMockTestServices = null;
        SNaatiPackage _naatiPackageServices = null;
        SQuestionSetMaster _questionSetMasterServices = null;
        SQuestionSetDetail _questionSetDetailServices = null;
        CommonServices _commonServices = null;
        SMockTestQuestion _mockTestQuestionServices = null;
        SNaatiMockTestAnswer _naatiMockTestAnswerServices = null;
        SNaatiMockTestTaken _takenNaatiMockTestServices = null;
        public RealTestController()
        {
            _naatiMockTestServices = new SNaatiMockTest();
            _naatiPackageServices = new SNaatiPackage();
            _questionSetMasterServices = new SQuestionSetMaster();
            _questionSetDetailServices = new SQuestionSetDetail();
            _commonServices = new CommonServices();
            _mockTestQuestionServices = new SMockTestQuestion();
            _naatiMockTestAnswerServices = new SNaatiMockTestAnswer();
            _takenNaatiMockTestServices = new SNaatiMockTestTaken();
        }

        // GET: MockTest/RealTest
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult QuestionSetsList(int mockTestPackageId)
        //{
        //    return View();
        //}

        //Used to get all dialogues in list
        //public ActionResult ConversationsList(string dialogueIdList)
        //{

        //    int userId = Common.StudentSession.LoggedStudent.StudentId;
        //    ConversationsListVm allConversations = new ConversationsListVm();
        //    allConversations.Converstions = new List<DialogueVm>();

        //    //Dialogue stands for questionset in the mockTest(package)

        //    //Common.NaatiMockTestSession.QuestionSetId = dialogueId;

        //    if (!string.IsNullOrEmpty(dialogueIdList))
        //    {
        //        string[] DialogueId = dialogueIdList.Split(',');

        //        for (int i = 0; i < DialogueId.Length - 1; i++)
        //        {

        //            ConversationsListVm vm = new ConversationsListVm();


        //            var idString = DialogueId[i];
        //            if (!string.IsNullOrEmpty(idString))
        //            {
        //                vm.DialogueId = Int32.Parse(DialogueId[i]);

        //                vm.DialogueTitle = _commonServices.getNaatiQuestionSetNameFromQuestionSetId(vm.DialogueId);

        //                //Take convos as segments
        //                vm.Converstions = (from c in _mockTestQuestionServices.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMaster.DialogueId == vm.DialogueId).ToList()
        //                                   join d in _mockTestQuestionServices.List().Data on c.MockTestQuestionMasterId equals d.Id
        //                                   select new DialogueVm()
        //                                   {
        //                                       Id = d.Id,
        //                                       Title = d.Title,
        //                                   }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();

        //                allConversations.Converstions.AddRange(vm.Converstions);
        //            }

        //        }

        //    }

        //    //Getting all the audios in these dialogues for test screen

        //    GetConversationsInSession(allConversations.Converstions);
        //    Common.NaatiMockTestSession.ScreenCount = 1;
        //    return RedirectToAction("StartTest", "RealTest", new { @Area = "MockTest", @screenCount = Common.NaatiMockTestSession.ScreenCount });


        //}

        public ActionResult ConversationsList(int dialogueId)
        {
            int userId = Common.StudentSession.LoggedStudent.StudentId;
            ConversationsListVm allConversations = new ConversationsListVm();
            allConversations.Converstions = new List<DialogueVm>();

            //Dialogue stands for questionset in the mockTest(package)

            //Common.NaatiMockTestSession.QuestionSetId = dialogueId;

            if (dialogueId > 0)
            {
                ConversationsListVm vm = new ConversationsListVm();

                vm.DialogueId = dialogueId;

                vm.DialogueTitle = _commonServices.getNaatiQuestionSetNameFromQuestionSetId(vm.DialogueId);

                //Take convos as segments
                vm.Converstions = (from c in _mockTestQuestionServices.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMaster.DialogueId == vm.DialogueId).ToList()
                                   join d in _mockTestQuestionServices.List().Data on c.MockTestQuestionMasterId equals d.Id
                                   select new DialogueVm()
                                   {
                                       Id = d.Id,
                                       Title = d.Title,
                                       Description =d.Description
                                   }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();

                allConversations.Converstions.AddRange(vm.Converstions);

            }

            //Getting all the audios in these dialogues for test screen

            GetConversationsInSession(allConversations.Converstions);
            Common.NaatiMockTestSession.ScreenCount = 1;



            return RedirectToAction("StartTest", "RealTest", new { @Area = "MockTest", @screenCount = Common.NaatiMockTestSession.ScreenCount });
        }

        //public ActionResult PrepareForTest(int mockTestQuestionMasterId)
        //{
        //    Common.NaatiMockTestSession.ScreenCount = 1;
        //    GetConversationsInSession(mockTestQuestionMasterId);
        //    return RedirectToAction("StartTest", "RealTest", new { @Area = "MockTest", @screenCount = Common.NaatiMockTestSession.ScreenCount });
        //}

        private void GetConversationsInSession(List<DialogueVm> Converstions)
        {
            SSegment _segmentServices = new SSegment();

            Common.NaatiMockTestSession.MocktestConversations = new List<LanguageTestViewModel>();
            Common.NaatiMockTestSession.TotalQuestionsCount = 0;
            int screenNO = 1;
            List<LanguageTestViewModel> tempList = new List<LanguageTestViewModel>();
            IQueryable<ESegment> segments = _segmentServices.List().Data;

            foreach (var item in Converstions)
            {
                Common.NaatiMockTestSession.MockTestQuestionMasterId = item.Id;
                var mockTestData = _mockTestQuestionServices.List().Data.Where(x => x.Id == item.Id).FirstOrDefault();
                string dialogueTitle = "";
                string languageType = "";
                string dialogueDescription = "";
                if (mockTestData != null)
                {
                    dialogueTitle = mockTestData.Title;
                    languageType = mockTestData.LanguageType.Name;
                    dialogueDescription = mockTestData.Description;
                }

                List<LanguageTestViewModel> list = (from c in _mockTestQuestionServices.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMasterId == item.Id).ToList()
                                                    select new LanguageTestViewModel()
                                                    {
                                                        Id = c.Id,
                                                        QuestionAudioLink = c.QuestionAudioUrl,
                                                        Description = c.Description,
                                                        DialogueTitle = dialogueTitle,
                                                        CorrectAnswerAudioLink = c.AnswerAudioUrl,
                                                        LanguageType = segments.Where(x => x.Id == c.SegmentId).Select(x => x.Name).FirstOrDefault(),
                                                        QuestionSetId = c.MockTestQuestionMaster.DialogueId,
                                                        Order = c.Order,
                                                        PackageId = NaatiMockTestSession.TestSource == TestSource.MockTest ? 0 : NaatiMockTestSession.PackageId,
                                                        DialogueDescription = dialogueDescription
                                                    }).OrderBy(x => x.Order).ToList();
                tempList.AddRange(list);

                Common.NaatiMockTestSession.TotalQuestionsCount += list.Count();



                foreach (var elem in list)
                {

                    elem.ScreenCount = screenNO;
                    screenNO++;
                    Common.NaatiMockTestSession.MocktestConversations.Add(elem);
                }

            }
            Common.NaatiMockTestSession.MocktestConversations = tempList;

        }

        public ActionResult StartTest(int screenCount = 0, string message = "")
        {

            Common.NaatiMockTestSession.ScreenCount = screenCount;
            TempData["Time"] = Common.MockTestSession.SpeakingTime;

            var currentScreenData = Common.NaatiMockTestSession.MocktestConversations.Where(x => x.ScreenCount == screenCount).FirstOrDefault();
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.TestMessage = message;
            }
            if (currentScreenData != null)
            {
                return View(currentScreenData);
            }

            return RedirectToAction("MockTestDialogues", "MockTestHome", new { @mockTestId = Common.NaatiMockTestSession.MockTestId, @testSource = NaatiMockTestSession.TestSource });
        }

        [HttpPost]
        public ActionResult StartTest(LanguageTestViewModel model)
        {

            if (string.IsNullOrEmpty(model.RecordedAudioLink))
            {
                return RedirectToAction("StartTest", "RealTest", new { @message = "Please answer this question first", @screenCount = Common.NaatiMockTestSession.ScreenCount });
            }
            UpdateAnswerInSession(model);
            Common.NaatiMockTestSession.ScreenCount++;

            // Timer Management  

            var timeSpend = Common.TimeManagement.ManageTime();
            Common.TimeManagement.SpeakingTimeManagement(timeSpend);

            if (Common.NaatiMockTestSession.ScreenCount > Common.NaatiMockTestSession.TotalQuestionsCount)
            {
                SaveAnswers();
                return RedirectToAction("TestSuccess", "RealTest");
            }
            var EndTime = new TimeSpan(0, 0, 0);
            if (Common.MockTestSession.SpeakingTime == EndTime)
            {
                return RedirectToAction("TimeEnded", "RealTest");
            }
            return RedirectToAction("StartTest", "RealTest", new { @screenCount = Common.NaatiMockTestSession.ScreenCount });
        }

        public ActionResult TimeEnded()
        {

            SaveAnswers();
            return View();
        }


        public ActionResult TestSuccess()
        {
            MailCommon mail = new MailCommon();
            var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

            string studentEmail = _commonServices.getStudentEmailFromId(Common.StudentSession.LoggedStudent.StudentId);
            string mailLink = baseUrl + "/EmailTemplete/TestCompletionEmailTemplate/Index";
            string mailBody = Common.CommonServices.GetTemplate(mailLink);

            string senderEmail = WebConfigurationManager.AppSettings["GmailUserName"].ToString();
            string senderPassword = WebConfigurationManager.AppSettings["GmailPassword"].ToString();

            mail.SendMail(studentEmail, "Mocktest Completion", mailBody, senderEmail, senderPassword);

            bool remainingDialogue = CheckRemainingDialogue();

            if (remainingDialogue == true)
            {
                return RedirectToAction("ProceedToNextDialogue", "RealTest", new { @Area = "MockTest" });
            }

            else
            {
                return View();
            }


        }

        private bool CheckRemainingDialogue()
        {
            int mockTestId = NaatiMockTestSession.MockTestId;
            TestSource testSource = NaatiMockTestSession.TestSource;
            int studentId = Common.StudentSession.LoggedStudent.StudentId;

            List<int> dialogueIds = _naatiMockTestServices.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == mockTestId).Select(x => x.QuestionSetId).ToList();


            foreach (var item in dialogueIds)
            {
                var takenTest = _naatiMockTestAnswerServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.QuestionSetId == item && x.TestSource == testSource).FirstOrDefault();
                if (takenTest == null)
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult ProceedToNextDialogue()
        {
            return View();
        }

        private void UpdateAnswerInSession(LanguageTestViewModel model)
        {
            var screenData = Common.NaatiMockTestSession.MocktestConversations.Where(x => x.ScreenCount == model.ScreenCount).FirstOrDefault();
            screenData.RecordedAudioLink = model.RecordedAudioLink;
        }

        [HttpPost]
        public ActionResult PostRecordedAudio()
        {
            var path = Server.MapPath("/AudioFiles");
            string returnFileLocation = "/AudioFiles/";
            foreach (string upload in Request.Files)
            {

                var file = Request.Files[upload];

                if (file == null) continue;

                file.SaveAs(Path.Combine(path, Request.Form[0]));
                returnFileLocation = returnFileLocation + Request.Form[0];
            }
            return new JsonResult() { Data = returnFileLocation };
            //return Json (new { Data = returnFileLocation }, JsonRequestBehavior.AllowGet  );

        }

        private void SaveAnswers()
        {
            List<ENaatiMockTestAnswer> answersList = new List<ENaatiMockTestAnswer>();

            int studentId = Common.StudentSession.LoggedStudent.StudentId;
            int mockTestId = Common.NaatiMockTestSession.MockTestId;

            if (Common.NaatiMockTestSession.MocktestConversations != null)
            {
                foreach (var item in Common.NaatiMockTestSession.MocktestConversations)
                {
                    ENaatiMockTestAnswer answer = new ENaatiMockTestAnswer()
                    {
                        AnswerAudioUrl = item.RecordedAudioLink,
                        AnsweredDate = DateTime.Now,
                        MockTestId = Common.NaatiMockTestSession.MockTestId,
                        QuestionDetailId = item.Id,
                        QuestionSetId = item.QuestionSetId,
                        StudentId = Common.StudentSession.LoggedStudent.StudentId,
                        MockTestQuestionMasterId = Common.NaatiMockTestSession.MockTestQuestionMasterId,
                        TestSource = Common.NaatiMockTestSession.TestSource,
                        PackageId = item.PackageId
                    };

                    answersList.Add(answer);
                }
                var result = _naatiMockTestAnswerServices.AddRange(answersList);

                if (result.Status == ResultStatus.Ok)
                {
                    ClearMockTestConversation();

                    ENaatiMockTestTaken testTaken = new ENaatiMockTestTaken();
                    if (Common.NaatiMockTestSession.TestSource == TestSource.MockTest)
                    {
                        testTaken = _takenNaatiMockTestServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.PackageId == null).FirstOrDefault();
                        if (testTaken == null)
                        {
                            ENaatiMockTestTaken MockTestTaken = new ENaatiMockTestTaken()
                            {
                                IsTaken = true,
                                MockTestId = mockTestId,
                                StudentId = studentId,
                                TestTakenDate = DateTime.Now,
                                IsScored = false,
                                PackageId = null
                            };
                            _takenNaatiMockTestServices.Add(MockTestTaken);
                        }
                        else
                        {
                            testTaken.TestTakenDate = DateTime.Now;
                            testTaken.IsTaken = true;
                            _takenNaatiMockTestServices.Update(testTaken);
                        }
                    }

                    else
                    {
                        testTaken = _takenNaatiMockTestServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.PackageId == Common.NaatiMockTestSession.PackageId).FirstOrDefault();
                        if (testTaken == null)
                        {
                            ENaatiMockTestTaken MockTestTaken = new ENaatiMockTestTaken()
                            {
                                IsTaken = true,
                                MockTestId = mockTestId,
                                StudentId = studentId,
                                TestTakenDate = DateTime.Now,
                                IsScored = false,
                                PackageId = Common.NaatiMockTestSession.PackageId
                            };
                            _takenNaatiMockTestServices.Add(MockTestTaken);
                        }
                        else
                        {
                            testTaken.TestTakenDate = DateTime.Now;
                            testTaken.IsTaken = true;
                            _takenNaatiMockTestServices.Update(testTaken);
                        }
                    }


                }


            }
        }


        private void ClearMockTestConversation()
        {
            Common.NaatiMockTestSession.MocktestConversations = new List<LanguageTestViewModel>();
        }
    }
}