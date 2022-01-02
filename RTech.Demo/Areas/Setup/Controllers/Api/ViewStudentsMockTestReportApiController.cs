using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Services;
using Riddhasoft.Setup.Services.ViewModel;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class ViewStudentsMockTestReportApiController : ApiController
    {
        SMockTestReport _mockTestReportServices = null;
        public ViewStudentsMockTestReportApiController()
        {
            _mockTestReportServices = new SMockTestReport();
        }
        [HttpPost]
        public KendoGridResult<List<MockTestReportKendoVm>> GetMockTestReportsList(KendoPageListArguments arg)
        {
            //var list = _mockTestReportServices.List().Data;
            //int totalRowNum = list.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            //IQueryable<EMockTestReport> paginatedQuery;
            var list = (from c in _mockTestReportServices.List().Data.ToList()
                          select new MockTestReportKendoVm()
                          {
                              Id = c.Id,
                              StudentId = c.StudentId,
                              StudentName = _mockTestReportServices.getStudentName(c.StudentId),
                              StudentEmailAddress = c.EmailAddress,
                              QuestionPackageId = c.QuestionPackageId,
                              QuestionSetId = c.QuestionSetId,
                              QuestionSetName = _mockTestReportServices.getQuestionSetName(c.QuestionSetId),
                              TestTakenDate = c.TestDate.ToString("yyyy-MM-dd"),
                              SpeakingMarks = c.SpeakingMarks,
                              WritingMarks = c.WritingMarks,
                              ReadingMarks = c.ReadingMarks,
                              ListeningMarks = c.ListeningMarks,
                              TotalMarks = c.OverallScore,
                              IsReportReady = c.IsReportReadyToView,
                          }).ToList();
            switch (searchField)
            {
                case "StudentName":
                    if (searchOp == "startswith")
                    {
                        
                        list = list.Where(x => x.StudentName.Trim().ToLower().StartsWith(searchValue.Trim().ToLower())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        list = list.Where(x => x.StudentName.Trim().ToLower() == searchValue.Trim().ToLower()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;
                case "QuestionSetName":
                    if (searchOp == "startswith")
                    {
                        list = list.Where(x => x.QuestionSetName.Trim().ToLower().StartsWith(searchValue.Trim().ToLower())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        list = list.Where(x => x.QuestionSetName.Trim().ToLower() == searchValue.Trim().ToLower()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;
                case "StudentEmailAddress":
                    if (searchOp == "startswith")
                    {
                        list = list.Where(x => x.StudentEmailAddress.Trim().ToLower().StartsWith(searchValue.Trim().ToLower())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        list = list.Where(x => x.StudentEmailAddress.Trim().ToLower() == searchValue.Trim().ToLower()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;
                case "TestTakenDate":
                    if (searchOp == "startswith")
                    {
                        list = list.Where(x => x.TestTakenDate.Trim().ToLower().StartsWith(searchValue.Trim().ToLower())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        list = list.Where(x => x.TestTakenDate.Trim().ToLower() == searchValue.Trim().ToLower()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;
                case "IsReportReady":
                    if (searchOp == "startswith")
                    {
                        list = list.Where(x => x.IsReportReady.ToString().Trim().ToLower().StartsWith(searchValue.Trim().ToLower())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    else
                    {
                        list = list.Where(x => x.IsReportReady.ToString().Trim().ToLower() == searchValue.Trim().ToLower()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take).ToList();
                    }
                    break;
                default:
                    list = list.OrderByDescending(x => x.Id).ThenBy(x => x.TestTakenDate).Skip(arg.Skip).Take(arg.Take).ToList();
                    break;
            }



            
            return new KendoGridResult<List<MockTestReportKendoVm>>()
            {
                Data = list,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = list.Count()
            };
        }



    }
}
