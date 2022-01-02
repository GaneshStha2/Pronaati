using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.QuestionSet
{
    public class EQuestionSetDetail
    {
        public int Id { get; set; }
        public int QuestionSetMasterId { get; set; }
        public int QuestionId { get; set; }
        public int LanguageId { get; set; }
        public int SegmentId { get; set; }
    }

    public enum QuestionType
    {
        Dummy = 0,
        SpeakingTypeOne = 1,
        SpeakingTypeTwo =2,
        SpeakingTypeThree = 3,
        SpeakingTypeFour = 4,
        SpeakingTypeFive = 5,
        SpeakingTypeSix = 6,
        SpeakingTypeSeven = 7,

        WritingTypeOne = 8,
        WritingTypeTwo = 9,
        
        ReadingTypeOne = 10,
        ReadingTypeTwo = 11,
        ReadingTypeThree = 12,
        ReadingTypeFour = 13,
        ReadingTypeFive = 14,

        ListeningTypeOne = 15,
        ListeningTypeTwo = 16,
        ListeningTypeThree = 17,
        ListeningTypeFour = 18,
        ListeningTypeFive = 19,
        ListeningTypeSix = 20,
        ListeningTypeSeven = 21,
        ListeningTypeEight = 22
    }
}
