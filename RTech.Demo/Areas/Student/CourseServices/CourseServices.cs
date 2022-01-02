using Riddhasoft.Setup.Entity.Course;
using Riddhasoft.Setup.Services.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTech.Demo.Areas.Student
{
    public class CourseServices
    {
        SOnlineClassRoomCourse _OnlineClassRoomCourseServies = null;

        public CourseServices()
        {
            _OnlineClassRoomCourseServies = new SOnlineClassRoomCourse();
        }

        public List<ViewModels.ClassRoomCourseMasterViewModel>  GetCourseList() {


            var result = (from c in _OnlineClassRoomCourseServies.List().Data
                          select new ViewModels.ClassRoomCourseMasterViewModel()
                          {
                              CourseId = c.Id,
                              Amount = c.Price,
                              CourseCode = c.Code,
                              CourseName = c.Name,
                              ImageUrl = c.ImageURL,
                              Description = c.Description

                          }).ToList();

            return result;

        }


    } 
}