﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Reading
{
    public class ReadingTypeFourgridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public string Option7 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UsedInQuestionSets { get; set; }
    }
}