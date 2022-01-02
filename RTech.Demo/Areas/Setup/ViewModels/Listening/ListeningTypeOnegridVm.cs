using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Listening
{
    public class ListeningTypeOnegridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public int TimeBeforeAudio { get; set; }
        public int TotalTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UsedInQuestionSets { get; set; }
    }
}