using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.QuestionPackage
{
    public class PackageQuestionSetDropDownVm
    {

        public int Id { get; set; }
        public int QuestionSetId { get; set; }
        public string QuestionSetName { get; set; }
        public string QuestionSetCode { get; set; }
        public string ValidDuration { get; set; }
    }
}