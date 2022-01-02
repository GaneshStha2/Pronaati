using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Reading
{
    public class ReadingTypeThreegridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionSource1 { get; set; }
        public string QuestionSource2 { get; set; }
        public string QuestionSource3 { get; set; }
        public string QuestionSource4 { get; set; }
        public string QuestionSource5 { get; set; }
        public string QuestionSource6 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UsedInQuestionSets { get; set; }
    }
}