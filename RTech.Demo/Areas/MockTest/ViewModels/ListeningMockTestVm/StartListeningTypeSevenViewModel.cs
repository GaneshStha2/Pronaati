using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.ListeningMockTestVm
{
    public class StartListeningTypeSevenViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string  Title { get; set; }
        public string  AudioUrl { get; set; }
        public string TranscriptionText { get; set; }
        public  int BeginWithin { get; set; }
        public List<string> Texts { get; set; }
    }
 
}