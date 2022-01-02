using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Listening
{
    public class ListeningTypeFourgridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string Paragraph3 { get; set; }
        public string Paragraph4 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int BeginWithin { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public string UsedInQuestionSets { get; set; }
    }

    public class ListeningTypeFourVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string Paragraph3 { get; set; }
        public string Paragraph4 { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int BeginWithin { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public bool Paragraph1IsCorrect { get; set; }
        public bool Paragraph2IsCorrect { get; set; }
        public bool Paragraph3IsCorrect { get; set; }
        public bool Paragraph4IsCorrect { get; set; }
        

    }
}