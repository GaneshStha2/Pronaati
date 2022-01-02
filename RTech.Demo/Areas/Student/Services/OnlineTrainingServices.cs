using Riddhasoft.Setup.Services.Course;
using RTech.Demo.Areas.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.Services
{
    public class OnlineTrainingServices
    {
        SOnlineTraining _onlineTRainingServices = null;
        SOnllineTrainingDetails _OnllineTrainingDetailsServices = null;
        public OnlineTrainingServices()
        {
            _onlineTRainingServices = new SOnlineTraining();
            _OnllineTrainingDetailsServices = new SOnllineTrainingDetails();
        }

        public List<OnlineTrainingMasterViewModel> GetOnlineTrainingList()
        {
            var result = (from c in _onlineTRainingServices.List().Data
                          select new OnlineTrainingMasterViewModel()
                          {
                              OnlineTrainigId = c.Id,
                              CourseCode = c.Code,
                              Description = c.Description,
                              ImageUrl = c.ImageURL,
                              Price = c.Price,
                              Title = c.PackageTitle,
                              CreatedDate = c.CreatedDate
                          }).ToList();

            return result;
        }
        public OnlineTrainingMasterViewModel GetOnlineTrainingDetails(int id)
        {

            var vm = (from c in _onlineTRainingServices.List().Data.Where(x => x.Id == id)
                      select new OnlineTrainingMasterViewModel()
                      {
                          OnlineTrainigId = c.Id,
                          Description = c.Description,
                          ImageUrl = c.ImageURL,
                          Price = c.Price,
                          Title = c.PackageTitle,
                          CourseCode = c.Code,
                      }).FirstOrDefault();


            vm.onlineTrainingFileURLList = (from c in _OnllineTrainingDetailsServices.List().Data.Where(x => x.EOnlineTrainingId == id).ToList()
                                            select new OnlineTrainingDetailsViewModel()
                                            {
                                                FileExtension = Common.CommonServices.GetExtension(c.FilerUrl),
                                                FileName = c.FileName,
                                                FileUrl = c.FilerUrl,
                                                OnlineTrainingDetailsId = c.Id
                                            }).ToList();


            return vm;

        }

    }
}