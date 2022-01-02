using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Package
{
    public class PackageDetailsVm
    {
        public int Id { get; set; }
        public int EPackageId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}