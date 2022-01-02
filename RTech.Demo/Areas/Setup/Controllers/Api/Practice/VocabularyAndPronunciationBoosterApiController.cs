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
    public class VocabularyAndPronunciationBoosterApiController : ApiController
    {
        SVocabularyAndPronunciationBooster _VocabularyAndPronunciationBoosterServices = null;
        public VocabularyAndPronunciationBoosterApiController()
        {
            _VocabularyAndPronunciationBoosterServices = new SVocabularyAndPronunciationBooster();
        }

        [HttpPost]
        public KendoGridResult<List<VocabularyAndPronunciationBoosterVm>> GetKendoGrid(KendoPageListArguments arg) {

           var list = _VocabularyAndPronunciationBoosterServices.List().Data;
            int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EVocabularyAndPronunciationBooster> paginatedQuery;

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
                default:
                    paginatedQuery = list;
                    break;
            }
            var result = (from c in paginatedQuery
                          select new VocabularyAndPronunciationBoosterVm()
                          {
                              Id = c.Id,
                              Word = c.Word,
                              WordType = c.WordType,
                              FileUrl = c.FileUrl,
                              WordMeaning =  c.WordMeaning
                          }).ToList();

            return new KendoGridResult<List<VocabularyAndPronunciationBoosterVm>>()
            {
                Data = result.OrderBy(x=>x.Word).Skip(arg.Skip).Take(arg.Take).ToList(),
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };
        }
        public ServiceResult<EVocabularyAndPronunciationBooster> Post(EVocabularyAndPronunciationBooster model) {

            var result = _VocabularyAndPronunciationBoosterServices.Add(model);
            return result;
        }

        public ServiceResult<EVocabularyAndPronunciationBooster> Put(EVocabularyAndPronunciationBooster model) {

            var result = _VocabularyAndPronunciationBoosterServices.Update(model);
            return result;
        }

        public ServiceResult<int> Delete(int Id) {

            var data = _VocabularyAndPronunciationBoosterServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _VocabularyAndPronunciationBoosterServices.Remove(data);
            return result;
        }  
    }
}
