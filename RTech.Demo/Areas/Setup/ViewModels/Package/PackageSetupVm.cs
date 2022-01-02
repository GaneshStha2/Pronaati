using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Package
{
    public class PackageSetupVm
    {
        public PackageMasterVm PackageMasterVm { get; set; }
        public List<PackageDetailsVm> PackageDetailsVm { get; set; }
    }
}