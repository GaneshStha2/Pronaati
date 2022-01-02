using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Practice
{
    public class MasterTopicSentenceBoosterVM
    {

        public MasterTopicSentenceBoosterMasterVm Master { get; set; }

        public List<MasterTopicSentenceBoosterOptionDetailVm> Details { get; set; }
    }

    public class MasterTopicSentenceBoosterMasterVm {

        public int Id { get; set; }
        public string QuestionTitle { get; set; }

        public string Question { get; set; }

        public string OptionStatement { get; set; }

    }

    public class MasterTopicSentenceBoosterOptionDetailVm {

        public int Id { get; set; }
        public int MasterTopicSentenceBoosterMasterId { get; set; }
        public string Options { get; set; }
        public bool IsCorrectAnswer { get; set; }

    }
}