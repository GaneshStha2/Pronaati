using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class LoggedStudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentUserName { get; set; }
        public string StudentImageUrl { get; set; }
        public string StudentNameForMockTest { get; set; }
    }
}