using Riddhasoft.Setup.Services.Package;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.PackageServices
{
    public class PackageServices
    {
        SPackage _packageServices = null;
        SPackageDetails _packageDetailsService = null;
        public PackageServices()
        {
            _packageDetailsService = new SPackageDetails();
            _packageServices = new SPackage();
        }

        public List<PackageViewModel> GetPackageList()
        {

            var result = (from c in _packageServices.List().Data
                          select new PackageViewModel()
                          {
                              PackageId = c.Id,
                              PackageTitle = c.PackageTitle,
                              Description = c.Description,
                          }).ToList();
            //foreach (var item in result)
            //{
            //    var details = (from c in _packageDetailsService.List().Data.Where(x => x.EPackageId == item.PackageId
            //                   select new PackageDetailVm()
            //                   {
            //                       FileName = c.FileName,
            //                       PackageDetailId = c.Id,
            //                       FileURL = c.FileUrl,
                                  
            //                   });
            //}

            return result;

        }




    }
}