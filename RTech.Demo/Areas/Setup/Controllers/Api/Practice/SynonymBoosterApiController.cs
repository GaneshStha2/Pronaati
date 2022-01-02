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
    public class SynonymBoosterApiController : ApiController
    {

        SSynonymBooster _synonymBoosterServices = null;
        public SynonymBoosterApiController()
        {
            _synonymBoosterServices = new SSynonymBooster();
        }


        [HttpPost]
        public KendoGridResult<List<SynonymBoosterKendoGridVm>> GetKendoGrid(KendoPageListArguments arg) {

            var list = _synonymBoosterServices.List().Data;
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<ESynonymBooster> paginatedQuery;

            switch (searchField)
            {
                case "Word":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.Word.StartsWith(searchValue.Trim()));
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.WordType == searchValue.Trim());
                    }
                    break;
                case "WordType":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.WordType.StartsWith(searchValue.Trim()));
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.WordType == searchValue.Trim());
                    }
                    break;
                case "Question":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.Question.StartsWith(searchValue.Trim()));
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.Question == searchValue.Trim());
                    }
                    break;
                default:
                    paginatedQuery = list;
                    break;
            }

            var result = (from c in paginatedQuery.ToList()
                          select new SynonymBoosterKendoGridVm()
                          {
                              Id = c.Id,
                              Word = c.Word,
                              WordType = c.WordType,
                              Question = c.Question,
                              CorrectAnswer = _synonymBoosterServices.OptionsList().Data.Where(x => x.IsAnswer == true && x.SynonymBoosterMasterId == c.Id).Select(x => x.Options).FirstOrDefault(),
                              Options = getOptions(c.Id)
                          }).ToList();

            return new KendoGridResult<List<SynonymBoosterKendoGridVm>>()
            {
                Data = result.OrderBy(x=>x.Word).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList(),
                TotalCount = totalRowNum
            };
        }
        public  string getOptions(int Id) {

            string Options = "";
            var data = _synonymBoosterServices.OptionsList().Data.Where(x => x.SynonymBoosterMasterId == Id).Select(x => x.Options).ToList();
            foreach (var item in data) {

                Options =Options + " "  +  item + ",";
            }
            return Options;
        }

        public ServiceResult<SynonymBoosterVm> Post(SynonymBoosterVm model)
        {
            ESynonymBooster synonymBooster = new ESynonymBooster()
            {
                Word = model.SynonymBoosterMaster.Word,
                WordType = model.SynonymBoosterMaster.WordType,
                Question = model.SynonymBoosterMaster.Question,
            };
            var master_result = _synonymBoosterServices.Add(synonymBooster);
            if (master_result.Status == ResultStatus.Ok)
            {

                foreach (var item in model.Options)
                {
                    item.SynonymBoosterMasterId = master_result.Data.Id;
                }

                var SynonmBoosterOptions = (from c in model.Options
                                            select new ESynonymBoosterOptionDetails()
                                            {
                                                IsAnswer = c.IsAnswer,
                                                Options = c.Options,
                                                SynonymBoosterMasterId = c.SynonymBoosterMasterId
                                            }).ToList();

                _synonymBoosterServices.AddDetails(SynonmBoosterOptions);
            }

            return new ServiceResult<SynonymBoosterVm>()
            {
                Data = model,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<SynonymBoosterVm> Put(SynonymBoosterVm model)
        {
            var master_data = _synonymBoosterServices.List().Data.Where(x => x.Id == model.SynonymBoosterMaster.Id).FirstOrDefault();
            master_data.Question = model.SynonymBoosterMaster.Question;
            master_data.Word = model.SynonymBoosterMaster.Word;
            master_data.WordType = model.SynonymBoosterMaster.WordType;

            var result = _synonymBoosterServices.Update(master_data);

            if (result.Status == ResultStatus.Ok) {
                // Remove Previous Options
                var previousOptionList = _synonymBoosterServices.OptionsList().Data.Where(x => x.SynonymBoosterMasterId == result.Data.Id).ToList();
                _synonymBoosterServices.RemoveDetails(previousOptionList);

                foreach (var item in model.Options) {

                    item.SynonymBoosterMasterId = result.Data.Id;
                }

                var newOptions = (from c in model.Options
                                  select new ESynonymBoosterOptionDetails()
                                  {
                                      Options = c.Options,
                                      IsAnswer =c.IsAnswer,
                                      SynonymBoosterMasterId = c.SynonymBoosterMasterId,
                                  }).ToList();
                _synonymBoosterServices.AddDetails(newOptions);
            }


            return new ServiceResult<SynonymBoosterVm>()
            {
                Data = model,
                Message ="Update Successfully",
                Status = ResultStatus.Ok
            };

        }

        public ServiceResult<int> Delete(int Id) {
            var data = _synonymBoosterServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _synonymBoosterServices.Remove(data);
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }
        [HttpGet]
        public ServiceResult<List<SynonymBoosterOptionDetailsVm>> getOptionDetails(int MasterId) {

            var result = (from c in _synonymBoosterServices.OptionsList().Data.Where(x => x.SynonymBoosterMasterId == MasterId)
                          select new SynonymBoosterOptionDetailsVm()
                          {
                              Id = c.Id,
                              IsAnswer = c.IsAnswer,
                              Options = c.Options,
                              SynonymBoosterMasterId = c.SynonymBoosterMasterId
                          }).ToList();

            return new ServiceResult<List<SynonymBoosterOptionDetailsVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };

        }
    }
}
