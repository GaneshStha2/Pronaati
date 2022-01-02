using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class SynonymBoosterViewModel
    {
        public int Id { get; set; }
        
        public string Word { get; set; }
        public string WordType { get; set; }

        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public List<SynonymBoosterOptionsViewmodel> OptionsList { get; set; }
    }

    public class SynonymBoosterOptionsViewmodel
    {
        public int Id { get; set; }
        public int SynonymBoosterMasterId { get; set; }
        public string Option { get; set; }
        public bool IsAnswer { get; set; }
    }
}