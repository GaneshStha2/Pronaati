using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class OnlineClassroomCoursePracticeMenusVm
    {
        public List<VocabDetailsVm> VocabDetails { get; set; }
        public List<SynonymBoosterDetailsVm> SynonymBoosterDetails { get; set; }
        public List<BoosterCollocationDetailsVm> BoosterCollocationDetails { get; set; }
        public List<MasterTopicSentenceDetailsVm> MasterTopicSentenceDetails { get; set; }
        public List<PracticeQuestionsDetailsVm> PracticeQuestionsDetails { get; set; }
    }

    public class VocabDetailsVm
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
    }

    public class SynonymBoosterDetailsVm
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
    }

    public class BoosterCollocationDetailsVm
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
    }

    public class MasterTopicSentenceDetailsVm
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
    }

    public class PracticeQuestionsDetailsVm
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
    }
}