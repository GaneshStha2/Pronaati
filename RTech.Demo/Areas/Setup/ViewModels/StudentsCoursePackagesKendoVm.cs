using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels
{
    public class StudentsCoursePackagesKendoVm
    {
        public int Id { get; set; }
        public string StudentName { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal CoursePrice { get; set; }
        public string PurchaseDate { get; set; }
        public string StudentEmail { get; set; }
        public string StudentAddress { get; set; }
        public string StudentPhoneNumber { get; set; }
        public string Institute { get; set; }
        public string ReceiptNumber { get; set; }
    }
}