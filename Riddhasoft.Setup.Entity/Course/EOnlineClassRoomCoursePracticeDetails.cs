using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Course
{
    public class EOnlineClassRoomCoursePracticeDetails
    {

        public int Id { get; set; }
        public int EOnlineClassRoomCourseID { get; set; }

        public PracticeType PracticeType { get; set; }

        /// <summary>
        /// Practice can differ according practice Type In Which practice type has their own entity and setup
        /// </summary>
        public int PracticeID { get; set; }

    }

    public enum PracticeType {

        VocabularyAndPronunciationBooster,
        SynonymBooster,
        BoosterCollocation,
        MasterTopicSentenceBooster,
    }
}
