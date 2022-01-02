using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.QuestionSet;
using Riddhasoft.Setup.Services.ViewModel;
using RTech.Demo.Areas.Setup.ViewModels.QuestionSet;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.QuestionSet
{
    public class QuestionSetApiController : ApiController
    {
        SQuestionSetMaster _questionSetMasterServicce = null;
        SQuestionSetDetail _questionSetDetailService = null;
        SMockTestQuestion _mockTestQuestionSercvice = null;

        public QuestionSetApiController()
        {
            _questionSetDetailService = new SQuestionSetDetail();
            _questionSetMasterServicce = new SQuestionSetMaster();
            _mockTestQuestionSercvice = new SMockTestQuestion();
        }
        [HttpPost]
        public KendoGridResult<List<QuestionSetGridVm>> GetQuestionSetKendoGrid(KendoPageListArguments arg)
        {

            var list = _questionSetMasterServicce.List().Data;
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EQuestionSetMaster> paginatedQuery;

            switch (searchField)
            {
                case "QuestionSetName":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.QuestionSetName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.QuestionSetName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "QuestionSetCode":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.QuestionSetCode.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.QuestionSetCode == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;

                default:
                    paginatedQuery = list.OrderByDescending(x => x.Id).ThenBy(x => x.QuestionSetName).Skip(arg.Skip).Take(arg.Take);
                    break;
            }



            var result = (from c in paginatedQuery
                          select new QuestionSetGridVm()
                          {
                              Id = c.Id,
                              QuestionSetName = c.QuestionSetName,
                              QuestionSetCode = c.QuestionSetCode,
                              IsUnscored = c.IsUnscored,
                              CreatedBy = c.CreatedBy,
                              CreatedDate = c.CreatedDate,
                              LastModifiedDate = c.LastModifiedDate,
                              LastModifiedBy = c.LastModifiedBy,
                          }).ToList();

            return new KendoGridResult<List<QuestionSetGridVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };
        }

        [HttpGet]
        public ServiceResult<QuestionSetVm> GetQuestionSet(int Id)
        {

            QuestionSetVm Vm = (from c in _questionSetMasterServicce.List().Data.Where(x => x.Id == Id)
                                select new QuestionSetVm()
                                {
                                    Id = c.Id,
                                    QuestionSetCode = c.QuestionSetCode,
                                    QuestionSetName = c.QuestionSetName,
                                    IsUnscored = c.IsUnscored
                                }).FirstOrDefault();

            return new ServiceResult<QuestionSetVm>()
            {
                Data = Vm,
                Message = "",
                Status = ResultStatus.Ok
            };

        }



        [HttpPost]
        public ServiceResult<QuestionSetVm> Post(QuestionSetVm model)
        {
            EQuestionSetMaster QuestionSetmodel = new EQuestionSetMaster()
            {

                QuestionSetCode = model.QuestionSetCode,
                QuestionSetName = model.QuestionSetName,
                IsUnscored = false,
                CreatedDate = DateTime.Now,
                CreatedBy = RiddhaSession.UserId,
            };

            var result = _questionSetMasterServicce.Add(QuestionSetmodel);

            return new ServiceResult<QuestionSetVm>()
            {
                Data = model,
                Message = result.Message,
                Status = result.Status
            };
        }



        [HttpPut]
        public ServiceResult<QuestionSetVm> Put(QuestionSetVm model)
        {
            var data = _questionSetMasterServicce.List().Data.Where(x => x.Id == model.Id).FirstOrDefault();
            data.LastModifiedDate = DateTime.Now;
            data.LastModifiedBy = RiddhaSession.UserId;
            data.QuestionSetCode = model.QuestionSetCode;
            data.QuestionSetName = model.QuestionSetName;
            data.IsUnscored = false;

            var result = _questionSetMasterServicce.Update(data);

    
            return new ServiceResult<QuestionSetVm>()
            {
                Data = model,
                Message = result.Message,
                Status = result.Status
            };

        }

        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {

            var data = _questionSetMasterServicce.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _questionSetMasterServicce.Remove(data);

            var detailsData = _questionSetDetailService.List().Data.Where(x => x.QuestionSetMasterId == id).ToList();
            var detailsResult = _questionSetDetailService.RemoveRange(detailsData);

            return new ServiceResult<int>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

       
      
        public ServiceResult<List<QuestionSetDropDownVm>> GetMockTestQuestionsForDropDown()
        {
            var data = _mockTestQuestionSercvice.List().Data.ToList();
            var result = (from c in data
                          select new QuestionSetDropDownVm()
                          {
                              Id = c.Id,
                              QuestionTitle = c.Title
                          }).ToList();
            return new ServiceResult<List<QuestionSetDropDownVm>>()
            {
                Data = result,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
     
    
        public List<EQuestionSetDetail> GetList(List<QuestionSetDetailsViewModel> list)
        {
            var EQuestionSetDetailList = (from c in list
                                          select new EQuestionSetDetail()
                                          {
                                              QuestionSetMasterId = c.QuestionSetMasterId,
                                              Id = c.Id,
                                              QuestionId = c.Id
                                          }).ToList();

            return EQuestionSetDetailList;
        }

        public List<QuestionSetDetailsViewModel> GetDetailsList(int id)
        {
            var result = (from c in _questionSetDetailService.List().Data.Where(x => x.QuestionSetMasterId == id).ToList()
                          select new QuestionSetDetailsViewModel()
                          {
                              QuestionSetMasterId = c.QuestionSetMasterId,
                              Id = c.Id,
                              QuestionId = c.QuestionId,
                              QuestionTitle = getQuestionTitle(c.QuestionId),
                              

                          }).ToList();
            return result;
        }

    
        public string getQuestionTitle(int questionId)
        {
            string title = _mockTestQuestionSercvice.List().Data.Where(x => x.Id == questionId).Select(x => x.Title).FirstOrDefault();

            return title;

        }

       

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id = 0)
        {
            var data = new EQuestionSetMaster();

            if (Id > 0)
            {
                data = _questionSetMasterServicce.List().Data.Where(x => x.Id != Id && x.QuestionSetCode == Code).FirstOrDefault();
            }
            else
            {

                data = _questionSetMasterServicce.List().Data.Where(x => x.QuestionSetCode == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }

    }


}
