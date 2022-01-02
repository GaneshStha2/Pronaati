using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using Riddhasoft.Student.Entity;
using Riddhasoft.Student.Services;
using RTech.Demo.Areas.MockTest.Services;
using RTech.Demo.Areas.MockTest.ViewModels;
using RTech.Demo.Areas.Student.Services;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTech.Demo.Areas.MockTest.Controllers
{
    public class MockTestHomeController : Controller
    {
        MockTestHomeServices _mockTestHomeServices = null;
        SStudentInformation _studentInformation = null;

        SMockTestReport _mockTestReportServices = null;
        ReportServices _reportServices = null;
        SMockTestWritingEvaluation _mockTestWritingEvaluationServices = null;
        SMockTestPurchasedPackages _mockTestPurchasedPackagesServices = null;
        SQuestionSetMaster _questionSetMasterServices = null;

        SNaatiMockTest _naatiMockTestServices = null;
        CommonServices _commonServices = null;
        public MockTestHomeController()
        {
            _mockTestHomeServices = new MockTestHomeServices();
            _studentInformation = new SStudentInformation();
            _mockTestReportServices = new SMockTestReport();
            _reportServices = new ReportServices();
            _mockTestWritingEvaluationServices = new SMockTestWritingEvaluation();
            _mockTestPurchasedPackagesServices = new SMockTestPurchasedPackages();
            _questionSetMasterServices = new SQuestionSetMaster();

            _naatiMockTestServices = new SNaatiMockTest();
            _commonServices = new CommonServices();
        }
        // GET: MockTest/MockTestHome
        public ActionResult Index()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn", new { Area = "Student" });
            }

            var questionPackagesList = _mockTestHomeServices.getQuestionPackages();
            return View(questionPackagesList);
        }

        public ActionResult MyNaatiMocktestList()
        {
            if (Common.StudentSession.LoggedStudent == null)
            {
                return RedirectToAction("Index", "LogIn", new { Area = "Student" });
            }

            //QuestionPAckageId == MOckTestId 


            var mockTestsList = _mockTestHomeServices.getBoughtNaatiMocktests();
            Common.MockTestSession.SpeakingTime = new TimeSpan(0, 20 , 0);
            Common.MockTestSession.StartDateTime = DateTime.Now;
            Common.MockTestSession.UserOnMockTestDateTime = DateTime.Now;
            return View(mockTestsList);
        }


        public ActionResult MockTestDialogues(int mockTestId, TestSource testSource)
        {

            Common.NaatiMockTestSession.MockTestId = mockTestId;
            Common.NaatiMockTestSession.TestSource = testSource;

            //Common.NaatiMockTestSession.PackageId = mockTestId;
            //Dialogue stands for questionset in the mockTest(package)

            int userId = Common.StudentSession.LoggedStudent.StudentId;
           

            List<DialogueVm> vm = new List<DialogueVm>();

            vm = (from c in _naatiMockTestServices.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == mockTestId).ToList()
                  select new DialogueVm()
                  {
                      Id = c.QuestionSetId,
                      Title = _commonServices.getNaatiQuestionSetNameFromQuestionSetId(c.QuestionSetId),
                      IsTaken = GetDialogueTestStatus(c.QuestionSetId, testSource)
                  }).ToList();

            return View(vm);
        }

        private bool GetDialogueTestStatus(int dialogueId, TestSource testSource)
        {
            SNaatiMockTestAnswer _naatiMockTestAnswerServices = new SNaatiMockTestAnswer();
            int mockTestId = NaatiMockTestSession.MockTestId;
            int studentId = Common.StudentSession.LoggedStudent.StudentId;

            var takenTest = _naatiMockTestAnswerServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.QuestionSetId == dialogueId && x.TestSource == testSource).FirstOrDefault();
            if (takenTest != null)
            {
                return true;
            }
            return false;
        }

        public ActionResult QuestionSetsPage(int dialogueId)
        {
            return RedirectToAction("ConversationsList", "RealTest", new { @Area = "MockTest", dialogueId = dialogueId });

        }


        //public ActionResult QuestionSetsPage(int questionPackageId, TestSource testSource)
        //{
        //    Common.NaatiMockTestSession.MockTestId = questionPackageId;
        //    Common.NaatiMockTestSession.TestSource = testSource;

        //    List<int> dialogueIdList = _mockTestHomeServices.getQuestionSetList(questionPackageId).Select(x => x.QuestionSetId).ToList();

        //    string dialogueIDString = "";
        //    foreach (var item in dialogueIdList)
        //    {
        //        dialogueIDString = dialogueIDString + item + ",";
        //    }
        //    Common.NaatiMockTestSession.PackageId = questionPackageId;

        //    return RedirectToAction("ConversationsList", "RealTest", new { @Area = "MockTest", @dialogueIdList = dialogueIDString });

        //}


        public ActionResult BeginMockTest(int questionSetId)
        {
            if (_mockTestPurchasedPackagesServices.List().Data.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault().IsTestTaken == true)
            {
                return RedirectToAction("Index");
            }
            Common.MockTestSession.QuestionSetId = questionSetId;
            _mockTestHomeServices.setQuestionSetValuesInSession(Common.MockTestSession.QuestionSetId);
            return RedirectToAction("Index", "MockTestSpeaking");
        }


        public ActionResult RenewMockTest(int mockTestId)
        {
            SNaatiMockTestTaken _naatiMockTestTakenServices = new SNaatiMockTestTaken();
            SNaatiMockTestAnswer _naatiMockTestAnswerServices = new SNaatiMockTestAnswer();
            int studentId = StudentSession.LoggedStudent.StudentId;
            ENaatiMockTestTaken mockTestTaken = _naatiMockTestTakenServices.List().Data.Where(x => x.MockTestId == mockTestId && x.StudentId == studentId).FirstOrDefault();
            if(mockTestTaken != null)
            {
                _naatiMockTestTakenServices.Remove(mockTestTaken);

                List<ENaatiMockTestAnswer> testAnswers = _naatiMockTestAnswerServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.TestSource == TestSource.MockTest).ToList();
                _naatiMockTestAnswerServices.RemoveRange(testAnswers);
            }
            return RedirectToAction("MyNaatiMocktestList", "MockTestHome", new { @Area = "MockTest" });
        }

    }
}