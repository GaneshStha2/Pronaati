using Riddhasoft.Setup.Entity.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class NaatiPackageVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PackageTypeName { get; set; }
        public PackageType PackageType { get; set; }
        public int Duration { get; set; }
        public int MocktestCount { get; set; }
        public decimal Price { get; set; }
    }

    public class NaatiMocktestPackageVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }
}