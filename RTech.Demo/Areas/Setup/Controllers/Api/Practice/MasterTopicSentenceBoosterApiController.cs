using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Practice;
using Riddhasoft.Setup.Services.Practice;
using RTech.Demo.Areas.Setup.ViewModels.Practice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.Practice
{
    public class MasterTopicSentenceBoosterApiController : ApiController
    {
        SMasterTopicSentenceBooster _masterTopicSentenceBoosterServices = null;
        public MasterTopicSentenceBoosterApiController()
        {
            _masterTopicSentenceBoosterServices = new SMasterTopicSentenceBooster();
        }


        [HttpPost]
        public KendoGridResult<List<MasterTopicSentenceBoosterKendoGridVm>> GetKendoGrid(KendoPageListArguments arg) {

            var list = _masterTopicSentenceBoosterServices.List().Data;
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EMasterTopicSentenceBooster> paginatedQuery;

            switch (searchField)
            {
                case "QuestionTitle":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.QuestionTitle.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.QuestionTitle == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Question":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.Question.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.Question == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "OptionStatement":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.Question.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.Question == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:
                    paginatedQuery = list.OrderByDescending(x => x.Id).ThenBy(x => x.QuestionTitle).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var result = (from c in paginatedQuery.ToList()
                          select new MasterTopicSentenceBoosterKendoGridVm()
                          {
                              Id = c.Id,
                              QuestionTitle = c.QuestionTitle,
                              Question = c.Question,
                              OptionStatement = c.OptionStatement,
                              CorrectAnswer = _masterTopicSentenceBoosterServices.DetailsList().Data.Where(x=> x.IsCorrectAnswer == true && x.MasterTopicSentenceBoosterMasterId == c.Id).Select(x => x.Options).FirstOrDefault(),
                              Options = getOptions(c.Id)
                          }).ToList();


            return new KendoGridResult<List<MasterTopicSentenceBoosterKendoGridVm>>()
            {
                Data = result,
                TotalCount = totalRowNum
            };

        }

        public string getOptions(int MasterId)
        {
            string options = "";
            var data = _masterTopicSentenceBoosterServices.DetailsList().Data.Where(x => x.MasterTopicSentenceBoosterMasterId == MasterId).Select(x => x.Options).ToList();

            foreach (var item in data)
            {
                options = options + " " + item + ",";

            }
            return options;
        }
        public ServiceResult<MasterTopicSentenceBoosterVM> Post(MasterTopicSentenceBoosterVM vm) {

            EMasterTopicSentenceBooster model = new EMasterTopicSentenceBooster()
            {
                Question = vm.Master.Question,
                QuestionTitle = vm.Master.QuestionTitle,
                OptionStatement = vm.Master.OptionStatement
            };

            var result = _masterTopicSentenceBoosterServices.Add(model);

            if (result.Status == ResultStatus.Ok) {

                foreach (var item in vm.Details) {

                    item.MasterTopicSentenceBoosterMasterId = result.Data.Id;
                }

                var MasterTopicSentenceBoosterOptions = (from c in vm.Details
                                                         select new EMasterTopicSentenceBoosterOptionDetails()
                                                         {
                                                             IsCorrectAnswer = c.IsCorrectAnswer,
                                                             Options = c.Options,
                                                             MasterTopicSentenceBoosterMasterId = c.MasterTopicSentenceBoosterMasterId
                                                         }).ToList();

                _masterTopicSentenceBoosterServices.AddDetails(MasterTopicSentenceBoosterOptions);

                
            }
            return new ServiceResult<MasterTopicSentenceBoosterVM>()
            {
                Data = vm,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<MasterTopicSentenceBoosterVM> Put(MasterTopicSentenceBoosterVM vm) {

            var data = _masterTopicSentenceBoosterServices.List().Data.Where(x => x.Id == vm.Master.Id).FirstOrDefault();
            data.OptionStatement = vm.Master.OptionStatement;
            data.Question = vm.Master.Question;
            data.QuestionTitle = vm.Master.QuestionTitle;

            var result = _masterTopicSentenceBoosterServices.Update(data);
            if(result.Status == ResultStatus.Ok)
            {
                // delete previous Option 
                var previousOptionDetails = _masterTopicSentenceBoosterServices.DetailsList().Data.Where(x => x.MasterTopicSentenceBoosterMasterId == result.Data.Id).ToList();
                _masterTopicSentenceBoosterServices.RemoveDetails(previousOptionDetails);
                // End 

                foreach (var item in vm.Details)
                {
                    item.MasterTopicSentenceBoosterMasterId = result.Data.Id;
                }

                // add new Details
                var newOptionDetails = (from c in vm.Details
                                        select new EMasterTopicSentenceBoosterOptionDetails()
                                        {
                                            IsCorrectAnswer = c.IsCorrectAnswer,
                                            MasterTopicSentenceBoosterMasterId = c.MasterTopicSentenceBoosterMasterId,
                                            Options = c.Options,
                                        }).ToList();

                _masterTopicSentenceBoosterServices.AddDetails(newOptionDetails);

            }

            return new ServiceResult<MasterTopicSentenceBoosterVM>()
            {
                Data = vm,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Delete(int Id) {

            var data = _masterTopicSentenceBoosterServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _masterTopicSentenceBoosterServices.Remove(data);
            return new ServiceResult<int>()
            {
                Data =  result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        public ServiceResult<List<MasterTopicSentenceBoosterOptionDetailVm>> GetOptionDetails(int MasterId) {

            var result = (from c in _masterTopicSentenceBoosterServices.DetailsList().Data.Where(x => x.MasterTopicSentenceBoosterMasterId == MasterId)
                          select new MasterTopicSentenceBoosterOptionDetailVm()
                          {
                              Options = c.Options,
                              IsCorrectAnswer = c.IsCorrectAnswer,
                              MasterTopicSentenceBoosterMasterId = c.MasterTopicSentenceBoosterMasterId
                          }).ToList();

            return new ServiceResult<List<MasterTopicSentenceBoosterOptionDetailVm>>()
            {
                Data =result,
                Status = ResultStatus.Ok
            };
        }
    }
}
