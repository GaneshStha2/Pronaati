using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.QuestionPackage
{
    public class QuestionPackageSaveVm
    {
        public QuestionPackageVm QuestionPackageVm { get; set; }
        public List<QuestionPackageDetailsVm> QuestionPackageDetailList { get; set; }
    }

    public class QuestionPackageVm {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string PackageCode { get; set; }
        public decimal PackagePrice { get; set; }
        public int ExpiryDuration { get; set; }
    }

    public class QuestionPackageDetailsVm {
        public int QuestionPackageMasterId { get; set; }
        public int Id { get; set; }
        public int QuestionSetId { get; set; }
        public string QuestionSetName { get; set; }
        public string QuestionSetCode { get; set; }
        public int ValidDuration { get; set; }

    }
}