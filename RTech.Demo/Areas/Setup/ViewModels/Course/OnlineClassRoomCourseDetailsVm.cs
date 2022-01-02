using Riddhasoft.Setup.Entity.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Setup.ViewModels.Course
{
    public class OnlineClassRoomCourseDetailsVm
    {
        public int Id { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int FileId { get; set; }

        public FileType FileType { get; set; }

        public string FileTypeName { get; set; }
    }
}