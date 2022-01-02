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
using System.Web.Mvc;

namespace RTech.Demo.Areas.MockTest.Controllers
{
    public class PracticeTestController : Controller
    {

        SNaatiMockTest _naatiMockTestServices = null;
        SNaatiPackage _naatiPackageServices = null;
        SQuestionSetMaster _questionSetMasterServices = null;
        SQuestionSetDetail _questionSetDetailServices = null;
        CommonServices _commonServices = null;
        SMockTestQuestion _mockTestQuestionServices = null;
        public PracticeTestController()
        {
            _naatiMockTestServices = new SNaatiMockTest();
            _naatiPackageServices = new SNaatiPackage();
            _questionSetMasterServices = new SQuestionSetMaster();
            _questionSetDetailServices = new SQuestionSetDetail();
            _commonServices = new CommonServices();
            _mockTestQuestionServices = new SMockTestQuestion();
        }

        // GET: MockTest/Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MockTestDialogues(int mockTestId)
        {

            //Dialogue stands for questionset in the mockTest(package)

            int userId = Common.StudentSession.LoggedStudent.StudentId;
            NaatiMockTestSession.MockTestId = mockTestId;
            DialoguesListVm vm = new DialoguesListVm();
            vm.MockTestId = mockTestId;
            vm.MocktestTitle = _naatiMockTestServices.List().Data.Where(x => x.Id == mockTestId).Select(x => x.Title).FirstOrDefault();
            vm.Dialogues = (from c in _naatiMockTestServices.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == mockTestId).ToList()
                            select new DialogueVm()
                            {
                                Id = c.QuestionSetId,
                                Title = _commonServices.getNaatiQuestionSetNameFromQuestionSetId(c.QuestionSetId)
                            }).ToList();

            return View(vm);
        }

        //For Practice MockTest
        public ActionResult ConversationsList(int dialogueId)
        {
            //Dialogue stands for questionset in the mockTest(package)

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
                                       Description = d.Description
                                   }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();

                allConversations.Converstions.AddRange(vm.Converstions);

            }


            GetConversationsInSession(allConversations.Converstions);
            Common.NaatiMockTestSession.ScreenCount = 1;
            return RedirectToAction("StartTest", "PracticeTest", new { @Area = "MockTest", @screenCount = Common.NaatiMockTestSession.ScreenCount });

        }


        public ActionResult PrepareForTest(int mockTestQuestionMasterId)
        {
            Common.NaatiMockTestSession.ScreenCount = 1;
            GetConversationsInSession(mockTestQuestionMasterId);
            return RedirectToAction("StartTest", "PracticeTest", new { @Area = "MockTest", @screenCount = Common.NaatiMockTestSession.ScreenCount });
        }

        private void GetConversationsInSession(int mockTestQuestionMasterId)
        {
            var mockTestData = _mockTestQuestionServices.List().Data.Where(x => x.Id == mockTestQuestionMasterId).FirstOrDefault();
            string dialogueTitle = "";
            string languageType = "";
            string dialogueDescription = "";
            if (mockTestData != null)
            {
                dialogueTitle = mockTestData.Title;
                languageType = mockTestData.LanguageType.Name;
                dialogueDescription = mockTestData.Description;
            }

            List<LanguageTestViewModel> list = (from c in _mockTestQuestionServices.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMasterId == mockTestQuestionMasterId).ToList()
                                                select new LanguageTestViewModel()
                                                {
                                                    Id = c.Id,
                                                    QuestionAudioLink = c.QuestionAudioUrl,
                                                    Description = c.Description,
                                                    DialogueTitle = dialogueTitle,
                                                    CorrectAnswerAudioLink = c.AnswerAudioUrl,
                                                    LanguageType = languageType,
                                                    QuestionSetId = c.MockTestQuestionMaster.DialogueId,
                                                    DialogueDescription = dialogueDescription
                                                }).ToList();
            Common.NaatiMockTestSession.TotalQuestionsCount = list.Count();

            Common.NaatiMockTestSession.MocktestConversations = new List<LanguageTestViewModel>();
            int i = 1;
            foreach (var item in list)
            {

                item.ScreenCount = i;
                i++;
                Common.NaatiMockTestSession.MocktestConversations.Add(item);
            }


        }

        private void GetConversationsInSession(List<DialogueVm> Converstions)
        {

            Common.NaatiMockTestSession.MocktestConversations = new List<LanguageTestViewModel>();
            Common.NaatiMockTestSession.TotalQuestionsCount = 0;
            int screenNO = 1;
            List<LanguageTestViewModel> tempList = new List<LanguageTestViewModel>();
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
                                                        LanguageType = languageType,
                                                        QuestionSetId = c.MockTestQuestionMaster.DialogueId,
                                                        Order = c.Order,
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
        public ActionResult StartTest(int screenCount, string message)
        {

            Common.NaatiMockTestSession.ScreenCount = screenCount;
            var currentScreenData = Common.NaatiMockTestSession.MocktestConversations.Where(x => x.ScreenCount == screenCount).FirstOrDefault();
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.TestMessage = message;
            }
            if (currentScreenData != null)
            {
                return View(currentScreenData);
            }

            return RedirectToAction("MockTestDialogues", "PracticeTest", new {@Area = "MockTest", @mockTestId = Common.NaatiMockTestSession.MockTestId});
        }

        [HttpPost]
        public ActionResult StartTest(LanguageTestViewModel model)
        {

            if (string.IsNullOrEmpty(model.RecordedAudioLink))
            {
                return RedirectToAction("StartTest", "PracticeTest", new { @message = "Please answer this question first", @screenCount = Common.NaatiMockTestSession.ScreenCount });
            }
            UpdateAnswerInSession(model);
            Common.NaatiMockTestSession.ScreenCount++;
            if (Common.NaatiMockTestSession.ScreenCount > Common.NaatiMockTestSession.TotalQuestionsCount)
            {
                return RedirectToAction("TestSuccess", "PracticeTest");
            }
            return RedirectToAction("StartTest", "PracticeTest", new { @screenCount = Common.NaatiMockTestSession.ScreenCount });
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
            return Json(returnFileLocation);

        }


        public ActionResult TestSuccess()
        {
            return View();
        }
    }
}