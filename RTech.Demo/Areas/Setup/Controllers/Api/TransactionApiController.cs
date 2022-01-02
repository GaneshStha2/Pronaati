using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class TransactionApiController : ApiController
    {
        STransactionHistory _transactionHistoryServices = null;
        public TransactionApiController()
        {
            _transactionHistoryServices = new STransactionHistory();
        }

        [HttpPost]
        public KendoGridResult<List<TransactionHistoryVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            var transactionHitory = _transactionHistoryServices.getTransactionHistory().Data;
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IEnumerable<TransactionHistoryVm> paginatedQuery;

            switch (searchField)
            {
               
                case "StudentName":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = transactionHitory.Where(x => x.StudentName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = transactionHitory.Where(x => x.StudentName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "TestName":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = transactionHitory.Where(x => x.TestName.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = transactionHitory.Where(x => x.TestName == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                 case "TransactionType":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = transactionHitory.Where(x => x.TransactionType.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = transactionHitory.Where(x => x.TransactionType == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
               
                default:

                    paginatedQuery = transactionHitory.OrderByDescending(x => x.Id).ThenBy(x => x.StudentName).Skip(arg.Skip).Take(arg.Take);
                    break;
            }
            return new KendoGridResult<List<TransactionHistoryVm>>()
            {
                Data = paginatedQuery.ToList(),
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = paginatedQuery.Count()
            };

        }
    }
}
