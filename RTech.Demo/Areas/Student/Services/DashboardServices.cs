using Riddhasoft.DB;
using Riddhasoft.Setup.Entity;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.Services
{
    public class DashboardServices
    {
        RiddhaDBContext dbContext = new RiddhaDBContext();

        public List<TransactionHistoryViewModel> getTransactionHistory()
        {
            var result = (from c in dbContext.PackagePaymentDetails.Where(x=>x.UserId == Common.StudentSession.LoggedStudent.StudentId).ToList()
                          select new TransactionHistoryViewModel()
                          {
                              Id = c.Id,
                              PackageTitle = getPackageTitle(c.CourseCode, c.PaymentFor),
                              Date = c.TransactionDateTime.ToString("dd-MM-yyyy"),
                              Amount = c.Amount,
                              PaymentMode = c.CardType,
                             
                          }).ToList();
            return result;
        }
        public string getPackageTitle(string code, PaymentFor paymentFor)
        {
            if (paymentFor == PaymentFor.OnlineCourse)
            {
                string packageTitle = dbContext.OnlineClassRoomCourse.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault().Name;
                return packageTitle;
            }
            else if (paymentFor == PaymentFor.OnlineTraining)
            {
                string packageTitle = dbContext.OnlineTraining.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault().PackageTitle;
                return packageTitle;
            }
            else if (paymentFor == PaymentFor.Tutorial)
            {
                string packageTitle = dbContext.ETutorials.Where(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault().Title;
                return packageTitle;
            }
            else if (paymentFor == PaymentFor.MockTestPackage)
            {
                string packageTitle = dbContext.QuestionPackageMaster.Where(x => x.PackageCode.ToLower() == code.ToLower()).FirstOrDefault().PackageName;
                return packageTitle;
            }

            else if(paymentFor == PaymentFor.NaatiMocktest)
            {
                string packageTitle = dbContext.NaatiMockTestMaster.Where(x => x.Code.ToLower() == code.ToLower()).Select(x=> x.Title).FirstOrDefault();
                return packageTitle;
            }

            else if(paymentFor == PaymentFor.NaatiPackage)
            {
                string packageTitle = dbContext.NaatiPackage.Where(x => x.Code.ToLower() == code.ToLower()).Select(x => x.Title).FirstOrDefault();
                return packageTitle;
            }
            return "";
        }
    

    }
}