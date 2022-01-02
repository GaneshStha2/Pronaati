using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class OnlineClassRoomCourseSetupVm
    {
        public OnlineClassRoomCourseMasterVm OnlineClassRoomCourseMasterVm { get; set; }
        public List<OnlineClassRoomCourseDetailsVm> OnlineClassRoomCourseDetailsVm { get; set; }

        public List<VocabDetailsVm> VocabDetails { get; set; }
        public List<SynonymBoosterDetailsVm> SynonymBoosterDetails { get; set; }
        public List<BoosterCollocationDetailsVm> BoosterCollocationDetails { get; set; }
        public List<MasterTopicSentenceDetailsVm> MasterTopicSentenceDetails { get; set; }
        public List<PracticeQuestionsDetailsVm> PracticeQuestionsDetails { get; set; }
    }
}