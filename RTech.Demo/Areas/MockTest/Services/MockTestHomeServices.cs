using Riddhasoft.DB;
using Riddhasoft.MockTest.Entity;
using Riddhasoft.MockTest.Services;
using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.MockTest.ViewModels;
using RTech.Demo.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.Services
{
    public class MockTestHomeServices
    {
        RiddhaDBContext db = new RiddhaDBContext();
        //public DateTime getDOBOfStudent()
        //{
        //    DateTime dob = db.StudentInformation.Where(x=>x.Id == Common.StudentSession.LoggedStudent.StudentId).FirstOrDefault().bi
        //}

        //fetching package list to display on mock test home page

        SQuestionPackageMaster _packageMasterServices = null;
        SQuestionPackageDetail _packageDetailServices = null;
        SQuestionSetMaster _questionSetMasterServices = null;
        SQuestionSetDetail _questionSetDetailServices = null;
        SMockTestPurchasedPackages _mockTestPurchasedPackages = null;
        SPackagePaymentDetails _packagePaymentDetailsServices = null;
        public MockTestHomeServices()
        {
            _packageMasterServices = new SQuestionPackageMaster();
            _packageDetailServices = new SQuestionPackageDetail();
            _questionSetMasterServices = new SQuestionSetMaster();
            _questionSetDetailServices = new SQuestionSetDetail();
            _mockTestPurchasedPackages = new SMockTestPurchasedPackages();
            _packagePaymentDetailsServices = new SPackagePaymentDetails();
        }

        public List<MockTestQuestionSetsViewModel> getQuestionSetList(int PackageId)
        {
            var scoredList = (from c in _mockTestPurchasedPackages.List().Data.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId && x.MockTestPackageId == PackageId).ToList()
                              join d in db.NaatiMockTestDetail on c.MockTestPackageId equals d.NaatiMockTestMasterId
                              join e in db.QuestionSetMaster on d.QuestionSetId equals e.Id
                              select new MockTestQuestionSetsViewModel()
                              {
                                  Id = c.Id,
                                  QuestionSetTitle = e.QuestionSetName,
                                  StartDate = c.PurchaseDate.ToString("dd-MM-yyyy"),
                                  EndDate = c.ExpiryDate.ToString("dd-MM-yyyy"),
                                  isTaken = c.IsTestTaken,
                                  PurchasedDate = c.PurchaseDate,
                                  QuestionSetId = e.Id,
                                  IsUnscored = false
                              }).GroupBy(x => x.QuestionSetId).Select(x => x.FirstOrDefault()).ToList();

            return scoredList;
        }

        public List<MockTestQuestionSetsViewModel> getUnscoredQuestionSetList(int PackageId)
        {
            var result = (from c in _mockTestPurchasedPackages.List().Data.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId && x.MockTestPackageId == PackageId && x.IsUnscored == true).ToList()
                          select new MockTestQuestionSetsViewModel()
                          {
                              Id = c.Id,
                              StartDate = c.PurchaseDate.ToString("dd-MM-yyyy"),
                              EndDate = c.ExpiryDate.ToString("dd-MM-yyyy"),
                              isTaken = c.IsTestTaken
                          }).ToList();
            return result;
        }

        public List<MockTestPackageViewModel> getQuestionPackages()
        {
            var resullt = (from c in db.MockTestPurchasedPackages.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).ToList()
                           join e in db.QuestionPackageMaster on c.MockTestPackageId equals e.Id
                           join d in db.PackagePaymentDetails.Where(x => x.PaymentFor == PaymentFor.MockTestPackage && x.UserId == Common.StudentSession.LoggedStudent.StudentId) on e.PackageCode.ToLower() equals d.CourseCode.ToLower()
                           select new MockTestPackageViewModel()
                           {
                               Id = c.Id,
                               PackageId = c.MockTestPackageId,
                               PackageTitle = e.PackageName,
                               PackageCode = e.PackageCode,
                               PurchasedDate = d.StartDate.ToString("dd-MM-yyyy"),
                               ExpiryDate = d.ExpiryDate.ToString("dd-MM-yyyy"),
                               IsExpired = d.ExpiryDate.Date < DateTime.Now.Date ? true : false
                           }).GroupBy(x => x.PackageId).Select(x => x.LastOrDefault()).ToList();
            return resullt;
        }

        public List<MockTestPackageViewModel> getBoughtNaatiMocktests()
        {

            //Package Id used as mocktest Id

            var resullt = (from c in db.MockTestPurchasedPackages.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId).ToList()
                           join e in db.NaatiMockTestMaster on c.MockTestPackageId equals e.Id
                           join d in db.PackagePaymentDetails.Where(x => x.PaymentFor == PaymentFor.NaatiMocktest && x.UserId == Common.StudentSession.LoggedStudent.StudentId) on e.Code.ToLower() equals d.CourseCode.ToLower()
                           select new MockTestPackageViewModel()
                           {
                               Id = c.Id,
                               PackageId = c.MockTestPackageId,
                               PackageTitle = e.Title,
                               PackageCode = e.Code,
                               PurchasedDate = d.StartDate.ToString("dd-MM-yyyy"),
                               ExpiryDate = d.ExpiryDate.ToString("dd-MM-yyyy"),
                               IsExpired = d.ExpiryDate.Date < DateTime.Now.Date ? true : false,
                               IsTaken = CheckTestGiven(c.MockTestPackageId)
                           }).GroupBy(x => x.PackageId).Select(x => x.LastOrDefault()).ToList();
            return resullt;
        }

        private bool CheckTestGiven(int mockTestId)
        {
            SNaatiMockTest _naatiMockTestServices = new SNaatiMockTest();
            bool isTaken = false;
            var mockTest = db.NaatiMockTestTaken.Where(x => x.MockTestId == mockTestId && x.PackageId == null && x.StudentId == StudentSession.LoggedStudent.StudentId).FirstOrDefault();
            if (mockTest != null)
            {
                List<DialogueVm> vm = (from c in _naatiMockTestServices.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == mockTestId).ToList()
                                       select new DialogueVm()
                                       {
                                           Id = c.QuestionSetId,
                                           IsTaken = GetDialogueTestStatus(c.QuestionSetId, TestSource.MockTest,mockTestId)
                                       }).ToList();
                
                if(vm.Any(x=> x.IsTaken == false))
                {
                    isTaken = false;
                }
                else
                {
                    isTaken = true;
                }
            }

            return isTaken;
        }
        private bool GetDialogueTestStatus(int dialogueId, TestSource testSource, int mockTestId)
        {
            SNaatiMockTestAnswer _naatiMockTestAnswerServices = new SNaatiMockTestAnswer();
            int studentId = Common.StudentSession.LoggedStudent.StudentId;

            var takenTest = _naatiMockTestAnswerServices.List().Data.Where(x => x.StudentId == studentId && x.MockTestId == mockTestId && x.QuestionSetId == dialogueId && x.TestSource == testSource).FirstOrDefault();
            if (takenTest != null)
            {
                return true;
            }
            return false;
        }

        public void setQuestionSetValuesInSession(int questionSetId)
        {
            Common.MockTestSession.QuestionList = new List<MockQuestionTypeListVm>();
            Common.MockTestSession.SpeakingTypeOne = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.SpeakingTypeTwo = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.SpeakingTypeThree = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.SpeakingTypeFour = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.SpeakingTypeFive = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.SpeakingTypeSix = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.SpeakingTypeSeven = new List<SpeakingAnswerMockTestViewModel>();
            Common.MockTestSession.WritingTypeOne = new List<WritingAnswerMockTestViewModel>();
            Common.MockTestSession.WritingTypeTwo = new List<WritingAnswerMockTestViewModel>();
            Common.MockTestSession.ReadingTypeOne = new List<ReadingTypeOneAnswerMockTestViewModel>();
            Common.MockTestSession.ReadingTypeTwo = new List<ReadingTypeTwoAnswerMockTestViewModel>();
            Common.MockTestSession.ReadingTypeThree = new List<ReadingTypeThreeAnswerMockTestViewModel>();
            Common.MockTestSession.ReadingTypeFour = new List<ReadingTypeFourAnswerMockTestViewModel>();
            Common.MockTestSession.ReadingTypeFive = new List<ReadingTypeFiveAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeOne = new List<ListeningTypeOneAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeTwo = new List<ListeningTypeTwoAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeThree = new List<ListeningTypeThreeAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeFour = new List<ListeningTypeFourAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeFive = new List<ListeningTypeFiveAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeSix = new List<ListeningTypeSixAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeSeven = new List<ListeningTypeSevenAnswerMockTestViewModel>();
            Common.MockTestSession.ListeningTypeEight = new List<ListeningTypeEightAnswerMockTestViewModel>();
            var data = _questionSetDetailServices.List().Data.Where(x => x.QuestionSetMasterId == questionSetId).ToList();
            foreach (var item in data)
            {
                MockQuestionTypeListVm obje = new MockQuestionTypeListVm()
                {
                    //QuestionId = item.QuestionId,
                    //QuestionType = item.QuestionType
                };
                Common.MockTestSession.QuestionList.Add(obje);
            }

        }

        public int getMockTestPackageIdFromCode(string packageCode)
        {
            if (!string.IsNullOrEmpty(packageCode))
            {
                var data = db.QuestionPackageMaster.Where(x => x.PackageCode.ToLower() == packageCode.ToLower()).FirstOrDefault();
                if (data != null)
                {
                    int packageId = data.Id;
                    return packageId;
                }
            }
            return 0;
        }

        public string getMockTestPackageCode(int mockTestPackageId)
        {
            if (mockTestPackageId != 0)
            {
                var data = _packageMasterServices.List().Data.Where(x => x.Id == mockTestPackageId).FirstOrDefault();
                if (data != null)
                {
                    return data.PackageCode.ToLower();
                }
            }
            return "";
        }

        public void ResetQuestionSetTaken(int questionSetId)
        {

            var data = db.MockTestPurchasedPackages.Where(x => x.StudentId == Common.StudentSession.LoggedStudent.StudentId && x.MockTestPackageId == Common.MockTestSession.QuestionPackageId).FirstOrDefault();
            if (data != null)
            {
                data.IsTestTaken = false;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public string[] RemoveNullEntriesInArray(string[] array)
        {
            if (array.Count() != 0)
            {
                List<string> tempList = array.ToList();

                checkingStartsHere:
                var xx = tempList.Where(x => x == null || x == "").FirstOrDefault();
                if (xx != null)
                {
                    tempList.Remove(xx);
                    goto checkingStartsHere;
                }


                array = tempList.ToArray();

            }
            return array;
        }


    }
}