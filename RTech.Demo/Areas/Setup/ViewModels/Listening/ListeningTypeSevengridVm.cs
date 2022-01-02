using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Listening
{
    public class ListeningTypeSevengridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AudioUrl { get; set; }
        public int BeginWithin { get; set; }
        public string TranscriptionText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Answers { get; set; }
        public string UsedInQuestionSets { get; set; }
    }
}