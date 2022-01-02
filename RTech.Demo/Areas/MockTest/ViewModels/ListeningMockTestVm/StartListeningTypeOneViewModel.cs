using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.Listening
{
    public class StartListeningTypeOneViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public int BeginWithinTime { get; set; }
        public int TaskCompletionTime { get; set; }
        public string   AudioUrl { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalWordCount { get; set; }
    }
}