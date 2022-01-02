using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services.Course;
using Riddhasoft.Setup.Services.QuestionSet;
using RTech.Demo.Areas.Setup.ViewModels.Course;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api.Course
{
    public class MockTestQuestionApiController : ApiController
    {

        SMockTestQuestion _mockTestQuestionService = null;
        SLanguageType _languageTypeServices = null;
        SQuestionSetDetail _quesitonSetDetailServices = null;

        public MockTestQuestionApiController()
        {
            _mockTestQuestionService = new SMockTestQuestion();
            _languageTypeServices = new SLanguageType();
            _quesitonSetDetailServices = new SQuestionSetDetail();
        }



        [HttpPost]
        public KendoGridResult<List<MockTestQuestionGridVm>> GetKendoGrid(KendoPageListArguments arg)
        {
            IQueryable<EMockTestQuestionMaster> packageQuery;
            packageQuery = _mockTestQuestionService.List().Data;
            int totalRowNum = packageQuery.Count();
            string searchField = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Field;
            string searchOp = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Operator;
            string searchValue = arg.Filter.Filters.Count() <= 0 ? "" : arg.Filter.Filters[0].Value;

            IQueryable<EMockTestQuestionMaster> paginatedQuery;

            switch (searchField)
            {
                case "Code":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Code.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Code == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                case "Title":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.Title.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.Title == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;

                case "Language":
                    if (searchOp == "startswith")
                    {
                        paginatedQuery = packageQuery.Where(x => x.LanguageType.Name.StartsWith(searchValue.Trim())).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    else
                    {
                        paginatedQuery = packageQuery.Where(x => x.LanguageType.Name == searchValue.Trim()).OrderByDescending(x => x.Id).ThenBy(x => x.Id).Skip(arg.Skip).Take(arg.Take);
                    }
                    break;
                default:

                    paginatedQuery = packageQuery.OrderByDescending(x => x.Id).ThenBy(x => x.Title).Skip(arg.Skip).Take(arg.Take);
                    break;
            }

            var vm = (from c in paginatedQuery.ToList()
                      select new MockTestQuestionGridVm()
                      {
                          Id = c.Id,
                          Code = c.Code,
                          Language = c.LanguageType.Name,
                          Title = c.Title,
                          CreatedDate = c.CreatedDate.ToFormatedString(),
                      }).ToList();
            return new KendoGridResult<List<MockTestQuestionGridVm>>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok,
                TotalCount = totalRowNum
            };

        }

        [HttpPost]
        public ServiceResult<MockTestQuestionSetupVm> Post(MockTestQuestionSetupVm vm)
        {


            EMockTestQuestionMaster mockTestQuestionMaster = new EMockTestQuestionMaster()
            {
                Code = vm.Master.Code,
                CreatedDate = DateTime.Now,
                Description = vm.Master.Description,
                LanguageTypeId = vm.Master.LanguageTypeId,
                Title = vm.Master.Title,
                DialogueId = vm.Master.DialogueId
            };

            var result = _mockTestQuestionService.Add(mockTestQuestionMaster);
            if (result.Status == ResultStatus.Ok && vm.Details != null)
            {

                foreach (var item in vm.Details)
                {
                    item.MockTestQuestionMasterId = result.Data.Id;
                }

                List<EMockTestQuestionDetail> mockTestQuestionDetails = (from c in vm.Details
                                                                         select new EMockTestQuestionDetail()
                                                                         {
                                                                             Id = c.Id,
                                                                             AnswerAudioUrl = c.AnswerAudioUrl,
                                                                             QuestionAudioUrl = c.QuestionAudioUrl,
                                                                             MockTestQuestionMasterId = c.MockTestQuestionMasterId,
                                                                             Description = c.Description,
                                                                             Order=c.Order,
                                                                             SegmentId = c.SegmentId,
                                                                         }).ToList();
                var questionDetailsResult = _mockTestQuestionService.AddMockTestQuestionDetail(mockTestQuestionDetails);

                //Adding to question set details


                //List<EQuestionSetDetail> questionSetDetails = (from c in vm.Details
                //                                               select new EQuestionSetDetail()
                //                                               {
                //                                                   QuestionSetMasterId = vm.Master.DialogueId,
                //                                                   QuestionId = result.Data.Id,
                //                                                   LanguageId = vm.Master.LanguageTypeId,
                //                                                   SegmentId   = c.SegmentId,
                //                                               }).ToList();
                //var questionSetDetailsResult = _quesitonSetDetailServices.AddDetails(questionSetDetails);

            }
            return new ServiceResult<MockTestQuestionSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpPut]
        public ServiceResult<MockTestQuestionSetupVm> Put(MockTestQuestionSetupVm vm)
        {


            var MasterData = _mockTestQuestionService.List().Data.Where(x => x.Id == vm.Master.Id).FirstOrDefault();
            MasterData.Id = vm.Master.Id;
            MasterData.Code = vm.Master.Code;
            MasterData.Description = vm.Master.Description;
            MasterData.LanguageTypeId = vm.Master.LanguageTypeId;
            MasterData.CreatedDate = DateTime.Now;
            MasterData.Title = vm.Master.Title;
            MasterData.DialogueId = vm.Master.DialogueId;
            var result = _mockTestQuestionService.Update(MasterData);
            if (result.Status == ResultStatus.Ok)
            {
                var existingDetails = _mockTestQuestionService.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMasterId == vm.Master.Id).ToList();
                _mockTestQuestionService.RemoveMockTestQuestionDetail(existingDetails);

                if (vm.Details != null)
                {
                    foreach (var item in vm.Details)
                    {
                        item.MockTestQuestionMasterId = result.Data.Id;
                    }
                    var DetailsVm = (from c in vm.Details
                                     select new EMockTestQuestionDetail()
                                     {
                                         Id = c.Id,
                                         AnswerAudioUrl = c.AnswerAudioUrl,
                                         QuestionAudioUrl = c.QuestionAudioUrl,
                                         MockTestQuestionMasterId = c.MockTestQuestionMasterId,
                                         Description = c.Description,
                                         Order=c.Order,
                                         SegmentId = c.SegmentId
                                     }).ToList();

                    _mockTestQuestionService.AddMockTestQuestionDetail(DetailsVm);

                    // For QuestionSet Details
                    var existingQuestionSetDetails = _quesitonSetDetailServices.List().Data.Where(x => x.QuestionSetMasterId == vm.Master.PreviousDialogueId).ToList();
                    _quesitonSetDetailServices.RemoveRange(existingQuestionSetDetails);

                }

            }

            return new ServiceResult<MockTestQuestionSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }

        [HttpGet]
        public ServiceResult<MockTestQuestionSetupVm> GetMockTestQuestion(int masterId)
        {
            var vm = new MockTestQuestionSetupVm();
            SSegment _segmentServices = new SSegment();

            vm.Master = (from c in _mockTestQuestionService.List().Data.Where(x => x.Id == masterId).ToList()
                         select new MockTestQuestionMasterVm()
                         {
                             Code = c.Code,
                             CreatedDate = c.CreatedDate,
                             Description = c.Description,
                             Id = c.Id,
                             LanguageTypeId = c.LanguageTypeId,
                             Title = c.Title,
                             DialogueId = c.DialogueId,
                             PreviousDialogueId = c.DialogueId
                         }).FirstOrDefault();

            if(vm.Master != null)
            {
                vm.Details = (from c in _mockTestQuestionService.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMasterId == masterId).ToList()
                              select new MockTestQuestionDetailVm()
                              {
                                  AnswerAudioUrl = c.AnswerAudioUrl,
                                  QuestionAudioUrl = c.QuestionAudioUrl,
                                  Id = c.Id,
                                  MockTestQuestionMasterId = c.MockTestQuestionMasterId,
                                  Description = c.Description,
                                  Order = c.Order,
                                  SegmentId = c.SegmentId,
                                  SegmentName = GetSegmentName(c.SegmentId)
                              }).ToList();

            }

            return new ServiceResult<MockTestQuestionSetupVm>()
            {
                Data = vm,
                Message = "",
                Status = ResultStatus.Ok
            };
        }

        private string GetSegmentName(int segmentId)
        {
            SSegment _segmentServices = new SSegment();
            string name = _segmentServices.List().Data.Where(x => x.Id == segmentId).Select(x => x.Name).FirstOrDefault();
            return name;
        }

        [HttpGet]
        public bool IsUniqueCode(string Code, int Id)
        {
            var data = new EMockTestQuestionMaster();

            if (Id > 0)
            {
                data = _mockTestQuestionService.List().Data.Where(x => x.Id != Id && x.Code == Code).FirstOrDefault();
            }
            else
            {

                data = _mockTestQuestionService.List().Data.Where(x => x.Code == Code).FirstOrDefault();

            }
            var result = data == null ? true : false;
            return result;

        }

        [HttpDelete]
        public ServiceResult<int> Delete(int id)
        {
            var MasterData = _mockTestQuestionService.List().Data.Where(x => x.Id == id).FirstOrDefault();
            var result = _mockTestQuestionService.Remove(MasterData);
            var details = _mockTestQuestionService.ListMockTestQuestionDetail().Data.Where(x => x.MockTestQuestionMasterId == id).ToList();
            _mockTestQuestionService.RemoveMockTestQuestionDetail(details);

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
            var data = _languageTypeServices.List().Data;
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

        public ServiceResult<List<GlobalDropdownVm>> GetDialogues()
        {
            SQuestionSetMaster _questionSetServices = new SQuestionSetMaster();

            var dialogues = (from c in _questionSetServices.List().Data.ToList()
                             select new GlobalDropdownVm()
                             {
                                 Id = c.Id,
                                 Name = c.QuestionSetName
                             }).ToList();
            return new ServiceResult<List<GlobalDropdownVm>>()
            {
                Data = dialogues,
                Message = "",
                Status = ResultStatus.Ok
            };
        }
    }
}
