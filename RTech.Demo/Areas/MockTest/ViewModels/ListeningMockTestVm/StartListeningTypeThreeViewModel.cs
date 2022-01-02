using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.Listening
{
    public class StartListeningTypeThreeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public List<string> TextList { get; set; }
        public string AudioUrl { get; set; }
        public int BeginWithinTime { get; set; }
        public int TotalTime{ get; set; }
        public List<string> Answers { get; set; }
    }
}