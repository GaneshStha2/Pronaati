using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public string PackageTitle { get; set; }       
        public string Description { get; set; }      
        public List<PackageDetailVm> PackageDetailList { get; set; }

    }

    public class PackageDetailVm
    {
        public int PackageDetailId { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }

    }

   

}