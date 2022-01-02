using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class OnlineTrainingSetupVm
    {
        public OnlineTrainingMasterVm OnlineTrainingMasterVm { get; set; }
        public List<OnlineTrainingDetailsVm> OnlineTrainingDetailsVm { get; set; }
    }
}