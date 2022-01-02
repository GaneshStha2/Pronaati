using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.TutorialSetup
{
    public class TutorialSetupVm
    {

        public TutorialMasterVm TutorialMasterVm { get; set; }

        public List<TutorialDetailVm> TutorialDetails { get; set; }
    }
}