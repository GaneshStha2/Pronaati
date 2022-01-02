using Riddhasoft.DB;
using Riddhasoft.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services
{
    public class STransactionHistory
    {
        RiddhaDBContext db = null;
        public STransactionHistory()
        {
            db = new RiddhaDBContext();
        }

        public ServiceResult<List<TransactionHistoryVm>> getTransactionHistory()
        {
            //from c in db.PackagePaymentDetails.Where(x => x.UserId == Common.StudentSession.LoggedStudent.StudentId).ToList()


            var result = (from c in db.StudentInformation.ToList()
                          join d in db.PackagePaymentDetails.ToList() on c.Id equals d.UserId
                          select new TransactionHistoryVm()
                          {
                              Id = c.Id,
                              Amount = "$ " + d.Amount,
                              PurchasedDate = d.TransactionDateTime.ToString("dd-MM-yyyy"),
                              StudentName = c.FirstName + " " + c.MiddleName + " " + c.LastName,
                              TestName = GetTestName(d.CourseCode),
                              TransactionType = d.CardType,
                          }).ToList();

            return new ServiceResult<List<TransactionHistoryVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        private string GetTestName(string courseCode)
        {
            var testName = "";
            string purchaseTestName = db.NaatiMockTestMaster.Where(x => x.Code == courseCode).Select(x => x.Title).FirstOrDefault();
            if (string.IsNullOrEmpty(purchaseTestName))
            {
                purchaseTestName = db.NaatiPackage.Where(x => x.Code == courseCode).Select(x => x.Title).FirstOrDefault();
                testName = purchaseTestName + " " + "(Package)";
            }

            testName = purchaseTestName + " " + "(Mocktest)";
            return testName;
        }


    }

    public class TransactionHistoryVm
    {
        public int Id { get; set; }

        public string StudentName { get; set; }
        public string TestName { get; set; }
        public string PurchasedDate { get; set; }
        public string Amount { get; set; }
        public string TransactionType { get; set; }
    }
}
