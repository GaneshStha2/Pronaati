using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionPackage;
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
    public class NaatiMockTestApiController : ApiController
    {
        SNaatiMockTest _naatiMockTestService = null;
        SQuestionSetMaster _questionSetService = null;
        SLanguageType _LanguageTypeServices = null;
        SQuestionSetDetail _questionSetDetailServices = null;


        public NaatiMockTestApiController()
        {
            _naatiMockTestService = new SNaatiMockTest();
            _LanguageTypeServices = new SLanguageType();
            _questionSetService = new SQuestionSetMaster();
            _questionSetDetailServices = new SQuestionSetDetail();
        }

        [HttpPost]
        public KendoGridResult<List<NaatiMocktestKendoGridVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<ENaatiMockTestMaster> mockTestQuery;
            mockTestQuery = _naatiMockTestService.List().Data;
            int totalRowNum = mockTestQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<ENaatiMockTestMaster> paginatedQuery;

            switch (searchField)
            {
                case "Code":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = mockTestQuery.Where(x => x.Code.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = mockTestQuery.Where(x => x.Code == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Title":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = mockTestQuery.Where(x => x.Title.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = mockTestQuery.Where(x => x.Title == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Price":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = mockTestQuery.Where(x => x.Price.ToString().StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = mockTestQuery.Where(x => x.Price.ToString() == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:

                    paginatedQuery = mockTestQuery.OrderByDescending(x => x.Id).ThenBy(x => x.Title).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var vm = (from c in paginatedQuery.ToList()
                      select new NaatiMocktestKendoGridVm()
                      {
                          Id = c.Id,
                          Code = c.Code,
                          Description = c.Description,
                          Duration = c.Duration,
                          LanguageType = c.LanguageType.Name,
                          Price = c.Price,
                          Name = c.Title,
                      }).ToList();
            return new KendoGridResult<List<NaatiMocktestKendoGridVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };

        }

        [HttpPost]
        public ServiceResult<NaatiMockTestSetupVm> Post(NaatiMockTestSetupVm vm)
        {
            ENaatiMockTestMaster naatiMocktestMaster = new ENaatiMockTestMaster()
            {
                Id = vm.NaatiMockTestVm.Id,
                Code = vm.NaatiMockTestVm.Code,
                CreatedBy = RiddhaSession.UserId,
                CreatedDate = DateTime.Now,
                Description = vm.NaatiMockTestVm.Description,
                Duration = vm.NaatiMockTestVm.Duration,
                ImageURL = vm.NaatiMockTestVm.ImageURL,
                Title = vm.NaatiMockTestVm.Title,
                Price = vm.NaatiMockTestVm.Price,
                LanguageTypeId = vm.NaatiMockTestVm.LanguageTypeId,
            };

            var result = _naatiMockTestService.Add(naatiMocktestMaster);
            if (result.Status == ResultStatus.Ok)
            {
                if (vm.NaatiMockTestDetailVm != null)
                {
                    foreach (var item in vm.NaatiMockTestDetailVm)
                    {
                        item.NaatiMockTestMasterId = result.Data.Id;
                    }

                    var naatiMockTestDetails = (from c in vm.NaatiMockTestDetailVm
                                                select new ENaatiMockTestDetail()
                                                {
                                                    Id = c.Id,
                                                    NaatiMockTestMasterId = c.NaatiMockTestMasterId,
                                                    QuestionSetId = c.QuestionSetId,
                                                   
                                                }).ToList();
                    _naatiMockTestService.AddDetails(naatiMockTestDetails);
                }

            }
            return new ServiceResult<NaatiMockTestSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut]
        public ServiceResult<NaatiMockTestSetupVm> Put(NaatiMockTestSetupVm vm)
        {


            var MasterData = _naatiMockTestService.List().Data.Where(x => x.Id == vm.NaatiMockTestVm.Id).FirstOrDefault();
            MasterData.Id = vm.NaatiMockTestVm.Id;
            MasterData.Code = vm.NaatiMockTestVm.Code;
            MasterData.Description = vm.NaatiMockTestVm.Description;
            MasterData.Duration = vm.NaatiMockTestVm.Duration;
            MasterData.Title = vm.NaatiMockTestVm.Title;
            MasterData.Price = vm.NaatiMockTestVm.Price;
            MasterData.ImageURL = vm.NaatiMockTestVm.ImageURL;
            MasterData.LanguageTypeId = vm.NaatiMockTestVm.LanguageTypeId;

            var result = _naatiMockTestService.Update(MasterData);
            if (result.Status == ResultStatus.Ok)
            {
                var existingDetails = _naatiMockTestService.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == vm.NaatiMockTestVm.Id).ToList();
                _naatiMockTestService.RemoveDetailss(existingDetails);

                if (vm.NaatiMockTestDetailVm != null)
                {
                    foreach (var item in vm.NaatiMockTestDetailVm)
                    {
                        item.NaatiMockTestMasterId = result.Data.Id;
                    }

                    var naatiMockTestDetails = (from c in vm.NaatiMockTestDetailVm
                                                select new ENaatiMockTestDetail()
                                                {
                                                    Id = c.Id,
                                                    NaatiMockTestMasterId = c.NaatiMockTestMasterId,
                                                    QuestionSetId = c.QuestionSetId,
                                                    
                                                }).ToList();
                    _naatiMockTestService.AddDetails(naatiMockTestDetails);
                }

            }

            return new ServiceResult<NaatiMockTestSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };

        }

        [HttpGet]
        public ServiceResult<NaatiMockTestSetupVm> GetNaatiMockTest(int masterId)
        {
            NaatiMockTestSetupVm vm = new NaatiMockTestSetupVm();

            vm.NaatiMockTestVm = (from c in _naatiMockTestService.List().Data.Where(x => x.Id == masterId).ToList()
                                  select new NaatiMockTestVm()
                                  {
                                      Code = c.Code,
                                      CreatedBy = c.CreatedBy,
                                      CreatedDate = c.CreatedDate,
                                      Description = c.Description,
                                      Duration = c.Duration,
                                      Id = c.Id,
                                      ImageURL = c.ImageURL,
                                      LanguageTypeId = c.LanguageTypeId,
                                      Price = c.Price,
                                      Title = c.Title
                                  }).FirstOrDefault();

            vm.NaatiMockTestDetailVm = (from c in _naatiMockTestService.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == masterId).ToList()
                                        select new NaatiMockTestDetailVm()
                                        {
                                            Id = c.Id,
                                            NaatiMockTestMasterId = c.NaatiMockTestMasterId,
                                            QuestionSetId = c.QuestionSetId,
                                            Name = _questionSetService.List().Data.Where(x => x.Id == c.QuestionSetId).Select(x => x.QuestionSetName).FirstOrDefault(),
                                        }).ToList();


            return new ServiceResult<NaatiMockTestSetupVm>()
            {
                Data = vm,
                Status = ResultStatus.Ok,
                Message = ""
            };
        }

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id = 0)
        {
            var data = new ENaatiMockTestMaster();

            if (Id > 0)
            {
                data = _naatiMockTestService.List().Data.Where(x => x.Id != Id && x.Code == Code).FirstOrDefault();
            }
            else
            {

                data = _naatiMockTestService.List().Data.Where(x => x.Code == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }

        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var MasterData = _naatiMockTestService.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _naatiMockTestService.Remove(MasterData);

            var details = _naatiMockTestService.ListDetails().Data.Where(x => x.NaatiMockTestMasterId == id).ToList();
            _naatiMockTestService.RemoveDetailss(details);
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

        [HttpGet]
        public ServiceResult<List<GlobalDropdownVm>> GetQuestionSetList(int languageId)
        {
            SMockTestQuestion _mockTestQuestionServices = new SMockTestQuestion();


            var data = _mockTestQuestionServices.List().Data.Where(x => x.LanguageTypeId == languageId).ToList();
            var result = (from c in data
                          join d in _questionSetService.List().Data.ToList()
                          on c.DialogueId equals d.Id
                          select new GlobalDropdownVm()
                          {
                              Id = d.Id,
                              Name = d.QuestionSetName
                          }).ToList();


            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }


        public ServiceResult<List<GlobalDropdownVm>> GetSegmentsFromLanguageId(int languageId)
        {
            SSegment _segmentServices = new SSegment();

            var segments = (from c in _segmentServices.List().Data.Where(x => x.LanguageId == languageId).ToList()
                            select new GlobalDropdownVm()
                            {
                                Id = c.Id,
                                Name = c.Name
                            }).ToList();
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = segments,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
        
    }

}