using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public int Title { get; set; }
        public string Day { get; set; }
        public string DayDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Image { get; set; }
        public string Topic { get; set; }
    }

    public class BlogDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}