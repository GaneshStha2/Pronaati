using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class MockTestReportViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentName { get; set; }
        public string TestDate { get; set; }
        public decimal OverallScore { get; set; }
        public string Language { get; set; }
        public string CustomerCode { get; set; }
        public string MockTestName { get; set; }
        public List<ReportScoreDetail> Scores { get; set; }
        public List<string> FeedBacks { get; set; }
    }

    public class ReportScoreDetail
    {
      
        public string DialogueName { get; set; }
        public decimal DialogueScore { get; set; }
    }

}