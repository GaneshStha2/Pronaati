using Riddhasoft.Setup.Entity.QuestionPackage;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.MockTest.Entity
{
    public class EMockTestWritingEvaluation
    {
        public int Id { get; set; }
        public int MockTestReportId { get; set; }
        public int QuestionPackageMasterId { get; set; }
        public int QuestionSetMasterId { get; set; }
        public int QuestionId { get; set; }
        public QuestionType QuestionType { get; set; }
        public int StudentInformationId { get; set; }
        public string AnswerText { get; set; }
        public int ObtainedMarks { get; set; }
        public DateTime TestDate { get; set; }

        public virtual EQuestionPackageMaster QuestionPackageMaster { get; set; }
        public virtual EQuestionSetMaster QuestionSetMaster { get; set; }
        public virtual EStudentInformation StudentInformation { get; set; }




    }
}
