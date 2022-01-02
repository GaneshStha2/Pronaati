using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Setup.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.QuestionSet
{
    public class QuestionSetSaveVm
    {
   
        public QuestionSetVm QuestionSetVm { get; set; }
        public List<QuestionSetDetailsViewModel> QuestionSetDetailsVm { get; set; }

    }

    public class QuestionSetVm {
        public int Id { get; set; }
        public string QuestionSetCode { get; set; }
        public string QuestionSetName { get; set; }
        public bool IsUnscored { get; set; }
    }

}