using Riddhasoft.Setup.Entity.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Reading
{
    public class ReadingTypeFivegridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UsedInQuestionSets { get; set; }
    }
    public class ReadingTypeFiveOptionVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }


        public List<EReadingTypeFiveDropdown> Dropdowns { get; set; }
    }
}