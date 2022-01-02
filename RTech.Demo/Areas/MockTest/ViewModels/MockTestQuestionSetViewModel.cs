using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels
{
    public class MockTestQuestionSetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsTaken { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
    }

    public class MockTestPackagesViewModel
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
    }

    public class MockTestQuestionPackages
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string PackageName { get; set; }
        public decimal Price{ get; set; }
    }

}