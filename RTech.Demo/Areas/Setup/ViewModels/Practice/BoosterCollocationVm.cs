using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Practice
{
    public class BoosterCollocationVm
    {
        public BoosterCollocationMasterVm BoosterCollocationMaster { get; set; }
        public List<BoosterCollocationOptionDetailVm> Options { get; set; }
    }

    public class BoosterCollocationMasterVm
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionText { get; set; }
        public string OptionStatement { get; set; }
    }

    public class BoosterCollocationOptionDetailVm
    {
        public int Id { get; set; }
        public int EBoosterCollocationMasterId { get; set; }
        public string Options { get; set; }
        public bool IsAnswer { get; set; }
    }
}