using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity
{
    public class EPackagePaymentDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public PaymentFor PaymentFor { get; set; }
        public string CourseCode { get; set; }
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardIssuer { get; set; }
        public string ReceiptNumber { get; set; }
        public string acquirerTransactionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string NameOnCard { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public enum PaymentFor
    {
        OnlineTraining,
        OnlineCourse,
        Tutorial,
        MockTestPackage,


        NaatiPackage,
        NaatiMocktest
    }
}
