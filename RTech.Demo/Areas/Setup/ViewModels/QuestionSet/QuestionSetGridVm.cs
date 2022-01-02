using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.QuestionSet
{
    public class QuestionSetGridVm
    {
        public int Id { get; set; }
        public string QuestionSetName { get; set; }
        public string QuestionSetCode { get; set; }
        public bool IsUnscored { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public int WritingTypeOneId { get; set; }
        public int WritingTypeTwoId { get; set; }
        public int ListeningTypeOneId { get; set; }
        public int ListeningTypeTwoId { get; set; }
        public int ListeningTypeThreeId { get; set; }
        public int ListeningTypeFourId { get; set; }
        public int ListeningTypeFiveId { get; set; }
        public int ListeningTypeSixId { get; set; }
        public int ListeningTypeSevenId { get; set; }
        public int ListeningTypeEightId { get; set; }
        public int ReadingTypeOneId { get; set; }
        public int ReadingTypeTwoId { get; set; }
        public int ReadingTypeThreeId { get; set; }
        public int ReadingTypeFourId { get; set; }
        public int ReadingTypeFiveId { get; set; }
        public int SpeakingTypeOneId { get; set; }
        public int SpeakingTypeTwoId { get; set; }
        public int SpeakingTypeThreeId { get; set; }
        public int SpeakingTypeFourId { get; set; }
        public int SpeakingTypeFiveId { get; set; }
        public int SpeakingTypeSixId { get; set; }
        public int SpeakingTypeSevenId { get; set; }


    }
}