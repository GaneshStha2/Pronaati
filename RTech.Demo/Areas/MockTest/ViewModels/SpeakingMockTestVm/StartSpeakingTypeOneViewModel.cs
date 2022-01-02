using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.MockTest.ViewModels.SpeakingMockTestVm
{
    public class StartSpeakingTypeOneViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }

        public string Title { get; set; }

        public string Question { get; set; }
        

        public int BeginWithinTImeSec { get; set; }

        public int SpeakingTimeSec { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public bool IsTaken { get; set; }
    }
}