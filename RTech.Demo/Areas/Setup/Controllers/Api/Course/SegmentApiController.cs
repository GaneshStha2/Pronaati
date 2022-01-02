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
    public class SegmentApiController : ApiController
    {
        SSegment _segmentServices = null;

        public SegmentApiController()
        {
            _segmentServices = new SSegment();
        }

        [HttpPost]
        public KendoGridResult<List<SegmentGridVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<ESegment> packageQuery;
            packageQuery = _segmentServices.List().Data;
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<ESegment> paginatedQuery;

            switch (searchField)
            {
                case "LanguageName":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Language.Name.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Language.Name == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;

                default:

                    paginatedQuery = packageQuery.OrderByDescending(x => x.Id).ThenBy(x => x.LanguageId).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var vm = (from c in paginatedQuery.ToList()
                      select new SegmentGridVm()
                      {
                          Id = c.Id,
                          FromLanguageId = c.FromLanguageId,
                          FromLanguageName = GetLanguageName(c.FromLanguageId),
                          LanguageId = c.LanguageId,
                          LanguageName = GetLanguageName(c.LanguageId),
                          ToLanguageId = c.ToLanguageId,
                          ToLanguageName = GetLanguageName(c.ToLanguageId),
                          Name = c.Name
                      }).ToList();
            return new KendoGridResult<List<SegmentGridVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };

        }


        [HttpPost]

        public ServiceResult<SegmentSetUpVm> Post(SegmentSetUpVm model)
        {
            if (model.Segments != null)
            {
                foreach (var item in model.Segments)
                {
                    item.LanguageId = model.LanguageId;

                }

                List<ESegment> segments = (from c in model.Segments
                                           select new ESegment()
                                           {
                                               FromLanguageId = c.FromLanguageId,
                                               LanguageId = c.LanguageId,
                                               ToLanguageId = c.ToLanguageId,
                                               Name = c.Name
                                           }).ToList();

                var result = _segmentServices.AddList(segments);

                return new ServiceResult<SegmentSetUpVm>()
                {
                    Data = model,
                    Message = result.Message,
                    Status = ResultStatus.Ok
                };

            }

            return new ServiceResult<SegmentSetUpVm>()
            {
                Data = model,
                Message = "Add Failed",
                Status = ResultStatus.processError
            };
        }

        [HttpDelete]

        public ServiceResult<int> Delete(int id)
        {
            var segment = _segmentServices.List().Data.Where(x => x.Id == id).FirstOrDefault();
            if (segment != null)
            {
                var result = _segmentServices.Remove(segment);
                return new ServiceResult<int>()
                {
                    Data = 1,
                    Message = result.Message,
                    Status = result.Status
                };
            }

            return new ServiceResult<int>()
            {
                Data = 1,
                Message = "RemoveFailed",
                Status = ResultStatus.processError
            };
        }


        [HttpPut]
        public ServiceResult<SegmentSetUpVm> Put(SegmentSetUpVm model)
        {

            foreach (var item in model.Segments)
            {
                item.LanguageId = model.LanguageId;
            }

            List<ESegment> segments = (from c in model.Segments
                                       select new ESegment()
                                       {
                                           FromLanguageId = c.FromLanguageId,
                                           LanguageId = c.LanguageId,
                                           ToLanguageId = c.ToLanguageId,
                                           Name = c.Name,
                                           Id = c.Id
                                       }).ToList();

            foreach(var item in segments)
            {
                if(item.Id == 0)
                {
                    var result = _segmentServices.Add(item);
                }
                else
                {
                    var result = _segmentServices.Update(item);
                }
              
            }

            return new ServiceResult<SegmentSetUpVm>()
            {
                Data = model,
                Message = "UpdateSuccess",
                Status = ResultStatus.Ok
            };

        }


        public ServiceResult<SegmentSetUpVm> GetSegmentsFromLanguageId(int languageId)
        {
            var segments = _segmentServices.List().Data.ToList();

            if(segments.Count >0)
            {
                SegmentSetUpVm vm = new SegmentSetUpVm();
                vm.LanguageId = languageId;
                vm.Segments = (from c in segments.Where(x=> x.LanguageId == languageId).ToList()
                               select new SegmentGridVm()
                               {
                                   FromLanguageId = c.FromLanguageId,
                                   FromLanguageName = GetLanguageName(c.FromLanguageId),
                                   LanguageId = c.LanguageId,
                                   LanguageName = GetLanguageName(c.LanguageId),
                                   ToLanguageId = c.ToLanguageId,
                                   ToLanguageName = GetLanguageName(c.ToLanguageId),
                                   Id = c.Id,
                                   Name = c.Name
                               }).ToList();

                return new ServiceResult<SegmentSetUpVm>()
                {
                    Data = vm,
                    Message = "",
                    Status = ResultStatus.Ok
                };
            }

            return new ServiceResult<SegmentSetUpVm>()
            {
                Data = null,
                Message = "Data not found",
                Status = ResultStatus.processError
            };

        }


        public ServiceResult<List<GlobalDropdownVm>> GetLanguages()
        {
            SLanguageType _languageServices = new SLanguageType();

            List<GlobalDropdownVm> languages = (from c in _languageServices.List().Data
                                                select new GlobalDropdownVm()
                                                {
                                                    Id = c.Id,
                                                    Name = c.Name,
                                                }).ToList();
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = languages,
                Message = "",
                Status = ResultStatus.Ok
            };
            
        }
        private string GetLanguageName(int id)
        {
            SLanguageType _languageTypeServices = new SLanguageType();
            string languageName = _languageTypeServices.List().Data.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
            return languageName;
        }
    }
}
