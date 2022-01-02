using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class MasterTopicSentenceViewModel
    {
        public int Id { get; set; }

        public string QuestionTitle { get; set; }

        public string Question { get; set; }

        public string OptionStatement { get; set; }
        public string CorrectAnswer { get; set; }

        public List<MasterTopicSentenceOptionsViewModel> OptionsList { get; set; }

    }

    public class MasterTopicSentenceOptionsViewModel
    {
        public int Id { get; set; }
        public int MasterTopicSentenceBoosterMasterId { get; set; }
        public string Options { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}