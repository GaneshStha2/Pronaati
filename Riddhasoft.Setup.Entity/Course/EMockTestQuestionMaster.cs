using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Course
{
    public class EMockTestQuestionMaster
    {
        public int Id { get; set; }
        public int LanguageTypeId { get; set; }

        public int DialogueId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ELanguageType LanguageType { get; set; }
    }

    public class EMockTestQuestionDetail
    {
        public int Id { get; set; }
        public int MockTestQuestionMasterId { get; set; }
        public string QuestionAudioUrl { get; set; }
        public string AnswerAudioUrl { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int SegmentId { get; set; }
        public virtual EMockTestQuestionMaster MockTestQuestionMaster { get; set; }
    }
}
