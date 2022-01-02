using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class CourseViewModel
    {

        public int CourseId { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        //paymentCredentials
        public string NameOnCard { get; set; }
        public int CardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public int SecurityCode { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public decimal PaymentAmount { get; set; }


    }
}