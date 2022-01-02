using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity
{
    public class ENaatiScore
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int MockTestId { get; set; }
        public int ScoredBy { get; set; }
        public string FeedBacks { get; set; }
        public DateTime ScoredDate { get; set; }
        public int PackageId { get; set; }
        public virtual EStudentInformation Student { get; set; }
    }


    public class ENaatiScoreDetail
    {
        public int Id { get; set; }
        public int NaatiScoreId { get; set; }
        public int QuestionSetId { get; set; }
        public int QuestionId { get; set; }
        public decimal QuestionScore { get; set; }
        public virtual EQuestionSetMaster QuestionSet { get; set; }
    }
}
