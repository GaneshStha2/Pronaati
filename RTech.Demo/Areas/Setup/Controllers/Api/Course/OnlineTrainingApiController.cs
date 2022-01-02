using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Services.Course;
using RTech.Demo.Areas.Setup.ViewModels.Course;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.Course
{
    public class OnlineTrainingApiController : ApiController
    {
        SOnlineTraining _onlineTrainingServices = null;
        SOnllineTrainingDetails _onllineTrainingDetailsServices = null;
        SLanguageType _courseTypeServices = null;
        public OnlineTrainingApiController()
        {
            _onlineTrainingServices = new SOnlineTraining();
            _onllineTrainingDetailsServices = new SOnllineTrainingDetails();
            _courseTypeServices = new SLanguageType();
        }

        [HttpPost]
        public KendoGridResult<List<OnlineTrainingMasterVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<EOnlineTraining> packageQuery;
            packageQuery = _onlineTrainingServices.List().Data;
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EOnlineTraining> paginatedQuery;

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
                      select new OnlineTrainingMasterVm()
                      {
                          Id = c.Id,
                          Code = c.Code,
                          CreatedBy = c.CreatedBy,
                          CreatedDate = c.CreatedDate,
                          Description = c.Description,
                          Duration = c.Duration,
                          ImageURL = c.ImageURL,
                          PackageTitle = c.PackageTitle,
                          CourseTypeId = c.CourseTypeId,
                          CourseTypeName = c.CourseType.Name,
                          Price = c.Price
                      }).ToList();
            return new KendoGridResult<List<OnlineTrainingMasterVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };

        }

        [HttpPost]

        public ServiceResult<OnlineTrainingSetupVm> Post(OnlineTrainingSetupVm vm)
        {
            EOnlineTraining onlineTraining = new EOnlineTraining()
            {
                Id = vm.OnlineTrainingMasterVm.Id,
                Code = vm.OnlineTrainingMasterVm.Code,
                CreatedBy = RiddhaSession.UserId,
                CreatedDate = vm.OnlineTrainingMasterVm.CreatedDate,
                Description = vm.OnlineTrainingMasterVm.Description,
                Duration = vm.OnlineTrainingMasterVm.Duration,
                ImageURL = vm.OnlineTrainingMasterVm.ImageURL,
                PackageTitle = vm.OnlineTrainingMasterVm.PackageTitle,
                Price = vm.OnlineTrainingMasterVm.Price,
                 CourseTypeId = vm.OnlineTrainingMasterVm.CourseTypeId
            };

            var result = _onlineTrainingServices.Add(onlineTraining);
            if (result.Status == ResultStatus.Ok && vm.OnlineTrainingDetailsVm != null)
            {
                foreach (var item in vm.OnlineTrainingDetailsVm)
                {
                    item.EOnlineTrainingId = result.Data.Id;
                }
                var onlinTrainingDetails = (from c in vm.OnlineTrainingDetailsVm
                                            select new EOnlineTrainingDetails()
                                            {
                                                Id = c.Id,
                                                FileName = c.FileName,
                                                EOnlineTrainingId = c.EOnlineTrainingId,
                                                FilerUrl = c.FileUrl,
                                                FileId = c.FileId,
                                                FileType = c.FileType
                                            }).ToList();
                _onllineTrainingDetailsServices.AddOnlineTrainingDetails(onlinTrainingDetails);
            }

            return new ServiceResult<OnlineTrainingSetupVm>()
            {
                Data = vm,
                Status = result.Status,
                Message = result.Message
            };
        }

        public ServiceResult<OnlineTrainingSetupVm> Put(OnlineTrainingSetupVm vm)
        {
            var MasterData = _onlineTrainingServices.List().Data.Where(x => x.Id == vm.OnlineTrainingMasterVm.Id).FirstOrDefault();
            MasterData.Code = vm.OnlineTrainingMasterVm.Code;
            MasterData.Description = vm.OnlineTrainingMasterVm.Description;
            MasterData.Duration = vm.OnlineTrainingMasterVm.Duration;
            MasterData.CreatedBy = vm.OnlineTrainingMasterVm.CreatedBy;
            MasterData.CreatedDate = vm.OnlineTrainingMasterVm.CreatedDate;
            MasterData.PackageTitle = vm.OnlineTrainingMasterVm.PackageTitle;
            MasterData.Price = vm.OnlineTrainingMasterVm.Price;
            MasterData.ImageURL = vm.OnlineTrainingMasterVm.ImageURL;
            MasterData.CourseTypeId = vm.OnlineTrainingMasterVm.CourseTypeId;

            var result = _onlineTrainingServices.Update(MasterData);

            if (result.Status == ResultStatus.Ok)
            {
                var onlineTrainingDetailsList = _onllineTrainingDetailsServices.List().Data.Where(x => x.EOnlineTrainingId == result.Data.Id).ToList();
                _onllineTrainingDetailsServices.RemoveOnlineTrainingDetails(onlineTrainingDetailsList);


                foreach (var item in vm.OnlineTrainingDetailsVm)
                {
                    item.EOnlineTrainingId = result.Data.Id;
                }

                var DetailsVm = (from c in vm.OnlineTrainingDetailsVm
                                      select new EOnlineTrainingDetails()
                                      {
                                          Id = c.Id,
                                          EOnlineTrainingId = c.EOnlineTrainingId,
                                          FileName = c.FileName,
                                          FilerUrl = c.FileUrl,
                                          FileId = c.FileId,
                                          FileType = c.FileType
                                      }).ToList();
                _onllineTrainingDetailsServices.AddOnlineTrainingDetails(DetailsVm);
               
            }
            return new ServiceResult<OnlineTrainingSetupVm>()
            {
                Data = vm,
                Status = result.Status,
                Message = result.Message
            };

        }


        public ServiceResult<List<OnlineTrainingDetailsVm>> GetDetails(int MasterId)
        {
            var vm = (from c in _onllineTrainingDetailsServices.List().Data.Where(x => x.EOnlineTrainingId == MasterId).ToList()
                      select new OnlineTrainingDetailsVm()
                      {
                          EOnlineTrainingId = c.EOnlineTrainingId,
                          FileName = c.FileName,
                          FileUrl = c.FilerUrl,
                          Id = c.Id,
                          FileId = c.FileId,
                          FileType = c.FileType,
                          FileTypeName = Enum.GetName(typeof(FileType) , c.FileType)
                      }).ToList();
           
            return new ServiceResult<List<OnlineTrainingDetailsVm>>
            {
                Data = vm,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        [HttpGet]
        public bool IsUniqueCode(string Code , int Id= 0)
        {

            var data = new EOnlineTraining();

            if (Id > 0)
            {
                data = _onlineTrainingServices.List().Data.Where(x => x.Id != Id && x.Code == Code).FirstOrDefault();
            }
            else
            {

                data = _onlineTrainingServices.List().Data.Where(x => x.Code == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;
        }

        public ServiceResult<OnlineTrainingDetailsVm> GetFileById(int FileId , int MasterId )
        {
            var vm = (from c in _onllineTrainingDetailsServices.List().Data.Where(x => x.FileId == FileId && x.EOnlineTrainingId == MasterId)
                      select new OnlineTrainingDetailsVm()
                      {
                          EOnlineTrainingId = c.EOnlineTrainingId,
                          FileId = c.FileId,
                          FileName = c.FileName,
                          FileUrl = c.FilerUrl,
                          Id = c.Id
                      }).FirstOrDefault();
            return new ServiceResult<OnlineTrainingDetailsVm>()
            {
                Data = vm,
                Status = ResultStatus.Ok,
                Message = ""

            };
        }



        public ServiceResult<int> Delete(int Id)
        {
            var MasterData = _onlineTrainingServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _onlineTrainingServices.Remove(MasterData);            
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Status = result.Status,
                Message = result.Message
            };
        }

        [HttpGet]
        public ServiceResult<List<LanguageTypeSetupVm>> GetCourseTypeList()
        {
            var data = _courseTypeServices.List().Data;
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
    }
}
