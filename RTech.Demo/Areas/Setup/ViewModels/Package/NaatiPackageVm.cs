using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Entity.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Package
{
   
    public class NaatiPackageVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
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


    public class PackageFileVm
    {
        public int Id { get; set; }
        public int NaatiPackageId { get; set; }
        public FileType FileType { get; set; }
        public string FileTypeName { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }

    public class PackageMockTestVm
    {
        public int Id { get; set; }
        public int NaatiPackageId { get; set; }
        public int QuestionSetId { get; set; }
    }

    public class NaatiPackageGridVm
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PackageType { get; set; }
        public string LanguageType { get; set; }
    }

    public class NaatiPackageSetUpVm
    {
        public NaatiPackageVm NaatiPackageVm { get; set; }
        public List<PackageFileVm> PackageFilesVm { get; set; }
        public List<PackageMockTestVm> PackageMockTestsVm { get; set; }
        public List<PackageMockTestVm> PracticeMockTestsVm { get; set; }
    }

}