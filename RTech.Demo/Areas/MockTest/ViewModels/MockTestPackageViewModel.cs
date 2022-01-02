using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class MockTestPackageViewModel
    {
       
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string PackageTitle{ get; set; }
        public string PackageCode{ get; set; }
        public string PurchasedDate{ get; set; }
        public string ExpiryDate{ get; set; }
        
        public bool IsExpired { get; set; }
        public bool IsTaken { get; set; }
    }
}