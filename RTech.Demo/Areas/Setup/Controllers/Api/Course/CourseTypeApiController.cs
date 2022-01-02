using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Services.Course;
using RTech.Demo.Areas.Setup.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.Course
{
    public class CourseTypeApiController : ApiController
    {
        SLanguageType _courseTypeServices = null;
        public CourseTypeApiController()
        {
            _courseTypeServices = new SLanguageType();
        }

        [HttpPost]
        public KendoGridResult<List<LanguageTypeSetupVm>> GetKendoGrid(KendoPageListArguments vm)
        {
            SLanguageType service = new SLanguageType();
            IQueryable<ELanguageType> courseTypeQuery;
            courseTypeQuery = _courseTypeServices.List().Data;
            int totalRowNum = courseTypeQuery.Count();
            string searchField = vm.Filter.Filters.Count() <= 0 ? "" : vm.Filter.Filters[0].Field;
            string searchOp = vm.Filter.Filters.Count() <= 0 ? "" : vm.Filter.Filters[0].Operator;
            string searchValue = vm.Filter.Filters.Count() <= 0 ? "" : vm.Filter.Filters[0].Value;
            IQueryable<ELanguageType> paginatedQuery;
            switch (searchField)
            {
                case "Code":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = courseTypeQuery.Where(x => x.Code.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    else
                    {
                        paginatedQuery = courseTypeQuery.Where(x => x.Code == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    break;
                case "Name":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = courseTypeQuery.Where(x => x.Name.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    else
                    {
                        paginatedQuery = courseTypeQuery.Where(x => x.Name == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(vm.Skip).Take(vm.Take);
                    }
                    break;
                default:
                    paginatedQuery = courseTypeQuery.OrderByDescending(x => x.Id).ThenBy(x => x.Name).Skip(vm.Skip).Take(vm.Take);
                    break;
            }

            var courseTypeList = (from c in paginatedQuery
                                      select new LanguageTypeSetupVm()
                                      {
                                          Id = c.Id,
                                          Code = c.Code,
                                          Name = c.Name,
                                      }).ToList();
            return new KendoGridResult<List<LanguageTypeSetupVm>>()
            {
                Data = courseTypeList,
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };
        }
        public ServiceResult<ELanguageType> Get(int id)
        {
            ELanguageType result = _courseTypeServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            return new ServiceResult<ELanguageType>()
            {
                Data = result,
                Message = "Added Successfully",
                Status = ResultStatus.Ok
            };
        }

        public ServiceResult<ELanguageType> Post(ELanguageType model)
        {
            var result = _courseTypeServices.Add(model);
            return new ServiceResult<ELanguageType>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        public ServiceResult<ELanguageType> Put(ELanguageType model)
        {
            var result = _courseTypeServices.Update(model);
            return new ServiceResult<ELanguageType>()
            {
                Message = result.Message,
                Status = result.Status
            };
        }
        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var coursetype = _courseTypeServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _courseTypeServices.Remove(coursetype);
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id = 0)
        {
            var data = new ELanguageType();

            if (Id > 0)
            {
                data = _courseTypeServices.List().Data.Where(x => x.Id != Id && x.Code == Code).FirstOrDefault();
            }
            else
            {

                data = _courseTypeServices.List().Data.Where(x => x.Code == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }

    }
}
