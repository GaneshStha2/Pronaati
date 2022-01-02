using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class MockTestQuestionGridVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }

        public string CreatedDate { get; set; }
    }

    public class MockTestQuestionMasterVm
    {
        public int Id { get; set; }
        public int LanguageTypeId { get; set; }
      
        public int DialogueId { get; set; }

        public int PreviousDialogueId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }


    public class MockTestQuestionDetailVm
    {
        public int Id { get; set; }
        public int MockTestQuestionMasterId { get; set; }
        public string QuestionAudioUrl { get; set; }
        public string Description { get; set; }
        public string AnswerAudioUrl { get; set; }


        public int Order { get; set; }
        public int SegmentId { get; set; }
        public string SegmentName { get; set; }
    }

    public class MockTestQuestionSetupVm
    {
        public MockTestQuestionMasterVm Master { get; set; }
        public List<MockTestQuestionDetailVm> Details { get; set; }
    }

}