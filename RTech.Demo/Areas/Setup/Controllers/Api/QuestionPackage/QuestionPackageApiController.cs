using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionPackage;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.Setup.ViewModels.QuestionPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.QuestionPackage
{
    public class QuestionPackageApiController : ApiController
    {
        SQuestionPackageMaster _questionPackageMasterServices = null;
        SQuestionPackageDetail _questionPackageDetailServices = null;
        SQuestionSetMaster _questionSetMasterServices = null;

        public QuestionPackageApiController()
        {
            _questionPackageMasterServices = new SQuestionPackageMaster();
            _questionPackageDetailServices = new SQuestionPackageDetail();
            _questionSetMasterServices = new SQuestionSetMaster();
        }

        [HttpPost]
        public KendoGridResult<List<QuestionPackageKengoGridVm>> GetQuestionPackageKendoGrid(KendoPageListArguments arg)
        {


            var list = _questionPackageMasterServices.List().Data;
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EQuestionPackageMaster> paginatedQuery;

            switch (searchField)
            {
                case "PackageCode":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.PackageCode.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.PackageCode == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "PackageName":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.PackageName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.PackageName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "PackagePrice":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.PackagePrice.ToString().StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.PackagePrice.ToString() == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:
                    paginatedQuery = list.OrderByDescending(x => x.Id).ThenBy(x => x.PackageCode).Skip(arg.Skip).Take(arg.Take);
                    break;
            }


            var result = (from c in paginatedQuery
                          select new QuestionPackageKengoGridVm()
                          {
                              Id = c.Id,
                              PackageCode = c.PackageCode,
                              PackageName = c.PackageName,
                              PackagePrice = c.PackagePrice,
                              CreatedDate = c.CreatedDate,
                              CreatedBy = c.CreatedBy
                          }).ToList();

            return new KendoGridResult<List<QuestionPackageKengoGridVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };
        }

        [HttpPost]
        public ServiceResult<QuestionPackageSaveVm> Post(QuestionPackageSaveVm model)
        {
            EQuestionPackageMaster questionPackage = new EQuestionPackageMaster()
            {
                PackageCode = model.QuestionPackageVm.PackageCode,
                PackageName = model.QuestionPackageVm.PackageName,
                PackagePrice = model.QuestionPackageVm.PackagePrice,
                CreatedDate = DateTime.Now,
                ExpiryDuration = model.QuestionPackageVm.ExpiryDuration,
                //CreatedBy = RTech.Demo.Utilities.RiddhaSession.UserId
            };
            var result = _questionPackageMasterServices.Add(questionPackage);
            if (result != null)
            {
                var questionPackageDetailList = model.QuestionPackageDetailList;
                if (questionPackageDetailList != null)
                {

                    foreach (var item in questionPackageDetailList)
                    {
                        item.QuestionPackageMasterId = result.Data.Id;
                    }
                    var newQuestionPackageDetailList = GetPackageDetailList(questionPackageDetailList);
                    _questionPackageDetailServices.AddDetails(newQuestionPackageDetailList);
                }
            }

            return new ServiceResult<QuestionPackageSaveVm>()
            {
                Data = model,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut]
        public ServiceResult<QuestionPackageSaveVm> Put(QuestionPackageSaveVm model)
        {
            var data = _questionPackageMasterServices.List().Data.Where(x => x.Id == model.QuestionPackageVm.Id).FirstOrDefault();
            data.LastModifiedDate = DateTime.Now;
            data.LastModifiedBy = 0;//needs to be changed later
            data.PackageName = model.QuestionPackageVm.PackageName;
            data.PackageCode = model.QuestionPackageVm.PackageCode;
            data.PackagePrice = model.QuestionPackageVm.PackagePrice;
            data.ExpiryDuration = model.QuestionPackageVm.ExpiryDuration;

            var result = _questionPackageMasterServices.Update(data);

            if (result != null)
            {
                var toDeleteList = _questionPackageDetailServices.List().Data.Where(x => x.QuestionPackageMasterId == model.QuestionPackageVm.Id).ToList();
                _questionPackageDetailServices.RemoveDetailss(toDeleteList);

                var questionPackageDetailList = model.QuestionPackageDetailList;
                foreach (var item in questionPackageDetailList)
                {
                    item.QuestionPackageMasterId = result.Data.Id;
                }
                var newQuestionPackageDetailList = GetPackageDetailList(questionPackageDetailList);
                _questionPackageDetailServices.AddDetails(newQuestionPackageDetailList);
            }
            return new ServiceResult<QuestionPackageSaveVm>()
            {
                Data = model,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var data = _questionPackageMasterServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _questionPackageMasterServices.Remove(data);

            var detailsData = _questionPackageDetailServices.List().Data.Where(x => x.QuestionPackageMasterId == id).ToList();
            var detailResult = _questionPackageDetailServices.RemoveDetailss(detailsData);

            return new ServiceResult<int>()
            {
                Data = 1,
                Message = result.Message,
                Status = result.Status
            };
        }

        public ServiceResult<List<PackageQuestionSetDropDownVm>> GetQuestionSetDropDown()

        {
            var result = (from c in _questionSetMasterServices.List().Data
                          select new PackageQuestionSetDropDownVm()
                          {
                              Id = c.Id,
                              QuestionSetId = c.Id,
                              QuestionSetName = c.QuestionSetName,
                              QuestionSetCode = c.QuestionSetCode
                          }).ToList();

            return new ServiceResult<List<PackageQuestionSetDropDownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        [HttpGet]
        public ServiceResult<QuestionPackageSaveVm> GetPackageDetailsByMasterId(int Id)
        {
            QuestionPackageSaveVm Vm = new QuestionPackageSaveVm();
            Vm.QuestionPackageVm = (from c in _questionPackageMasterServices.List().Data.Where(x => x.Id == Id)
                                    select new QuestionPackageVm()
                                    {
                                        Id = c.Id,
                                        PackageName = c.PackageName,
                                        PackageCode = c.PackageCode,
                                        PackagePrice = c.PackagePrice                                        
                                    }).FirstOrDefault();

            Vm.QuestionPackageDetailList = (from c in _questionPackageDetailServices.List().Data.Where(x => x.QuestionPackageMasterId == Id).ToList()
                                            select new QuestionPackageDetailsVm()
                                            {
                                                QuestionPackageMasterId = c.QuestionPackageMasterId,
                                                Id = c.QuestionSetId,
                                                QuestionSetId = c.QuestionSetId,
                                                QuestionSetName = _questionSetMasterServices.List().Data.Where(x => x.Id == c.QuestionSetId).FirstOrDefault().QuestionSetName,
                                                QuestionSetCode = _questionSetMasterServices.List().Data.Where(x => x.Id == c.QuestionSetId).FirstOrDefault().QuestionSetCode,
                                                ValidDuration = c.ValidDuration
                                            }).ToList();
            return new ServiceResult<QuestionPackageSaveVm>()
            {
                Data = Vm,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        public List<EQuestionPackageDetail> GetPackageDetailList(List<QuestionPackageDetailsVm> list)
        {
            var detailList = (from c in list
                              select new EQuestionPackageDetail()
                              {
                                  QuestionPackageMasterId = c.QuestionPackageMasterId,
                                  QuestionSetId = c.QuestionSetId,
                                  ValidDuration = c.ValidDuration
                              }).ToList();
            return detailList;
        }

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id = 0)
        {
            var data = new EQuestionPackageMaster();

            if (Id > 0)
            {
                data = _questionPackageMasterServices.List().Data.Where(x => x.Id != Id && x.PackageCode == Code).FirstOrDefault();
            }
            else
            {

                data = _questionPackageMasterServices.List().Data.Where(x => x.PackageCode == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }
    }
}
