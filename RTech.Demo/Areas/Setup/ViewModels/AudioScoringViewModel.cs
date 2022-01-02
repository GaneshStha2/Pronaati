using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels
{
    public class AudioScoringViewModel
    {
        public int Id { get; set; }
        public int MockTestId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string MockTestName { get; set; }
        public string PackageName { get; set; }
        public bool IsScored { get; set; }
        public int PackageId { get; set; }
    }

    public class AudioScoringSetupVm
    {
        public AudioScoringViewModel  Master { get; set; }
        public List<AudioScoreDetailsViewModel> DialogueDetails { get; set; }
        //public List<AudioScoreDetailsViewModel> SegmentTwoDetails { get; set; }

        public string FeedBacks { get; set; }
    }
    public class AudioScoreDetailsViewModel
    {
        public int Id { get; set; }
        public int NaatiScoreId { get; set; }
        public int QuestionSetId { get; set; }
        public string QuestionSetName { get; set; }

        public int QuestionId { get; set; }
        public string QuestionAudioUrl { get; set; }
        public string CorrectAnswerUrl { get; set; }
        public string GivenAnswerUrl { get; set; }
        public decimal QuestionScore { get; set; }
    }

}