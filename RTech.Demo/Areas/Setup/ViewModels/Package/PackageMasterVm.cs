using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Package
{
    public class PackageMasterVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string PackageTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        
    }
}