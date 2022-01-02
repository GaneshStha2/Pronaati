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
    public class BoosterCollocationApiController : ApiController
    {
        SBoosterCollocation _boosterCollocationServices = null;
        public BoosterCollocationApiController()
        {
            _boosterCollocationServices = new SBoosterCollocation();
        }

        [HttpPost]
        public KendoGridResult<List<BoosterCollocationKendoGridVm>> GetKendoGrid (KendoPageListArguments arg)
        {
            var list = _boosterCollocationServices.List().Data;
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EBoosterCollocation> paginatedQuery;

            switch(searchField)
            {
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
                case "QuestionText":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.QuestionText.StartsWith(searchValue.Trim()));
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.QuestionText == searchValue.Trim());
                    }
                    break;
                case "OptionStatement":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = list.Where(x => x.OptionStatement.StartsWith(searchValue.Trim()));
                    }
                    else
                    {
                        paginatedQuery = list.Where(x => x.OptionStatement == searchValue.Trim());
                    }
                    break;
                default:
                    paginatedQuery = list;
                    break;
            }

            var result = (from c in paginatedQuery.ToList()
                          select new BoosterCollocationKendoGridVm()
                          {
                              Id = c.Id,
                              Question = c.Question,
                              QuestionText = c.QuestionText,
                              OptionStatement = c.OptionStatement,
                              Options = getOptions(c.Id),
                              CorrectAnswer = _boosterCollocationServices.OptionsList().Data.Where(x => x.IsAnswer == true && x.EBoosterCollocationMasterId == c.Id).Select(x => x.Options).FirstOrDefault()
                          }).ToList();
            return new KendoGridResult<List<BoosterCollocationKendoGridVm>>()
            {
                Data = result.OrderBy(x=>x.Question).Skip(arg.Skip).Take(arg.Take).ToList(),
                TotalCount = totalRowNum
            };
        }


        public string getOptions(int Id)
        {

            string Options = "";
            var data = _boosterCollocationServices.OptionsList().Data.Where(x => x.EBoosterCollocationMasterId == Id).Select(x => x.Options).ToList();
            foreach (var item in data)
            {

                Options = Options + " " + item + ",";
            }
            return Options;
        }

        public ServiceResult<BoosterCollocationVm> Post(BoosterCollocationVm model)
        {
            EBoosterCollocation boosterCollocation = new EBoosterCollocation()
            {
                Question = model.BoosterCollocationMaster.Question,
                QuestionText = model.BoosterCollocationMaster.QuestionText,
                OptionStatement = model.BoosterCollocationMaster.OptionStatement
            };
            var master_result = _boosterCollocationServices.Add(boosterCollocation);
            if (master_result.Status == ResultStatus.Ok)
            {
                foreach (var item in model.Options)
                {
                    item.EBoosterCollocationMasterId = master_result.Data.Id;
                }

                var BoosterCollocationOptions = (from c in model.Options
                                                 select new EBoosterCollocationDetails()
                                                 {
                                                     EBoosterCollocationMasterId = c.EBoosterCollocationMasterId,
                                                     IsAnswer = c.IsAnswer,
                                                     Options = c.Options

                                                 }).ToList();
                _boosterCollocationServices.AddDetails(BoosterCollocationOptions);
            }
            return new ServiceResult<BoosterCollocationVm>()
            {
                Data = model,
                Message = "Added Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<BoosterCollocationVm> Put(BoosterCollocationVm model)
        {
            var master_data = _boosterCollocationServices.List().Data.Where(x => x.Id == model.BoosterCollocationMaster.Id).FirstOrDefault();
            master_data.Question = model.BoosterCollocationMaster.Question;
            master_data.QuestionText = model.BoosterCollocationMaster.QuestionText;
            master_data.OptionStatement = model.BoosterCollocationMaster.OptionStatement;

            var result = _boosterCollocationServices.Update(master_data);

            if (result.Status == ResultStatus.Ok)
            {
                //Remove previous options
                var previousOptionList = _boosterCollocationServices.OptionsList().Data.Where(x => x.EBoosterCollocationMasterId == result.Data.Id).ToList();
                _boosterCollocationServices.RemoveDetails(previousOptionList);
                foreach (var item in model.Options)
                {
                    item.EBoosterCollocationMasterId = result.Data.Id;
                }
                var newOptions = (from c in model.Options
                                  select new EBoosterCollocationDetails()
                                  {
                                      EBoosterCollocationMasterId = c.EBoosterCollocationMasterId,
                                      Options = c.Options,
                                      IsAnswer = c.IsAnswer
                                  }).ToList();
                _boosterCollocationServices.AddDetails(newOptions);
            }
            return new ServiceResult<BoosterCollocationVm>()
            {
                Data = model,
                Message = "Updated Successfully !",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<int> Delete(int id)
        {
            var data = _boosterCollocationServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _boosterCollocationServices.Remove(data);
            return new ServiceResult<int>()
            {
                Data = 1,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet]
        public ServiceResult<List<BoosterCollocationOptionDetailVm>> getOptionDetails(int MasterId)
        {
            var result = (from c in _boosterCollocationServices.OptionsList().Data.Where(x => x.EBoosterCollocationMasterId == MasterId)
                          select new BoosterCollocationOptionDetailVm()
                          {
                              Id = c.Id,
                              IsAnswer = c.IsAnswer,
                              Options = c.Options,
                              EBoosterCollocationMasterId = c.EBoosterCollocationMasterId
                          }).ToList();
            return new ServiceResult<List<BoosterCollocationOptionDetailVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok
            };
        }

    }
}
