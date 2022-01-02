using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Services.ViewModel
{
    public class MockTestReportKendoVm
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmailAddress { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }

        public string QuestionSetName { get; set; }
        public string TestTakenDate { get; set; }
        public int SpeakingMarks { get; set; }
        public int WritingMarks { get; set; }
        public int ReadingMarks { get; set; }
        public int ListeningMarks { get; set; }
        public int TotalMarks { get; set; }
        public bool IsReportReady { get; set; }
    }
}
