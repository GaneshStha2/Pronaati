using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Student.Entity
{
    public class EMockTestReport
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int QuestionPackageId { get; set; }
        public int QuestionSetId { get; set; }
        
        public string TestTakerId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CountryOfCitizenship { get; set; }
        public string CountryOfResidence { get; set; }
        public Gender Gender { get; set; }
        public string EmailAddress { get; set; }
        public string RegistrationId { get; set; }
        public DateTime TestDate { get; set; }
        public string TestTakerCountry { get; set; }
        public bool FirstTimeTestTaker { get; set; }
        public DateTime ReportIssueDate { get; set; }
        public string ScoresValidUntil { get; set; }
        public int OverallScore { get; set; }
        public int ListeningMarks { get; set; }
        public int ReadingMarks { get; set; }
        public int SpeakingMarks { get; set; }
        public int WritingMarks { get; set; }
        public int GrammarMarks { get; set; }
        public int OralFluencyMarks { get; set; }
        public int PronounciationMarks { get; set; }
        public int SpellingMarks { get; set; }
        public int VocabularyMarks { get; set; }
        public int WrittenDiscourseMarks { get; set; }
        public bool IsReportReadyToView { get; set; }

    }
}
