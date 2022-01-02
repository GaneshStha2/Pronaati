using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class BoosterCollocationViewModel
    {
        public int Id { get; set; }
        
        public string Question { get; set; }
        public string QuestionText { get; set; }
        public string OptionStatement { get; set; }
        public string CorrectAnswer { get; set; }
        public List<BoosterCollocationOptionsViewModel> OptionsList { get; set; }
    }

    public class BoosterCollocationOptionsViewModel
    {
        public int Id { get; set; }
        public int EBoosterCollocationMasterId { get; set; }
        public string Options { get; set; }
        public bool IsAnswer { get; set; }
    }

}