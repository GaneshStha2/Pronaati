using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Riddhasoft.Setup.Entity.Package
{
    public class ENaatiPackage
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public PackageType PackageType { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int LanguageTypeId { get; set; }
        public virtual ELanguageType LanguageType { get; set; }
    }


    public class EPackageFile
    {
        public int Id { get; set; }
        public int NaatiPackageId { get; set; }
        public FileType FileType { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }

    public class EPackageMockTest
    {
        public int Id { get; set; }
        public int NaatiPackageId { get; set; }
        public int NaatiMockTestId { get; set; }
        public PackageTestType PackageTestType { get; set; }
    }

    public enum PackageTestType
    {
        PracticeTest,
        RealTest
    }


    public enum PackageType
    {
        Express,
        Advanced,
        Master
    }
}
