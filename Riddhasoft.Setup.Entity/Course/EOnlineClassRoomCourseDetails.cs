using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Setup.Entity.Course
{
    public class EOnlineClassRoomCourseDetails
    {
        public int Id { get; set; }
        public int OnlineClassRoomCourseId { get; set; }
        public string  FileName { get; set; }
        public string FileUrl { get; set; }
        public int FileId { get; set; }
        public FileType FileType { get; set; }

        public virtual EOnlineClassRoomCourse OnlineClassRoomCourse { get; set; }
    }

    public enum FileType {

        Video,
        Audio,
        PDF,

    }
}
