using Riddhasoft.Setup.Entity.QuestionPackage;
using Riddhasoft.Setup.Entity.QuestionSet;
using Riddhasoft.Student.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Writing
{
    public class MockTestWritingEvaluationVM
    {
        public int Id { get; set; }
        public int MockTestReportId { get; set; }
        public int QuestionPackageMasterId { get; set; }
        public int QuestionSetMasterId { get; set; }
        public int QuestionId { get; set; }
        public QuestionType QuestionType { get; set; }
        public int StudentInformationId { get; set; }
        public string StudentName { get; set; }
        public string QuestionText { get; set; }
        public string PackageName { get; set; }
        public string QuestionSetName { get; set; }
        public string AnswerText { get; set; }
        public int ObtainedMarks { get; set; }
        public string TestDate { get; set; }

        public virtual EQuestionSetMaster QuestionSetMaster { get; set; }
        public virtual EQuestionPackageMaster QuestionPackageMaster { get; set; }
        public virtual EStudentInformation StudentInformation { get; set; }

    }
}