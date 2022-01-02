using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Speaking
{
    public class SpeakingTypeFiveViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string NameOfImage { get; set; }
        public int BeginWithInTimeSec { get; set; }

        public int SpeakingTimeSec { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByUserId { get; set; }
        public string UsedInQuestionSets { get; set; }

    }
}