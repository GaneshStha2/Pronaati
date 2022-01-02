using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.QuestionPackage
{
    public class QuestionPackageKengoGridVm
    {
        public int Id { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public decimal PackagePrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        //public DateTime? LastModifiedDate { get; set; }
        //public int LastModifiedBy { get; set; }

    }
}