using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Listening
{
    public class ListeningTypeSixgridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public int BeginWithin { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public string UsedInQuestionSets { get; set; }
    }

    public class ListeningTypeSixVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public int BeginWithin { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public bool Option1IsCorrect { get; set; }
        public bool Option2IsCorrect { get; set; }
        public bool Option3IsCorrect { get; set; }
        public bool Option4IsCorrect { get; set; }
        public bool Option5IsCorrect { get; set; }
    }


}