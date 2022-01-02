using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Practice
{
    public class SynonymBoosterKendoGridVm
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string WordType { get; set; }

        public string Question { get; set; }

        public string Options { get; set; }
        public string CorrectAnswer { get; set; }

    }
}