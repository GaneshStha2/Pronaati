using Riddhasoft.Services.Common;
using Riddhasoft.Setup.Entity;
using Riddhasoft.Setup.Services;
using RTech.Demo.Areas.Setup.ViewModels.TutorialSetup;
using RTech.Demo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTech.Demo.Areas.Setup.Controllers.Api
{
    public class TutorialSetupApiController : ApiController
    {
        STutorial _tutorialSetupServices = null;
        STutorialDetails _tutorialDetailsServices = null;
        public TutorialSetupApiController()
        {
            _tutorialSetupServices = new STutorial();
            _tutorialDetailsServices = new STutorialDetails();
        }


        [HttpPost]
        public KendoGridResult<List<TutorialMasterVm>> GetkendoList(KendoPageListArguments arg)
        {

            var data = _tutorialSetupServices.List().Data;

            var result = (from c in data.ToList()
                          select new TutorialMasterVm()
                          {
                              Id = c.Id,
                              Title = c.Title,
                              Description = c.Description,
                              CreatedDateTime = c.CreatedDateTime,
                              CreatedBy = c.CreatedBy,
                              LastModifiedBy = c.LastModifiedBy,
                              LastModifiedDateTime = c.LastModifiedDateTime
                          }).ToList();
            return new KendoGridResult<List<TutorialMasterVm>>()
            {
                Data = result,
                Status = ResultStatus.Ok,
                TotalCount = result.Count
            };
        }


        public ServiceResult<TutorialSetupVm> Post(TutorialSetupVm vm)
        {
            ETutorials tutorials = new ETutorials()
            {
                Title = vm.TutorialMasterVm.Title,
                Description = vm.TutorialMasterVm.Description,
                CreatedDateTime = DateTime.Now,
                CreatedBy = RiddhaSession.UserId
            };
            var result = _tutorialSetupServices.Add(tutorials);
            if (result.Status == ResultStatus.Ok)
            {
                foreach (var item in vm.TutorialDetails)
                {
                    item.TutorialID = result.Data.Id;
                }
                var eTutorialDetails = (from c in vm.TutorialDetails
                                        select new ETutorialDetails()
                                        {
                                            Id = c.Id,
                                            FileURL = c.FileURL,
                                            FileName = c.FileName,
                                            TutorialID = c.TutorialID
                                        }).ToList();
                _tutorialDetailsServices.AddTutorialDetails(eTutorialDetails);
            }
            return new ServiceResult<TutorialSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status

            };
        }


        public ServiceResult<TutorialSetupVm> Put(TutorialSetupVm vm)
        {

            var Masterdata = _tutorialSetupServices.List().Data.Where(x => x.Id == vm.TutorialMasterVm.Id).FirstOrDefault();
            Masterdata.Title = vm.TutorialMasterVm.Title;
            Masterdata.Description = vm.TutorialMasterVm.Description;
            Masterdata.LastModifiedDateTime = DateTime.Now;
            Masterdata.LastModifiedBy = RiddhaSession.UserId;


            var result = _tutorialSetupServices.Update(Masterdata);
            if (result.Status == ResultStatus.Ok)
            {

                var TutorialDetailList = _tutorialDetailsServices.List().Data.Where(x => x.TutorialID == result.Data.Id).ToList();
                _tutorialDetailsServices.RemoveTutorialDetails(TutorialDetailList);

                if (vm.TutorialDetails != null)
                {


                    foreach (var item in vm.TutorialDetails)
                    {
                        item.TutorialID = result.Data.Id;
                    }

                    var eTutorialDetails = (from c in vm.TutorialDetails
                                            select new ETutorialDetails()
                                            {
                                                Id = c.Id,
                                                FileURL = c.FileURL,
                                                FileName = c.FileName,
                                                TutorialID = c.TutorialID
                                            }).ToList();

                    _tutorialDetailsServices.AddTutorialDetails(eTutorialDetails);
                }
            }
            return new ServiceResult<TutorialSetupVm>()
            {
                Data = vm,
                Message = result.Message,
                Status = result.Status
            };
        }


        public ServiceResult<int> Delete(int Id)
        {
            var MasterData = _tutorialSetupServices.List().Data.Where(x => x.Id == Id).FirstOrDefault();
            var result = _tutorialSetupServices.Remove(MasterData);
            var Details = _tutorialDetailsServices.List().Data.Where(x => x.TutorialID == Id).ToList();
            _tutorialDetailsServices.RemoveTutorialDetails(Details);
            return new ServiceResult<int>()
            {
                Data = result.Data,
                Message = result.Message,
                Status = result.Status
            };
        }

        public ServiceResult<TutorialSetupVm> GetTutorialDetailsByMasterId(int Id)
        {
            TutorialSetupVm Vm = new TutorialSetupVm();
            Vm.TutorialMasterVm = (from c in _tutorialSetupServices.List().Data.Where(x => x.Id == Id)
                                   select new TutorialMasterVm()
                                   {
                                       Id = c.Id,
                                       Title = c.Title,
                                       Description = c.Description,
                                       CreatedDateTime = c.CreatedDateTime,
                                       CreatedBy = c.CreatedBy,
                                       LastModifiedDateTime = c.LastModifiedDateTime,
                                       LastModifiedBy = c.LastModifiedBy
                                   }).FirstOrDefault();

            Vm.TutorialDetails = (from c in _tutorialDetailsServices.List().Data.Where(x => x.TutorialID == Id)
                                  select new TutorialDetailVm()
                                  {
                                      Id = c.Id,
                                      FileName = c.FileName,
                                      TutorialID = c.TutorialID,
                                      FileURL = c.FileURL
                                  }).ToList();
            return new ServiceResult<TutorialSetupVm>()
            {
                Data = Vm,
                Message = "",
                Status = ResultStatus.Ok
            };
        }


    }
}
