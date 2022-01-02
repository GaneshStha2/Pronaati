using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Package;
using Riddhasoft.Setup.Services.Package;
using RTech.Demo.Areas.Setup.ViewModels.Package;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.Package
{
    public class PackageApiController : ApiController
    {
        SPackage _packageServices = null;
        SPackageDetails _packageDetailServices = null;
        public PackageApiController()
        {
            _packageServices = new SPackage();
            _packageDetailServices = new SPackageDetails();
        }

        [HttpPost]
        public KendoGridResult<List<PackageMasterVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<EPackage> packageQuery;
            packageQuery = _packageServices.List().Data;
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EPackage> paginatedQuery;

            switch (searchField)
            {
                case "Code":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Code.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Code == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "PackageTitle":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.PackageTitle.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.PackageTitle == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:
                    paginatedQuery = packageQuery.OrderByDescending(x => x.Id).ThenBy(x => x.PackageTitle).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var vm = (from c in paginatedQuery
                      select new PackageMasterVm()
                      {
                          Id = c.Id,
                          PackageTitle = c.PackageTitle,
                          Code = c.Code,
                          Description = c.Description,
                          CreatedBy = c.CreatedBy,
                          CreatedDate = c.CreatedDate,

                      }).ToList();
            return new KendoGridResult<List<PackageMasterVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };
        }

        [HttpPost]
        public ServiceResult<PackageSetupVm> Post(PackageSetupVm vm)
        {
            EPackage package = new EPackage()
            {
                PackageTitle = vm.PackageMasterVm.PackageTitle,
                Description = vm.PackageMasterVm.Description,
                Code = vm.PackageMasterVm.Code,
                CreatedDate = vm.PackageMasterVm.CreatedDate,
                CreatedBy = RiddhaSession.UserId
            };

            var result = _packageServices.Add(package);

            if (result.Status == ResultStatus.Ok)
            {
                foreach (var item in vm.PackageDetailsVm)
                {
                    item.EPackageId = result.Data.Id;
                }
                var packageDetails = (from c in vm.PackageDetailsVm
                                      select new EPackageDetails()
                                      {
                                          Id = c.Id,
                                          FileName = c.FileName,
                                          FileUrl = c.FileUrl,
                                          EPackageId = c.EPackageId
                                      }).ToList();
                _packageDetailServices.AddPackageDetails(packageDetails);

            }

            return new ServiceResult<PackageSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };

        }

        [HttpPut]
        public ServiceResult<PackageSetupVm> Put(PackageSetupVm vm)
        {
            var MasterData = _packageServices.List().Data.Where(x => x.Id == vm.PackageMasterVm.Id).FirstOrDefault();
            MasterData.PackageTitle = vm.PackageMasterVm.PackageTitle;
            MasterData.Description = vm.PackageMasterVm.Description;
            MasterData.Code = vm.PackageMasterVm.Code;
            MasterData.CreatedBy = vm.PackageMasterVm.CreatedBy;
            MasterData.CreatedDate = vm.PackageMasterVm.CreatedDate;

            var result = _packageServices.Update(MasterData);
            if (result.Status == ResultStatus.Ok)
            {
                var PackageDetailsList = _packageDetailServices.List().Data.Where(x => x.EPackageId == result.Data.Id).ToList();
                _packageDetailServices.RemovePackageDetails(PackageDetailsList);
                foreach (var item in vm.PackageDetailsVm)
                {
                    item.EPackageId = result.Data.Id;
                }

                var packageDetails = (from c in vm.PackageDetailsVm
                                      select new EPackageDetails()
                                      {
                                          Id = c.Id,
                                          FileName = c.FileName,
                                          FileUrl = c.FileUrl,
                                          EPackageId = c.EPackageId
                                      }).ToList();

                _packageDetailServices.AddPackageDetails(packageDetails);

            }
            return new ServiceResult<PackageSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet]
        public ServiceResult<List<EPackageDetails>> GetDetails(int MasterId)
        {
            var result = _packageDetailServices.List().Data.Where(x => x.EPackageId == MasterId).ToList();
            return new ServiceResult<List<EPackageDetails>>()
            {
                Data = result,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }


        [HttpDelete]

        public ServiceResult<int> Delete(int id)
        {
            var masterData = _packageServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _packageServices.Remove(masterData);
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }


    }
}
