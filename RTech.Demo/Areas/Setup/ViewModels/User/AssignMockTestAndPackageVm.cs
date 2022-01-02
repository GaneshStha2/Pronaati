using RTech.Demo.Areas.Setup.ViewModels.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.User
{
    public class AssignMockTestAndPackageVm
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public List<NaatiPackageVm> Packages { get; set; }
        public List<NaatiMockTestVm> MockTests { get; set; }
    }
}