using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student.ViewModels
{


    //Used for Naati Package List aswell
    public class ClassRoomCourseMasterViewModel
    {

        public int CourseId { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public bool IsPracticeEnabled { get; set; }
        public List<ClassRoomCourseDetailsViewModel> ClassRoomCourseDetailsList { get; set; }

    }

    public class ClassRoomCourseDetailsViewModel
    {

        public int CourseDetailsId { get; set; }
        public string FileName { get; set; }
        public int FileId { get; set; }
        public string FileUrl { get; set; }
        public string FileExtension { get; set; }
        public FileType FileType { get; set; }

    }

    
}