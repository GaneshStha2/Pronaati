using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Reading
{
    public class ReadingTypeTwogridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReadingText { get; set; }
        public string Question { get; set; }
        public string Response1 { get; set; }
        public string Response2 { get; set; }
        public string Response3 { get; set; }
        public string Response4 { get; set; }
        public string Response5 { get; set; }
        public string Response6 { get; set; }
        public string Response7 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UsedInQuestionSets { get; set; }
    }

    public class ReadingTypeTwoVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ReadingText { get; set; }
        public string Question { get; set; }
        public string Response1 { get; set; }
        public string Response2 { get; set; }
        public string Response3 { get; set; }
        public string Response4 { get; set; }
        public string Response5 { get; set; }
        public string Response6 { get; set; }
        public string Response7 { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public bool Response1IsCorrect { get; set; }
        public bool Response2IsCorrect { get; set; }
        public bool Response3IsCorrect { get; set; }
        public bool Response4IsCorrect { get; set; }
        public bool Response5IsCorrect { get; set; }
        public bool Response6IsCorrect { get; set; }
        public bool Response7IsCorrect { get; set; }
    }
}