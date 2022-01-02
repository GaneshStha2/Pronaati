using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class OnlineClassRoomCourseMasterVm
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public int CourseTypeId { get; set; }
        public string CourseTypeName { get; set; }
        public bool IsPracticeEnabled { get; set; }

    }

    public class OnlineClassRoomCourseKendoVm {

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string CreatedDate { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public int CourseTypeId { get; set; }
        public string CourseTypeName { get; set; }
    }
}