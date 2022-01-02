using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Entity.Package;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.Package;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.Setup.ViewModels.Course;
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
    public class NaatiPackageApiController : ApiController
    {
        SNaatiPackage _naatiPackageServices = null;
        SLanguageType _LanguageTypeServices = null;
        SQuestionSetMaster _questionSetService = null;
        SNaatiMockTest _naatiMockTestServices = null;
        public NaatiPackageApiController()
        {
            _naatiPackageServices = new SNaatiPackage();
            _LanguageTypeServices = new SLanguageType();
            _questionSetService = new SQuestionSetMaster();
            _naatiMockTestServices = new SNaatiMockTest();
        }


        [HttpPost]
        public KendoGridResult<List<NaatiPackageGridVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<ENaatiPackage> packageQuery;
            packageQuery = _naatiPackageServices.List().Data;
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<ENaatiPackage> paginatedQuery;

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
                case "Title":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Title.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Title == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Price":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Price.ToString().StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Price.ToString() == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "LanguageType":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.LanguageType.Name.ToString().StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.LanguageType.Name.ToString() == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:

                    paginatedQuery = packageQuery.OrderByDescending(x => x.Id).ThenBy(x => x.Title).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var vm = (from c in paginatedQuery.ToList()
                      select new NaatiPackageGridVm()
                      {
                          Id = c.Id,
                          Code = c.Code,
                          Description = c.Description,
                          Duration = c.Duration,
                          LanguageType = c.LanguageType.Name,
                          PackageType = Enum.GetName(typeof(PackageType), c.PackageType),
                          Price = c.Price,
                          Name = c.Title
                      }).ToList();
            return new KendoGridResult<List<NaatiPackageGridVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };

        }

        [HttpPost]
        public ServiceResult<NaatiPackageSetUpVm> Post(NaatiPackageSetUpVm vm)
        {
            ENaatiPackage naatiPackage = new ENaatiPackage()
            {
                Id = vm.NaatiPackageVm.Id,
                Code = vm.NaatiPackageVm.Code,
                CreatedBy = RiddhaSession.UserId,
                CreatedDate = DateTime.Now,
                Description = vm.NaatiPackageVm.Description,
                Duration = vm.NaatiPackageVm.Duration,
                ImageURL = vm.NaatiPackageVm.ImageURL,
                Title = vm.NaatiPackageVm.Name,
                Price = vm.NaatiPackageVm.Price,
                LanguageTypeId = vm.NaatiPackageVm.LanguageTypeId,
                PackageType = vm.NaatiPackageVm.PackageType
            };

            var result = _naatiPackageServices.Add(naatiPackage);
            if (result.Status == ResultStatus.Ok)
            {
                if (vm.PackageFilesVm != null)
                {
                    foreach (var item in vm.PackageFilesVm)
                    {
                        item.NaatiPackageId = result.Data.Id;
                    }

                    var packageFiles = (from c in vm.PackageFilesVm
                                        select new EPackageFile()
                                        {
                                            Id = c.Id,
                                            FileName = c.FileName,
                                            FileType = c.FileType,
                                            FileUrl = c.FileUrl,
                                            NaatiPackageId = c.NaatiPackageId
                                        }).ToList();
                    _naatiPackageServices.AddPackageFileList(packageFiles);
                }
                if (vm.PackageMockTestsVm != null)
                {
                    foreach (var item in vm.PackageMockTestsVm)
                    {
                        item.NaatiPackageId = result.Data.Id;

                    }
                    var packageMockTestList = (from c in vm.PackageMockTestsVm
                                               select new EPackageMockTest()
                                               {
                                                   NaatiPackageId = c.NaatiPackageId,
                                                   Id = c.Id,
                                                   NaatiMockTestId = c.QuestionSetId,
                                                   PackageTestType = PackageTestType.RealTest
                                               }).ToList();
                    _naatiPackageServices.AddPackageMockTestList(packageMockTestList);
                }
                if (vm.PracticeMockTestsVm != null)
                {
                    foreach (var item in vm.PracticeMockTestsVm)
                    {
                        item.NaatiPackageId = result.Data.Id;

                    }
                    var practiceMockTestList = (from c in vm.PracticeMockTestsVm
                                                select new EPackageMockTest()
                                                {
                                                    NaatiPackageId = c.NaatiPackageId,
                                                    Id = c.Id,
                                                    NaatiMockTestId = c.QuestionSetId,
                                                    PackageTestType = PackageTestType.PracticeTest
                                                }).ToList();
                    _naatiPackageServices.AddPackageMockTestList(practiceMockTestList);
                }


            }
            return new ServiceResult<NaatiPackageSetUpVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut]
        public ServiceResult<NaatiPackageSetUpVm> Put(NaatiPackageSetUpVm vm)
        {


            var MasterData = _naatiPackageServices.List().Data.Where(x => x.Id == vm.NaatiPackageVm.Id).FirstOrDefault();
            MasterData.Id = vm.NaatiPackageVm.Id;
            MasterData.Code = vm.NaatiPackageVm.Code;
            MasterData.Description = vm.NaatiPackageVm.Description;
            MasterData.Duration = vm.NaatiPackageVm.Duration;
            MasterData.Title = vm.NaatiPackageVm.Name;
            MasterData.Price = vm.NaatiPackageVm.Price;
            MasterData.ImageURL = vm.NaatiPackageVm.ImageURL;
            MasterData.LanguageTypeId = vm.NaatiPackageVm.LanguageTypeId;
            MasterData.PackageType = vm.NaatiPackageVm.PackageType;

            var result = _naatiPackageServices.Update(MasterData);
            if (result.Status == ResultStatus.Ok)
            {
                var existingFiles = _naatiPackageServices.ListPackageFile().Data.Where(x => x.NaatiPackageId == vm.NaatiPackageVm.Id).ToList();
                _naatiPackageServices.RemovePackageFileList(existingFiles);

                if (vm.PackageFilesVm != null)
                {
                    foreach (var item in vm.PackageFilesVm)
                    {
                        item.NaatiPackageId = result.Data.Id;
                    }

                    var packageFiles = (from c in vm.PackageFilesVm
                                        select new EPackageFile()
                                        {
                                            Id = c.Id,
                                            FileName = c.FileName,
                                            FileType = c.FileType,
                                            FileUrl = c.FileUrl,
                                            NaatiPackageId = c.NaatiPackageId
                                        }).ToList();
                    _naatiPackageServices.AddPackageFileList(packageFiles);
                }

                var existingMockTests = _naatiPackageServices.ListPackageMockTest().Data.Where(x => x.NaatiPackageId == vm.NaatiPackageVm.Id).ToList();
                _naatiPackageServices.RemovePackageMockTestList(existingMockTests);

                if (vm.PackageMockTestsVm != null)
                {
                    foreach (var item in vm.PackageMockTestsVm)
                    {
                        item.NaatiPackageId = result.Data.Id;

                    }
                    var packageMockTestList = (from c in vm.PackageMockTestsVm
                                               select new EPackageMockTest()
                                               {
                                                   NaatiPackageId = c.NaatiPackageId,
                                                   Id = c.Id,
                                                   NaatiMockTestId = c.QuestionSetId,
                                                   PackageTestType = PackageTestType.RealTest
                                               }).ToList();
                    _naatiPackageServices.AddPackageMockTestList(packageMockTestList);
                }

                if (vm.PracticeMockTestsVm != null)
                {
                    foreach (var item in vm.PracticeMockTestsVm)
                    {
                        item.NaatiPackageId = result.Data.Id;

                    }
                    var practiceMockTestList = (from c in vm.PracticeMockTestsVm
                                                select new EPackageMockTest()
                                                {
                                                    NaatiPackageId = c.NaatiPackageId,
                                                    Id = c.Id,
                                                    NaatiMockTestId = c.QuestionSetId,
                                                    PackageTestType = PackageTestType.PracticeTest
                                                }).ToList();
                    _naatiPackageServices.AddPackageMockTestList(practiceMockTestList);
                }



            }

            return new ServiceResult<NaatiPackageSetUpVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet]
        public ServiceResult<NaatiPackageSetUpVm> GetNaatiPackage(int masterId)
        {
            NaatiPackageSetUpVm vm = new NaatiPackageSetUpVm();

            vm.NaatiPackageVm = (from c in _naatiPackageServices.List().Data.Where(x => x.Id == masterId).ToList()
                                 select new NaatiPackageVm()
                                 {
                                     Code = c.Code,
                                     CreatedBy = c.CreatedBy,
                                     CreatedDate = c.CreatedDate,
                                     Description = c.Description,
                                     Duration = c.Duration,
                                     Id = c.Id,
                                     ImageURL = c.ImageURL,
                                     LanguageTypeId = c.LanguageTypeId,
                                     PackageType = c.PackageType,
                                     Price = c.Price,
                                     Name = c.Title
                                 }).FirstOrDefault();

            vm.PackageFilesVm = (from c in _naatiPackageServices.ListPackageFile().Data.Where(x => x.NaatiPackageId == masterId).ToList()
                                 select new PackageFileVm()
                                 {
                                     NaatiPackageId = c.NaatiPackageId,
                                     FileName = c.FileName,
                                     FileUrl = c.FileUrl,
                                     Id = c.Id,
                                     FileType = c.FileType,
                                     FileTypeName = Enum.GetName(typeof(FileType), c.FileType)
                                 }).ToList();


            vm.PackageMockTestsVm = (from c in _naatiPackageServices.ListPackageMockTest().Data.Where(x => x.NaatiPackageId == masterId && x.PackageTestType == PackageTestType.RealTest).ToList()
                                     select new PackageMockTestVm()
                                     {
                                         Id = c.Id,
                                         NaatiPackageId = c.NaatiPackageId,
                                         QuestionSetId = c.NaatiMockTestId,
                                     }).ToList();
            vm.PracticeMockTestsVm = (from c in _naatiPackageServices.ListPackageMockTest().Data.Where(x => x.NaatiPackageId == masterId && x.PackageTestType == PackageTestType.PracticeTest).ToList()
                                      select new PackageMockTestVm()
                                      {
                                          Id = c.Id,
                                          NaatiPackageId = c.NaatiPackageId,
                                          QuestionSetId = c.NaatiMockTestId,
                                      }).ToList();


            return new ServiceResult<NaatiPackageSetUpVm>()
            {
                Data = vm,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id = 0)
        {
            var data = new ENaatiPackage();

            if (Id > 0)
            {
                data = _naatiPackageServices.List().Data.Where(x => x.Id != Id && x.Code == Code).FirstOrDefault();
            }
            else
            {

                data = _naatiPackageServices.List().Data.Where(x => x.Code == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }

        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var MasterData = _naatiPackageServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _naatiPackageServices.Remove(MasterData);

            var packageFiles = _naatiPackageServices.ListPackageFile().Data.Where(x => x.NaatiPackageId == id).ToList();
            _naatiPackageServices.RemovePackageFileList(packageFiles);

            var practiceMockTests = _naatiPackageServices.ListPackageMockTest().Data.Where(x => x.NaatiPackageId == id).ToList();
            _naatiPackageServices.RemovePackageMockTestList(practiceMockTests);

            return new ServiceResult<int>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }

        [HttpGet]
        public ServiceResult<List<LanguageTypeSetupVm>> GetLanguageTypeList()
        {
            var data = _LanguageTypeServices.List().Data;
            var result = (from c in data
                          select new LanguageTypeSetupVm()
                          {
                              Id = c.Id,
                              Name = c.Name
                          }).ToList();
            return new ServiceResult<List<LanguageTypeSetupVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok

            };
        }

        //[HttpGet]
        //public ServiceResult<List<GlobalDropdownVm>> GetQuestionSetList()
        //{
        //    var result = (from c in _questionSetService.List().Data
        //                  select new GlobalDropdownVm()
        //                  {
        //                      Id = c.Id,
        //                      Name = c.QuestionSetName
        //                  }).ToList();
        //    return new ServiceResult<List<GlobalDropdownVm>>()
        //    {
        //        Data = result,
        //        Message = "",
        //        Status = ResultStatus.Ok
        //    };
        //}


        public ServiceResult<List<GlobalDropdownVm>> GetNaatiMockTestList(int languageId)
        {
            var result = (from c in _naatiMockTestServices.List().Data.Where(x=> x.LanguageTypeId == languageId)
                          select new GlobalDropdownVm()
                          {
                              Id = c.Id,
                              Name = c.Title
                          }).ToList();
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

    }
}
