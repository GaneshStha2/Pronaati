using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class ReportsViewModel
    {
        public int Id { get; set; }
        public List<ReportGridViewModel> ReportGridVm { get; set; }
    }

    public class ReportGridViewModel {
        public int Id { get; set; }
        public int MockTestId { get; set; }
        //public int QuestionSetId { get; set; }
        public string MockTestSetName { get; set; }
        public string Date { get; set; }
        public bool IsReportReady { get; set; }
    }
}