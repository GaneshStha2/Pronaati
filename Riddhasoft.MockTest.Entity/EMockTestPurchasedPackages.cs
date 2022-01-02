using Riddhasoft.Setup.Entity.QuestionSet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
   public class EMockTestPurchasedPackages
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int MockTestPackageId { get; set; }
        public bool IsUnscored { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? TestTakenDate { get; set; }
        public bool IsTestTaken { get; set; }
    }
}
