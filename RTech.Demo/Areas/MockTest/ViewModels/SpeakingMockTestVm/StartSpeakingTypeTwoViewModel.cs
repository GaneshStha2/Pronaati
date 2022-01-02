using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.SpeakingMockTestVm
{
    public class StartSpeakingTypeTwoViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int BeginWithinTime { get; set; }
        public int SpeakingTime { get; set; }
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string TextToRead { get; set; }
        public string AudioUrl { get; set; }

    }
}