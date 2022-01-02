using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Practice
{
    public class SynonymBoosterVm
    {

        public SynonymBoosterMasterVm SynonymBoosterMaster { get; set; }
        public List<SynonymBoosterOptionDetailsVm> Options { get; set; }
    }

    public class SynonymBoosterMasterVm
    {

        public int Id { get; set; }
        public string Word { get; set; }
        public string WordType { get; set; }
        public string Question { get; set; }
    }

    public class SynonymBoosterOptionDetailsVm
    {

        public int Id { get; set; }
        public string Options { get; set; }
        public bool IsAnswer { get; set; }
        public int SynonymBoosterMasterId { get; set; }
    }
}