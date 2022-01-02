using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.ListeningMockTestVm
{
    public class StartListeningTypeEightViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AudioUrl { get; set; }
        public string Title { get; set; }
        public string Sentence { get; set; }
        public string AnswerText { get; set; }
        public string TotalWord { get; set; }
        public int BeginWithin { get; set; }
    }
}